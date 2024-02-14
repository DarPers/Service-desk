using AutoMapper;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.BLL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, Models.User>().ReverseMap();
        CreateMap<Ticket, Models.Ticket>().ReverseMap();
        CreateMap<ExecutionRequest, Models.ExecutionRequest>().ReverseMap();
    }
}
