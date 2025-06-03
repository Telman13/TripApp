using Entities.Common;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Order : BaseEntity
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TripDate { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
