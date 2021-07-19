using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Pokedex.Controllers;
using Pokemon.DataModel;
using Pokemon.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.Tests
{
    public class PokemonControllerTests
    {
        private PokemonController controller;
        private Mock<IPokemonClient> pokemonClientMock;
        private Mock<IYodaTranslation> yodaTranslate;
        private Mock<IShakespeareTranslation> shakespearTranslate;
        private Dictionary<string, PokemonInfo> pokemonDict = new Dictionary<string, PokemonInfo>();

        public PokemonControllerTests()
        {
            pokemonClientMock = new Mock<IPokemonClient>();
            yodaTranslate = new Mock<IYodaTranslation>();
            shakespearTranslate = new Mock<IShakespeareTranslation>();

            // setup the dictionary with different pokemon objects
            pokemonDict = new Dictionary<string, PokemonInfo>();
            pokemonDict.Add("Poke1", new PokemonInfo() { name = "Poke1", description = "pokemon1 is amazing", habitat = "special", isLegendary = true });
            pokemonDict.Add("Poke1YodaTranslate", new PokemonInfo() { name = "Poke1", description = "Amazing pokemon!", habitat = "special", isLegendary = true });             

            pokemonDict.Add("Poke2NoLegend", new PokemonInfo() { name = "Poke2NoLegend", description = "pokemon2 is super", habitat = "all over", isLegendary = false });
            pokemonDict.Add("Poke2SSTranslate", new PokemonInfo() { name = "Poke2NoLegend", description = "How super pokemon2 is", habitat = "all over", isLegendary = false });

            pokemonDict.Add("Poke3Cave", new PokemonInfo() { name = "Poke1", description = "How Amazing pokemon3 is", habitat = "cave", isLegendary = false });
            pokemonDict.Add("Poke3CaveYodaTranslate", new PokemonInfo() { name = "Poke1", description = "Is pokemon3 amazing", habitat = "cave", isLegendary = false });

            pokemonDict.Add("Poke4NoTranslate", new PokemonInfo() { name = "Poke2NoTranslate", description = "pokemon2 is super", habitat = "all over", isLegendary = true });

            // update the pokemon client mock with relevant mappinngs for api calls
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke1")).ReturnsAsync(pokemonDict["Poke1"]);
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke2NoLegend")).ReturnsAsync(pokemonDict["Poke1"]);
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke3Cave")).ReturnsAsync(pokemonDict["Poke3Cave"]);
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke4NoTranslate")).ReturnsAsync(pokemonDict["Poke4NoTranslate"]);

            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke1")).ReturnsAsync(pokemonDict["Poke1YodaTranslate"]);
            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke2NoLegend")).ReturnsAsync(pokemonDict["Poke2SSTranslate"]);
            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke3Cave")).ReturnsAsync(pokemonDict["Poke3CaveYodaTranslate"]);
            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke4NoTranslate")).ReturnsAsync(pokemonDict["Poke4NoTranslate"]);

            controller = new PokemonController(pokemonClientMock.Object);
        }

        // Pokemon valid test
        [Fact]
        public void Valid_Pokemon_Available_Test()
        {
            var result = controller.GetPokemon("Poke1");

            // assert
            pokemonDict["Poke1"].Should().BeEquivalentTo(result.Result);
        }

        // Pokemon not available test
        [Fact]
        public void Pokemon_Not_Available_Test()
        {
            var result = controller.GetPokemon("PokeNotAvailable");

            // assert
            Assert.Null(result.Result);
        }

        // Pokemon not available test
        [Fact]
        public void Pokemon_Not_Available_Test2()
        {
            var result = controller.GetPokemonTranslated("PokeNotAvailable");

            // assert
            Assert.Null(result.Result);
        }

        // Pokemon Yoda translation test
        [Fact]
        public void Pokemon_YodaTranslate_Test()
        {
            var result = controller.GetPokemonTranslated("Poke1");

            // assert
            pokemonDict["Poke1YodaTranslate"].Should().BeEquivalentTo(result.Result);
        }

        // Polemon Shakeshpear translation test
        [Fact]
        public void Pokemon_ShakeshpearTranslate_Test()
        {
            var result = controller.GetPokemonTranslated("Poke2NoLegend");

            // assert
            pokemonDict["Poke2SSTranslate"].Should().BeEquivalentTo(result.Result);
        }

        // Pokemon cave habitat test
        [Fact]
        public void Pokemon_CaveHabitat_Test()
        {
            var result = controller.GetPokemonTranslated("Poke3Cave");

            // assert
            pokemonDict["Poke3CaveYodaTranslate"].Should().BeEquivalentTo(result.Result);
        }

        // Pokemon no-translation available test
        [Fact]
        public void Pokemon_NoTranslate_Test()
        {
            var result = controller.GetPokemonTranslated("Poke4NoTranslate");

            // assert
            pokemonDict["Poke4NoTranslate"].Should().BeEquivalentTo(result.Result);
        }
    
    }
}
