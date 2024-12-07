using SdCustomer.Models;
using Microsoft.EntityFrameworkCore;
using SdCustomer.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("CustomerList"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<DateOnlySchemaFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
       policy => policy.WithOrigins("http://localhost:5276")
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowLocalhost");

//app.Run();
app.Run("http://0.0.0.0:5276");
