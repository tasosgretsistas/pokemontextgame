using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Items
{
    /// <summary>
    /// This class represents Pokeball type items, which are tools that the player can use to capture wild Pokemon.
    /// </summary>
    class PokeBall : Item
    {
        protected float catchRate;

        /// <summary>
        /// Determines the Pokeball's multiplier for trying to catch Pokemon. Should always be a non-negative float.
        /// <para>Example: 0 = will never catch a Pokemon, 1.5f = 50% increased capture chance, etc.</para>
        /// </summary>
        public float CatchRate
        {
            get
            {
                return catchRate;
            }

            set
            {
                if (value >= 0)
                    catchRate = value;

                else
                    catchRate = 0;
            }
        }

        /// <summary>
        /// Main Pokeball constructor. Creates a generic Pokeball with the specified parameters, and sets its type to <see cref="ItemType.Pokeball"/>.
        /// </summary>
        /// <param name="iID">The Pokeball's unique ID number.</param>
        /// <param name="iName">The Pokeball's name.</param>
        /// <param name="iDescription">A description of the Pokeball's purpose.</param>
        /// <param name="iValue">The Pokeball's value when buying from a store.</param>
        /// <param name="iCatchRate">The Pokeball's specific catch rate multiplier. Should be a non-negative float. Refer to the Pokeball's CatchRate property for more info.</param>
        public PokeBall(int iID, string iName, string iDescription, int iValue, float iCatchRate)
            : base(iID, iName, iDescription, iValue)
        {
            Type = ItemType.Pokeball;

            CatchRate = iCatchRate;            
        }

        /// <summary>
        /// Attempts to use a Pokeball type item in combat.
        /// </summary>
        /// <returns>Always returns false, as Pokeballs are used with the special command "catch" during combat.</returns>
        public override bool UseCombat()
        {
            UI.WriteLine("To use a " + Name + ", use the \"(c)atch\" command from the actions screen.\n");

            return false;
        }

    }
}
