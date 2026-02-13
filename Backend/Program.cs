using Microsoft.EntityFrameworkCore;
using ContributorHubApi.Data;
using Microsoft.OpenApi.Models; // Add this using directive
using Swashbuckle; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // This will now resolve

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Auto-create database and seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        db.Users.Add(new ContributorHubApi.Models.User
        {
            Email = "demo@example.com",
            Password = "demo123",
            Name = "Demo User",
            Phone = "+1234567890",
            Address = "123 Demo Street, Demo City, DC 12345",
            CreatedAt = DateTime.UtcNow
        });
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    });
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAngular");
app.MapControllers();

app.Run();
