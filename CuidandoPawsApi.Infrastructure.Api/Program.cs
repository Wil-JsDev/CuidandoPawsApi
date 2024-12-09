using CuidandoPawsApi.Infrastructure.Persistence.IOC;
using CuidandoPawsApi.Application.IOC;
using dotenv.net;
using CuidandoPawsApi.Infrastructure.Shared;
using CuidandoPawsApi.Infrastructure.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExtension();


//DI
builder.Services.AddVersioning();
builder.Services.AddPersistence(configuration);
builder.Services.AddApplicationService();
builder.Services.AddSharedLayer(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();
app.UseSwaggerExtension();

app.MapControllers();

app.Run();
