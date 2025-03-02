using GrpcPersonService.Models;
using GrpcPersonService;
using GrpcPersonService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddSingleton<IRepository<Person>, Repository<Person>>();
builder.Services.AddLogging();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PersonGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
