using Microsoft.EntityFrameworkCore;
using TurboCart.Application.Interfaces;
using TurboCart.Application.UseCases;
using TurboCart.Infrastructure.Persistance.Contexts;
using TurboCart.Infrastructure.Persistance.Repositories;
using TurboCart.Infrastructure.Persistance.UnitOfWorks;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddDbContext<DbContext, TurboCartContext>();

builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDeletedBookingRepository, DeletedBookingRepository>();
builder.Services.AddTransient<ITurboCartUnitOfWork, TurboCartUnitOfWork>();
builder.Services.AddTransient<IBookingUseCase, BookingUseCase>();
builder.Services.AddTransient<ICustomerUseCase, CustomerUseCase>();
builder.Services.AddTransient<IDeletedBookingUseCase, DeletedBookingUseCase>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
