using Microsoft.EntityFrameworkCore;
using MinimalApiPractice;
using MinimalApiPractice.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello World!";
});


app.MapGet("/api/v1", (ApplicationDbContext db ) =>
{
    var result = db.FormBodies.ToList();
    return Results.Ok(result);
});

app.MapPost("/api/v1", (FormBody formBody, ApplicationDbContext db) =>
{
    db.Add(formBody);
    bool isAdded = db.SaveChanges() > 0;
    if (isAdded)
    {
        return Results.Created($"Saved in DB, Id:{formBody.Id}", formBody);
    }
    return Results.BadRequest("Problem In Saving");
});


app.Run();