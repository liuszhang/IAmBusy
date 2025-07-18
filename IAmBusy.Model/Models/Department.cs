using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAmBusy.Model.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public List<User>? Users { get; set; } = new List<User>();
    }
}
