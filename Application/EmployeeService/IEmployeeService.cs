using Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.EmployeeService
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDTO>?> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
        public Task<EmployeeDTO?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
        public Task<bool> CreateEmployeeAsync(CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default);
        public Task<bool> UpdateEmployeeAsync(int id, CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default);
        public Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);
    }
}
