using EmployeeDemo.Domain.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDemo.Web.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public DateTime? DOB { get; set; }
        [Required]
        public DateTime? JoiningDate { get; set; }
        [Required]
        public string? Designation { get; set; }
        public string? image { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? SkillName { get; set; }
        public List<string>? AllSkill { get; set; }
    }

    enum Designation 
    { 
        Jr_Developer,
        Sr_Developer,
        ProjectManager
    }
}
