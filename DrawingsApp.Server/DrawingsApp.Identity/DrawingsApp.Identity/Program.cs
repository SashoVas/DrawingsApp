using DrawingsApp.Identity.Data;
using DrawingsApp.Identity.Services;
using DrawingsApp.Identity.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using DrawingsApp.Identity.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var defaultConnection = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DrawingsAppIdentityDbContext>(options=>options.UseSqlServer(defaultConnection));
builder.AddIdentity();
builder.AddAuthenticationWithJWT();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddCors();
builder.Services.AddTransient<IIdentityService, IdentityService>();
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    using (var context=serviceScope.ServiceProvider.GetService<DrawingsAppIdentityDbContext>()!)
    {
        context.Database.EnsureCreated();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
