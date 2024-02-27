using EmployeeDemo.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public SkillsRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _employeeDbContext.Skills.ToListAsync();
        }

        public async Task<List<Skill>> GetSkillsById(int? id)
        {
            return await _employeeDbContext.Skills.Where(x => x.EmployeeId == id).ToListAsync();
        }

        public async Task<List<Skill>> DeleteSkills(int? id)
        {
            var skillId = await GetSkillsById(id);
            _employeeDbContext.Skills.RemoveRange(skillId);
            await _employeeDbContext.SaveChangesAsync();
            return skillId;
        }
    }
}
