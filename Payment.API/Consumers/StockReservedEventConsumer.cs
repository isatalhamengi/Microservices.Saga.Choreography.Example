using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        IPublishEndpoint _publishEndPoint;

        public StockReservedEventConsumer(IPublishEndpoint publishEndPoint)
        {
            _publishEndPoint = publishEndPoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            if (true)
            {
                PaymentCompletedEvent paymentCompletedEvent = new()
                {
                    OrderId = context.Message.OrderId
                };
                await _publishEndPoint.Publish(paymentCompletedEvent);
                await Console.Out.WriteLineAsync("Ödeme Başarılı!");
            }
            else
            {
                PaymentFailedEvent paymentFailedEvent = new()
                {
                    OrderId = context.Message.OrderId,
                    Message = "Başarısız",
                    OrderItems = context.Message.OrderItems
                };
                await _publishEndPoint.Publish(paymentFailedEvent);
                await Console.Out.WriteLineAsync("Ödeme Başarısız!");
            }
        }
    }
}
