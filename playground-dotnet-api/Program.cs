using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using playground_dotnet_api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy  =>
        {
            policy.WithOrigins("https://localhost:4000", "http://www.contoso.com");
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    rateLimiterOptions.AddFixedWindowLimiter("fixed-window", fixedWindowOptions =>
    {
        fixedWindowOptions.Window = TimeSpan.FromSeconds(5);
        fixedWindowOptions.PermitLimit = 5;
        fixedWindowOptions.QueueLimit = 10;
        fixedWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(180);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSecurityHeaders();
app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();

