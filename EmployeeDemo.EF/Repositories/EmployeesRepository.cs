using EmployeeDemo.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeesRepository(EmployeeDbContext db)
        {
            _dbContext = db;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployee(int? id)
        {
            var employeeId = await GetEmployeeById(id);
            _dbContext.Employees.Remove(employeeId);
            await _dbContext.SaveChangesAsync();
            return employeeId;
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            return await _dbContext.Employees.Where(e => e.Id == id).Include(x => x.Skills).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _dbContext.Employees.Include(x => x.Skills).ToListAsync();
        }

        public async Task<List<Employee>> SearchEmployee(string? searchString)
        {
            var getemployee = _dbContext.Employees.Where(n => n.FirstName.Contains(searchString) || n.LastName.Contains(searchString)).Include(x => x.Skills).ToListAsync();
            return await getemployee;
        }

        public async Task<Employee> UpdateEmployee(int? id,Employee employee)
        {
               var employeeId = await GetEmployeeById(id);
               if (employeeId != null)
               {
                    employeeId.FirstName = employee.FirstName;
                    employeeId.LastName = employee.LastName;
                    employeeId.Email = employee.Email;
                    employeeId.Gender = employee.Gender;
                    employeeId.DOB = employee.DOB;
                    employeeId.JoiningDate = employee.JoiningDate;
                    employeeId.Designation = employee.Designation;
                    employeeId.image = employee.image;
                    employeeId.Description = employee.Description;
                    employeeId.Skills = employee.Skills;
               }
               await _dbContext.SaveChangesAsync();
           return employee;
        }
    }
}
