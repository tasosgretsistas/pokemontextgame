using PokemonTextEdition.Classes;
using System.Collections.Generic;

namespace PokemonTextEdition.Engine
{
    #region Enums

    /// <summary>
    /// Represents the current state of the game - i.e. the player is in the overworld navigating around (Overworld), the player is
    /// talking to an NPC (Dialogue) or the player is currently in a battle (Battle).
    /// </summary>
    enum GameState
    {
        MainMenu,
        Overworld,
        Dialogue,
        Battle
    }

    /// <summary>
    /// Represents the options for the Pokemon the player may start with - i.e. Bulbasaur, Charmander, Squirtle.
    /// </summary>
    enum StarterPokemon
    {
        Bulbasaur,
        Charmander,
        Squirtle,
        Pikachu
    }

    #endregion

    class Game
    {
        #region Static Fields

        /// <summary>
        /// Represents the current state of the game - i.e. the player is in the overworld navigating around (Overworld), the player is
        /// talking to an NPC (Dialogue) or the player is currently in a battle (Battle).
        /// </summary>
        public static GameState State { get; set; }

        /// <summary>
        /// This object represents the player globally - most classes and methods will call this player.
        /// </summary>
        public static Player Player { get; set; }

        /// <summary>
        /// The name of the player's rival.
        /// </summary>
        public static string RivalName { get; set; }

        /// <summary>
        /// The player's starting Pokemon. Determines the rival's Pokemon during various stages of the game.
        /// </summary>
        public static string StarterPokemon { get; set; }

        /// <summary>
        /// The player's current location within the game.
        /// </summary>
        public static string Location { get; set; }

        /// <summary>
        /// The last city in which the player healed his Pokemon. 
        /// This is used to determine where the player will return upon losing a battle.
        /// </summary>
        public static string LastHealLocation { get; set; }

        /// <summary>
        /// All of the various trainers that the player has defeated during his journey.
        /// </summary>
        public static List<int> DefeatedTrainers { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Restores every Pokemon in the player's party to their maximum health and cures them of any status ailments.
        /// </summary>
        /// <param name="displayMessage">Determines whether a message will be displayed to the player.</param>
        public static void PartyHeal(bool displayMessage)
        {
            if (displayMessage)
                UI.WriteLine("Your Pokemon were restored to full health.\n");

            foreach (Pokemon p in Player.Party)
            {
                p.HealFull(false);
                p.CureStatus(false);
                p.ResetTemporaryEffects(true);
            }
        }

        /// <summary>
        /// This code handles the event of the player "blacking out" -- running out of available Pokemon during a fight.
        /// Heals of all the player's Pokemon then invokes the Overworld to load the location where the player last healed at.
        /// </summary>
        public static void BlackOut()
        {
            Program.Log("The player has been defeated, and is now returning to the last Pokemon Center he visited - " + LastHealLocation, 1);

            UI.WriteLine("You will now be taken to the last city you rested at.");

            UI.AnyKey();

            PartyHeal(false);

            Overworld.LoadLocationString(LastHealLocation);
        }

        #endregion
    }
}
