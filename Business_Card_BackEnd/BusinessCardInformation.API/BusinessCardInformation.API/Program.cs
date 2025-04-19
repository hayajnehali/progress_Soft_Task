using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Mapper;
using BusinessCardInformation.Core.Repositorys;
using BusinessCardInformation.Infra.ApplicationDbContext;
using BusinessCardInformation.Infra.Services;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);


// Register DbContext with SQL Server provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBLocalConnection"), d => d.MigrationsAssembly("BusinessCardInformation.API")));


// add services and repositories registration
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IBusinessCardRepository, BusinessCardRepo>();
builder.Services.AddScoped<IBusinessCardServices, BusinessCardServices>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
object value = builder.Services.AddSwaggerGen();

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
