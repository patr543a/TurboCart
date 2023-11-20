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
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DbContext, TurboCartContext>();

builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ITurboCartUnitOfWork, TurboCartUnitOfWork>();
builder.Services.AddTransient<IBookingUseCase, BookingUseCase>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
