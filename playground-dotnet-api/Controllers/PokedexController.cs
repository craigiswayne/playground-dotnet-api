using Microsoft.AspNetCore.Mvc;
using playground_dotnet_api.Models;

namespace playground_dotnet_api.Controllers;

[ApiController]
// [LogActionFilter] // TODO
[Route("[controller]")]
public class PokedexController : ControllerBase
{
    // TODO: Custom ControllerBase
    // TODO: Custom ControllerBase for logging
    private readonly ILogger<PokedexController> _logger;

    public PokedexController(ILogger<PokedexController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<Pokemon>> List()
    {
        var pokemon = new List<Pokemon>
        {
            new Pokemon {
                Id = 1,
                Name = "Bulbasaur"
            },
            new Pokemon {
                Id = 2,
                Name = "Ivysaur"
            },
            new Pokemon {
                Id = 3,
                Name = "Venasaur"
            },
            new Pokemon {
                Id = 4,
                Name = "Charmander"
            }
        };

        if (pokemon.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        else
        {
            return Ok(pokemon);
        }
    }
}