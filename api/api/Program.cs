var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.MapGet("/veletlen", () => {
    var r = Random.Shared.Next(10, 100);
    return TypedResults.Ok(new {
        szam = r
    });
});

string[] csoporttagok = ["1. tag", "2. tag", "3. tag", "4. tag"];

app.MapGet("/egytag", () => Random.Shared.GetItems(csoporttagok, 1).First());
app.MapGet("/egytag/{sorszam:int}", (int sorszam) => csoporttagok[sorszam - 1]);

app.Run();