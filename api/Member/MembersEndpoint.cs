

namespace newFitnet.Member
{
    using newFitnet.Member.Data.Database;
    using newFitnet.Member.Data;
    using Microsoft.EntityFrameworkCore;
    using Razor.Templating.Core;
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using System;
    using RazorHtmlEmails.RazorClassLib.Services;
    using RazorHtmlEmails.RazorClassLib.Views.Emails.ConfirmAccount;
    using newFitnet.Common.EmailService;
    using Microsoft.Extensions.DependencyInjection;
    using newFitnet.Member.Events;
    using newFitnet.Common.Events.EventBus;
    using Microsoft.Extensions.Logging;

    internal static class MembersEndpoint
    {        
        internal static void MapCreateOrUpdateMember(this IEndpointRouteBuilder app) =>
            app.MapPost(MembersApiPaths.Root,
                async (CreateOrUpdateMemberRequest request, MembersPersistence persistence,
                CancellationToken cancellationToken, TimeProvider timeProvider, IEventBus eventBus) =>
            {
                var dupMember = await persistence.Members.Where(m => m.Email == request.email || m.Phone == request.phone).Select(m => true).FirstOrDefaultAsync();
                var member = Member.CreateOrUpdate(request.name, request.phone, request.email, request.address,dupMember) ;
                await persistence.Members.AddAsync(member) ;
                var addsuccess = await persistence.SaveChangesAsync(cancellationToken) ;
                if (addsuccess >= 1)
                {

                   var newMemberEvent = NewMemberEvent.Create(member.Id, member.Name, member.Phone, member.Email, member.Address, timeProvider.GetUtcNow());
                    await eventBus.PublishAsync(newMemberEvent,cancellationToken);
                }
                else
                    return Results.BadRequest("Member not added");

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

        internal static void MapRenderPdf(this IEndpointRouteBuilder app) =>
            app.MapGet($"{MembersApiPaths.Delete}/pdf",
                async (Guid MemberId, MembersPersistence membersPersistence,
                    CancellationToken cancellationToken, INewRazorViewString newRazorViewString) =>
                {
                    var member = await membersPersistence.Members.SingleOrDefaultAsync(m => m.Id == MemberId);
                    //var html = await RazorTemplateEngine.RenderAsync("Member/DocTemplates/NewMemberDoc.cshtml", member);

                    var confirmAccountModel = new ConfirmAccountEmailViewModel($"{member.Name ?? "NA"}/{Guid.NewGuid()}",
                        member.Name, member.Phone, member.Email, member.Address);
                    //string body = await razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);
                    string html = await newRazorViewString.GetStringFromRazor("/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);

                    var converter = new BasicConverter(new PdfTools());
                    var doc = new HtmlToPdfDocument()
                    {
                        Objects = { new ObjectSettings() { HtmlContent = html } }
                    };
                    byte[] pdf = converter.Convert(doc);

                    return Results.File(pdf,"application/pdf","converted.pdf");
                });

        internal static void MapSendEmail(this IEndpointRouteBuilder app) =>
            app.MapGet($"{MembersApiPaths.Delete}/email",
                async (Guid MemberId, MembersPersistence memberPersistence, IEmailService emailService,
                    CancellationToken cancellationToken, IRazorViewToStringRenderer razorViewToStringRenderer) => 
                {
                    var member = await memberPersistence.Members.SingleOrDefaultAsync(m => m.Id == MemberId);
                    var confirmAccountModel = new ConfirmAccountEmailViewModel($"{member.Name??"NA"}/{Guid.NewGuid()}",
                        member.Name,member.Phone,member.Email,member.Address);
                    string body = await razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);
                    var toAddresses = new List<string> { "zlatansubhajit@gmail.com"};
                    await emailService.SendEmail(toAddresses, "TEST", body);
                    return Results.Ok("Sent");
                });

        internal static void MapMembers(this IEndpointRouteBuilder app)
        {
            app.MapCreateOrUpdateMember();
            app.MapListMembers();
            app.MapRenderPdf();
            app.MapSendEmail();
        }
    }
}
