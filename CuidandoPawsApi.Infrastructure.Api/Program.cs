using CuidandoPawsApi.Infrastructure.Persistence.IOC;
using CuidandoPawsApi.Application.IOC;
using dotenv.net;
using CuidandoPawsApi.Infrastructure.Shared;
using CuidandoPawsApi.Infrastructure.Api.Extensions;
using CuidandoPawsApi.Infrastructure.Identity.IOC;
using Microsoft.AspNetCore.Identity;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using CuidandoPawsApi.Infrastructure.Identity.Seeds;


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
builder.Services.AddIdentity(configuration);
builder.Services.AddValidations();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

	try
	{
		var userManager = services.GetRequiredService<UserManager<User>>();
		var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();

		await DefaultAdmin.SeedAsync(userManager, rolManager);
		await DefaultRoles.SeedAsync(userManager, rolManager);
	}
	catch (Exception)
	{
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerExtension();

app.MapControllers();

app.Run();
