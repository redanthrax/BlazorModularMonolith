using API.Modules.Boxes.Infrastructure.Persistence;
using Common.Application.Models;
using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Boxes.Features.ListBoxes;

public class ListBoxesEndpoint : IEndpoint {
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapGet("/api/boxes",
            async (BoxesDbContext dbContext, int page = 1, int pageSize = 10) => {
                var pagedRequest = new PagedRequest(page, pageSize);

                var totalCount = await dbContext.Boxes.CountAsync();

                var boxes = await dbContext.Boxes
                    .OrderBy(b => b.Name)
                    .Skip(pagedRequest.Skip)
                    .Take(pagedRequest.Take)
                    .Select(b => new BoxListItemResponse(
                        b.Id,
                        b.Name,
                        b.Location,
                        b.Capacity
                    ))
                    .ToListAsync();

                return Results.Ok(PagedResult<BoxListItemResponse>.Success(boxes, totalCount, page,
                    pageSize));
            })
            .WithName("ListBoxes")
            .WithTags("Boxes")
            .RequireAuthorization();
    }
}
