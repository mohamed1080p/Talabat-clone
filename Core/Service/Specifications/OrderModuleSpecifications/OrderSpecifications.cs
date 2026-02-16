using Domain.Models.OrderModule;
using Service.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.OrderModuleSpecifications
{
    internal class OrderSpecifications:BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string Email):base(a=>a.UserEmail==Email)
        {
            AddInclude(a => a.DeliveryMethod);
            AddInclude(a => a.Items);
            AddOrderByDesc(a => a.OrderDate);
        }

        public OrderSpecifications(Guid Id):base(a=>a.Id==Id)
        {
            AddInclude(a => a.DeliveryMethod);
            AddInclude(a => a.Items);
        }
    }
}
