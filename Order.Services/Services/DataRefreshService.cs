using Microsoft.Extensions.Logging;
using Order.Dto.Dtos;
using Order.Model.Entities;
using Order.Repository.Context;
using Order.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Services.Services
{
    public interface IDataRefreshService
    {
        Task<RefreshResponse> TriggerRefresh(string csvFilePath);
        Task<RefreshLogListResponse> GetRefreshLogs(int count = 10);
    }

    public class DataRefreshService : IDataRefreshService
    {
        private readonly ICsvLoaderRepository _csvLoader;
        private readonly OrderDBContext _context;
        private readonly ILogger<DataRefreshService> _logger;

        public DataRefreshService(ICsvLoaderRepository csvLoader, OrderDBContext context, ILogger<DataRefreshService> logger)
        {
            _csvLoader = csvLoader;
            _context = context;
            _logger = logger;
        }

        public async Task<RefreshResponse> TriggerRefresh(string csvFilePath)
        {
            var logEntry = new RefreshLog
            {
                StartedAt = DateTime.UtcNow,
                TriggerType = "manual"
            };
            _context.RefreshLogs.Add(logEntry);
            await _context.SaveChangesAsync();

            try
            {
                var (loaded, skipped, errors) = await _csvLoader.LoadCsvFile(csvFilePath);

                logEntry.Status = "success";
                logEntry.RecordsLoaded = loaded;
                logEntry.RecordsSkipped = skipped;
                logEntry.FinishedAt = DateTime.UtcNow;
                if (errors.Count > 0)
                    logEntry.ErrorMessage = string.Join("; ", errors.Take(20));

                await _context.SaveChangesAsync();

                return new RefreshResponse
                {
                    Success = true,
                    Status = "success",
                    RecordsLoaded = loaded,
                    RecordsSkipped = skipped,
                    Errors = errors.Take(20).ToList(),
                    Message = $"Loaded {loaded} records, skipped {skipped}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Data refresh failed");

                logEntry.Status = "failed";
                logEntry.ErrorMessage = ex.Message;
                logEntry.FinishedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return new RefreshResponse
                {
                    Success = false,
                    Status = "failed",
                    Message = ex.Message
                };
            }
        }

        public async Task<RefreshLogListResponse> GetRefreshLogs(int count = 10)
        {
            var logs = _context.RefreshLogs
                .OrderByDescending(l => l.StartedAt)
                .Take(count)
                .Select(l => new RefreshLogDto
                {
                    Id = l.Id,
                    StartedAt = l.StartedAt,
                    FinishedAt = l.FinishedAt,
                    Status = l.Status,
                    RecordsLoaded = l.RecordsLoaded,
                    RecordsSkipped = l.RecordsSkipped,
                    ErrorMessage = l.ErrorMessage,
                    TriggerType = l.TriggerType
                })
                .ToList();

            return new RefreshLogListResponse { Success = true, Logs = logs };
        }
    }
}
