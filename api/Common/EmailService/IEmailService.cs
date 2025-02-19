namespace newFitnet.Common.EmailService
{
    internal interface IEmailService
    {
        Task SendEmail(IEnumerable<string> toAddresses, string subject, string body, string fromAddress="subhajitpaul630@gmail.com");
    }
}
