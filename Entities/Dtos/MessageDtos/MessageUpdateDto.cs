using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MessageDtos
{
    public class MessageUpdateDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public Guid OrderId { get; set; }
    }
}