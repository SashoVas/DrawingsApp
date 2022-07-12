using DrawingsApp.Images.Data;
using DrawingsApp.Images.Services;
using DrawingsApp.Images.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var defaultConnection = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DrawingsAppDbContext>(
        options => options.UseSqlServer(defaultConnection));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IImageService, ImageService>();
var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetRequiredService<DrawingsAppDbContext>())
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

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions 
{ 
    OnPrepareResponse=preperation=>
    {
        var headers=preperation.Context.Response.GetTypedHeaders();
        headers.CacheControl = new CacheControlHeaderValue
        {
            Public = true,
            MaxAge = TimeSpan.FromDays(1)
        };
        headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(1));
    }
});
app.UseAuthorization();

app.MapControllers();

app.Run();
