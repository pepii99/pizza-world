using Microsoft.EntityFrameworkCore;
using pizza_hub.Data;
using pizza_hub.Helpers;
using pizza_hub.Infrastructure.Extensions;
using pizza_hub.Infrastructure.Filters;
using pizza_hub.Services.Identity;
using pizza_hub.Services.Pizza;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddCors()
    .AddControllers(options => options
    .Filters
    .Add<ModelOrNotFoundActionFilter>());

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAutoMapper(typeof(Program).Assembly);

services.AddAuthorization();
services.AddIdentity();

services.AddScoped<IPizzaService, PizzaService>();
services.AddScoped<IIdentityService, IdentityService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseCors(x => x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
