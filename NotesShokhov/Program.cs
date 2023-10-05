using Microsoft.EntityFrameworkCore;
using NotesShokhov.Data;
using NotesShokhov.Helpers;
using NotesShokhov.Interfaces;
using NotesShokhov.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

// Conect with database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add signalR for sending objects in real time
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseCors(options => options.WithOrigins("https://localhost:7233").AllowAnyMethod());
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//For SignalR
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NoteHubSignalR>("/notificationHub");
    
});
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "script-src 'self'");

    await next();

}); //Add Content Security Policy Prevents XSS-attack

app.Run();
