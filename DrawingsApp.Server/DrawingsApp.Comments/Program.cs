using DrawingsApp.Comments.Data;
using DrawingsApp.Comments.Data.Repositories;
using DrawingsApp.Comments.Messages.Group;
using DrawingsApp.Comments.Messages.Post;
using DrawingsApp.Comments.Messages.User;
using DrawingsApp.Comments.Services;
using DrawingsApp.Comments.Services.Contracts;
using DrawingsApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddCors();
builder.AddAuthenticationWithJWT();
builder.AddMessages(
    typeof(PromoteUserRoleInGroupConsumer),
    typeof(RemoveRoleFromUserMessageConsumer),
    typeof(LikePostConsumer),
    typeof(GroupCreateConsumer));
builder.Services.AddSingleton<MongoDbCommentsDb>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IUserRoleInGroupRepository, UserRoleInGroupRepository>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ICommentsService, CommentsService>();

var app = builder.Build();

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
