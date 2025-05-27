using FinalProjectConsume.Services;
using FinalProjectConsume.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();      

builder.Services.AddHttpClient();

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IInstagramService, InstagramService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IDestinationFeatureService, DestinationFeatureService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
builder.Services.AddScoped<ITrandingDestinationService, TrandingDestinationService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<INewsLetterService, NewsLetterService>();


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
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();