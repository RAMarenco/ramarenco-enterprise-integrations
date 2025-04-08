using DragonBall.Api.Filters.SwaggerFilters;
using DragonBall.Api.Middlewares;
using DragonBall.Application;
using DragonBall.Domain.Interfaces;
using DragonBall.Domain.Interfaces.ExternalServices;
using DragonBall.Infra;
using DragonBall.Infra.ExternalServices;
using DragonBall.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpClient<IDragonBallApi, DragonBallApi>(client =>
{
    client.BaseAddress = new Uri("https://dragonball-api.com/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DragonBall API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.OperationFilter<AuthResponsesOperationFilter>();
});

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["jwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)
            )
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                // Get the raw JWT token from the request
                var token = context.HttpContext.Request.Headers["Authorization"]
                    .FirstOrDefault()?
                    .Split(" ")
                    .Last(); // Gets the token after "Bearer "

                if (string.IsNullOrEmpty(token))
                {
                    context.Fail("Token not found in Authorization header");
                    return;
                }

                var jwtService = context.HttpContext.RequestServices
                    .GetRequiredService<IJwtService>();

                var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId is null)
                {
                    context.Fail("Token validation failed");
                    return;
                }

                if (!await jwtService.ValidateTokenBelongsToUserAsync(token, userId))
                {
                    context.Fail("Token validation failed");
                }
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
