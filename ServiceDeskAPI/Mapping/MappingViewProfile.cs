using AutoMapper;
using ServiceDesk.BLL.Models;
using ServiceDesk.Domain.Enums;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Mapping;

public class MappingViewProfile : Profile
{
    public MappingViewProfile()
    {
        CreateMap<UserModel, UserViewModel>();
        CreateMap<TicketModel, TicketViewModel>();
        CreateMap<ExecutionRequestModel, ExecutionRequestViewModel>();
        ConfigureMapping(CreateMap<UserRegistrationViewModel, UserModel>());
        ConfigureMapping(CreateMap<TicketCreationViewModel, TicketModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(o => Status.Free)));
        ConfigureMapping(CreateMap<ExecutionRequestCreationViewModel, ExecutionRequestModel>());
        CreateMap<TicketUpdatingViewModel, TicketModel>();
    }

    private static void ConfigureMapping<TSource, TDestination> (IMappingExpression<TSource, TDestination> mappingExpression)
        where TDestination : BaseModel
    {
        mappingExpression.ForMember(dest => dest.Id, opt => opt.MapFrom(o => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(o => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(o => DateTime.UtcNow));
    }
}
