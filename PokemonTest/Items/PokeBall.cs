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

        public override bool UseCombat()
        {
            Program.Log("The player tried to use a " + Name + " during combat.", 0);

            Console.WriteLine("To use a {0}, use the \"(c)atch\" command from the actions screen.\n", Name);

            return false;
        }

    }
}
