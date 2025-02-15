
namespace newFitnet.Pass
{
    using Microsoft.EntityFrameworkCore;
    using newFitnet.Member.Data.Database;
    using newFitnet.Pass.Data;
    using newFitnet.Pass.Data.Database;
    internal static class PassesEndpoint
    {
        internal static void MapRegisterPass(this IEndpointRouteBuilder app) =>
            app.MapPost(PassApiPaths.Root,
                async (RegisterPassRequest request, PassPersistence passPersistence
                       ,MembersPersistence memberPersistence , CancellationToken cancellationToken) => 
                {
                    var pass = Pass.Register(request.MemberId, request.Start, request.End,
                        request.Location, request.Type,passPersistence,memberPersistence);
                    await passPersistence.Passes.AddAsync(pass);
                    await passPersistence.SaveChangesAsync(cancellationToken);
                    
                    return Results.Created($"{PassApiPaths.Root}/{pass.Id}", pass.Id);
                    
                })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Register new Passes for Members",
                Description = "This endpoint is used to create new Passes for Members"
            })
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);

        internal static void MapGetAllPasses(this IEndpointRouteBuilder app) =>
            app.MapGet(PassApiPaths.Root,
                async (PassPersistence persistence, CancellationToken cancellationToken) =>
                {
                    var passList = await persistence.Passes.ToListAsync();
                    return passList;
                })
            .WithOpenApi(options => new(options)
            {
                Summary = "Endpoint to list all the Passes"
            })
            .Produces<IEnumerable<Pass>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        internal static void MapGetPassForMember(this IEndpointRouteBuilder app) =>
            app.MapGet(PassApiPaths.Specific,
                async (Guid memberId, PassPersistence persistence, CancellationToken cancellationToken) =>
                {
                    var passes = await persistence.Passes.Where(pass => pass.MemberId == memberId).ToListAsync();
                    return passes;
                })
            .WithOpenApi(options => new(options)
            {
                Summary = "Get Passes for specific Member"
            })
            .Produces<IEnumerable<Pass>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        internal static void MapDeletePass(this IEndpointRouteBuilder app) =>
            app.MapDelete(PassApiPaths.Specific,
                async (Guid passId, PassPersistence passPersistence, CancellationToken cancellationToken) => 
                {
                    var delResult = await passPersistence.Passes.Where(p => p.Id == passId)
                                                                .ExecuteDeleteAsync(cancellationToken);
                    if (delResult > 0)
                        return Results.Ok("Deleted");
                    else
                        return Results.NotFound("Pass not found for Delete!");
                })
            .WithOpenApi(operations => new(operations) 
            {
                Summary = "Delete specific Passes by Id",
                Description = "This endpoint is used to delete Passes by their Id property"
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);


        internal static void MapPasses(this IEndpointRouteBuilder app)
        {
            app.MapRegisterPass();
            app.MapGetAllPasses();
            app.MapGetPassForMember();
            app.MapDeletePass();
        }
    }

    
}
