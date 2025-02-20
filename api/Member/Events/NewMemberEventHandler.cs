using newFitnet.Common.EmailService;
using newFitnet.Common.Events;
using newFitnet.Common.Events.EventBus;
using newFitnet.Member.Data;
using Razor.Templating.Core;
using RazorHtmlEmails.RazorClassLib.Services;
using RazorHtmlEmails.RazorClassLib.Views.Emails.ConfirmAccount;

namespace newFitnet.Member.Events
{
    internal sealed class NewMemberEventHandler(
        IEmailService emailService,
        INewRazorViewString newRazorViewString) : IIntegrationEventHandler<NewMemberEvent>
    {
        public async Task Handle(NewMemberEvent @event, CancellationToken cancellationToken)
        {
            var confirmAccountModel = new ConfirmAccountEmailViewModel($"{@event.Name ?? "NA"}/{Guid.NewGuid()}",
                        @event.Name, @event.Phone, @event.Email, @event.Address);
            //string body = await razorViewToStringRenderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);
            string body = await newRazorViewString.GetStringFromRazor("/Views/Emails/ConfirmAccount/ConfirmAccountEmail.cshtml", confirmAccountModel);
            var toAddresses = new List<string> { "zlatansubhajit@gmail.com" };
            await emailService.SendEmail(toAddresses, "TEST", body);
        }
    }
}
