using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Dtos.MessageDtos;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;
        public MessageManager(IMessageDal messageDal) 
        {
            _messageDal = messageDal;
        }

        public async Task<IResult> CreateAsync(MessageCreateDto dto)
        {
            var message = new Message
            {
                OrderId = dto.OrderId,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Description = dto.Description
            };

            await _messageDal.AddAsync(message);
            return new SuccessResult(HttpStatusCode.Created, "Mesaj uğurla yaradıldı");
        }

        public async Task<IDataResult<IList<MessageListDto>>> GetAllAsync()
        {
            var messages = await _messageDal.GetAllAsync(
                tracking: false,
                include: q => q.Include(m => m.Sender)
                               .Include(m => m.Receiver)
            );

            var dtoList = messages.Select(m => new MessageListDto
            {
                Id = m.Id,
                Description = m.Description,
                SenderFullName = m.Sender != null ? $"{m.Sender.FirstName} {m.Sender.LastName}" : "N/A",
                ReceiverFullName = m.Receiver != null ? $"{m.Receiver.FirstName} {m.Receiver.LastName}" : "N/A",
                OrderId = m.OrderId
            }).ToList();

            return new SuccessDataResult<IList<MessageListDto>>(dtoList, HttpStatusCode.OK, "Bütün mesajlar uğurla əldə edildi");
        }

        public async Task<IResult> UpdateAsync(MessageUpdateDto dto)
        {
            var message = await _messageDal.GetAsync(
                predicate: m => m.Id == dto.Id,
                tracking: true
            );

            if (message is null)
                return new ErrorResult(HttpStatusCode.NotFound, "Mesaj tapılmadı");

            message.Description = dto.Description;

            await _messageDal.UpdateAsync(message);
            return new SuccessResult(HttpStatusCode.OK, "Mesaj uğurla yeniləndi");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var message = await _messageDal.GetAsync(
                predicate: m => m.Id == id,
                tracking: true
            );

            if (message is null)
                return new ErrorResult(HttpStatusCode.NotFound, "Mesaj tapılmadı");

            await _messageDal.DeleteAsync(message);
            return new SuccessResult(HttpStatusCode.OK, "Mesaj uğurla silindi");
        }

        public async Task<IDataResult<MessageListDto>> GetByIdAsync(Guid id)
        {
            var message = await _messageDal.GetAsync(
                predicate: m => m.Id == id,
                tracking: false,
                include: q => q.Include(m => m.Sender)
                               .Include(m => m.Receiver)
            );  

            if (message is null)
                return new ErrorDataResult<MessageListDto>(null, HttpStatusCode.NotFound, "Mesaj tapılmadı");

            var dto = new MessageListDto
            {
                Id = message.Id,
                Description = message.Description,
                SenderFullName = message.Sender != null ? $"{message.Sender.FirstName} {message.Sender.LastName}" : "N/A",
                ReceiverFullName = message.Receiver != null ? $"{message.Receiver.FirstName} {message.Receiver.LastName}" : "N/A",
                OrderId = message.OrderId
            };

            return new SuccessDataResult<MessageListDto>(dto, HttpStatusCode.OK, "Mesaj uğurla əldə edildi");
        }
    }
}