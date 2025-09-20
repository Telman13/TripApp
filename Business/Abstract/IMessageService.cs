using Core.Utilities.Results.Abstract;
using Entities.Dtos.MessageDtos;
using Entities.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMessageService
    {
        Task<IResult> CreateAsync(MessageCreateDto dto);
        Task<IDataResult<IList<MessageListDto>>> GetAllAsync();
        Task<IResult> UpdateAsync(MessageUpdateDto dto);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<MessageListDto>> GetByIdAsync(Guid id);
    }
}