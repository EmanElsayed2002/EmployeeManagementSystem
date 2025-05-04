using Core.Features.Employee.Core.Bases;
using Core.Features.Employee.Query.Models;
using MediatR;
using Services.Abstract;
using Services.DTO;

namespace Core.Features.Employee.Query.Handler
{
    public class EmployeeHandler : ResponseHandler, IRequestHandler<GetAllEmployeeList, Response<IEnumerable<ReadEmployeeDTO>>>, IRequestHandler<GetEmployeeById, Response<ReadEmployeeDTO>>, IRequestHandler<Search, Response<IEnumerable<ReadEmployeeDTO>>>, IRequestHandler<PaginatedSearch, Response<IEnumerable<ReadEmployeeDTO>>>
    {
        private readonly IEmployeeService _service;

        public EmployeeHandler(IEmployeeService employeeService)
        {
            _service = employeeService;
        }
        public async Task<Response<IEnumerable<ReadEmployeeDTO>>> Handle(GetAllEmployeeList request, CancellationToken cancellationToken)
        {
            {
                var emps = await _service.GetAll();
                var res = Success(emps);
                res.Meta = new { Count = emps.Count() };
                return res;
            }
        }

        public async Task<Response<ReadEmployeeDTO>> Handle(GetEmployeeById request, CancellationToken cancellationToken)
        {

            var res = await _service.GetEmployeeById(request.Id);
            return res.Match(
                 error => BadRequest<ReadEmployeeDTO>("Error occured can not get employee"),
                 success => Success(success, "Employee retrieved successfully")
             );
        }

        public async Task<Response<IEnumerable<ReadEmployeeDTO>>> Handle(Search request, CancellationToken cancellationToken)
        {
            var res = await _service.Search(request.Key);
            return res.Match(
             err => BadRequest<IEnumerable<ReadEmployeeDTO>>(err.Description),
             success => Success(success)
            );
        }

        public async Task<Response<IEnumerable<ReadEmployeeDTO>>> Handle(PaginatedSearch request, CancellationToken cancellationToken)
        {
            var res = await _service.Paginated(request.PageNumber, request.PageSize);
            return res.Match(
             err => BadRequest<IEnumerable<ReadEmployeeDTO>>(err.Description),
             success => Success(success)
         );
        }
    }
}
