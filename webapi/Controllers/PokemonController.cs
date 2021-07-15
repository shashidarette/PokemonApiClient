using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Pokemon.DataModel;
using Pokemon.Interfaces;

namespace Pokedex.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonClient _pokeApiClient;

        public PokemonController(IPokemonClient pokeApiClient) => _pokeApiClient = pokeApiClient;

        [HttpGet("pokemon/{name}")]
        public async Task<PokemonInfo> GetPokemon(string name)
        {
            return await _pokeApiClient.GetPokeman(name);
        }

        [HttpGet("pokemon/translated/{name}")]
        public async Task<PokemonInfo> GetPokemonTranslated(string name)
        {
            return await _pokeApiClient.GetTranslatedPokeman(name);
        }

    }
}
