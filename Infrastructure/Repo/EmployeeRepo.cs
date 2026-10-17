using Domain;
using Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repo
{
    public class EmployeeRepo(ApplicationDbContext dbContext) : IEmployeeRepo
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IEnumerable<EmployeeDTO>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (await _dbContext.Employees.AnyAsync(cancellationToken))
                return await _dbContext.Employees.Select(e => new EmployeeDTO
                {
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary
                }).ToListAsync(cancellationToken);

            return null;
        }
        public async Task<EmployeeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (await _dbContext.Employees.AnyAsync(e => e.Id == id, cancellationToken))
            {
                var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                if (employee != null)
                {
                    return new EmployeeDTO
                    {
                        Name = employee.Name,
                        Age = employee.Age,
                        Salary = employee.Salary
                    };
                }
            }
            return null;
        }

        public async Task<bool> AddAsync(CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default)
        {
            if (employee != null)
            {
                var newEmployee = new Employee
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary
                };
                await _dbContext.Employees.AddAsync(newEmployee, cancellationToken);
                return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(int id, CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default)
        {
            if (employee != null)
            {
                var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                if (existingEmployee != null)
                {
                    existingEmployee.Name = employee.Name;
                    existingEmployee.Age = employee.Age;
                    existingEmployee.Salary = employee.Salary;
                    _dbContext.Employees.Update(existingEmployee);
                    return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
                }
            }
            return false;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (existingEmployee != null)
            {
                _dbContext.Employees.Remove(existingEmployee);
                return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
            return false;
        }

    }
}
