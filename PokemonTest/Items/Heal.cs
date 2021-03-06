﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Items
{
    [Serializable]
    class Heal : Item
    {
        public string healType; //The type of status ailment this particular item heals.

        public Heal(string iName, string iDescription, bool iMultiple, int iValue, string iHeal)
            : base(iName, iDescription, iMultiple, iValue)
        {
            healType = iHeal;
            Type = "heal";
        }

        public override bool Use()
        {
            //Code for using a Healing type item. Since using a Healing item inside and outside of combat 
            //does the exact same thing, the UseCombat() method simply redirects here again.

            Program.Log("The player is trying to use a " + Name + ".", 0);

            Console.WriteLine("\nUse {0} on which Pokemon?\n(Valid input: 1-{0} or press Enter to return)\n", Name, Overworld.player.party.Count);

            Pokemon pokemon = Overworld.player.SelectPokemon(false);

            //If the Pokemon the user selected is alive and is suffering from a status this item can cure, it gets healed for the healAmount and this method returns
            // "true" for operation success. Otherwise, an appropriate error message is displayed and the method returns "false" for operation failure.
            if (pokemon.name != "Blank")
            {
                if (pokemon.currentHP > 0 && pokemon.status == healType)
                {
                    pokemon.status = "";

                    Console.WriteLine("\n{0} was cured of its {1}.", pokemon.name, healType);

                    Program.Log("The uses a " + Name + " on " + pokemon.name + ", curing it of its " + healType + ".", 1);

                    Count--;

                    return true;
                }

                else if (pokemon.currentHP <= 0)
                {
                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    Console.WriteLine("You cannot use a {0} on a Pokemon that has fainted.", Name);

                    return false;
                }

                else
                {
                    Program.Log("The Pokemon the user selected was not afflicted by " + healType + ".", 0);
                    Console.WriteLine("{0} is not suffering.", Name);

                    return false;
                }
            }

            else
                return false;
        }

        public override bool UseCombat()
        {
            return Use();
        }



    }
}
