using AutoMapper;
using Warehouse.BLL.Models;
using Warehouse.DAL.Entities;

namespace Warehouse.BLL.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DeviceModel, Device>().ReverseMap();
    }
}
