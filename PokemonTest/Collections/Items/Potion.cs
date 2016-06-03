using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Items
{
    /// <summary>
    /// This class represents Potion type items, which are items that the player can use to restore HP to his Pokemon.
    /// </summary>
    class Potion : Item
    {
        protected int healAmount;

        /// <summary>
        /// The amount of HP restored by this particular potion. Should always be a non-negative integer. Leave as 0 if the item would heal the Pokemon to full health.
        /// </summary>
        public int HealAmount
        {
            get
            {
                return healAmount;
            }

            set
            {
                if (value >= 0)
                    healAmount = value;

                else
                    healAmount = 0;
            }
        }

        /// <summary>
        /// Main potion constructor. Creates a generic potion with the specified parameters, and sets its type to <see cref="ItemType.Potion"/>.
        /// </summary>
        /// <param name="iID">The potion's unique ID number.</param>
        /// <param name="iName">The potion's name.</param>
        /// <param name="iDescription">A description of the potion's purpose.</param>
        /// <param name="iValue">The potion's value when buying from a store.</param>
        /// <param name="iHeal">The amount of health that the potion restores. Set to 0 if the potion always heals to full HP.</param>
        public Potion(int iID, string iName, string iDescription, int iValue, int iHeal)
            : base(iID, iName, iDescription, iValue)
        {
            Type = ItemType.Potion;

            HealAmount = iHeal;            
        }

        /// <summary>
        /// Attempts to use a Potion type item.
        /// </summary>
        /// <returns>True if the player succesfully used the item, or false if he did not.</returns>
        public override bool Use()
        {
            Program.Log("The player is trying to use a " + Name + ".", 0);

            UI.WriteLine("Use " + Name + " on which Pokemon?\n(Valid input: 1-" + Game.Player.Party.Count +  " or press Enter to return)\n");

            //First, the player is asked to select a Pokemon in his party.
            Pokemon pokemon = Game.Player.SelectPokemon(false);

            //If the player's input was valid, the operation carries on.
            if (pokemon != null)
            {
                //If the Pokemon the user selected is alive and not at full life, then...
                if (!pokemon.Fainted && pokemon.CurrentHP < pokemon.MaxHP)
                {
                    // [FIX]
                    //One of this item is removed from the player's bag.
                    //Remove(1, RemoveType.Use);

                    //Then, the Pokemon gets healed for the HealAmount.
                    int previousHP = pokemon.CurrentHP;

                    if (pokemon.MaxHP > (previousHP + HealAmount))
                        pokemon.CurrentHP += HealAmount;

                    else
                        pokemon.CurrentHP = pokemon.MaxHP;

                    UI.WriteLine((pokemon.CurrentHP - previousHP) + " HP was restored to " + pokemon.Name +".");

                    Program.Log("The player uses a Potion on " + pokemon.Name + ", restoring " + (pokemon.CurrentHP - previousHP) + "HP.", 1);                    

                    //And finally, this method returns "true" for operation success.
                    return true;
                }

                //Otherwise, an appropriate error message is displayed and the method returns "false" for operation failure.
                else if (pokemon.Fainted)
                {
                    UI.WriteLine("You cannot use a " + Name + " on a Pokemon that has fainted.\n");

                    Program.Log("The player selected a Pokemon that has fainted.", 0);                   

                    return false;
                }

                else
                {
                    UI.WriteLine("That Pokemon is already at max HP.\n");

                    Program.Log("The player selected a Pokemon that was already at max HP.", 0);
                    

                    return false;
                }
            }

            else
                return false;
        }

        /// <summary>
        /// Attempts to use a Potion type item during combat. As Potions have the same effect inside and outside of combat, this simply calls the Use() method.
        /// </summary>
        /// <returns>True if the player succesfully used the item, or false if he did not.</returns>
        public override bool UseCombat()
        {
            return Use();
        }
    }
}
