using AnonymousMail.Server.Data;
using AnonymousMail.Server.Hubs;
using AnonymousMail.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql("Host=ec2-44-205-41-76.compute-1.amazonaws.com;Port=5432;Database=d2vqghquqteg21;Username=gmuvxsxyylhcum;Password=6a17d33ec8f04553fc002c815ad26acb6774f55b6289376b4a5d3373040e3502"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MailUser")
    .AddCookie("MailUser");
builder.Services.AddAuthorization();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapHub<MailMessageHub>("/mailMessageHub");
app.MapFallbackToFile("index.html");

app.Run();
