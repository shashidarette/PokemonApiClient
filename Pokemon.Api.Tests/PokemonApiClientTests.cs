using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace Pokemon.Api.Tests
{
    /// <summary>
    /// PokemonAPI Client tests class using WebApplicationFactory.
    /// It contains tests to check the end-points. 
    /// </summary>
    public class PokemonApiClientTests : IClassFixture<WebApplicationFactory<Pokedex.Startup>>
    {
        HttpClient Client { get; }

        /// <summary>
        /// Responsible to create the client to execute Pokedex API
        /// </summary>
        /// <param name="fixture"></param>
        public PokemonApiClientTests(WebApplicationFactory<Pokedex.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        /// <summary>
        /// Checks whether valid request for API end point : pokemon/{name} returns OK (200)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Should_Retrieve_Pokemon()
        {
            var response = await Client.GetAsync("/pokemon/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Checks whether valid request for API end point : pokemon/translated/{name} returns OK (200)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Should_Retrieve_Pokemon_Translation()
        {
            var response = await Client.GetAsync("/pokemon/translated/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Checks whether invalid request for API end point : /pokemon1/mewtwo returns not found (404)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ShouldNot_Retrieve_Pokemon()
        {
            var response = await Client.GetAsync("/pokemon1/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Checks whether invalid request for API end point : /pokemon/translated1/mewtwo returns not found (404)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ShouldNot_Retrieve_Pokemon_Translation()
        {
            var response = await Client.GetAsync("/pokemon/translated1/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Checks whether valid request for API end point : /pokemon/translated/mewtwo returns OK (200) with translation.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Should_Retrieve_Pokemon_YodaTranslation()
        {
            var response = await Client.GetAsync("/pokemon/translated/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Checks whether valid request for API end point : /pokemon/translated/pikachu returns OK (200) without translation.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Should_Retrieve_Pokemon_ShakesTranslation()
        {
            var response = await Client.GetAsync("/pokemon/translated/pikachu");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
