using Core.Utilities.Results.Abstract;
using Entities.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task<IResult> CreateAsync(OrderCreateDto dto);
        Task<IDataResult<IList<OrderListDto>>> GetAllAsync();
        Task<IResult> UpdateAsync(OrderUpdateDto dto);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<OrderListDto>> GetByIdAsync(Guid id);
    }
}