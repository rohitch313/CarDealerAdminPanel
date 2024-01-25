using Admin.UI.Service;
using Admin.UI.Service.IService;
using Admin.UI.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IUserInfoService, UserInfoService>();
ApiTypeSD.UserInfoAPIBase = builder.Configuration["ServicerUrls:UserinfoAPIBase"];
ApiTypeSD.StateAPIBase = builder.Configuration["ServicerUrls:StateAPI"];

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAggregatorsService, AggregatorsService>();
builder.Services.AddScoped<IPvOpenMarketsService, PvOpenMarketsService>();
builder.Services.AddScoped<IPvNewCarDealersService, PvNewCarDealersService>();
builder.Services.AddScoped<ICustomerSupportService, CustomerSupportService>();
builder.Services.AddScoped<IPvaMakeService, PvaMakeService>();
builder.Services.AddScoped<IPvaModelService, PvaModelService>();
builder.Services.AddScoped<IPvaYearOfRegService, PvaYearOfRegService>();
builder.Services.AddScoped<IPvaVariantService, PvaVariantService>();
builder.Services.AddScoped<IPvaMakeService, PvaMakeService>();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Home/LoginIndex";

    }

    );

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=LoginIndex}/{id?}");

app.Run();
