using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using L3WebProjet.DataAccess;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Business.Implementations;
using L3WebProjet.Business.Interfaces;
using L3WebProjet.DataAccess.Implementations;

var builder = WebApplication.CreateBuilder(args);

const string CorsPolicyName = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<IResourceService, ResourceService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();

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

app.UseCors(CorsPolicyName);

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