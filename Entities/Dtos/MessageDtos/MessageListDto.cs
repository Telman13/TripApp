using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MessageDtos
{
    public class MessageListDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string SenderFullName { get; set; }
        public string ReceiverFullName { get; set; }
        public Guid OrderId { get; set; }
    }
}