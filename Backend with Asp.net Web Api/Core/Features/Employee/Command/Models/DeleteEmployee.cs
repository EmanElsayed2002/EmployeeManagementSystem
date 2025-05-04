

using Core.Features.Employee.Core.Bases;
using MediatR;

namespace Core.Features.Employee.Command.Models
{
    public class DeleteEmployee : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
