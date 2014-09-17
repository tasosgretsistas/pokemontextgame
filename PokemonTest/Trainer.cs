using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class Trainer
    {
        //A class that represents enemy trainers within the game.

        public string Name { get; set; } //The trainer's name.
        public string Greeting { get; set; }  //The trainer's generic greeting.
        public string DefeatSpeech { get; set; }  //The trainer's generic message upon losing.
        public string VictorySpeech { get; set; } //The trainer's generic message upon winning.

        //A unique identifier that marks each and every trainer in the TrainerList class individually. 
        //An "r" after the number identifies that the trainer has already been defeated once and the battle is a rematch.
        public string ID { get; set; }

        public int Money { get; set; }  //The money the trainer awards.

        public List<Pokemon> party = new List<Pokemon>(); //The trainer's party.

        /// <summary>
        /// The main constructor for initializing an enemy trainer NPC.
        /// </summary>
        /// <param name="tName">The trainer's name.</param>
        /// <param name="tGreeting">The trainer's greeting pre-combat.</param>
        /// <param name="tDefeat">The trainer's message upon being defeated.</param>
        /// <param name="tVictory">The trainer's message if he defeats a player.</param>
        /// <param name="tParty">The trainer's party of Pokemon. Maximum size is 6.</param>
        /// <param name="tMoney">The amount of money the trainer yields upon being defeated.</param>
        /// <param name="tid">The trainer's unique identifier number for the TrainerList list.</param>
        public Trainer(string tName, string tGreeting, string tDefeat, string tVictory, List<Pokemon> tParty, int tMoney, string tid)
        {
            Name = tName;
            Greeting = "\n\"" + tGreeting + "\"";
            DefeatSpeech = "\n\"" + tDefeat + "\"";
            VictorySpeech = "\n\"" + tVictory + "\"";
            party = tParty;
            Money = tMoney;
            ID = tid;
        }

        public Trainer(string n)
        {
            Name = n;
        }

        public Trainer()
        {
            Name = "";
            Greeting = "Hello!";
            DefeatSpeech = "Oh no! I can't believe I lost!";
            VictorySpeech = "Haha! Eat it!";

            ID = "0";
            Money = 0; 
        }

        public bool Defeated()
        {
            //This method checks if the player has defeated a trainer by checking whether the player's defeatedTrainers list contains the trainer's ID.

            if (Overworld.player.defeatedTrainers.Contains(ID))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public virtual void Encounter()
        {
            //Code for triggering an encounter with an enemy trainer.
            //First, all of the trainer's Pokemon are healed, then the trainer plays his specific greeting, and finally battle starts.

            foreach (Pokemon p in party)
            {
                p.currentHP = p.maxHP;
                p.status = "";
            }

            Console.WriteLine(Greeting);

            Console.WriteLine("");

            new Battle().Start(this, "trainer");
        }

        public virtual void Defeat()
        {
            //Code that triggers upon defeating a trainer.

            Overworld.player.defeatedTrainers.Add(ID);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Console.WriteLine(DefeatSpeech);
        }

        public virtual void Victory()
        {            
            Console.WriteLine(VictorySpeech);
            Console.WriteLine("\nYou will now be taken to the last city you rested at.");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            Overworld.player.PartyHeal();

            Overworld.LoadLocation(Overworld.player.LastHeal);
        }
    }
}
