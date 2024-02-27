using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Employee
{
    public interface IEmployeesRepository
    {
        public Task<List<Employee>> GetEmployees();

        public Task<Employee> GetEmployeeById(int? id);

        public Task<Employee> AddEmployee(Employee employee);

        public Task<Employee> DeleteEmployee(int? id);
        public Task<Employee> UpdateEmployee(int? id, Employee employee);
        public Task<List<Employee>> SearchEmployee(string? searchString);
    }
}

