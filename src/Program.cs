using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CreditLineDbContext>(opt => opt.UseInMemoryDatabase("CreditLine"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "CreditLineAPI";
    config.Title = "CreditLineAPI v1";
    config.Version = "v1";
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "CreditLineAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/creditline", async (CreditLineDbContext db) =>
    await db.CreditLines.ToListAsync());

app.MapGet("/creditline/{id}", async (int id, CreditLineDbContext db) =>
    await db.CreditLines.FindAsync(id)
        is CreditLine creditline
            ? Results.Ok(creditline)
            : Results.NotFound());

app.MapPost("/creditline/payoff-min/", ([FromBody]MinPayoffRequest minPayoffRequest) => {
    return CreditCardPaymentHelper.CalculateMinPaymentPayoff(minPayoffRequest.CreditLines);
});

app.MapPost("/creditline/payoff-snowball/", ([FromBody]SnowBallPayoffRequest request) => {
    return CreditCardPaymentHelper.CalculateDebtSnowballPayoff(request.CreditLines, request.ExtraPayment);
});

app.MapPost("/creditline", async (CreditLine creditline, CreditLineDbContext db) =>
{
    db.CreditLines.Add(creditline);
    await db.SaveChangesAsync();

    return Results.Created($"/creditline/{creditline.Id}", creditline);
});

app.MapPut("/creditline/{id}", async (int id, CreditLine inputCreditLine, CreditLineDbContext db) =>
{
    var creditline = await db.CreditLines.FindAsync(id);

    if (creditline is null) return Results.NotFound();

    creditline.Name = inputCreditLine.Name;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/creditline/{id}", async (int id, CreditLineDbContext db) =>
{
    if (await db.CreditLines.FindAsync(id) is CreditLine creditline)
    {
        db.CreditLines.Remove(creditline);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();