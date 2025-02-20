namespace RazorHtmlEmails.RazorClassLib.Views.Emails.ConfirmAccount;

public record ConfirmAccountEmailViewModel(string ConfirmEmailUrl,string Name, string Phone, string? Email,string?Address);
