using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Pokemon.DataModel;
using Pokemon.Interfaces;

namespace Pokedex.Controllers
{
    /// <summary>
    /// Main controller to serve the end-points.
    /// It consumes IPokemonClient to get the information of the pokemon.
    /// </summary>
    [ApiController]
    public class PokemonController : ControllerBase
    {
        /// <summary>
        /// PokemonClient responsible to get the pokemon informaiton.
        /// </summary>
        private readonly IPokemonClient _pokeApiClient;

        /// <summary>
        /// Pokemon Controller constructor
        /// </summary>
        /// <param name="pokeApiClient"></param>
        public PokemonController(IPokemonClient pokeApiClient) => _pokeApiClient = pokeApiClient;

        /// <summary>
        /// API end point : pokemon/{name}
        /// Gets the pokemon JSON object with { name, description, habitat, isLegendary }.  
        /// </summary>
        /// <param name="name">Name of the pokemon</param>
        /// <returns>PokemonInfo</returns>
        [HttpGet("pokemon/{name}")]
        public async Task<PokemonInfo> GetPokemon(string name)
        {
            return await _pokeApiClient.GetPokeman(name);
        }

        /// <summary>
        /// API end point : pokemon/translated//{name}
        /// Gets the pokemon JSON object with { name, description, habitat, isLegendary } 
        /// with description translated using Yoda, Shakespear fun translation.
        /// </summary>
        /// <param name="name">Name of the pokemon</param>
        /// <returns>PokemonInfo</returns>
        [HttpGet("pokemon/translated/{name}")]
        public async Task<PokemonInfo> GetPokemonTranslated(string name)
        {
            return await _pokeApiClient.GetTranslatedPokeman(name);
        }

    }
}
