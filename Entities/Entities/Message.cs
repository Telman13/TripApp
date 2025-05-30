using Entities.Common;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Message : BaseEntity
    {
        public string Description { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Pending;
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
