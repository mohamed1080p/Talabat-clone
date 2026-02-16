
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }


        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        // not mapped
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;

    }
}
