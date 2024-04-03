using MassTransit;
using Order.API.Models.Contexts;
using Shared.Events;

namespace Order.API.Consumers
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        OrderAPIDbContext _db;

        public StockNotReservedEventConsumer(OrderAPIDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            var order = await _db.Orders.FindAsync(context.Message.OrderId);
            if (order == null)
                throw new NullReferenceException();
            order.OrderStatus = Enums.OrderStatus.Fail;
            await _db.SaveChangesAsync();
        }
    }
}
