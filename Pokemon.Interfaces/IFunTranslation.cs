using Pokemon.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Interfaces
{
    public interface IFunTranslation
    {
        Task<string> GetPokemonTranslation(string data);
    }
}
