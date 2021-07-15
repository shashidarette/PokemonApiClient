using System;
using Pokemon.Interfaces;
using Pokemon.DataModel;
using PokeApiNet;
using System.Threading.Tasks;

namespace Pokemon.Client
{
    public class PokemonApiClient : IPokemonClient
    {
        PokeApiClient pokeClient = new PokeApiClient();
        PokemonYodaClient pokeYoda = new PokemonYodaClient();
        PokemonShakespeareClient pokeShakes = new PokemonShakespeareClient();

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

            string desc = species.FlavorTextEntries[0].FlavorText;

            if (species.Habitat.Name == "cave" || species.IsLegendary)
            {
                desc = await pokeYoda.GetPokemonTranslation(desc);
            }
            else
            {
                desc = await pokeShakes.GetPokemonTranslation(desc);
            }

            return new PokemonInfo { name = pok.Name, description = desc, habitat = species.Habitat.Name, isLegendary = species.IsLegendary };
        }
    }
}
