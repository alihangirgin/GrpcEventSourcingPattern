using EventSourcingPattern.QueryEventsApi.Protos;
using MediatR;

namespace EventSourcingPattern.CommandEventsApi.Commands
{
    public sealed record OrderItem(Guid ProductId, int Quantity);

    public sealed record CreateOrderCommand(List<OrderItem> OrderItems) : IRequest;

    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly OrderProtoService.OrderProtoServiceClient _client;
        public CreateOrderCommandHandler(OrderProtoService.OrderProtoServiceClient client)
        {
            _client = client;
        }
        //private readonly IEventStoreService _eventStoreService;

        //public CreateOrderCommandHandler(IEventStoreService eventStoreService)
        //{
        //    _eventStoreService = eventStoreService;
        //}

        public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //OrderCreated orderCreated = new()
            //{
            //    Id = Guid.NewGuid(),
            //    OrderItems = command.OrderItems.Select(x=> new OrderItemMessage()
            //    {
            //        Id = Guid.NewGuid(),
            //        ProductId = x.ProductId,
            //        Quantity = x.Quantity

            //    }).ToList()
            //};
            //await _eventStoreService.AppendEventToStreamAsync(StreamConstants.OrderStream, orderCreated,
            //    orderCreated.GetType().Name);

            _client.OrderCreated(new OrderCreatedModel()
            {
                Id = Guid.Empty.ToString(),
                OrderItems = { command.OrderItems.Select(x => new OrderItemModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = x.ProductId.ToString(),
                    Quantity = x.Quantity

                }).ToList() }
            });
        }
    }
}
