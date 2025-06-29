using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Password { get; set; }
        public string? JobTitle { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
} 