using PokemonTextEdition.Engine;
using PokemonTextEdition.NPCs;
using System;
using System.Linq;

namespace PokemonTextEdition
{
    /// <summary>
    /// This class holds any non-generic events within the game.
    ///  One of these events is the very beginning of the game, invoked with the Introduction() method.
    /// </summary>
    class Story
    {
        //Declaration for the rival.
        static Rival1 rival = new Rival1();

        public static DateTime beginDate;

        public static void Introduction()
        {
            //The story's narration.
            Console.WriteLine("Welcome to the world of Pokemon! In this world, boys and girls can choose to");
            Console.WriteLine("become Pokemon trainers when they come of age. Pokemon trainers are people who");
            Console.WriteLine("train Pokemon in order to battle other Pokemon. It just so happens that today");
            Console.WriteLine("is the day you finally become a Pokemon trainer yourself!");
            Console.WriteLine("");

            Console.WriteLine("You wake up to the buzzing sound of your alarm clock. It's already 11 AM!");
            Console.WriteLine("Uh oh, you're gonna be late! You quickly put on the first set of clothes you");
            Console.WriteLine("find and head straight for Professor Oak's lab. The Professor is standing");
            Console.WriteLine("beside a table with three Pokeballs on it.");

            UI.AnyKey();

            Console.WriteLine("\"Ah, there you are -- I've been waiting all morning for you. Say, could you");
            Console.WriteLine(" remind me what your name was again?\"");
            Console.WriteLine("");

            NameSelection();
        }

        static void NameSelection()
        {
            Console.WriteLine("To begin with, choose your name. 14 characters maximum.");

            string name = Console.ReadLine();

            int charCount = 0; 

            foreach (char c in name)
                charCount++; //Count the characters in the player's selection.           

            if (name == "")
            {
                Console.WriteLine("Please select a valid name.");
                Console.WriteLine("");  

                NameSelection();
            }

            else if (charCount > 14)
            {
                Console.WriteLine("Name too long. Maximum name size is 14 characters.");
                Console.WriteLine("");

                NameSelection();                
            }

            else
            {
                Overworld.player.Name = name;

                Program.Log("The player named him/herself " + name + ". How imaginative.", 0);

                Console.WriteLine(""); 
                Console.WriteLine("\"Oh yes, of course, how could I forget! Which reminded me, my grandson has yet");
                Console.WriteLine(" to arrive. You remember him, right? You two used to be rivals for the longest");
                Console.WriteLine(" time when you were children. His name is...\"");
                Console.WriteLine("");

                RivalName();
            }
        }

        static void RivalName()
        {
            Console.WriteLine("What will your rival's name be? 14 characters maximum.");

            string name = Console.ReadLine();

            int charCount = 0;

            foreach (char c in name)
                charCount++; //Count the characters in the player's selection.           

            if (name == "")
            {
                Console.WriteLine("Please select a valid name.");
                Console.WriteLine("");

                RivalName();
            }

            else if (charCount > 14)
            {
                Console.WriteLine("Name too long. Maximum name size is 14 characters.");
                Console.WriteLine("");

                RivalName();
            }

            else
            {
                Overworld.player.RivalName = name;
                rival.Name = name;

                Program.Log("The player named the rival " + name + ". He must be getting tired of these offensive names.", 0);

                Console.WriteLine("");
                Console.WriteLine("\"He should be joining us shortly. I know you've been waiting for this moment");
                Console.WriteLine(" for a long time, so let's get right to it - pick your first Pokemon!");
                Console.WriteLine("");

                SelectPokemon();
            }
        }

        /// <summary>
        /// This is the code for selecting the player's starting Pokemon.
        /// </summary>
        public static void SelectPokemon()
        {
            beginDate = DateTime.Now;

            Console.WriteLine("Select the Pokemon you wish to start your adventure with!");
            Console.WriteLine("Available choices: (B)ulbasaur (Grass), (C)harmander (Fire), (S)quirtle (Water)");

            Generator generator = new Generator();

            Pokemon pokemon; //The player's Pokemon.
            Pokemon rivalPokemon; //The rival's Pokemon.

            string selection = Console.ReadLine();
            bool validInput = false;
         
            if (selection != "")
                Console.WriteLine("");

            //The Generator is invoked to create two Pokemon, based on the player's input.
            //One for the player depending on his input, and a Pokemon that's strong vs the player's Pokemon for the rival.
            switch (selection.ToLower())
            {
                case "bulbasaur":
                case "b":
                    pokemon = generator.Create("Bulbasaur", 5);
                    rivalPokemon = generator.Create("Charmander", 4);

                    validInput = true;

                    break;

                case "charmander":
                case "c":
                    pokemon = generator.Create("Charmander", 5);
                    rivalPokemon = generator.Create("Squirtle", 4);

                    validInput = true;

                    break;

                case "squirtle":
                case "s":
                    pokemon = generator.Create("Squirtle", 5);
                    rivalPokemon = generator.Create("Bulbasaur", 4);

                    validInput = true;

                    break;

                //Pikachu! He will serve as a little cheat of sorts for me to debug the game.
                case "pikachu":
                case "p":
                    pokemon = generator.Create("Pikachu", 25);
                    rivalPokemon = generator.Create("Eevee", 5);

                    validInput = true;

                    Console.WriteLine("Quite the cheater, aren't you...\n");

                    break;

                default:
                    Console.WriteLine("Invalid selection! Please try again.\n");

                    pokemon = new Pokemon();
                    rivalPokemon = new Pokemon();

                    break;
            }

            if (validInput)
            {
                Program.Log("The player selected " + pokemon.Name + ".", 1);

                Overworld.player.AddPokemon(pokemon, false);

                rival.party.Add(rivalPokemon);

                Overworld.player.StartingPokemon = pokemon.Name;
                Overworld.player.AddToCaught(pokemon.Name);

                //A short introduction for the Pokemon.

                Console.WriteLine("\"So you selected {0}, the {1} Pokemon, Pokedex #{2}!", pokemon.Name, pokemon.species.PokedexSpecies, pokemon.species.PokedexNumber);
                Console.WriteLine(" His maximum HP is {0}, and his starting move is {1}.", pokemon.MaxHP, pokemon.knownMoves.ElementAt(0).Name);
                Console.WriteLine(" An excellent choice - I hope you and {0} become great friends!\"", pokemon.Name);
                Console.WriteLine("");

                Console.WriteLine("Just as you pick up the Pokeball containing {0}, you hear someone", pokemon.Name);
                Console.WriteLine("calling your name from behind you. It's your rival, {0} - it seems", rival.Name);
                Console.WriteLine("that he just became a Pokemon trainer as well. Which Pokemon did he pick...?");

                UI.AnyKey();

                Console.WriteLine("\"So, {0}, huh? I figured you'd pick a chump Pokemon like that!", pokemon.Name);
                Console.WriteLine(" Very well - let me show you why my {0} is the superior Pokemon!\"", rivalPokemon.Name);
                Console.WriteLine("");

                //The first battle with the rival then starts.

                new Battle().Start(rival, "trainer");

                UI.AnyKey();

                PostRival();
            }

            else
                SelectPokemon();
        }

        static void PostRival()
        {
            //When the battle with the rival ends (regardless of outcome) this code will run.
            //It is supposed to give the player some context for the adventure.

            Console.WriteLine("\"It looks like we both need practice. Or well, mostly you. I'm planning to");
            Console.WriteLine(" take on the Pokemon League, so I'm heading out to earn the 8 gym badges.");
            Console.WriteLine(" If you aspire to become half as good as me, I suggest you do the same!\"");
            Console.WriteLine("");

            Console.WriteLine("And just like that, {0} took off.", rival.Name);

            UI.AnyKey();

            Console.WriteLine("\"That kid might be a bit rude sometimes, but he really means you no ill.");
            Console.WriteLine(" Either way, you'd better chase after him - I know you've always dreamed");
            Console.WriteLine(" of collecting the gym badges and battling at the Pokemon League too. The");
            Console.WriteLine(" closest gym is the one in Pewter City to the north. Off you go then!\"");
            Console.WriteLine("");

            Console.WriteLine("The Professor is right. You leave the lab and head back home. You pack some");
            Console.WriteLine("basic things and start heading towards Route 1 to the north, but suddenly you");
            Console.WriteLine("hear a voice calling out to you from behind.");
            Console.WriteLine("");           

            Console.WriteLine("\"{0}!! Hold on!!\"", Overworld.player.Name);

            UI.AnyKey();

            Console.WriteLine("It's your mum, and she appears to be holding something.");
            Console.WriteLine("");

            Console.WriteLine("\"I almost forgot! I was supposed to give you these before you leave. They are ");
            Console.WriteLine(" called Pokeballs and they are used for catching Pokemon. I have heard that");
            Console.WriteLine("  it is easier to capture wild Pokemon if you weaken them a bit first, though.\"");
            Console.WriteLine("");

            ItemList.pokeball.Add(5, "obtain");

            Console.WriteLine("");
            Console.WriteLine("You can use the Pokeballs by typing \"catch\" during a battle with a wild Pokemon");
            Console.WriteLine("in order to attempt to capture it.");

            UI.AnyKey();

            Console.WriteLine("\"Oh, and take these Potions too! You can use them to heal your injured Pokemon.");
            Console.WriteLine(" The poor critters will faint if they run out of vitality, and that's bad!\"");
            Console.WriteLine("");

            ItemList.potion.Add(5, "obtain");

            Console.WriteLine("");
            Console.WriteLine("You can use Potions by typing \"item\", both during and outside of a battle.");

            UI.AnyKey();
           
            Console.WriteLine("\"Oh, and don't forget - you can also heal your Pokemon at Pokemon Centers.");
            Console.WriteLine(" It's free, but you can only find Pokemon Centers in towns and cities.\"");

            Console.WriteLine("");
            Console.WriteLine("In many locations, you can type \"center\" or \"heal\" to fully heal your Pokemon");
            Console.WriteLine("at the local Pokemon Center.");

            UI.AnyKey();

            Console.WriteLine("Your mother sobs for a bit before finally speaking again.");
            Console.WriteLine("");

            Console.WriteLine("\"Good luck in your journey honey! And don't forget to change underpants daily!\"");
            Console.WriteLine("");

            Console.WriteLine("She waves you goodbye one last time before heading back home. You are finally");
            Console.WriteLine("ready to go out on your own Pokemon adventure with your new buddy, {0}!", Overworld.player.StartingPokemon);

            UI.AnyKey();

            //The user's Pokemon is then healed and the game moves on to the Overworld, where the player can navigate and perform additional actions.

            Overworld.player.PartyHeal();

            Overworld.LoadLocation("pallet");
        }
    }
}
