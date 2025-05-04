using AutoMapper;
using Core.Features.Employee.Command.Models;
using Services.DTO;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployee, CreateEmployeeDTO>();
            CreateMap<ReadEmployeeDTO, UpdateEmployeeDTO>();
        }
    }
}
