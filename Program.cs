using Microsoft.EntityFrameworkCore;
using minimalistiskdotcom.api.Service;
using minimalistiskdotcom.Service.Repository;
using minimalistiskdotcom.Service.Services;
using minimalistiskdotcom.Service.Services.Booking.Interface;
using minimalistiskdotcom.Service.Services.Email;
using minimalistiskdotcom.Service.Services.Email.Interface;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Register the MockProductService before building the app
builder.Services.AddTransient<MockProductService>();

//DBContext
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDb")));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Set up Umbraco
builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
