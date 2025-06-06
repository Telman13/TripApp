using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.OrderDtos
{
    public class OrderUpdateDto
    {
        public Guid Id { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TripDate { get; set; }
        public string Description { get; set; }
    }
}
