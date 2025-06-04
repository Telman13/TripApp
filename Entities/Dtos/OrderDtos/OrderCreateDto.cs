using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.OrderDtos
{
    public class OrderCreateDto
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TripDate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
