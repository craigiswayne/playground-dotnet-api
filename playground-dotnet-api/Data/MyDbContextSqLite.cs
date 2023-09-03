using Microsoft.EntityFrameworkCore;
using playground_dotnet_api.Models;

namespace playground_dotnet_api.Data;

public class MyDbContextSqLite : DbContext
{
    public MyDbContextSqLite(DbContextOptions<MyDbContextSqLite> options) : base(options)
    {
    }

    public DbSet<Pokemon> Pokedex { get; set; }

}