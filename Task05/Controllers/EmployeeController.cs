using Application.EmployeeService;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Task05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet]
        public async Task<ActionResult> GetAllEmployees(CancellationToken cancellationToken = default)
        {
            if (await _employeeService.GetAllEmployeesAsync(cancellationToken) == null)
            {
                return NotFound("Employee service is not available.");
            }

            var employees = await _employeeService.GetAllEmployeesAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            return Ok(employee);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default)
        {
            var result = await _employeeService.CreateEmployeeAsync(employee, cancellationToken);
            if (!result)
            {
                return BadRequest("Failed to create employee.");
            }
            return Ok("Employee created successfully.");
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult> UpdateEmployee([FromRoute] int id, [FromBody] CreateAndUpdateEmployeeDTO employee, CancellationToken cancellationToken = default)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, employee, cancellationToken);
            if (!result)
            {
                return BadRequest("Failed to update employee.");
            }
            return Ok("Employee updated successfully.");
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
            if (!result)
            {
                return BadRequest("Failed to delete employee.");
            }
            return Ok("Employee deleted successfully.");
        }

        [HttpGet("PerformanceMeasurement")]
        public async Task<ActionResult> PerformanceMeasurement(CancellationToken cancellationToken = default)
        {
            // 1. Sequential execution
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            await Task.Delay(5000, cancellationToken);

            await Task.Delay(3000, cancellationToken);

            stopwatch.Stop();

            var sequentialTime = stopwatch.Elapsed.TotalMilliseconds;

            // 2. Parallel execution
            stopwatch.Restart();

            var Delay1 = Task.Delay(5000, cancellationToken);

            var Delay2 = Task.Delay(3000, cancellationToken);

            await Task.WhenAll(Delay1, Delay2);

            stopwatch.Stop();

            var parallelTime = stopwatch.Elapsed.TotalMilliseconds;

            // 3. Compare the results
            var improvement = sequentialTime > 0
                ? (sequentialTime - parallelTime)
                    / sequentialTime * 100
                : 0;

            return Ok(new
            {
                SequentialTimeMs = Math.Round(sequentialTime, 2),
                ParallelTimeMs = Math.Round(parallelTime, 2),
                ImprovementPercentage = Math.Round(improvement, 2),
                ParallelImprovedPerformance = parallelTime < sequentialTime
            });

            #region Comment
            // In the above code, we measure the time taken for sequential execution of two delays (5 seconds and 3 seconds)
            // and compare it with the time taken for parallel execution of the same delays.
            // The results are returned in a JSON format:
            // including the time taken for both executions and the percentage improvement achieved through parallel execution.
            // In sequential execution the two delays are executed one after the other, resulting in a total time of 8 seconds because they work on a single thread for each task.
            // In parallel execution, both delays are executed simultaneously, resulting in a total time of 5 seconds because they work on separate threads for each task.
            #endregion
        }

    }
}
