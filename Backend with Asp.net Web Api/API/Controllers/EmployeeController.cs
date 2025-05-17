using API.Base;
using Core.Features.Employee.Command.Models;
using Core.Features.Employee.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ResultController
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
            return NewResult(res);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployee request)
        {
            var res = await _mediator.Send(request);
            return NewResult(res);
        }
        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var res = await _mediator.Send(new GetEmployeeById { Id = id });
            return NewResult(res);
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpadteEmployee(UpdateEmployee request)
        {
            var res = await _mediator.Send(request);
            return NewResult(res);
        }
        [HttpDelete("DeleteEmpoyee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var res = await _mediator.Send(new DeleteEmployee { Id = id });
            return NewResult(res);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> SearchPaginated([FromQuery] Search request)
        {
            var res = await _mediator.Send(request);
            return NewResult(res);
        }
        [HttpGet("Paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginatedDto request)
        {
            var res = await _mediator.Send(request);
            return NewResult(res);
        }

    }
}
