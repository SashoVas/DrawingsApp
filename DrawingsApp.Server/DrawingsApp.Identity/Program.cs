using DrawingsApp.Identity.Data;
using DrawingsApp.Identity.Services;
using DrawingsApp.Identity.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using DrawingsApp.Identity.Infrastructure;
using DrawingsApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var defaultConnection = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DrawingsAppIdentityDbContext>(options => options.UseSqlServer(defaultConnection));

builder.AddIdentity();
builder.AddAuthenticationWithJWT();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddCors();
builder.Services.AddTransient<IIdentityService, IdentityService>();
var app = builder.Build();
app.SettupDb<DrawingsAppIdentityDbContext>();
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
