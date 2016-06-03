using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Engine
{
    /// <summary>
    /// This class holds any non-generic events within the game.
    ///  One of these events is the very beginning of the game, invoked with the Introduction() method.
    /// </summary>
    class Story
    {
        //Declaration for the rival.
        public static Rival1 rival = new Rival1();

        public static DateTime beginDate;

        public static void Introduction()
        {
            //The story's narration.
            UI.WriteLine("Welcome to the world of Pokemon! In this world, boys and girls can choose to");
            UI.WriteLine("become Pokemon trainers when they come of age. Pokemon trainers are people who");
            UI.WriteLine("train Pokemon in order to battle other Pokemon. It just so happens that today");
            UI.WriteLine("is the day you finally become a Pokemon trainer yourself!\n");

            UI.WriteLine("You wake up to the buzzing sound of your alarm clock. It's already 11 AM!");
            UI.WriteLine("Uh oh, you're gonna be late! You quickly put on the first set of clothes you");
            UI.WriteLine("find and head straight for Professor Oak's lab. The Professor is standing");
            UI.WriteLine("beside a table with three Pokeballs on it.");

            UI.AnyKey();

            UI.WriteLine("\"Ah, there you are -- I've been waiting all morning for you. Say, could you");
            UI.WriteLine(" remind me what your name was again?\"\n");

            Game.Player.Name = PlayerName();

            UI.WriteLine("\"Oh yes, of course, how could I forget! Which reminded me, my grandson has yet");
            UI.WriteLine(" to arrive. You remember him, right? You two used to be rivals for the longest");
            UI.WriteLine(" time when you were children. His name is...\"\n");

            rival.Name = RivalName();

            UI.WriteLine("\"He should be joining us shortly. I know you've been waiting for this moment");
            UI.WriteLine(" for a long time, so let's get right to it - pick your first Pokemon!\n");

            SelectPokemon();
        }

        /// <summary>
        /// Asks the player to select a name for himself. Maximum of 14 characters is enforced.
        /// </summary>
        /// <returns>The name that the player selected.</returns>
        static string PlayerName()
        {
            string name;
            bool validInput = false;

            do
            {
                UI.WriteLine("To begin with, choose your name. 14 characters maximum.");

                name = UI.ReceiveInput();

                int charCount = 0;

                foreach (char c in name)
                    charCount++; //Count the characters in the player's selection.           

                if (name == "")
                {
                    UI.WriteLine("Please select a valid name.\n");
                }

                else if (charCount > 14)
                {
                    UI.WriteLine("Name too long. Maximum name size is 14 characters.\n");
                }

                else
                {
                    Program.Log("The player named him/herself " + name + ". How imaginative.", 0);

                    validInput = true;                   
                }
            }

            while (!validInput);

            return name;
        }

        /// <summary>
        /// Asks the player to select a name for his rival. Maximum of 14 characters is enforced.
        /// </summary>
        /// <returns>The name that the player selected.</returns>
        static string RivalName()
        {
            string name;
            bool validInput = false;

            do
            {
                UI.WriteLine("What will your rival's name be? 14 characters maximum.");

                name = UI.ReceiveInput();

                int charCount = 0;

                foreach (char c in name)
                    charCount++; //Count the characters in the player's selection.           

                if (name == "")
                {
                    UI.WriteLine("Please select a valid name.\n");
                }

                else if (charCount > 14)
                {
                    UI.WriteLine("Name too long. Maximum name size is 14 characters.\n");
                }

                else
                {
                    Program.Log("The player named the rival " + name + ". He must be getting tired of these offensive names.", 0);

                    validInput = true;                    
                }
            }

            while (!validInput);

            return name;
        }

        /// <summary>
        /// This is the code for selecting the player's starting Pokemon.
        /// </summary>
        public static void SelectPokemon()
        {
            beginDate = DateTime.Now;

            PokemonGenerator generator = new PokemonGenerator();

            Pokemon playerPokemon; //The player's Pokemon.
            Pokemon rivalPokemon; //The rival's Pokemon.

            bool validInput = false;

            do
            {
                UI.WriteLine("Select the Pokemon you wish to start your adventure with!\n");

                UI.WriteLine("[B] - Bulbasaur, the Grass Pokemon. An all-around balanced Pokemon.");
                UI.WriteLine("[C] - Charmander, the Fire Pokemon. A relatively fragile attacker.");
                UI.WriteLine("[S] - Squirtle, the Water Pokemon. A defensively strong Pokemon.");

                string selection = UI.ReceiveKey();

                //The PokemonGenerator is invoked to create two Pokemon based on the player's input.
                //One for the player depending on his input, and a Pokemon that's strong vs the player's Pokemon for the rival.
                switch (selection)
                {
                    case "b":
                        playerPokemon = generator.Create("Bulbasaur", 5);
                        rivalPokemon = generator.Create("Charmander", 4);

                        validInput = true;

                        break;
                        
                    case "c":
                        playerPokemon = generator.Create("Charmander", 5);
                        rivalPokemon = generator.Create("Squirtle", 4);

                        validInput = true;

                        break;
                        
                    case "s":
                        playerPokemon = generator.Create("Squirtle", 5);
                        rivalPokemon = generator.Create("Bulbasaur", 4);

                        validInput = true;

                        break;

                    //Pikachu! He will serve as a little cheat of sorts for me to debug the game.
                    case "p":
                        playerPokemon = generator.Create("Pikachu", 25);
                        rivalPokemon = generator.Create("Eevee", 5);

                        validInput = true;

                        UI.WriteLine("Quite the cheater, aren't you...\n");

                        break;

                    default:
                        UI.InvalidInput();

                        playerPokemon = null;
                        rivalPokemon = null;

                        break;
                }
            }

            while (!validInput);

            Program.Log("The player selected " + playerPokemon.Name + " as his starting Pokemon.", 1);

            Game.Player.AddPokemon(playerPokemon, false);
            Game.StarterPokemon = playerPokemon.Name;

            rival.Party.Add(rivalPokemon);

            StartStory(playerPokemon, rivalPokemon);            
        }

        static void StartStory(Pokemon playerPokemon, Pokemon rivalPokemon)
        {
            Game.RivalName = rival.Name;
            Game.Location = "Pallet";
            Game.LastHealLocation = "Pallet";

            //A short introduction for the Pokemon.

            UI.WriteLine("\"So you selected " + playerPokemon.Name + ", the " + playerPokemon.species.PokedexSpecies + " Pokemon, Pokedex #" + 
                playerPokemon.species.PokedexNumber + "!\n" +
                         " His maximum HP is " + playerPokemon.MaxHP + ", and his starting move is " + playerPokemon.knownMoves.ElementAt(0).Name + ".\n" +
                         " An excellent choice - I hope you and " + playerPokemon.Name + " become great friends!\"\n");

            UI.WriteLine("Just as you pick up the Pokeball containing " + playerPokemon.Name + ", you hear someone\n" +
                         "calling your name from behind you. It's your rival, " + rival.Name + " - it seems\n" +
                         "that he just became a Pokemon trainer as well. Which Pokemon did he pick...?");

            UI.AnyKey();

            UI.WriteLine("\"So, " + playerPokemon.Name + ", huh? I figured you'd pick a chump Pokemon like that!\n" +
                         " Very well - let me show you why my " + rivalPokemon.Name + " is the superior Pokemon!\"\n");

            //The first battle with the rival then starts.

            Battle battle = new Battle(rival);

            UI.AnyKey();

            StoryContext();
        }

        /// <summary>
        /// This code is meant to run when the first battle with the rival ends, regardless of the outcome.
        /// It gives the player some context in regards to the adventure as well as general help.
        /// </summary>
        static void StoryContext()
        {
            UI.WriteLine("\"It looks like we both need practice. Or well, mostly you. I'm planning to");
            UI.WriteLine(" take on the Pokemon League, so I'm heading out to earn the 8 gym badges.");
            UI.WriteLine(" If you aspire to become half as good as me, I suggest you do the same!\"\n");

            UI.WriteLine("And just like that, " + rival.Name + " took off.");

            UI.AnyKey();

            UI.WriteLine("\"That kid might be a bit rude sometimes, but he really means you no ill.");
            UI.WriteLine(" Either way, you'd better chase after him - I know you've always dreamed");
            UI.WriteLine(" of collecting the gym badges and battling at the Pokemon League too. The");
            UI.WriteLine(" closest gym is the one in Pewter City to the north. Off you go then!\"\n");

            UI.WriteLine("The Professor is right. You leave the lab and head back home. You pack some");
            UI.WriteLine("basic things and start heading towards Route 1 to the north, but suddenly you");
            UI.WriteLine("hear a voice calling out to you from behind.\n");

            UI.WriteLine("\"" + Game.Player.Name + "!! Hold on!!\"");

            UI.AnyKey();

            UI.WriteLine("It's your mum, and she appears to be holding something.\n");

            UI.WriteLine("\"I almost forgot! I was supposed to give you these before you leave. They are ");
            UI.WriteLine(" called Pokeballs and they are used for catching Pokemon. I have heard that");
            UI.WriteLine("  it is easier to capture wild Pokemon if you weaken them a bit first, though.\"\n");

            // [FIX]
            //ItemList.pokeball.Add(5, AddType.Obtain);

            UI.WriteLine("You can use the Pokeballs by typing \"catch\" during a battle with a wild Pokemon");
            UI.WriteLine("in order to attempt to capture it.");

            UI.AnyKey();

            UI.WriteLine("\"Oh, and take these Potions too! You can use them to heal your injured Pokemon.");
            UI.WriteLine(" The poor critters will faint if they run out of vitality, and that's bad!\"\n");

            // [FIX]
            //ItemList.potion.Add(5, AddType.Obtain);

            UI.WriteLine("You can use Potions by typing \"item\", both during and outside of a battle.");

            UI.AnyKey();
           
            UI.WriteLine("\"Oh, and don't forget - you can also heal your Pokemon at Pokemon Centers.");
            UI.WriteLine(" It's free, but you can only find Pokemon Centers in towns and cities.\"\n");

            UI.WriteLine("In many locations, you can type \"center\" or \"heal\" to fully heal your Pokemon");
            UI.WriteLine("at the local Pokemon Center.");

            UI.AnyKey();

            UI.WriteLine("Your mother quietely sobs for a bit before finally speaking again.\n");

            UI.WriteLine("\"Good luck in your journey honey! And don't forget to change underpants daily!\"\n");

            UI.WriteLine("She waves you goodbye one last time before heading back home. You are finally");
            UI.WriteLine("ready to go out on your own Pokemon adventure with your new buddy, " + Game.StarterPokemon + "!");

            UI.AnyKey();

            Game.DefeatedTrainers = new List<int> { 1 };

            //The user's Pokemon is healed to full.
            Game.PartyHeal(false);

            Game.LastHealLocation = Game.Location;

            //The game finally moves on to the Overworld, where the player can navigate about in the world and perform additional actions.
            Overworld.ChangeLocation(LocationList.pallet);
        }
    }
}
