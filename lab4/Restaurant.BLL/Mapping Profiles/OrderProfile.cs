using AutoMapper;
using Restaurant.Common.DTOs.Order;
using Restaurant.Common.DTOs.OrderItem;
using Restaurant.DAL.Entities;

namespace Restaurant.BLL.Mapping_Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<NewOrderItemDto, OrderItem>();
        }
    }
}
