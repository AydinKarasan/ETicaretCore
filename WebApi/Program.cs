using AppCore.DataAccess.Configs;
using Business.Services;
using Business.Services.Bases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConnectionConfig.ConnectionString = builder.Configuration.GetConnectionString("ETicaretContext");

builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IKategoriService, KategoriServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
