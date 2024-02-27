using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Employee
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
