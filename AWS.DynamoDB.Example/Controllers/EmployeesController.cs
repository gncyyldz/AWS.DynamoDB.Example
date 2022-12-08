using Amazon.DynamoDBv2.DataModel;
using AWS.DynamoDB.Example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.DynamoDB.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        readonly IDynamoDBContext _dynamoDBContext;

        public EmployeesController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Employee> employees = await _dynamoDBContext.ScanAsync<Employee>(default).GetRemainingAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Employee employee = await _dynamoDBContext.LoadAsync<Employee>(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employeeRequest)
        {
            Employee employee = await _dynamoDBContext.LoadAsync<Employee>(employeeRequest.Id);
            if (employee != null)
                return BadRequest($"Employee with Id {employeeRequest.Id} already exists.");
            await _dynamoDBContext.SaveAsync(employeeRequest);

            return CreatedAtAction(nameof(EmployeesController.Post), new { employeeRequest.Id }, employeeRequest);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employeeRequest)
        {
            Employee employee = await _dynamoDBContext.LoadAsync<Employee>(employeeRequest.Id);
            if (employee == null)
                return NotFound();
            await _dynamoDBContext.SaveAsync(employeeRequest);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = await _dynamoDBContext.LoadAsync<Employee>(id);
            if (employee == null)
                return NotFound();
            await _dynamoDBContext.DeleteAsync(employee);

            return NoContent();
        }
    }
}
