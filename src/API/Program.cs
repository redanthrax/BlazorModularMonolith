using System.Text;
using API.Extensions;
using Common.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.WithOrigins("http://localhost:5265")
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Postgres");

var boxesAssembly = typeof(API.Modules.Boxes.BoxesModule).Assembly;
var comicsAssembly = typeof(API.Modules.Comics.ComicsModule).Assembly;
var authAssembly = typeof(API.Modules.Authentication.AuthenticationModule).Assembly;
var recommendationsAssembly = typeof(API.Modules.Recommendations.RecommendationsModule).Assembly;

builder.Services.AddModules(builder.Configuration);

builder.Services.AddEndpoints(boxesAssembly);
builder.Services.AddEndpoints(comicsAssembly);
builder.Services.AddEndpoints(authAssembly);
builder.Services.AddEndpoints(recommendationsAssembly);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured")))
        };
    });

builder.Services.AddAuthorization();

builder.Host.UseWolverine(opts => {
    opts.UseRabbitMq("amqp://guest:guest@localhost:5672")
        .AutoProvision()
        .BindExchange("integration-events").ToQueue("integration-events");

    opts.PublishAllMessages().ToRabbitExchange("integration-events");

    opts.ListenToRabbitQueue("integration-events");

    opts.ConfigureModules();
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new {
    name = "BlazorModularMonolith API",
    version = "1.0.0",
    status = "running"
})).WithName("Root");

app.MapEndpoints();

app.Run();
