using System;

namespace PokemonTextEdition.Items
{
    [Serializable]
    class PokeBall : Item
    {
        public float catchRate;

        public PokeBall(string iName, string iDescription, bool iMultiple, int iValue, float iRate)
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
