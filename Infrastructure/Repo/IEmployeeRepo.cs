using Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repo
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<EmployeeDTO>?> GetAllAsync(CancellationToken cancellationToken = default);
        Task<EmployeeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
