using Business.Abstract;
using DataAccess.Abstract;
using Entities.Dtos.OrderDtos;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<List<OrderListDto>> GetAllAsync()
        {
            var orders = await _orderDal.GetAllAsync(
                tracking: false,
                include: q => q.Include(o => o.User)
                               .Include(o => o.Messages));

            return orders.Select(o => new OrderListDto
            {
                Id = o.Id,
                FromCity = o.FromCity,
                ToCity = o.ToCity,
                TripDate = o.TripDate,
                Description = o.Description,
                UserFullName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : "N/A"
            }).ToList();
        }
    }
}
