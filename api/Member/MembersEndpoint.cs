

namespace newFitnet.Member
{
    using newFitnet.Member.Data.Database;
    using newFitnet.Member.Data;
    using Microsoft.EntityFrameworkCore;

    internal static class MembersEndpoint
    {
        internal static void MapCreateOrUpdateMember(this IEndpointRouteBuilder app) =>
            app.MapPost(MembersApiPaths.Root,
                async (CreateOrUpdateMemberRequest request, MembersPersistence persistence,
                CancellationToken cancellationToken) =>
            {
                var dupMember = await persistence.Members.Where(m => m.Email == request.email || m.Phone == request.phone).Select(m => true).FirstOrDefaultAsync();
                var member = Member.CreateOrUpdate(request.name, request.phone, request.email, request.address,dupMember) ;
                await persistence.Members.AddAsync(member) ;
                await persistence.SaveChangesAsync(cancellationToken) ;
                return Results.Created($"{MembersApiPaths.Root}/{member.Id}", member.Id);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Triggers the creation or update of a Member",
                Description = "This endpoint is used to create or update details of a member of the gym",
            })
            .Produces<string>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);


        internal static void MapListMembers(this IEndpointRouteBuilder app) =>
            app.MapGet(MembersApiPaths.Root,
                async (MembersPersistence persistence, CancellationToken cancellationToken) =>
                {
                    var members = await persistence.Members.ToListAsync(cancellationToken);
                    return members;
                })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all the Members"
            })
            .Produces<IEnumerable<Member>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);


        internal static void MapMembers(this IEndpointRouteBuilder app)
        {
            app.MapCreateOrUpdateMember();
            app.MapListMembers();
        }
    }
}
