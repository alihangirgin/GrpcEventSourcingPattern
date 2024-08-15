using EventSourcingPattern.CommandEventsApi.Protos;
using MediatR;

namespace EventSourcingPattern.CommandEventsApi.Commands
{
    public sealed record CreateProductCommand(string Name, decimal Price, int Stock) : IRequest;

    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        //private readonly IEventStoreService _eventStoreService;

        //public CreateProductCommandHandler(IEventStoreService eventStoreService)
        //{
        //    _eventStoreService = eventStoreService;
        //}
        private readonly ProductProtoService.ProductProtoServiceClient _client;
        public CreateProductCommandHandler(ProductProtoService.ProductProtoServiceClient client)
        {
            _client = client;
        }

        public async Task Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //ProductCreated productCreatedEvent = new()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = command.Name,
            //    Price = command.Price,
            //    Stock = command.Stock,
            //    Timestamp = DateTime.Now
            //};
            //await _eventStoreService.AppendEventToStreamAsync(StreamConstants.ProductStream, productCreatedEvent,
            //    productCreatedEvent.GetType().Name);

            _client.ProductCreated(new ProductCreatedModel()
            {
                Price = new DecimalValue()
                {
                    Value = (long)(command.Price * 100),
                    Scale = 2
                },
                Stock = command.Stock,
                Name = command.Name
            });
        }
    }
}
