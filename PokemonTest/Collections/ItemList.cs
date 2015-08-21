using PokemonTextEdition.Items;
using System;
using System.Collections.Generic;

namespace PokemonTextEdition
{
    class ItemList
    {
        //A list of all items available in the game.

        #region Pokeballs

        static public PokeBall pokeball = new PokeBall("PokeBall", "A tool for catching wild Pokemon.", false, 100, 1f);
        static public PokeBall greatball = new PokeBall("Great Ball", "A tool for catching wild Pokemon. Better than PokeBall.", false, 300, 1.5f);
        static public PokeBall ultraball = new PokeBall("Ultra Ball", "A tool for catching wild Pokemon. Better catch rate than Great Ball.", false, 600, 2f);
        static public PokeBall masterball = new PokeBall("Master Ball", "The ultimate tool for wild catching Pokemon. Never fails.", false, 0, -1.0f);

        #endregion

        #region Potions & HP Restoration

        static public Potion potion = new Potion("Potion", "Restores 20HP to a selected Pokemon.", false, 100, 20);
        static public Potion superpotion = new Potion("Super Potion", "Restores 50HP to a selected Pokemon.", false, 250, 50);
        static public Potion hyperpotion = new Potion("Hyper Potion", "Restores 200HP to a selected Pokemon.", false, 500, 20);
        static public Potion maxpotion = new Potion("Max Potion", "Restores a Pokemon to full HP.", false, 1000, 0);
        static public Potion fullrestore = new Potion("Full Restore", "Restores a Pokemon to full HP and heals all status conditions.", false, 1250, 0);

        #endregion

        #region Status Heal

        static public StatusHeal antidote = new StatusHeal("Antidote", "Cures poison from a selected Pokemon.", false, 50, "poison");
        static public StatusHeal paralyzeheal = new StatusHeal("Paralyze Heal", "Heals paralysis from a selected Pokemon.", false, 100, "paralysis");
        static public StatusHeal awakening = new StatusHeal("Awakening", "Awakens a selected Pokemon from sleep.", false, 125, "sleep");
        static public StatusHeal burnheal = new StatusHeal("Burn Heal", "Heals burn from a selected Pokemon.", false, 125, "burn");
        static public StatusHeal iceheal = new StatusHeal("Ice Heal", "Defrosts a frozen Pokemon.", false, 125, "freeze");
        static public StatusHeal fullheal = new StatusHeal("Full Heal", "Heals all status conditions from a Pokemon.", false, 300, "full");

        #endregion

        public static List<Item> allItems = new List<Item> 
        {
            pokeball, greatball, ultraball, masterball,
            potion, superpotion, hyperpotion, maxpotion, fullrestore,
            antidote, paralyzeheal, awakening, burnheal, iceheal, fullheal
        };


        /// <summary>
        /// List all items in the "allItems" list.
        /// </summary>
        public static void ListAllItems()
        {
            foreach (Item i in allItems)
            {
                Console.WriteLine(i.Name);
            }

            Console.WriteLine("");
        }
    }
}
