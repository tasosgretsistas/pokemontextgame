using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Items
{
    /// <summary>
    /// This class represents status heal type items, which are used by the player in order to cure Pokemon of their status ailments, such as burn and poison.
    /// </summary>
    class StatusHeal : Item
    {
        /// <summary>
        /// The type of status ailment this particular item can cure. Set to FullHeal if the item can cure any status.
        /// </summary>
        public StatusCondition CureType { get; set; }

        /// <summary>
        /// Main status heal item constructor. Creates a generic potion with the specified parameters, and sets its type to <see cref="ItemType.StatusHeal"/>.
        /// </summary>
        /// <param name="iID">The status heal item's unique ID number.</param>
        /// <param name="iName">The status heal item's name.</param>
        /// <param name="iDescription">A description of the status heal item's purpose.</param>
        /// <param name="iValue">The status heal item's value when buying from a store.</param>
        /// <param name="iCureType">The type of status ailment that this particular status heal item can cure. Set to FullHeal if the item can cure any status.</param>
        public StatusHeal(int iID, string iName, string iDescription, int iValue, StatusCondition iCureType)
            : base(iID, iName, iDescription, iValue)
        {            
            Type = ItemType.StatusHeal;

            CureType = iCureType;
        }

        /// <summary>
        /// Attempts to use a StatusHeal type item.
        /// </summary>
        /// <returns>True if the player succesfully used the item, or false if he did not.</returns>
        public override bool Use()
        {
            Program.Log("The player is trying to use a " + Name + ".", 0);

            UI.WriteLine("Use " + Name + " on which Pokemon?\n(Valid input: 1-" + Game.Player.Party.Count + " or press Enter to return)\n");

            //First, the player is asked to select a Pokemon in his party.
            Pokemon pokemon = Game.Player.SelectPokemon(false);

            //If the player's input was valid, the operation carries on.
            if (pokemon.Name != null)
            {
                //If the Pokemon the user selected is alive and is suffering from a status condition that can be cured by this item, then...
                if (!pokemon.Fainted && pokemon.Status == CureType)
                {
                    // [FIX]
                    //One of this item is removed from the player's bag.
                    //Remove(1, RemoveType.Use);

                    //Then, the Pokemon's status is cured.
                    pokemon.CureStatus(true);

                    Program.Log("The uses a " + Name + " on " + pokemon.Name + ", curing it of its " + CureType + ".", 1);                    

                    return true;
                }

                else if (pokemon.CurrentHP <= 0)
                {
                    UI.WriteLine("You cannot use a " + Name + " on a Pokemon that has fainted.");

                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    

                    return false;
                }

                else
                {
                    UI.WriteLine(Name + " is not suffering from " + CureType + ".");

                    Program.Log("The Pokemon the user selected was not afflicted by " + CureType + ".", 0);                    

                    return false;
                }
            }

            else
                return false;
        }

        /// <summary>
        /// Attempts to use a status heal type item during combat. As status heal items have the same effect inside and outside of combat, this simply calls the Use() method.
        /// </summary>
        /// <returns>True if the player succesfully used the item, or false if he did not.</returns>
        public override bool UseCombat()
        {
            return Use();
        }



    }
}
