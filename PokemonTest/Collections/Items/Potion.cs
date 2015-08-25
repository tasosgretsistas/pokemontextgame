using System;

namespace PokemonTextEdition.Items
{
    [Serializable]
    class Potion : Item
    {
        public int healAmount; //The amount of HP this particular potion restores.

        public Potion(int iID, string iName, string iDescription, bool iMultiple, int iValue, int iHeal)
            : base(iID, iName, iDescription, iMultiple, iValue)
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
            if (pokemon != null)
            {
                if (pokemon.CurrentHP > 0 && pokemon.CurrentHP < pokemon.MaxHP)
                {
                    int previousHP = pokemon.CurrentHP;

                    if (pokemon.MaxHP > (previousHP + healAmount))
                        pokemon.CurrentHP += healAmount;

                    else
                        pokemon.CurrentHP = pokemon.MaxHP;

                    Console.WriteLine("{0} HP was restored to {1}.", (pokemon.CurrentHP - previousHP), pokemon.Name);

                    Program.Log("The player uses a Potion on " + pokemon.Name + ", restoring " + (pokemon.CurrentHP - previousHP) + "HP.", 1);

                    Remove(1, "use");

                    return true;
                }

                else if (pokemon.CurrentHP <= 0)
                {
                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    Console.WriteLine("You cannot use a {0} on a Pokemon that has fainted.\n", Name);

                    return false;
                }

                else
                {
                    Program.Log("The player selected a Pokemon that was already at max HP.", 0);
                    Console.WriteLine("That Pokemon is already at max HP.\n");

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
