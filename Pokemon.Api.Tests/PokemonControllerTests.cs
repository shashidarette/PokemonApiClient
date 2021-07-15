using System;
using Xunit;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Pokedex;

namespace Pokemon.Api.Tests
{
    public class PokemonControllerTests : IClassFixture<WebApplicationFactory<Pokedex.Startup>>
    {
        public HttpClient Client { get; }

        public PokemonControllerTests(WebApplicationFactory<Pokedex.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }
    }
}
