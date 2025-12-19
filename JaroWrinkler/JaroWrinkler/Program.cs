using JaroWrinklerSimilarity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var conn = builder.Configuration.GetConnectionString("OracleDb");
if (string.IsNullOrWhiteSpace(conn))
    throw new InvalidOperationException("Missing connection string 'OracleDb' in configuration.");
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseOracle(conn));
builder.Services.AddScoped<IAddressMatchingService, AddressMatchingService>();
builder.Services.AddScoped<ISimilarityService, SimilarityService>();
builder.Services.AddScoped<INormalizerService, NormalizerService>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Address Matching API",
        Version = "v1",
        Description = "Validates addresses using Exact Match and Jaro-Winkler similarity"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DefaultCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();