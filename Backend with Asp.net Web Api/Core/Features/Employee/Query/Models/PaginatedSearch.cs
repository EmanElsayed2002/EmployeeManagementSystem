using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.DTO;

namespace Core.Features.Employee.Query.Models
{
    public class PaginatedSearch : IRequest<Response<IEnumerable<ReadEmployeeDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
