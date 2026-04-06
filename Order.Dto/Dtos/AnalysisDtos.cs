using System;
using System.Collections.Generic;

namespace Order.Dto.Dtos
{
    public class TopProductRequest
    {
        public int N { get; set; } = 5;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TopProductByCategoryRequest : TopProductRequest
    {
        public string Category { get; set; } = string.Empty;
    }

    public class TopProductByRegionRequest : TopProductRequest
    {
        public string Region { get; set; } = string.Empty;
    }

    public class TopProductResult
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
    }

    public class TopProductResponse
    {
        public bool Success { get; set; }
        public int Count { get; set; }
        public List<TopProductResult> Results { get; set; } = new();
        public string? Error { get; set; }
    }
}
