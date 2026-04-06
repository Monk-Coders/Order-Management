using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Repository.Repository
{
    public class TopProductRow
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
    }

    public interface ISalesAnalysisRepository
    {
        Task<List<TopProductRow>> GetTopNProducts(int n, DateTime startDate, DateTime endDate);
        Task<List<TopProductRow>> GetTopNByCategory(int n, DateTime startDate, DateTime endDate, string category);
        Task<List<TopProductRow>> GetTopNByRegion(int n, DateTime startDate, DateTime endDate, string region);
    }

    public class SalesAnalysisRepository : ISalesAnalysisRepository
    {
        private readonly OrderDBContext _context;
        private readonly ILogger<SalesAnalysisRepository> _logger;

        public SalesAnalysisRepository(OrderDBContext context, ILogger<SalesAnalysisRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TopProductRow>> GetTopNProducts(int n, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Fetching top {N} products between {Start} and {End}", n, startDate, endDate);

            var results = await (
                from oi in _context.OrderItems
                join o in _context.Orders on oi.OrderGuid equals o.OrderGuid
                join p in _context.Products on oi.ProductId equals p.ProductGuid
                join pc in _context.ProductCategories on p.CategoryGuid equals pc.ProductCategoryGuid into catJoin
                from pc in catJoin.DefaultIfEmpty()
                where o.OrderedDate >= startDate && o.OrderedDate <= endDate
                      && o.IsActive && !o.IsDeleted
                      && oi.IsActive && !oi.IsDeleted
                group oi by new { p.ProductGuid, p.Name, CategoryName = pc != null ? pc.Name : "Uncategorized" } into g
                orderby g.Sum(x => x.Quantity) descending
                select new TopProductRow
                {
                    ProductId = g.Key.ProductGuid,
                    ProductName = g.Key.Name,
                    Category = g.Key.CategoryName,
                    TotalQuantitySold = g.Sum(x => x.Quantity)
                }
            ).Take(n).ToListAsync();

            return results;
        }

        public async Task<List<TopProductRow>> GetTopNByCategory(int n, DateTime startDate, DateTime endDate, string category)
        {
            _logger.LogInformation("Fetching top {N} products for category '{Cat}' between {Start} and {End}", n, category, startDate, endDate);

            var results = await (
                from oi in _context.OrderItems
                join o in _context.Orders on oi.OrderGuid equals o.OrderGuid
                join p in _context.Products on oi.ProductId equals p.ProductGuid
                join pc in _context.ProductCategories on p.CategoryGuid equals pc.ProductCategoryGuid
                where o.OrderedDate >= startDate && o.OrderedDate <= endDate
                      && o.IsActive && !o.IsDeleted
                      && oi.IsActive && !oi.IsDeleted
                      && pc.Name == category
                group oi by new { p.ProductGuid, p.Name, CategoryName = pc.Name } into g
                orderby g.Sum(x => x.Quantity) descending
                select new TopProductRow
                {
                    ProductId = g.Key.ProductGuid,
                    ProductName = g.Key.Name,
                    Category = g.Key.CategoryName,
                    TotalQuantitySold = g.Sum(x => x.Quantity)
                }
            ).Take(n).ToListAsync();

            return results;
        }

        public async Task<List<TopProductRow>> GetTopNByRegion(int n, DateTime startDate, DateTime endDate, string region)
        {
            _logger.LogInformation("Fetching top {N} products for region '{Region}' between {Start} and {End}", n, region, startDate, endDate);

            var results = await (
                from oi in _context.OrderItems
                join o in _context.Orders on oi.OrderGuid equals o.OrderGuid
                join p in _context.Products on oi.ProductId equals p.ProductGuid
                join pc in _context.ProductCategories on p.CategoryGuid equals pc.ProductCategoryGuid into catJoin
                from pc in catJoin.DefaultIfEmpty()
                where o.OrderedDate >= startDate && o.OrderedDate <= endDate
                      && o.IsActive && !o.IsDeleted
                      && oi.IsActive && !oi.IsDeleted
                      && o.Region == region
                group oi by new { p.ProductGuid, p.Name, CategoryName = pc != null ? pc.Name : "Uncategorized" } into g
                orderby g.Sum(x => x.Quantity) descending
                select new TopProductRow
                {
                    ProductId = g.Key.ProductGuid,
                    ProductName = g.Key.Name,
                    Category = g.Key.CategoryName,
                    TotalQuantitySold = g.Sum(x => x.Quantity)
                }
            ).Take(n).ToListAsync();

            return results;
        }
    }
}
