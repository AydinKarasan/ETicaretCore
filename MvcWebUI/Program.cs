using AppCore.DataAccess.Configs;
using Business.Services;
using Business.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using MvcWebUI.Settings;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

List<CultureInfo> _cultures = new List<CultureInfo>()
{
    new CultureInfo("tr-TR")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(_cultures.FirstOrDefault().Name);
    options.SupportedCultures = _cultures;
    options.SupportedUICultures = _cultures;
});



// Add services to the container.
builder.Services.AddControllersWithViews();
//yetkilendirmeyi �zelle�tiriyoruz 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config => 
    {
        config.LoginPath = "/Hesaplar/Giris";
        config.AccessDeniedPath = "/Hesaplar/YetkisizIslem";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });

builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30); //default: 20 dakika
});

ConnectionConfig.ConnectionString = builder.Configuration.GetConnectionString("ETicaretContext");

//IConfiguration section = builder.Configuration.GetSection("AppSettings");
IConfiguration section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

#region IoC Container (Inversion of Control) (Autofac, Ninject) //ba��ml�l�klar� y�netti�imiz yer
builder.Services.AddScoped<IKategoriService, KategoriServices>(); // objeyi ba�ka bir yerde new leyip dependansy injection ile inject e edip IService kullanamad���m�zdan IKategoriService �nterface i kullanrak newl lenmi� objeyi ald�k kulland�k. //IKetegoriService g�rd���n yerde KategoriService i kullan demek // objeyle i�i bitene kadar kullanan metod AddScope istek sona erene kadar obje hayatta kal�yor
//builder.Services.AddSingleton<IKategoriService, KategoriServices>(); // objeyi ba�ka yerde kullanabilmek i�in s�rekli hayatta tutuyor
//builder.Services.AddTransient<IKategoriService, KategoriServices>(); // s�rekli new leme yapar her seferinde obje olu�turup kullan�r
//builder.Services.AddScoped<KategoriRepoBase, KategoriRepo>();
//builder.Services.AddDbContext<ETicaretContext>();
builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IMagazaService, MagazaService>();
builder.Services.AddScoped<IHesapService, HesapService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IUlkeService, UlkeService>();
builder.Services.AddScoped<ISehirService, SehirService>();
builder.Services.AddScoped<IUrunRaporService, UrunRaporService>();
#endregion

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(_cultures.FirstOrDefault().Name),
    SupportedCultures = _cultures,
    SupportedUICultures = _cultures,
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//s�ras� �nemli
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

//yeni route tan�m�n� default un �zerine yap��t�r // area i�in
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
