using System;
using System.Collections.Generic;

namespace Order.Dto.Dtos
{
    public class RefreshResponse
    {
        public bool Success { get; set; }
        public string Status { get; set; } = string.Empty;
        public int RecordsLoaded { get; set; }
        public int RecordsSkipped { get; set; }
        public List<string> Errors { get; set; } = new();
        public string? Message { get; set; }
    }

    public class RefreshLogDto
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public int RecordsLoaded { get; set; }
        public int RecordsSkipped { get; set; }
        public string? ErrorMessage { get; set; }
        public string TriggerType { get; set; } = string.Empty;
    }

    public class RefreshLogListResponse
    {
        public bool Success { get; set; }
        public List<RefreshLogDto> Logs { get; set; } = new();
    }
}
