using Order.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services.Request
{
    public class GetOrdersByCustomerRequest
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = default(DateTime);
    }

    public class GetOrdersByCustomerRepsonse
    {
        public IList<OrderDto> Orders { get; set; } = new List<OrderDto>();
        public bool IsSuccess { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
