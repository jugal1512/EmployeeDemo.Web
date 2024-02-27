using AutoMapper;
using EmployeeDemo.Domain.Employee;
using EmployeeDemo.EF;
using EmployeeDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace EmployeeDemo.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeesService _employeesService;
        private readonly SkillsService _skillsService;
        private readonly IMapper _mapper;

        public EmployeeController(EmployeesService employeesService,SkillsService skillsService,IMapper mapper)
        {
            _employeesService = employeesService;
            _skillsService = skillsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetSkills()
        {
            var getSkills = await _skillsService.GetSkills();
            var skillData = _mapper.Map<List<SkillDto>>(getSkills);
            return Ok(skillData);
            //var getSkill = await _skillsService.GetSkillsById(id);
            //var skillData = _mapper.Map<List<SkillDto>>(getSkill);
            //return Ok(skillData);
        }
        public async Task<IActionResult> Index()
        {
                var getEmployee = await _employeesService.GetEmployees();
                var employeedata = _mapper.Map<List<EmployeeDto>>(getEmployee);
                return View(employeedata);
        }

        public async Task<IActionResult> getEmployeeList()
        {
            var getEmployee = await _employeesService.GetEmployees();
            var employeedata = _mapper.Map<List<EmployeeDto>>(getEmployee);
            return new JsonResult(employeedata);
        }

        public async Task<IActionResult> Search(string? searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var employee = await _employeesService.SearchEmployee(searchString);
                var getEmployee = _mapper.Map<List<EmployeeDto>>(employee);
                return View(getEmployee);
            }
            else
            {
                return RedirectToAction("Index");    
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Create(EmployeeDto employeeDto,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    employeeDto.image = await UploadImage(image);
                }
                List<Skill> skillItem = new List<Skill>();
                if (!string.IsNullOrEmpty(employeeDto.SkillName))
                {
                    var skillArray = employeeDto.SkillName.Split(",");
                    if (skillArray.Length > 0)
                    {
                        foreach (var item in skillArray)
                        {
                            Skill skill = new Skill();
                            skill.SkillName = item;
                            skillItem.Add(skill);
                        }
                    }
                }
                var employee = _mapper.Map<Employee>(employeeDto);
                employee.Skills = skillItem;
                var addEmployee = await _employeesService.AddEmployee(employee);
                _mapper.Map<EmployeeDto>(addEmployee);
                TempData["success"] = "Employee Inserted Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _employeesService.DeleteEmployee(id);
            var deleteEmployee = _mapper.Map<EmployeeDto>(employee);
            await DeleteImagePath(deleteEmployee);
            TempData["success"] = "Employee Deleted Successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSkill(int? id)
        { 
            var skills = await _skillsService.DeleteSkills(id);
            var deleteSkills = _mapper.Map<List<SkillDto>>(skills);
            TempData["success"] = "Employee Deleted Successfully.";
            return View(deleteSkills);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var getEmployeeId = await _employeesService.GetEmployeeById(id);
            var employeeId = _mapper.Map<EmployeeDto>(getEmployeeId);
            return View(employeeId);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id,EmployeeDto employeeDto, IFormFile image)
        {
            var employeeById = await _employeesService.GetEmployeeById(id);
            var updateEmployee = _mapper.Map<EmployeeDto>(employeeById);
            var oldImage = updateEmployee.image;
            if (image != null)
            {
                await DeleteImagePath(updateEmployee);
                employeeDto.image = await UploadImage(image);
            }
            else
            {
                updateEmployee.image = oldImage;
            }
            if (!string.IsNullOrEmpty(updateEmployee.SkillName))
            { 
                await _skillsService.DeleteSkills(id);
            }
            List<Skill> skillItem = new List<Skill>();
            if (!string.IsNullOrEmpty(employeeDto.SkillName))
            {
                var skillArray = employeeDto.SkillName.Split(",");
                if (skillArray.Length > 0)
                {
                    foreach (var item in skillArray)
                    {
                        Skill skill = new Skill();
                        skill.SkillName = item;
                        skillItem.Add(skill);
                    }
                }
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Skills = skillItem;
            var UpdateEmployeeById = await _employeesService.UpdateEmployee(id,employee);
            _mapper.Map<EmployeeDto>(UpdateEmployeeById);
            TempData["success"] = "Employee Update Successfully.";
            return RedirectToAction("Index");
        }

        private async Task<string> UploadImage(IFormFile image)
        {
            string newFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine("wwwroot/uploads", newFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return newFileName;
        }

        private async Task DeleteImagePath(EmployeeDto deleteEmployee)
        {
            var imagePath = Path.Combine("wwwroot/uploads", deleteEmployee.image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}