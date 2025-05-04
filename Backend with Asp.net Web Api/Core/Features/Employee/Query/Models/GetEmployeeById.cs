using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.DTO;

namespace Core.Features.Employee.Query.Models
{
    public class GetEmployeeById : IRequest<Response<ReadEmployeeDTO>>
    {
        public int Id { get; set; }
    }
}
