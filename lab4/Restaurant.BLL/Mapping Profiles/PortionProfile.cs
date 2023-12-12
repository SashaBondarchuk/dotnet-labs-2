using AutoMapper;
using Restaurant.Common.DTOs.Portion;
using Restaurant.Common.DTOs.Unit;
using Restaurant.DAL.Entities;

namespace Restaurant.BLL.Mapping_Profiles
{
    public class PortionProfile : Profile
    {
        public PortionProfile()
        {
            CreateMap<Portion, PortionDto>();

            CreateMap<NewPortionDto, Portion>();

            CreateMap<UpdatePortionDto, Portion>();

            CreateMap<Unit, UnitDto>();
        }
    }
}
