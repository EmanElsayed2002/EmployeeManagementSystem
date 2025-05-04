using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.DTO;

namespace Core.Features.Employee.Query.Models
{
    public class GetAllEmployeeList : IRequest<Response<IEnumerable<ReadEmployeeDTO>>>
    {

    }
}
