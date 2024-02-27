using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Employee
{
    public interface ISkillsRepository
    {
        public Task<List<Skill>> GetSkills();
        public Task<List<Skill>> GetSkillsById(int? id);
        public Task<List<Skill>> DeleteSkills(int? id);
    }
}
