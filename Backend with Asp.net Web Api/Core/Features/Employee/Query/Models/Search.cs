using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.DTO;

namespace Core.Features.Employee.Query.Models
{
    public class Search : IRequest<Response<IEnumerable<ReadEmployeeDTO>>>
    {
        public string Key { get; set; }
    }
}
