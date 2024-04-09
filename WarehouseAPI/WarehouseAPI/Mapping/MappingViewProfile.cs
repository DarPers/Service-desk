using AutoMapper;
using Warehouse.BLL.Models;
using WarehouseAPI.ViewModels.Device;

namespace WarehouseAPI.Mapping;

public class MappingViewProfile : Profile
{
    public MappingViewProfile()
    {
        CreateMap<DeviceModel, DeviceViewModel>().ReverseMap();
        CreateMap<DeviceUpdatedViewModel, DeviceModel>();
        CreateMap<DeviceAddedViewModel, DeviceModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(o => Guid.NewGuid()));
    }
}
