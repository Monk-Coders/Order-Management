using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Model.Entities;
using Order.Repository.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Repository.Repository
{
    public interface ICsvLoaderRepository
    {
        Task<(int loaded, int skipped, List<string> errors)> LoadCsvFile(string filePath);
    }

    public class CsvLoaderRepository : ICsvLoaderRepository
    {
        private readonly OrderDBContext _context;
        private readonly ILogger<CsvLoaderRepository> _logger;

        public CsvLoaderRepository(OrderDBContext context, ILogger<CsvLoaderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(int loaded, int skipped, List<string> errors)> LoadCsvFile(string filePath)
        {
            var errors = new List<string>();
            int loaded = 0;
            int skipped = 0;

            // cache existing records to avoid duplicate inserts
            var existingCustomers = await _context.Customers.ToDictionaryAsync(c => c.CustomerGuid);
            var existingProducts = await _context.Products.ToDictionaryAsync(p => p.ProductGuid);
            var existingCategories = await _context.ProductCategories.ToDictionaryAsync(pc => pc.ProductCategoryGuid);
            var existingOrderGuids = await _context.Orders.Select(o => o.OrderGuid).ToHashSetAsync();

            using var reader = new StreamReader(filePath);
            var header = await reader.ReadLineAsync();
            if (header == null)
            {
                errors.Add("CSV file is empty");
                return (0, 0, errors);
            }

            var columns = header.Split(',');
            int lineNum = 1;

            while (!reader.EndOfStream)
            {
                lineNum++;
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var fields = ParseCsvLine(line);
                    if (fields.Length < 15)
                    {
                        errors.Add($"Line {lineNum}: not enough columns");
                        skipped++;
                        continue;
                    }

                    var orderGuid = GenerateGuidFromId(fields[0].Trim());

                    if (existingOrderGuids.Contains(orderGuid))
                    {
                        skipped++;
                        continue;
                    }

                    // parse and validate fields
                    var productIdStr = fields[1].Trim();
                    var customerIdStr = fields[2].Trim();
                    var productName = fields[3].Trim();
                    var categoryName = fields[4].Trim();
                    var region = fields[5].Trim();
                    var dateStr = fields[6].Trim();
                    var qtyStr = fields[7].Trim();
                    var priceStr = fields[8].Trim();
                    var discountStr = fields[9].Trim();
                    var shippingStr = fields[10].Trim();
                    var paymentMethod = fields[11].Trim();
                    var customerName = fields[12].Trim();
                    var customerEmail = fields[13].Trim();
                    var customerAddress = fields[14].Trim();

                    if (!DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out var saleDate))
                    {
                        errors.Add($"Line {lineNum}: invalid date '{dateStr}'");
                        skipped++;
                        continue;
                    }

                    if (!int.TryParse(qtyStr, out var quantity) || quantity <= 0)
                    {
                        errors.Add($"Line {lineNum}: invalid quantity '{qtyStr}'");
                        skipped++;
                        continue;
                    }

                    if (!decimal.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var unitPrice))
                    {
                        errors.Add($"Line {lineNum}: invalid unit price '{priceStr}'");
                        skipped++;
                        continue;
                    }

                    decimal.TryParse(discountStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var discount);
                    decimal.TryParse(shippingStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var shippingCost);

                    // ensure customer exists
                    var customerGuid = GenerateGuidFromId(customerIdStr);
                    if (!existingCustomers.ContainsKey(customerGuid))
                    {
                        var customer = new Customer
                        {
                            CustomerGuid = customerGuid,
                            Name = customerName,
                            Email = customerEmail,
                            Address = customerAddress
                        };
                        _context.Customers.Add(customer);
                        existingCustomers[customerGuid] = customer;
                    }

                    // ensure category exists
                    var categoryGuid = GenerateGuidFromId("CAT_" + categoryName);
                    if (!existingCategories.ContainsKey(categoryGuid))
                    {
                        var cat = new ProductCategory
                        {
                            ProductCategoryGuid = categoryGuid,
                            Name = categoryName
                        };
                        _context.ProductCategories.Add(cat);
                        existingCategories[categoryGuid] = cat;
                    }

                    // ensure product exists
                    var productGuid = GenerateGuidFromId(productIdStr);
                    if (!existingProducts.ContainsKey(productGuid))
                    {
                        var product = new Product
                        {
                            ProductGuid = productGuid,
                            Name = productName,
                            Code = productIdStr,
                            Price = unitPrice,
                            CategoryGuid = categoryGuid
                        };
                        _context.Products.Add(product);
                        existingProducts[productGuid] = product;
                    }

                    // create order
                    var order = new Order.Model.Entities.Order
                    {
                        OrderGuid = orderGuid,
                        CustomerId = customerGuid,
                        OrderedDate = DateTime.SpecifyKind(saleDate, DateTimeKind.Utc),
                        TotalAmount = (unitPrice * quantity) * (1 - discount) + shippingCost,
                        ItemCount = quantity,
                        Region = region,
                        Discount = discount,
                        ShippingCost = shippingCost,
                        PaymentMethod = paymentMethod
                    };
                    _context.Orders.Add(order);

                    // create order item
                    var orderItem = new OrderItem
                    {
                        OrderItemGuid = Guid.NewGuid(),
                        OrderGuid = orderGuid,
                        ProductId = productGuid,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        TotalPrice = unitPrice * quantity
                    };
                    _context.OrderItems.Add(orderItem);

                    existingOrderGuids.Add(orderGuid);
                    loaded++;

                    // batch commit every 5000 rows
                    if (loaded % 5000 == 0)
                    {
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Committed batch - {Loaded} records so far", loaded);
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Line {lineNum}: {ex.Message}");
                    skipped++;
                }
            }

            if (loaded % 5000 != 0)
                await _context.SaveChangesAsync();

            _logger.LogInformation("CSV import done: {Loaded} loaded, {Skipped} skipped", loaded, skipped);
            return (loaded, skipped, errors);
        }

        private static Guid GenerateGuidFromId(string id)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(id);
            var hash = md5.ComputeHash(bytes);
            return new Guid(hash);
        }

        private static string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var current = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            result.Add(current.ToString());
            return result.ToArray();
        }
    }

    internal static class AsyncEnumerableExtensions
    {
        public static async Task<HashSet<T>> ToHashSetAsync<T>(this IQueryable<T> source)
        {
            var set = new HashSet<T>();
            await foreach (var item in source.AsAsyncEnumerable())
            {
                set.Add(item);
            }
            return set;
        }
    }
}
