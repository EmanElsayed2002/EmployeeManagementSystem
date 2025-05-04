using Core.Features.Employee.Core.Bases;
using MediatR;

namespace Core.Features.Employee.Command.Models
{
    public class CreateEmployee : IRequest<Response<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
    }
}
