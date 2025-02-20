using newFitnet.Common.Clock;
using newFitnet.Common.EmailService;
using newFitnet.Common.ErrorHandling;
using newFitnet.Common.Events.EventBus;
using newFitnet.Member;
using newFitnet.Pass;
using RazorHtmlEmails.RazorClassLib.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options => 
options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddExceptionHandling();
builder.Services.AddClock();
builder.Services.AddEventBus();

builder.Services.AddMembers(builder.Configuration);
builder.Services.AddPasses(builder.Configuration);
builder.Services.AddSingleton<IEmailService,  EmailService>();
builder.Services.AddMvcCore().AddRazorViewEngine();
builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
builder.Services.AddSingleton<INewRazorViewString, NewRazorViewString>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMembers();
app.UsePasses();

app.UseHttpsRedirection();

app.UseExceptionHandling();
app.MapControllers();
app.MapMembers();
app.MapPasses();





app.Run();

