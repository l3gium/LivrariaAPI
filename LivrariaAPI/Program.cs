using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICliente, ClienteRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();
builder.Services.AddScoped<ITipoPagamento, TipoPagamentoRepository>();
builder.Services.AddScoped<IVendedor, VendedorRepository>();
builder.Services.AddScoped<IVenda, VendaRepository>();

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
