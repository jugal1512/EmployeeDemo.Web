using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Employee
{
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository;
        public SkillsService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _skillsRepository.GetSkills();
        }

        public async Task<List<Skill>> GetSkillsById(int? id)
        {
            return await _skillsRepository.GetSkillsById(id);
        }

        public async Task<List<Skill>> DeleteSkills(int? id)
        {
            return await _skillsRepository.DeleteSkills(id);
        }
    }
}
 