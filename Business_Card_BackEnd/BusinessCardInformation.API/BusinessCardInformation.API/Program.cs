using BusinessCardInformation.Core.IRepositorys;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Core.Mapper;
using BusinessCardInformation.Core.Models.Request;
using BusinessCardInformation.Core.Models.Response;
using BusinessCardInformation.Core.Repositorys;
using BusinessCardInformation.Infra.ApplicationDbContext;
using BusinessCardInformation.Infra.Services;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);


// Register DbContext with SQL Server provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBLocalConnection"), d => d.MigrationsAssembly("BusinessCardInformation.API")));
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
 


// add services and repositories registration

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped<IBaseRepository<BusinessCard, BusinessCardFilter>, BusinessCardRepo>();
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
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
