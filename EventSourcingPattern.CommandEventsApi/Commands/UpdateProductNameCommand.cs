using EventSourcingPattern.CommandEventsApi.Protos;
using MediatR;

namespace EventSourcingPattern.CommandEventsApi.Commands
{
    public sealed record UpdateProductNameCommand(Guid Id, string Name) : IRequest
    {
        public UpdateProductNameCommand SetId(Guid newId)
        {
            return this with { Id = newId };
        }
    }
    public sealed class UpdateProductNameCommandHandler : IRequestHandler<UpdateProductNameCommand>
    {
        //private readonly IEventStoreService _eventStoreService;

        //public UpdateProductNameCommandHandler(IEventStoreService eventStoreService)
        //{
        //    _eventStoreService = eventStoreService;
        //}
        private readonly ProductProtoService.ProductProtoServiceClient _client;
        public UpdateProductNameCommandHandler(ProductProtoService.ProductProtoServiceClient client)
        {
            _client = client;
        }

        public async Task Handle(UpdateProductNameCommand command, CancellationToken cancellationToken)
        {
            //ProductNameUpdated productNameUpdated = new()
            //{   
            //    Id = command.Id,
            //    Name = command.Name,
            //    Timestamp = DateTime.Now
            //};
            //await _eventStoreService.AppendEventToStreamAsync(StreamConstants.ProductStream, productNameUpdated, 
            //    productNameUpdated.GetType().Name);

            _client.ProductNameUpdated(new ProductNameUpdatedModel()
            {
                Id = command.Id.ToString(),
                Name = command.Name
            });
        }
    }
}
