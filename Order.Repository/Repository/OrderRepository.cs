using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Model.Entities;
using Order.Repository.Context;
using Order.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Repository
{
    public interface IOrderRepository
    {
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;
        private readonly OrderDBContext _context;


        public OrderRepository(ILogger<OrderRepository> logger, OrderDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<List<Order.Model.Entities.Order>> GetOrdersByCustomerGuid(GetOrdersByCustomerRequest req)
        {
            try
            {
                _logger.LogInformation("Checking invite phone existence for {CustomerId}", req.CustomerId);
                var orders = await _context.Orders
                    .Where(i => i.CustomerId == req.CustomerId && i.IsActive && !i.IsDeleted)
                    .ToListAsync();
                _logger.LogInformation("Found {OrderCount} orders for {CustomerId}", orders?.Count, req.CustomerId);
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking invite phone existence for {CustomerId}", req.CustomerId);
                throw;
            }
        }
    }
    

}
