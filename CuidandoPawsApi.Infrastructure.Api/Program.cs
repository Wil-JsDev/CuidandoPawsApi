using CuidandoPawsApi.Infrastructure.Persistence.IOC;
using CuidandoPawsApi.Application.IOC;
using CuidandoPawsApi.Infrastructure.Shared;
using CuidandoPawsApi.Infrastructure.Api.Extensions;
using CuidandoPawsApi.Infrastructure.Identity.IOC;
using Microsoft.AspNetCore.Identity;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using CuidandoPawsApi.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.RateLimiting;
using CuidandoPawsApi.Infrastructure.Api.ExceptionHandling;

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

// Register the global exception handler
builder.Services.AddException();

// Rate Limiter
builder.Services.AddRateLimiter(options =>
{
	options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, token) =>
    {
        await context.HttpContext.Response.WriteAsync("Request limit exceeded. Please try again later");
    };
	options.AddFixedWindowLimiter("fixed", limiterOptions =>
	{
		limiterOptions.Window = TimeSpan.FromMinutes(3);
        limiterOptions.PermitLimit = 5;
    });
    options.AddSlidingWindowLimiter("sliding", limiterOptions =>
    {
        limiterOptions.PermitLimit = 5;
        limiterOptions.SegmentsPerWindow = 5;
        limiterOptions.Window = TimeSpan.FromMinutes(15);
    });
    options.AddTokenBucketLimiter("token", limiterOptions =>
    {
        limiterOptions.TokenLimit = 100;
        limiterOptions.TokensPerPeriod = 3;
        limiterOptions.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
    });
});

var app = builder.Build();
app.UseExceptionHandler(_ => { });

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
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerExtension();

app.MapControllers();

app.Run();
