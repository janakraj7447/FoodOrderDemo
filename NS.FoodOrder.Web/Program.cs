using Microsoft.EntityFrameworkCore;
using NS.FoodOrder.Business;
using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using  Microsoft.Extensions.Configuration;
using NS.FoodOrder.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFoodBussiness,FoodBussiness>();
builder.Services.AddScoped<IFoodRepository,FoodRepository>();
builder.Services.AddScoped<IUserBussiness,UserBussiness>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
    

builder.Services.AddDbContext<FoodOrderDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("FoodOrderDatabase")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
