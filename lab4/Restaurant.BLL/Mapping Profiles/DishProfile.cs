using AutoMapper;
using Restaurant.Common.DTOs.Dish;
using Restaurant.DAL.Entities;

namespace Restaurant.BLL.Mapping_Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDto>();

            CreateMap<NewDishDto, Dish>()
                .ForMember(dest => dest.Ingredients, opt => opt.Ignore());

            CreateMap<UpdateDishDto, Dish>();
        }
    }
}
