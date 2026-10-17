using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }
}
