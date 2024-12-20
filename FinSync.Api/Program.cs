var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n=>n.FullName);
});
builder.Services.AddTransient<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Endpoints
app.MapGet("/", () => "Hello World!");
app.MapPost("/v1/transaction", (Request request, Handler handler) => handler.Handle(request))
    .WithName("Transactions: Create")
    .WithSummary("Cria uma nova transa��o")
    .Produces<Response>();
app.MapPut("/", () => "Hello World!");
app.MapDelete("/", () => "Hello World!");


app.Run();

// Request
public class Request
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}
// Response
public class Response
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
// Handler
public class Handler
{
    public Response Handle(Request request)
    {
        return new Response
        {
            Id = 4,
            Title = request.Title,
        };
    }
}

