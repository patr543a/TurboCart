using TurboCart.Application.Interfaces;
using TurboCart.Infrastructure.Networking.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
// Adds session states which store the id that determines weather or not a user is logged in
// Aparently states may in some cases be shared across multiple users on the same machine, so this isn't an optimal solusion,
// however the alternative would be to implement something like this through cookies instead, which has its own issues to deal with
builder.Services.AddSession(options => {
    //options.IdleTimeout = TimeSpan.FromSeconds(10); //default is 20 minutes
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddTransient<IBookingUseCase, BookingService>();
builder.Services.AddTransient<ICustomerUseCase, CustomerService>();
builder.Services.AddTransient<IUserUseCase, UserService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
