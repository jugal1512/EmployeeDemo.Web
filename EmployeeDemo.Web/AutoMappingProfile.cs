using AutoMapper;
using EmployeeDemo.Domain.Employee;
using EmployeeDemo.Web.Models;

namespace EmployeeDemo.Web
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile() 
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => string.Join(",", src.Skills.Select(x => x.SkillName).ToList())))
                .ForMember(dest => dest.AllSkill, opt => opt.MapFrom(src => src.Skills.Select(x => x.SkillName).ToList()));
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Skill, SkillDto>();
            CreateMap<SkillDto, Skill>();
        }
    }
}
