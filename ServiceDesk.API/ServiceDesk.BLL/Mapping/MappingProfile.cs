using AutoMapper;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.BLL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<Ticket, TicketModel>().ReverseMap();
        CreateMap<ExecutionRequest, ExecutionRequestModel>().ReverseMap();
    }
}
