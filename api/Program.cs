using newFitnet.Common.ErrorHandling;
using newFitnet.Member;
using newFitnet.Pass;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options => 
options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddExceptionHandling();

builder.Services.AddMembers(builder.Configuration);
builder.Services.AddPasses(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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

