using AutoMapper;
using DbModels;
using DbModels.Dtos;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientDB, ClientDto>();
            CreateMap<EmployeeDB, EmployeeDto>();
        }
    }
}

