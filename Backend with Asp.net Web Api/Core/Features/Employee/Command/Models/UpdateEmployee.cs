
using Core.Features.Employee.Core.Bases;
using MediatR;

namespace Core.Features.Employee.Command.Models
{
    public class UpdateEmployee : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
    }
}
