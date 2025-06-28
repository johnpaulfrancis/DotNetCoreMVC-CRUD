using DotNetCoreMVC_CRUD.Models.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreMVC_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<EmployeeMaster_Schema> EmployeeMaster { get; set; }
    }
}
