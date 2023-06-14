using Microsoft.CodeAnalysis.Host;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Profile_Service.Entities;
using Profile_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Social_Service.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            //you can configure your custom policy
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

string domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("manage:profile", policy => policy.Requirements.Add(new HasScopeRequirement("manage:profile", domain)));
    options.AddPolicy("create:user", policy => policy.Requirements.Add(new HasScopeRequirement("create:user", domain)));
    options.AddPolicy("manage:functions", policy => policy.Requirements.Add(new HasScopeRequirement("manage:functions", domain)));
    options.AddPolicy("manage:educations", policy => policy.Requirements.Add(new HasScopeRequirement("manage:educations", domain)));
    options.AddPolicy("manage:specializations", policy => policy.Requirements.Add(new HasScopeRequirement("manage:specializations", domain)));
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


// Add services to the container.
builder.Services.Configure<DBContext>(
    builder.Configuration.GetSection("MongoDb"));

builder.Services.AddScoped<DBContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<EducationService>();
builder.Services.AddScoped<SpecializationService>();
builder.Services.AddScoped<FunctionService>();

builder.Services.AddControllers();

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
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
