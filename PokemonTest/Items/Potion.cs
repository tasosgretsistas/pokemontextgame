using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Items
{
    [Serializable]
    class Potion : Item
    {
        public int healAmount; //The amount of HP this particular potion restores.

        public Potion(string iName, string iDescription, bool iMultiple, int iValue, int iHeal)
            : base(iName, iDescription, iMultiple, iValue)
        {
            healAmount = iHeal;
            Type = "potion";
        }

        public override bool Use()
        {
            //Code for using a Potion type item. Since using a Potion inside and outside of combat 
            //does the exact same thing, the UseCombat() method simply redirects here again.

            Program.Log("The player is trying to use a " + Name + ".", 0);

            Console.WriteLine("Use {0} on which Pokemon?\n(Valid input: 1-{1} or press Enter to return)\n", Name, Overworld.player.party.Count);

            Pokemon pokemon = Overworld.player.SelectPokemon(false);

            //If the Pokemon the user selected is alive and not at full life, it gets healed for the healAmount and this method returns "true" for operation success.
            //Otherwise, an appropriate error message is displayed and the method returns "false" for operation failure.
            if (pokemon.name != "Blank")
            {
                if (pokemon.currentHP > 0 && pokemon.currentHP < pokemon.maxHP)
                {
                    int previousHP = pokemon.currentHP;

                    if (pokemon.maxHP > (previousHP + healAmount))
                        pokemon.currentHP += healAmount;

                    else
                        pokemon.currentHP = pokemon.maxHP;

                    Console.WriteLine("\n{0} HP was restored to {1}.\n", (pokemon.currentHP - previousHP), pokemon.name);

                    Program.Log("The player uses a Potion on " + pokemon.name + ", restoring " + (pokemon.currentHP - previousHP) + "HP.", 1);

                    Remove(1, "use");

                    return true;
                }

                else if (pokemon.currentHP <= 0)
                {
                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    Console.WriteLine("\nYou cannot use a {0} on a Pokemon that has fainted.\n", Name);

                    return false;
                }

                else
                {
                    Program.Log("The player selected a Pokemon that was already at max HP.", 0);
                    Console.WriteLine("\nThat Pokemon is already at max HP.\n");

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
