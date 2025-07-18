using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IAmBusy.Model.Models
{
    public class User : IdentityUser<int>
    {
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; } // 部门名称
        [JsonIgnore]
        public Department? Department { get; set; } // 导航属性
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Identifier { get; set; } = "dropzone";


        public List<UserTask>? UserTasks { get; set; } = new List<UserTask>();
    }
}
