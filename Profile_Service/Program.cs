using Microsoft.CodeAnalysis.Host;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Profile_Service.Entities;
using Profile_Service.Services;
using MassTransit;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DBContext>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddScoped<DBContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<EducationService>();
builder.Services.AddScoped<ThemeService>();

builder.Services.AddControllers();

// Configure host
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
