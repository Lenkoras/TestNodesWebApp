var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureJsonSerializer();

builder.Services.AddSwaggerGenWithDocs()
    .AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseCors(Environments.Development)
    .UseMiddleware<EnableRequestBufferingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseHsts();

app.MapControllers();

app.Run();
