using AuditTrail.API.Infrastructure;
using AuditTrail.Core.Services.Implementation;
using AuditTrail.Core.Services;
using Infrastructure.Abstraction;
using Infrastructure.ORM.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DbContext
builder.Services.AddDbContext<AuditDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Unit of Work and Repository pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AuditDbContext>>(); // Generic UoW
builder.Services.AddScoped<IRepositoryFactory, UnitOfWork<AuditDbContext>>(); // Optional if needed
builder.Services.AddScoped<IAuditService, AuditService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
