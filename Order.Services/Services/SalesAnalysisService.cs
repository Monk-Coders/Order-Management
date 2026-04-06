using Microsoft.Extensions.Logging;
using Order.Dto.Dtos;
using Order.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Services.Services
{
    public interface ISalesAnalysisService
    {
        Task<TopProductResponse> GetTopNProducts(TopProductRequest request);
        Task<TopProductResponse> GetTopNByCategory(TopProductByCategoryRequest request);
        Task<TopProductResponse> GetTopNByRegion(TopProductByRegionRequest request);
    }

    public class SalesAnalysisService : ISalesAnalysisService
    {
        private readonly ISalesAnalysisRepository _repository;
        private readonly ILogger<SalesAnalysisService> _logger;

        public SalesAnalysisService(ISalesAnalysisRepository repository, ILogger<SalesAnalysisService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<TopProductResponse> GetTopNProducts(TopProductRequest request)
        {
            if (request.N <= 0 || request.N > 100)
                return new TopProductResponse { Success = false, Error = "N must be between 1 and 100" };

            if (request.StartDate > request.EndDate)
                return new TopProductResponse { Success = false, Error = "start_date must be before end_date" };

            var rows = await _repository.GetTopNProducts(request.N, request.StartDate, request.EndDate);

            return new TopProductResponse
            {
                Success = true,
                Count = rows.Count,
                Results = rows.Select(r => new TopProductResult
                {
                    ProductId = r.ProductId,
                    ProductName = r.ProductName,
                    Category = r.Category,
                    TotalQuantitySold = r.TotalQuantitySold
                }).ToList()
            };
        }

        public async Task<TopProductResponse> GetTopNByCategory(TopProductByCategoryRequest request)
        {
            if (request.N <= 0 || request.N > 100)
                return new TopProductResponse { Success = false, Error = "N must be between 1 and 100" };

            if (string.IsNullOrWhiteSpace(request.Category))
                return new TopProductResponse { Success = false, Error = "category is required" };

            if (request.StartDate > request.EndDate)
                return new TopProductResponse { Success = false, Error = "start_date must be before end_date" };

            var rows = await _repository.GetTopNByCategory(request.N, request.StartDate, request.EndDate, request.Category);

            return new TopProductResponse
            {
                Success = true,
                Count = rows.Count,
                Results = rows.Select(r => new TopProductResult
                {
                    ProductId = r.ProductId,
                    ProductName = r.ProductName,
                    Category = r.Category,
                    TotalQuantitySold = r.TotalQuantitySold
                }).ToList()
            };
        }

        public async Task<TopProductResponse> GetTopNByRegion(TopProductByRegionRequest request)
        {
            if (request.N <= 0 || request.N > 100)
                return new TopProductResponse { Success = false, Error = "N must be between 1 and 100" };

            if (string.IsNullOrWhiteSpace(request.Region))
                return new TopProductResponse { Success = false, Error = "region is required" };

            if (request.StartDate > request.EndDate)
                return new TopProductResponse { Success = false, Error = "start_date must be before end_date" };

            var rows = await _repository.GetTopNByRegion(request.N, request.StartDate, request.EndDate, request.Region);

            return new TopProductResponse
            {
                Success = true,
                Count = rows.Count,
                Results = rows.Select(r => new TopProductResult
                {
                    ProductId = r.ProductId,
                    ProductName = r.ProductName,
                    Category = r.Category,
                    TotalQuantitySold = r.TotalQuantitySold
                }).ToList()
            };
        }
    }
}
