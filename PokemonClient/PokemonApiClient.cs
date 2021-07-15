using System;
using Pokemon.Interfaces;
using Pokemon.DataModel;
using PokeApiNet;
using System.Threading.Tasks;

namespace Pokemon.Client
{
    public class PokemonApiClient : IPokemonClient
    {
        private const string CAVE_SPECIES = "cave";

        PokeApiClient pokeClient = new PokeApiClient();
        private readonly IYodaTranslation _pokeYoda;
        private readonly IShakespeareTranslation _pokeShakes;

        public PokemonApiClient(IYodaTranslation pokeYoda, IShakespeareTranslation pokeShakes)
        {
            _pokeYoda = pokeYoda; 
            _pokeShakes = pokeShakes;
        }

        public async Task<PokemonInfo> GetPokeman(string name)
        {
            PokeApiNet.Pokemon pok = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(name);
            PokemonSpecies species = await pokeClient.GetResourceAsync(pok.Species);
            return new PokemonInfo { name = pok.Name, description = species.FlavorTextEntries[0].FlavorText, habitat = species.Habitat.Name, isLegendary = species.IsLegendary };
        }

        public async Task<PokemonInfo> GetTranslatedPokeman(string name)
        {
            string translatedDesc;
            PokemonInfo pokemon = await GetPokeman(name);

            if (pokemon.habitat.Equals(CAVE_SPECIES, StringComparison.InvariantCultureIgnoreCase) || pokemon.isLegendary)
            {
                translatedDesc = await _pokeYoda.GetPokemonTranslation(pokemon.description);
            }
            else
            {
                translatedDesc = await _pokeShakes.GetPokemonTranslation(pokemon.description);
            }

            if (!string.IsNullOrEmpty(translatedDesc))
            {
                pokemon.description = translatedDesc;
            }

            return pokemon;
        }
    }
}
