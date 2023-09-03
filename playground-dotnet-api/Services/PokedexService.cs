using playground_dotnet_api.Data;
using playground_dotnet_api.Models;

namespace playground_dotnet_api.Services;

public interface IPokedexService
{
    IQueryable<Pokemon> List();
}

public class PokedexService: IPokedexService
{
    private readonly MyDbContextSqLite _dbContext;
    
    public PokedexService(MyDbContextSqLite dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<Pokemon> List()
    {
        return _dbContext.Pokedex.OrderBy(columns => columns.Id).Skip(0).Take(5);
    }
}