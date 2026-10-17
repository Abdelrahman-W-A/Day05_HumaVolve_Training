using Infrastructure.DTOs;
using Infrastructure.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.EmployeeService
{
    public class EmployeeService(IEmployeeRepo employeeRepo) : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo = employeeRepo;

        public async Task<IEnumerable<EmployeeDTO>?> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.GetAllAsync(cancellationToken) ?? null;
        }
        public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> CreateEmployeeAsync(CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.AddAsync(employee, cancellationToken);
        }
        public async Task<bool> UpdateEmployeeAsync(int id, CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.UpdateAsync(id, employee, cancellationToken);
        }
        public async Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.DeleteAsync(id, cancellationToken);
        }
    }
}
