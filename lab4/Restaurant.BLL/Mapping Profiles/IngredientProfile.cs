using AutoMapper;
using Restaurant.Common.DTOs.Ingredient;
using Restaurant.DAL.Entities;

namespace Restaurant.BLL.Mapping_Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<NewIngredientDto, Ingredient>();

            CreateMap<UpdateIngredientDto, Ingredient>();
        }
    }
}
