using AutoMapper;
using Data.Models;
using Services.DTO;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, ReadEmployeeDTO>();

            CreateMap<CreateEmployeeDTO, Employee>();
        }
    }
}
