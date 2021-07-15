using Pokemon.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Interfaces
{
    public interface ITranslationClient
    {
        Task<string> GetPokemonTranslation(string data);
    }
}
