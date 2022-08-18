using DrawingsApp.Groups.Data;
using DrawingsApp.Groups.Data.Seeders;
using DrawingsApp.Groups.Infrastructure;
using DrawingsApp.Groups.Messages.Post;
using DrawingsApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddCors();
builder.AddAuthenticationWithJWT();
builder.AddMessages(
    typeof(PostCreatedConsumer),
    typeof(PostUpdatedConsumer),
    typeof(PostDeleteConsumer));
var defaultConnection = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DrawingsAppGroupsDbContext>(options => options.UseSqlServer(defaultConnection));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddServices();
var app = builder.Build();
app.SettupDb<DrawingsAppGroupsDbContext>(typeof(DbSeeder));
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