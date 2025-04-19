using BusinessCardInformation.Core.Models.Response;
using Microsoft.EntityFrameworkCore; 


namespace BusinessCardInformation.Infra.ApplicationDbContext
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        } 
        public DbSet<BusinessCard> BusinessCards { get; set; }
    }

}
