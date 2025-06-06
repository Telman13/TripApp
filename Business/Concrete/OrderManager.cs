using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Dtos.OrderDtos;
using Entities.Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

        public async Task<IResult> CreateAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                FromCity = dto.FromCity,
                ToCity = dto.ToCity,
                TripDate = dto.TripDate,
                Description = dto.Description,
                UserId = dto.UserId,
                Status = OrderStatus.Pending
            };

            await _orderDal.AddAsync(order);
            return new SuccessResult(HttpStatusCode.Created, "Sifariş uğurla yaradıldı");
        }


        public async Task<IDataResult<List<OrderListDto>>> GetAllAsync()
        {
            var orders = await _orderDal.GetAllAsync(
                tracking: false,
                include: q => q.Include(o => o.User)
                               .Include(o => o.Messages));

            var dtoList = orders.Select(o => new OrderListDto
            {
                Id = o.Id,
                FromCity = o.FromCity,
                ToCity = o.ToCity,
                TripDate = o.TripDate,
                Description = o.Description,
                UserFullName = o.User != null ? $"{o.User.FirstName} {o.User.LastName}" : "N/A"
            }).ToList();

            return new SuccessDataResult<List<OrderListDto>>(dtoList, HttpStatusCode.OK, "Sifarişlər uğurla gətirildi");
        }

        public async Task<IResult> UpdateAsync(OrderUpdateDto dto)
        {
            var order = await _orderDal.GetAsync(o => o.Id == dto.Id);

            if (order == null)
                return new ErrorResult(HttpStatusCode.NotFound, "Sifariş tapılmadı");

            order.FromCity = dto.FromCity;
            order.ToCity = dto.ToCity;
            order.TripDate = dto.TripDate;
            order.Description = dto.Description;

            await _orderDal.UpdateAsync(order);
            return new SuccessResult(HttpStatusCode.OK, "Sifariş uğurla yeniləndi");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var order = await _orderDal.GetAsync(o => o.Id == id);

            if (order == null)
                return new ErrorResult(HttpStatusCode.NotFound, "Sifariş tapılmadı");

            await _orderDal.DeleteAsync(order);
            return new SuccessResult(HttpStatusCode.OK, "Sifariş uğurla silindi");
        }
    }
}
