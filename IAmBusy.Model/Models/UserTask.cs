using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IAmBusy.Model.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!; // 关联的用户
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? StartTime { get; set; } = DateTime.UtcNow.ToString();
        public string? EndTime { get; set; } = null;
        public string? Status { get; set; } = TaskStatusEnum.计划中.ToString();
        public bool IsPrivate { get; set; }=false;

    }
}
