using Core.Features.Employee.Core.Bases;
using MediatR;
using Services.Implementation;

namespace Core.Features.Employee.Query.Models
{
    public class PaginatedDto : IRequest<Response<PaginatedResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
