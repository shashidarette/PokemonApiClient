using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Moq;
using Pokedex.Controllers;
using Pokemon.DataModel;
using Pokemon.Interfaces;

namespace Pokemon.Api.Tests
{
    /// <summary>
    /// Pokemon Controller Tests using IPokemonClient mock
    /// </summary>
    public class PokemonControllerTests
    {
        /// <summary>
        /// Instance to main controller class
        /// </summary>
        private PokemonController controller;

        /// <summary>
        /// IPokemonClient interface mock
        /// </summary>
        private Mock<IPokemonClient> pokemonClientMock;

        /// <summary>
        /// Dictionary with different pokemon info objects used to mock behavior on PokemonController
        /// </summary>
        private Dictionary<string, PokemonInfo> pokemonDict = new Dictionary<string, PokemonInfo>();

        /// <summary>
        /// Setup pokemonClient mock with relevant behaviors and pokemon info objects
        /// </summary>
        public PokemonControllerTests()
        {
            pokemonClientMock = new Mock<IPokemonClient>();

            // setup the dictionary with different pokemon objects
            pokemonDict = new Dictionary<string, PokemonInfo>();

            // Pokemon - Poke1 object with legendary status
            pokemonDict.Add("Poke1", new PokemonInfo() { Name = "Poke1", Description = "pokemon1 is amazing", Habitat = "special", IsLegendary = true });

            // Pokemon object with translation for Poke1
            pokemonDict.Add("Poke1YodaTranslate", new PokemonInfo() { Name = "Poke1", Description = "Amazing pokemon!", Habitat = "special", IsLegendary = true });

            // Pokemon - Poke2NoLegend object without legendary status
            pokemonDict.Add("Poke2NoLegend", new PokemonInfo() { Name = "Poke2NoLegend", Description = "pokemon2 is super", Habitat = "all over", IsLegendary = false });

            // Pokemon object with translation for Poke2NoLegend
            pokemonDict.Add("Poke2SSTranslate", new PokemonInfo() { Name = "Poke2NoLegend", Description = "How super pokemon2 is", Habitat = "all over", IsLegendary = false });

            // Pokemon - Poke3Cave object with habitat as cave
            pokemonDict.Add("Poke3Cave", new PokemonInfo() { Name = "Poke1", Description = "How Amazing pokemon3 is", Habitat = "cave", IsLegendary = false });
            
            // Pokemon object with translation for Poke3Cave
            pokemonDict.Add("Poke3CaveYodaTranslate", new PokemonInfo() { Name = "Poke1", Description = "Is pokemon3 amazing", Habitat = "cave", IsLegendary = false });

            pokemonDict.Add("Poke4NoTranslate", new PokemonInfo() { Name = "Poke2NoTranslate", Description = "pokemon2 is super", Habitat = "all over", IsLegendary = true });

            // update the pokemon client mock with relevant mappinngs for api calls
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke1")).ReturnsAsync(pokemonDict["Poke1"]);
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke2NoLegend")).ReturnsAsync(pokemonDict["Poke2NoLegend"]);
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke3Cave")).ReturnsAsync(pokemonDict["Poke3Cave"]);
            pokemonClientMock.Setup(poke => poke.GetPokeman("Poke4NoTranslate")).ReturnsAsync(pokemonDict["Poke4NoTranslate"]);

            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke1")).ReturnsAsync(pokemonDict["Poke1YodaTranslate"]);
            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke2NoLegend")).ReturnsAsync(pokemonDict["Poke2SSTranslate"]);
            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke3Cave")).ReturnsAsync(pokemonDict["Poke3CaveYodaTranslate"]);
            pokemonClientMock.Setup(poke => poke.GetTranslatedPokeman("Poke4NoTranslate")).ReturnsAsync(pokemonDict["Poke4NoTranslate"]);

            controller = new PokemonController(pokemonClientMock.Object);
        }

        /// <summary>
        /// Pokemon valid test for Poke1, checks whether the result matches with Poke1 object
        /// </summary>
        [Fact]
        public void Valid_Pokemon_Available_Test()
        {
            var result = controller.GetPokemon("Poke1");

            // assert
            pokemonDict["Poke1"].Should().BeEquivalentTo(result.Result);
        }

        /// <summary>
        /// Pokemon not available test for PokeNotAvailable
        /// </summary>
        [Fact]
        public void Pokemon_Not_Available_Test()
        {
            var result = controller.GetPokemon("PokeNotAvailable");

            // assert
            Assert.Null(result.Result);
        }

        /// <summary>
        /// Pokemon not available test for PokeNotAvailable with translation
        /// </summary>
        [Fact]
        public void Pokemon_Not_Available_Test2()
        {
            var result = controller.GetPokemonTranslated("PokeNotAvailable");

            // assert
            Assert.Null(result.Result);
        }

        /// <summary>
        /// Pokemon valid test for Poke1.
        /// Checks whether the result matches with translated Poke1 object with Yoda translation.
        /// </summary>
        [Fact]
        public void Pokemon_YodaTranslate_Test()
        {
            var result = controller.GetPokemonTranslated("Poke1");

            // assert
            pokemonDict["Poke1YodaTranslate"].Should().BeEquivalentTo(result.Result);
        }

        /// <summary>
        /// Pokemon valid test for Poke2NoLegend, 
        /// checks whether the result matches with translated object.
        /// </summary>
        [Fact]
        public void Pokemon_ShakeshpearTranslate_Test()
        {
            var result = controller.GetPokemonTranslated("Poke2NoLegend");

            // assert
            pokemonDict["Poke2SSTranslate"].Should().BeEquivalentTo(result.Result);
        }

        /// <summary>
        /// Pokemon valid test for Poke3Cave with cave habitat, 
        /// checks whether the result matches with translated object.
        /// </summary>
        [Fact]
        public void Pokemon_CaveHabitat_Test()
        {
            var result = controller.GetPokemonTranslated("Poke3Cave");

            // assert
            pokemonDict["Poke3CaveYodaTranslate"].Should().BeEquivalentTo(result.Result);
        }

        /// <summary>
        /// Pokemon valid test for Poke4NoTranslate with no-translation available.
        /// </summary>
        [Fact]
        public void Pokemon_NoTranslate_Test()
        {
            var result = controller.GetPokemonTranslated("Poke4NoTranslate");

            // assert
            pokemonDict["Poke4NoTranslate"].Should().BeEquivalentTo(result.Result);
        }
    
    }
}
