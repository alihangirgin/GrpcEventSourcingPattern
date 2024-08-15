using EventSourcingPattern.CommandEventsApi.Protos;
using EventSourcingPattern.Shared.Events;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace EventSourcingPattern.QueryEventsApi.Services
{
    public class ProductGrpcService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IProductService _productService;

        public ProductGrpcService(IProductService productService)
        {
            _productService = productService;
        }

        public override async Task<Empty> ProductCreated(ProductCreatedModel request, ServerCallContext context)
        {
            await _productService.CreateProduct(new ProductCreated()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = (decimal)request.Price.Value / (decimal)Math.Pow(10, request.Price.Scale),
                Stock = request.Stock,
                Timestamp = DateTime.Now
            });
            return new Empty();
        }
        public override async Task<Empty> ProductNameUpdated(ProductNameUpdatedModel request, ServerCallContext context)
        {
            await _productService.UpdateProductName(new ProductNameUpdated()
            {
                Id = new Guid(request.Id),
                Name = request.Name,
                Timestamp = DateTime.Now
            });
            return new Empty();
        }
        public override async Task<Empty> ProductPriceUpdated(ProductPriceUpdatedModel request, ServerCallContext context)
        {
            await _productService.UpdateProductPrice(new ProductPriceUpdated()
            {
                Id = new Guid(request.Id),
                Price = (decimal)request.Price.Value / (decimal)Math.Pow(10, request.Price.Scale),
                Timestamp = DateTime.Now
            });
            return new Empty();
        }
    }
}
