using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }
}
