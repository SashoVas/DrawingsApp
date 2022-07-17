using DrawingsApp.Images.Data;
using DrawingsApp.Images.Services;
using DrawingsApp.Images.Services.Contracts;
using DrawingsApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using DrawingsApp.Images.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var defaultConnection = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DrawingsAppImagesDbContext>(
        options => options.UseSqlServer(defaultConnection));
builder.AddAuthenticationWithJWT();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddCors();
builder.Services.AddTransient<IImageService, ImageService>();
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetRequiredService<DrawingsAppImagesDbContext>())
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
app.AddStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();