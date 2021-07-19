using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Pokedex.Controllers;
using Pokemon.DataModel;
using Pokemon.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.Tests
{
    public class PokemonApiClientTests : IClassFixture<WebApplicationFactory<Pokedex.Startup>>
    {
        HttpClient Client { get; }

        public PokemonApiClientTests(WebApplicationFactory<Pokedex.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Pokemon()
        {
            var response = await Client.GetAsync("/pokemon/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Should_Retrieve_Pokemon_Translation()
        {
            var response = await Client.GetAsync("/pokemon/translated/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_ShouldNot_Retrieve_Pokemon()
        {
            var response = await Client.GetAsync("/pokemon1/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_ShouldNot_Retrieve_Pokemon_Translation()
        {
            var response = await Client.GetAsync("/pokemon/translated1/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Should_Retrieve_Pokemon_YodaTranslation()
        {
            var response = await Client.GetAsync("/pokemon/translated/mewtwo");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Should_Retrieve_Pokemon_ShakesTranslation()
        {
            var response = await Client.GetAsync("/pokemon/translated/pikachu");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
