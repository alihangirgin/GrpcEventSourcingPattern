using EventSourcingPattern.QueryEventsApi.Protos;
using EventSourcingPattern.Shared.Events;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace EventSourcingPattern.QueryEventsApi.Services
{
    public class OrderGrpcService : OrderProtoService.OrderProtoServiceBase
    {
        private readonly IOrderService _orderService;

        public OrderGrpcService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public override async Task<Empty> OrderCreated(OrderCreatedModel request, ServerCallContext context)
        {
            await _orderService.CreateOrder(new OrderCreated()
            {
                Id = Guid.NewGuid(),
                OrderItems = request.OrderItems.Select(x => new OrderItemMessage()
                {
                    Id = new Guid(x.Id),
                    ProductId = new Guid(x.ProductId),
                    Quantity = x.Quantity
                }).ToList(),
                Timestamp = DateTime.Now
            });
            return new Empty();
        }
    }
}
