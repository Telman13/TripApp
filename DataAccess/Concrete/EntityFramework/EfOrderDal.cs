using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfRepositoryBase<Order, TripAppDbContext>, IOrderDal
    {
        public EfOrderDal(TripAppDbContext context) : base(context)
        {
        }
    }
}
