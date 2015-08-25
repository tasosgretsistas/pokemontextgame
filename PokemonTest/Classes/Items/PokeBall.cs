using System;

namespace PokemonTextEdition.Items
{
    [Serializable]
    class PokeBall : Item
    {
        public float catchRate;

        public PokeBall(int iID, string iName, string iDescription, bool iMultiple, int iValue, float iRate)
            : base(iID, iName, iDescription, iMultiple, iValue)
        {
            catchRate = iRate;
            Type = "pokeball";
        }

        public override bool UseCombat()
        {
            Program.Log("The player tried to use a " + Name + " during combat.", 0);

            Console.WriteLine("To use a " + Name + ", use the \"(c)atch\" command from the actions screen.\n");

            return false;
        }

    }
}
