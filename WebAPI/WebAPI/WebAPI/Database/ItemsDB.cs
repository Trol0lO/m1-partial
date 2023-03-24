using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Database
{
    public class ItemsDB : DbContext

    {
        public ItemsDB(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Items> Items { get; set; }
    }
}
