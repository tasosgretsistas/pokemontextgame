using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Items;

namespace PokemonTextEdition
{
    class ItemsList
    {
        //A list of all items available in the game.

        //Pokeballs.
        static public PokeBall pokeball = new PokeBall("PokeBall", "A tool for catching wild Pokemon.", false, 100, 1.0);
        static public PokeBall greatball = new PokeBall("Great Ball", "A tool for catching wild Pokemon. Better than PokeBall.", false, 300, 1.5);
        static public PokeBall ultraball = new PokeBall("Ultra Ball", "A tool for catching wild Pokemon. Better catch rate than Great Ball.", false, 600, 2.0);
        static public PokeBall masterball = new PokeBall("Master Ball", "The ultimate tool for wild catching Pokemon. Never fails.", false, 0, -1.0);

        //Potions and general HP restoration.
        static public Potion potion = new Potion("Potion", "Restores 20HP to a selected Pokemon.", false, 100, 20);
        static public Potion superpotion = new Potion("Super Potion", "Restores 50HP to a selected Pokemon.", false, 250, 50);
        static public Potion hyperpotion = new Potion("Hyper Potion", "Restores 200HP to a selected Pokemon.", false, 500, 20);
        static public Potion maxpotion = new Potion("Max Potion", "Restores a Pokemon to full HP.", false, 1000, 0);
        static public Potion fullrestore = new Potion("Full Restore", "Restores a Pokemon to full HP and heals all status conditions.", false, 1250, 0);

        //Status condition healing items.
        static public Heal antidote = new Heal("Antidote", "Cures poison from a selected Pokemon.", false, 50, "poison");
        static public Heal paralyzeheal = new Heal("Paralyze Heal", "Heals paralysis from a selected Pokemon.", false, 100, "paralysis");
        static public Heal awakening = new Heal("Awakening", "Awakens a selected Pokemon from sleep.", false, 125, "sleep");
        static public Heal burnheal = new Heal("Burn Heal", "Heals burn from a selected Pokemon.", false, 125, "burn");
        static public Heal iceheal = new Heal("Ice Heal", "Defrosts a frozen Pokemon.", false, 125, "freeze");
        static public Heal fullheal = new Heal("Full Heal", "Heals all status conditions from a Pokemon.", false, 300, "full");

    }
}
