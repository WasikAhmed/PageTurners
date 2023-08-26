using Microsoft.EntityFrameworkCore;
using PageTurnersWeb.Models;

namespace PageTurnersWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
}