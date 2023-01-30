using InsourceData.AuthHelper;
using InsourceData.Context;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Repository;
using InsourceData.Repository.admin;
using InsourceData.Repository.Auth;
using InsourceData.Repository.Enquiry;
using Microsoft.AspNetCore.Authentication.Cookies;
using Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<Database>();
builder.Services.AddTransient<ILookupRepository, LookUpRepository>();
builder.Services.AddTransient<IEnquiryRepository, EnquiryRepository>();
builder.Services.AddTransient<ICryptoMD5, CryptoMD5>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<IIssueRepository, IssueRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/Forbidden/";
    });

var app = builder.Build();
var ctx = app.Services.GetService<IHttpContextAccessor>();
SessionData.SetHttpContextAccessor(ctx);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};
app.UseCookiePolicy(cookiePolicyOptions);


app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
