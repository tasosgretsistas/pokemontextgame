using PokemonTextEdition.Items;
using System.Collections.Generic;

namespace PokemonTextEdition
{
    /// <summary>
    /// A list of all the items currently in the game.
    /// </summary>
    class ItemList
    {
        #region Pokeballs

        static public PokeBall pokeball = new PokeBall(1, "PokeBall", "A tool for catching wild Pokemon.", false, 100, 1f);
        static public PokeBall greatball = new PokeBall(2, "Great Ball", "A tool for catching wild Pokemon. Better than PokeBall.", false, 300, 1.5f);
        static public PokeBall ultraball = new PokeBall(3, "Ultra Ball", "A tool for catching wild Pokemon. Better catch rate than Great Ball.", false, 600, 2f);
        static public PokeBall masterball = new PokeBall(4, "Master Ball", "The ultimate tool for wild catching Pokemon. Never fails.", false, 0, -1.0f);

        #endregion

        #region Potions & HP Restoration

        static public Potion potion = new Potion(5, "Potion", "Restores 20HP to a selected Pokemon.", false, 100, 20);
        static public Potion superpotion = new Potion(6, "Super Potion", "Restores 50HP to a selected Pokemon.", false, 250, 50);
        static public Potion hyperpotion = new Potion(7, "Hyper Potion", "Restores 200HP to a selected Pokemon.", false, 500, 20);
        static public Potion maxpotion = new Potion(8, "Max Potion", "Restores a Pokemon to full HP.", false, 1000, 0);
        static public Potion fullrestore = new Potion(9, "Full Restore", "Restores a Pokemon to full HP and heals all status conditions.", false, 1250, 0);

        #endregion

        #region Status Heal

        static public StatusHeal antidote = new StatusHeal(10, "Antidote", "Cures poison from a selected Pokemon.", false, 50, PokemonStatus.Poison);
        static public StatusHeal paralyzeheal = new StatusHeal(11, "Paralyze Heal", "Heals paralysis from a selected Pokemon.", false, 100, PokemonStatus.Paralysis);
        static public StatusHeal awakening = new StatusHeal(12, "Awakening", "Awakens a selected Pokemon from sleep.", false, 125, PokemonStatus.Sleep);
        static public StatusHeal burnheal = new StatusHeal(13, "Burn Heal", "Heals burn from a selected Pokemon.", false, 125, PokemonStatus.Burn);
        static public StatusHeal iceheal = new StatusHeal(14, "Ice Heal", "Defrosts a frozen Pokemon.", false, 125, PokemonStatus.None);
        static public StatusHeal fullheal = new StatusHeal(15, "Full Heal", "Heals all status conditions from a Pokemon.", false, 300, PokemonStatus.None);

        #endregion

        public static List<Item> allItems = new List<Item> 
        {
            pokeball, greatball, ultraball, masterball,
            potion, superpotion, hyperpotion, maxpotion, fullrestore,
            antidote, paralyzeheal, awakening, burnheal, iceheal, fullheal
        };
    }
}
