using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using L3WebProjet.DataAccess;
using L3WebProjet.DataAccess.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Configuration
    .AddUserSecrets<Program>(true)
    .Build();

builder.Services.AddDbContext<VideoclubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Videoclub")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(options => {
    options.ConfigObject.Urls = [new UrlDescriptor {
        Name = "L3 Web API",
        Url = "/openapi/v1.json"
    }];
});

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<VideoclubDbContext>();
    dbContext.Database.Migrate();
}

app.Run();