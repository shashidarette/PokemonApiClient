using System;
using System.Threading.Tasks;
using PokeApiNet;
using Pokemon.Interfaces;
using Pokemon.DataModel;

namespace Pokemon.Client
{
    /// <summary>
    /// Pokemon API client implements IPokemonClient interface
    /// </summary>
    public class PokemonApiClient : IPokemonClient
    {
        /// <summary>
        /// Pokemon cave species string
        /// </summary>
        private const string CAVE_SPECIES = "cave";

        /// <summary>
        /// Instance to PokeApiClient within PokeApiNet library
        /// </summary>
        PokeApiClient pokeClient = new PokeApiClient();

        /// <summary>
        /// Interface to yoda translation, injected during the startup
        /// </summary>
        private readonly IYodaTranslation _pokeYoda;

        /// <summary>
        /// Interface to shakespeare translation, injected during the startup
        /// </summary>
        private readonly IShakespeareTranslation _pokeShakespeare;

        /// <summary>
        /// PokemonApiClient constructor
        /// </summary>
        /// <param name="pokeYoda"></param>
        /// <param name="pokeShakes"></param>
        public PokemonApiClient(IYodaTranslation pokeYoda, IShakespeareTranslation pokeShakes)
        {
            _pokeYoda = pokeYoda; 
            _pokeShakespeare = pokeShakes;
        }

        /// <summary>
        /// Gets the pokemon information for the given pokemon name
        /// </summary>
        /// <param name="name">Pokemon Name</param>
        /// <returns>PokemonInfo</returns>
        public async Task<PokemonInfo> GetPokeman(string name)
        {
            PokeApiNet.Pokemon pok = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(name);
            PokemonSpecies species = await pokeClient.GetResourceAsync(pok.Species);
            return new PokemonInfo { Name = pok.Name, Description = species.FlavorTextEntries[0].FlavorText, Habitat = species.Habitat.Name, IsLegendary = species.IsLegendary };
        }

        /// <summary>
        /// Gets the pokemon information for the given pokemon name with description translated
        /// </summary>
        /// <param name="name">Pokemon Name</param>
        /// <returns>PokemonInfo</returns>
        public async Task<PokemonInfo> GetTranslatedPokeman(string name)
        {
            string translatedDesc;
            PokemonInfo pokemon = await GetPokeman(name);

            // check if the pokemon is legendary or has "cave" habitat
            // if yes, apply yoda translation else shakespeare translation.
            if (pokemon.Habitat.Equals(CAVE_SPECIES, StringComparison.InvariantCultureIgnoreCase) || pokemon.IsLegendary)
            {
                translatedDesc = await _pokeYoda.GetPokemonTranslation(pokemon.Description);
            }
            else
            {
                translatedDesc = await _pokeShakespeare.GetPokemonTranslation(pokemon.Description);
            }

            if (!string.IsNullOrEmpty(translatedDesc))
            {
                pokemon.Description = translatedDesc;
            }

            return pokemon;
        }
    }
}
