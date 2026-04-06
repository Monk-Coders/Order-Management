using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Model.Entities
{
    [Table("refresh_log")]
    public class RefreshLog
    {
        [Key]
        [Column("rl_id")]
        public int Id { get; set; }

        [Column("rl_started_at")]
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        [Column("rl_finished_at")]
        public DateTime? FinishedAt { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("rl_status")]
        public string Status { get; set; } = "running";

        [Column("rl_records_loaded")]
        public int RecordsLoaded { get; set; } = 0;

        [Column("rl_records_skipped")]
        public int RecordsSkipped { get; set; } = 0;

        [Column("rl_error_message")]
        public string? ErrorMessage { get; set; }

        [MaxLength(20)]
        [Column("rl_trigger_type")]
        public string TriggerType { get; set; } = "manual";
    }
}
