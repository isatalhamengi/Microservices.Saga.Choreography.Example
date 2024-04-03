using MassTransit;
using Order.API.Models.Contexts;
using Shared.Events;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        OrderAPIDbContext _db;

        public PaymentCompletedEventConsumer(OrderAPIDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _db.Orders.FindAsync(context.Message.OrderId);
            if (order == null)
                throw new NullReferenceException();
            order.OrderStatus = Enums.OrderStatus.Completed;
            await _db.SaveChangesAsync();
        }
    }
}
