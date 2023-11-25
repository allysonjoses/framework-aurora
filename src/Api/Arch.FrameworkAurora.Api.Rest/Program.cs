using Arch.FrameworkAurora.Core.Application.Configuration;
using Arch.FrameworkAurora.Infra.Persistence.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationConfiguration()
                .AddPersistenceConfiguration(); 
// todo: add others configurations

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapHealthChecks("/health-check");
app.Run();