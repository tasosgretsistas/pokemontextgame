using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Items
{
    [Serializable]
    class PokeBall : Item
    {
        public double catchRate;

        public PokeBall(string iName, string iDescription, bool iMultiple, int iValue, double iRate)
            : base(iName, iDescription, iMultiple, iValue)
        {
            catchRate = iRate;
            Type = "pokeball";
        }



    }
}
