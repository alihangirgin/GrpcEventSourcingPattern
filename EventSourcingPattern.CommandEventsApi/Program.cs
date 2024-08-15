using EventSourcingPattern.CommandEventsApi.Protos;
using EventSourcingPattern.CommandEventsApi.Services;
using EventSourcingPattern.QueryEventsApi.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
//builder.Services.AddSingleton<IEventStoreService>(provider => new EventStoreService(builder.Configuration.GetConnectionString("EventStore") ?? string.Empty));

builder.Services.AddGrpcClient<OrderProtoService.OrderProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:ServiceUrl"]);
});
builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:ServiceUrl"]);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();