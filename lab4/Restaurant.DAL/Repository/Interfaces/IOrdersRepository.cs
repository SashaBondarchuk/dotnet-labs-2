﻿using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Repository.Interfaces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int id);
    }
}
