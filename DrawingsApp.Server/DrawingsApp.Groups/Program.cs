using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Infrastructure;
using DrawingsApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddCors();
builder.AddAuthenticationWithJWT();
var defaultConnection = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DrawingsAppGroupsDbContext>(opitons => opitons.UseSqlServer(defaultConnection));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddServices();
var app = builder.Build();
using (var scope=app.Services.CreateScope())
{
    using (var context=scope.ServiceProvider.GetService<DrawingsAppGroupsDbContext>()!)
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();