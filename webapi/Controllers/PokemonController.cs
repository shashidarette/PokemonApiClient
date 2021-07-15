using Microsoft.AspNetCore.Mvc;
using Pokemon.Client;
using Pokemon.DataModel;
using System.Threading.Tasks;

namespace webapi.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        // instantiate client
        PokemonApiClient pokeApiClient = new PokemonApiClient();

        [HttpGet("pokemon/{name}")]
        public async Task<PokemonInfo> GetPokemon(string name)
        {
            return await pokeApiClient.GetPokeman(name);
        }

        [HttpGet("pokemon/translated/{name}")]
        public async Task<PokemonInfo> GetPokemonTranslated(string name)
        {
            return await pokeApiClient.GetTranslatedPokeman(name);
        }

    }
}
