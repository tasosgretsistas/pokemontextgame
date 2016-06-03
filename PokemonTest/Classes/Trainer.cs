using PokemonTextEdition.Engine;
using System.Collections.Generic;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// This class represents the various enemy trainers that the player can battle within the game.
    /// <para>This class can represent either generic trainers by creating an object in the TrainerList.allTrainers list,
    /// or signify unique trainers with their own respective scripts through inheritance.</para>
    /// </summary>
    class Trainer
    {
        #region Fields

        //A unique identifier that marks each and every trainer in the TrainerList class individually. 
        //A negative ID identifies that the trainer has already been defeated once and the battle is a rematch.
        public int TrainerID { get; set; }

        public string Name { get; set; } //The trainer's name.
        public string Type { get; set; } //The trainer's "class" - i.e., Hiker, Bug Catcher, etc.

        //The trainer's displayed name - a combination of his trainer type (if any) and name.
        public string DisplayName 
        {
            get
            {
                if (Type != "")
                    return Type + " " + Name;

                return Name;
            }
        }

        //The trainer's money yield upon defeat.
        public int Money { get; set; }

        //The various chit-chat that the trainer produces.
        public string Greeting { get; set; }
        public string DefeatSpeech { get; set; }
        public string VictorySpeech { get; set; }

        //The trainer's party of Pokemon.
        public List<Pokemon> Party;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructor for blank trainers. Creates a Trainer named "Undefined Trainer" with an empty party and all of its other attributes set to 0, false and empty strings.
        /// </summary>
        public Trainer()
        {
            Type = string.Empty;
            Name = "Undefined Trainer";

            Greeting = string.Empty;
            DefeatSpeech = string.Empty;
            VictorySpeech = string.Empty;

            Party = new List<Pokemon> { };

            TrainerID = 0;
            Money = 0;
        }

        /// <summary>
        /// Constructor for creating generic trainers. Most enemy trainers will be generic so this should be used most of the time.
        /// </summary>
        /// <param name="tid">The trainer's ID.</param>
        /// <param name="tType">The trainer's type, i.e. "Pokemon Trainer" or "Hiker".</param>
        /// <param name="tName">The trainer's name.</param>
        /// <param name="tMoney">The amount of money the trainer yields upon being defeated.</param>
        /// <param name="tGreeting">The trainer's greeting message upon being encountered.</param>
        /// <param name="tDefeat">The message the trainer displays upon being defeated.</param>
        /// <param name="tVictory">The message the trainer displays upon defeating the player.</param>
        /// <param name="tParty">The trainer's party of Pokemon, as a list of Pokemon.</param>
        public Trainer(int tid, string tType, string tName, int tMoney, string tGreeting, string tDefeat, string tVictory, List<Pokemon> tParty)
        {
            TrainerID = tid;

            Type = tType;
            Name = tName;

            Money = tMoney;

            Greeting = "\"" + tGreeting + "\"";
            DefeatSpeech = "\"" + tDefeat + "\"";
            VictorySpeech = "\"" + tVictory + "\"";

            Party = tParty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the player has defeated the trainer by checking whether the player's DefeatedTrainers list contains the trainer's ID.
        /// </summary>
        public bool HasBeenDefeated(Player player)
        {
            if (Game.DefeatedTrainers.Contains(TrainerID))
                    return true;

                else
                    return false;
            
        }

        /// <summary>
        /// Begins an encounter with the trainer.
        /// </summary>
        public virtual void Encounter()
        {
            //First, all of the trainer's Pokemon are healed, just as a failsafe.
            foreach (Pokemon p in Party)
            {
                p.HealFull(false);
                p.CureStatus(false);
            }

            //Then, the trainer's greeting message is displayed.
            UI.WriteLine(Greeting + "\n");

            //Finally, battle with the trainer commences.
            Battle battle = new Battle(this);

            //if (battle.PlayerVictory)
        }

        /// <summary>
        /// Handles the events which need to take place when the trainer is defeated.
        /// </summary>
        public virtual void Defeat(Player player)
        {
            Game.DefeatedTrainers.Add(TrainerID);

            UI.AnyKey();

            UI.WriteLine(DefeatSpeech + "\n");
        }

        public virtual void Victory(Player player)
        {            
            UI.WriteLine(VictorySpeech + "\n");

            Game.BlackOut();
        }

        #endregion
    }
}
        