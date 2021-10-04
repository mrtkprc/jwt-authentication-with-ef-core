using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomerId(int id);
    }
}
