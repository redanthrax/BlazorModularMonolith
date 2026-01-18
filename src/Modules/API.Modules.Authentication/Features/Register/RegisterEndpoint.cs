using API.Modules.Authentication.Entities;
using API.Modules.Authentication.Infrastructure.Persistence;
using API.Modules.Authentication.Infrastructure.Services;
using API.Modules.Authentication.IntegrationEvents;
using Common.Application.Models;
using Common.Presentation.Contracts;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace API.Modules.Authentication.Features.Register;

public class RegisterEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapPost("/api/auth/register",
            async ([FromBody] RegisterRequest request,
                AuthDbContext dbContext,
                TokenService tokenService,
                IMessageBus messageBus) => {
                var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (existingUser != null) {
                    return Results.BadRequest(Result<LoginResponse>.Failure("Email already registered"));
                }

                var passwordHasher = new PasswordHasher<string>();
                var passwordHash = passwordHasher.HashPassword(request.Email, request.Password);

                var user = new User {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    FullName = request.FullName
                };

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();

                var token = tokenService.GenerateToken(user);
                var response = new LoginResponse(token, user.Email, user.Id.ToString());

                var integrationEvent = new UserRegisteredIntegrationEvent(user.Id, user.Email, user.FullName);
                await messageBus.PublishAsync(integrationEvent);

                return Results.Created($"/api/auth/users/{user.Id}", Result<LoginResponse>.Success(response));
            })
            .WithName("Register")
            .WithTags("Auth");
    }
}
