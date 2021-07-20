using Newtonsoft.Json;
using System;

namespace Pokemon.DataModel
{
    /// <summary>
    /// Pokemon object with information required for the rest api
    /// </summary>
    public class PokemonInfo
    {
        /// <summary>
        /// Name of the pokemon
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description available from Pokemon API
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Habitat of the pokemon
        /// </summary>
        [JsonProperty("habitat")]
        public string Habitat { get; set; }

        /// <summary>
        /// Legendary status of the pokemon
        /// </summary>
        [JsonProperty("isLegendary")]
        public bool IsLegendary { get; set; }
    }
}
