using PokemonTextEdition.Classes;
using PokemonTextEdition.Items;
using System.Collections.Generic;

namespace PokemonTextEdition.Collections
{
    /// <summary>
    /// Represents all of the items in the game. The items are accessible through the <see cref="AllItems"/> list.
    /// </summary>
    class ItemList
    {
        #region Pokeballs

        static public PokeBall pokeball = new PokeBall(1, "PokeBall", "A tool for catching wild Pokemon.", 100, 1);
        static public PokeBall greatball = new PokeBall(2, "Great Ball", "A tool for catching wild Pokemon. Better than PokeBall.", 300, 1.5f);
        static public PokeBall ultraball = new PokeBall(3, "Ultra Ball", "A tool for catching wild Pokemon. Better catch rate than Great Ball.", 600, 2);
        static public PokeBall masterball = new PokeBall(4, "Master Ball", "The ultimate tool for wild catching Pokemon. Never fails.", 0, 255);

        #endregion

        #region Potions & HP Restoration

        static public Potion potion = new Potion(5, "Potion", "Restores 20HP to a selected Pokemon.", 100, 20);
        static public Potion superpotion = new Potion(6, "Super Potion", "Restores 50HP to a selected Pokemon.", 250, 50);
        static public Potion hyperpotion = new Potion(7, "Hyper Potion", "Restores 200HP to a selected Pokemon.", 500, 200);
        static public Potion maxpotion = new Potion(8, "Max Potion", "Restores a Pokemon to full HP.", 1000, 0);
        static public Potion fullrestore = new Potion(9, "Full Restore", "Restores a Pokemon to full HP and heals all status conditions.", 1250, 0);

        #endregion

        #region Status Heal

        static public StatusHeal antidote = new StatusHeal(10, "Antidote", "Cures poison from a selected Pokemon.", 50, StatusCondition.Poison);
        static public StatusHeal paralyzeheal = new StatusHeal(11, "Paralyze Heal", "Heals paralysis from a selected Pokemon.", 100, StatusCondition.Paralysis);
        static public StatusHeal awakening = new StatusHeal(12, "Awakening", "Awakens a selected Pokemon from sleep.", 125, StatusCondition.Sleep);
        static public StatusHeal burnheal = new StatusHeal(13, "Burn Heal", "Heals burn from a selected Pokemon.", 125, StatusCondition.Burn);
        static public StatusHeal iceheal = new StatusHeal(14, "Ice Heal", "Defrosts a frozen Pokemon.", 125, StatusCondition.None);
        static public StatusHeal fullheal = new StatusHeal(15, "Full Heal", "Heals all status conditions from a Pokemon.", 300, StatusCondition.None);

        #endregion

        /// <summary>
        /// A list that contains all of the items currently available in the game.
        /// </summary>
        public static List<Item> AllItems = new List<Item> 
        {
            pokeball, greatball, ultraball, masterball,
            potion, superpotion, hyperpotion, maxpotion, fullrestore,
            antidote, paralyzeheal, awakening, burnheal, iceheal, fullheal
        };
    }
}
