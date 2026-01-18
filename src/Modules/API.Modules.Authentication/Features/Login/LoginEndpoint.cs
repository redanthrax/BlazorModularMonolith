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

namespace API.Modules.Authentication.Features.Login;

public class LoginEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapPost("/api/auth/login",
            async ([FromBody] LoginRequest request,
                AuthDbContext dbContext,
                TokenService tokenService,
                IMessageBus messageBus) => {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null) {
                    return Results.BadRequest(Result<LoginResponse>.Failure("Invalid email or password"));
                }

                var passwordHasher = new PasswordHasher<string>();
                var result = passwordHasher.VerifyHashedPassword(user.Email, user.PasswordHash, request.Password);

                if (result == PasswordVerificationResult.Failed) {
                    return Results.BadRequest(Result<LoginResponse>.Failure("Invalid email or password"));
                }

                var token = tokenService.GenerateToken(user);
                var response = new LoginResponse(token, user.Email, user.Id.ToString());

                var integrationEvent = new UserLoggedInIntegrationEvent(user.Id, user.Email);
                await messageBus.PublishAsync(integrationEvent);

                return Results.Ok(Result<LoginResponse>.Success(response));
            })
            .WithName("Login")
            .WithTags("Auth");
    }
}
