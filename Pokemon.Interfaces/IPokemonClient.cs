using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pokemon.DataModel;

namespace Pokemon.Interfaces
{
    /// <summary>
    /// Interface for Pokemon Client
    /// </summary>
    public interface IPokemonClient
    {
        Task<PokemonInfo> GetPokeman(string name);
        Task<PokemonInfo> GetTranslatedPokeman(string name);
    }
}
