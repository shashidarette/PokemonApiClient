using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokemon.Client;
using Pokemon.Interfaces;

namespace Pokedex
{
    /// <summary>
    /// Program class acts as a start point and responsible to initiate relevant services for the REST API
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function responsible to create the host for rest api. 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the rest api host with relevant services and startup.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IPokemonClient, PokemonApiClient>()
                            .AddSingleton<IYodaTranslation, PokemonYodaClient>()
                            .AddSingleton<IShakespeareTranslation, PokemonShakespeareClient>())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
