using AutoMapper;
using Core.Features.Employee.Command.Models;
using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.Abstract;
using Services.DTO;

namespace Core.Features.Employee.Command.Handler
{
    public class EmployeeHandler : ResponseHandler, IRequestHandler<CreateEmployee, Response<string>>, IRequestHandler<UpdateEmployee, Response<string>>, IRequestHandler<DeleteEmployee, Response<string>>
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeeHandler(IEmployeeService employeeService, IMapper mediator)
        {
            _service = employeeService;
            _mapper = mediator;
        }

        public async Task<Response<string>> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            var emp = _mapper.Map<CreateEmployeeDTO>(request);
            var res = await _service.CreateEmployee(emp);
            return res.Match(
                 error => BadRequest<string>(),
                 success => Created($"Employee created successfully"));
        }

        public async Task<Response<string>> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            try
            {
                var emp = await _service.GetEmployeeById(request.Id);


                if (emp.IsT0)
                {
                    var error = emp.AsT0;
                    return new Response<string>($"error: {error}", false);
                }
                var employee = emp.AsT1;
                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    employee.Email = request.Email;
                }

                if (!string.IsNullOrWhiteSpace(request.FirstName))
                {
                    employee.FirstName = request.FirstName;
                }

                if (!string.IsNullOrWhiteSpace(request.LastName))
                {
                    employee.LastName = request.LastName;
                }

                if (!string.IsNullOrWhiteSpace(request.Position))
                {
                    employee.Position = request.Position;
                }
                var map = _mapper.Map<UpdateEmployeeDTO>(employee);
                var res = await _service.UpdateEmployee(map);

                return Success("Employee Updated Successfully");


            }
            catch (Exception ex)
            {
                return BadRequest<string>("Error when Updated");
            }

        }

        public async Task<Response<string>> Handle(DeleteEmployee request, CancellationToken cancellationToken)
        {
            var res = await _service.DeleteEmployee(request.Id);
            return res.Match(error => BadRequest<string>("Error Occured Please Confirm Employee Is Exist First!!"), success => Success("Employee Deleted successfully"));
        }

    }
}
