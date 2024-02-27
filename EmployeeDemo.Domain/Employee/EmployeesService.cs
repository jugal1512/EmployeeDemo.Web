using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Employee
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await _employeesRepository.AddEmployee(employee);
        }

        public async Task<Employee> DeleteEmployee(int? id)
        {
            return await _employeesRepository.DeleteEmployee(id);
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            return await _employeesRepository.GetEmployeeById(id);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeesRepository.GetEmployees();
        }

        public async Task<List<Employee>> SearchEmployee(string? searchString)
        {
            return await _employeesRepository.SearchEmployee(searchString);
        }

        public async Task<Employee> UpdateEmployee(int? id,Employee employee)
        {
            return await _employeesRepository.UpdateEmployee(id,employee);
        }
    }
}
