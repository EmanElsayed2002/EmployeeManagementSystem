using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.Implementation;

namespace Core.Features.Employee.Query.Models
{
    public class Search : IRequest<Response<PaginatedResponse>>
    {
        public string Key { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
