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
            PokeApiNet.Pokemon pok = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(name);
            PokemonSpecies species = await pokeClient.GetResourceAsync(pok.Species);
            string text = species.FlavorTextEntries[0].FlavorText;
            string desc;
            if (species.Habitat.Name.Equals(CAVE_SPECIES, StringComparison.InvariantCultureIgnoreCase) || species.IsLegendary)
            {
                desc = await _pokeYoda.GetPokemonTranslation(text);
            }
            else
            {
                desc = await _pokeShakes.GetPokemonTranslation(text);
            }

            if (string.IsNullOrEmpty(desc))
            {
                desc = text;
            }

            return new PokemonInfo { name = pok.Name, description = desc, habitat = species.Habitat.Name, isLegendary = species.IsLegendary };
        }
    }
}
