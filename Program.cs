using eTickets.Data.Services;
using eTickets.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using eTickets.Data.Cart;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

//DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//Services configuration
builder.Services.AddScoped<IActorsServices, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducerService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Authentication and authorization

builder.Services.AddControllersWithViews();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Movies}/{action=Index}/{id?}");
});


//Seed database
AppDbInitializer.Seed(app);

app.Run();
