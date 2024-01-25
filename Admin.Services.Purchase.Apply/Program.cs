using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Admin.Services.Purchase.Apply.BusinessLayer.Services;
using Admin.Services.Purchase.Apply.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IAggregatorsService, PvAggregatorsService>();
builder.Services.AddScoped<IPvNewCarDealersService, PvNewCarDealersService>();
builder.Services.AddScoped<IPvOpenMarketsService, PvOpenMarketsService>();
builder.Services.AddScoped<IPvaMakeService, PvaMakeService>();
builder.Services.AddScoped<IPvaModelService, PvaModelService>();
builder.Services.AddScoped<IPvaVariantService, PvaVariantService>();
builder.Services.AddScoped<IPvaYearOfRegService, PvaYearOfRegService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DealerApifinalContext>();


// AutoMapper Config
builder.Services.AddAutoMapper(typeof(Program));
var SettingSection = builder.Configuration.GetSection("AppSettings");
var Issuer = SettingSection.GetValue<string>("Issuer");
var audience = SettingSection.GetValue<string>("audience");
var secret = SettingSection.GetValue<string>("Token");
var key = Encoding.UTF8.GetBytes(secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = audience


    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
