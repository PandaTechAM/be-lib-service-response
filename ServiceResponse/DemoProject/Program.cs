using System.Text.Json.Serialization;
using PandaTech.ServiceResponse;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IExceptionHandler, PublicExceptionHandler>();
builder.Services.AddMvcCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();