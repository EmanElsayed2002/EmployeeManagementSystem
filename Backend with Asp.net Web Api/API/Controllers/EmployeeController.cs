using Core.Features.Employee.Command.Models;
using Core.Features.Employee.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var res = await _mediator.Send(new GetAllEmployeeList());
            return Ok(res);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployee request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var res = await _mediator.Send(new GetEmployeeById { Id = id });
            return Ok(res);
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpadteEmployee(UpdateEmployee request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
        [HttpDelete("DeleteEmpoyee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var res = await _mediator.Send(new DeleteEmployee { Id = id });
            return Ok(res);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> SearchPaginated([FromQuery] Search request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
        [HttpGet("Paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginatedSearch request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

    }
}
