using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.NPCs;

namespace PokemonTextEdition
{
    class Story
    {
        //This class will hold any non-generic events with the game.
        //One of these events is the very beginning of the game, with the Introduction() method.

        //Declaration for the rival.
        static Rival1 rival = new Rival1();

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
            Console.WriteLine("");

            AnyKey();

            Console.WriteLine("\"Ah, there you are -- I've been waiting all morning for you. Could you remind");
            Console.WriteLine(" me what's your name?\"");
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
                Console.WriteLine("\"Oh yes, of course, {0}, how could I forget!", name);
                Console.WriteLine(" My grandson hasn't arrived yet either... He has been something of a rival to");
                Console.WriteLine(" you for the longest time, hasn't he? Say, what was his name again?\"");
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

                Program.Log("The player named the rival " + name + ". He must be pretty tired of these offensive names.", 0);

                Console.WriteLine(""); 
                Console.WriteLine("\"I was just testing you! I wouldn't really forget my own grandson's name. Ahem.");
                Console.WriteLine(" Well, no matter. I know you've been waiting for this for a long time, so let's");
                Console.WriteLine(" get right to it - pick your Pokemon!\"");
                Console.WriteLine("");

                SelectPokemon();
            }
        }


        public static void SelectPokemon()
        {
            //Code for selecting the player's starting Pokemon.

            Console.WriteLine("Select your Pokemon!");
            Console.WriteLine("Available choices: (B)ulbasaur (Grass), (C)harmander (Fire), (S)quirtle (Water)");

            Generator generator = new Generator();

            string selection = Console.ReadLine();

           

            //The Generator is invoked to create two Pokemon, based on the player's input.
            //One for the player depending on his input, and a Pokemon that's strong vs the user's for the rival.
            switch (selection)
            {
                case "Bulbasaur":
                case "bulbasaur":
                case "B":
                case "b":
                    Overworld.player.party.Add(generator.Create("Bulbasaur", 5));
                    rival.party.Add(generator.Create("Charmander", 4));
                    Program.Log("The player selected Bulbasaur. Right on!", 0);
                    break;

                case "Charmander":
                case "charmander":
                case "C":
                case "c":
                    Overworld.player.party.Add(generator.Create("Charmander", 5));
                    rival.party.Add(generator.Create("Squirtle", 4));
                    Program.Log("The player selected Squirtle.", 0);
                    break;

                case "Squirtle":
                case "squirtle":
                case "S":
                case "s":
                    Overworld.player.party.Add(generator.Create("Squirtle", 5));
                    rival.party.Add(generator.Create("Bulbasaur", 4));
                    Program.Log("The player selected Charmander. Pansy.", 0);
                    break;

                //Pikachu! He will serve as a little cheat of sorts for me to debug the game.
                case "Pikachu":
                case "pikachu":
                case "P":
                case "p":
                    Overworld.player.party.Add(generator.Create("Pikachu", 25));
                    rival.party.Add(generator.Create("Eevee", 5));
                    Console.WriteLine("\nQuite the cheater, aren't you...");
                    Program.Log("The player selected Pikachu. Hopefully I am the player and my secret's not out yet.", 0);
                    break;

                default:
                    Console.WriteLine("\nInvalid selection! Please try again.");
                    Console.WriteLine("");
                    SelectPokemon();
                    break;
            }

            Overworld.player.Starter = Overworld.player.party.ElementAt(0).name;

            //A short introduction for the Pokemon.
            //TODO: Add a PokeDex entry?
            Console.WriteLine("\n\"So you selected {0}, the {1} Pokemon, Pokedex #{2}!", Overworld.player.party.ElementAt(0).name, Overworld.player.party.ElementAt(0).pokedexSpecies, Overworld.player.party.ElementAt(0).pokedexNumber);
            Console.WriteLine(" His maximum HP is {0}, and his starting move is {1}.", Overworld.player.party.ElementAt(0).maxHP, Overworld.player.party.ElementAt(0).knownMoves.ElementAt(0).Name);
            Console.WriteLine(" An excellent choice - I hope you and {0} become great friends!\"", Overworld.player.party.ElementAt(0).name);
            Console.WriteLine("");           

            Console.WriteLine("Just as you pick up the Pokeball containing {0}, you hear someone", Overworld.player.party.ElementAt(0).name);
            Console.WriteLine("calling your name from behind you. It's your rival, {0} - it seems", rival.Name);
            Console.WriteLine("that he just became a Pokemon trainer as well. Which Pokemon did he pick...?");
            Console.WriteLine("");

            AnyKey();

            Console.WriteLine("\"So, {0}, huh? I figured you'd pick a chump Pokemon like that!", Overworld.player.party.ElementAt(0).name);
            Console.WriteLine(" Very well - let me show you why my {0} is the superior Pokemon!\"", rival.party.ElementAt(0).name);

            //The first battle with the rival then starts.

            new Battle().Start(rival, "trainer");

            AnyKey();

            PostRival();
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
            Console.WriteLine("");

            AnyKey();

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
            Console.WriteLine("");

            AnyKey();

            Console.WriteLine("It's your mum, and she appears to be holding something.");
            Console.WriteLine("");

            Console.WriteLine("\"I almost forgot! I was supposed to give you these before you leave. They are ");
            Console.WriteLine(" called Pokeballs, and they are used for catching Pokemon. Type \"catch\" during");
            Console.WriteLine(" a battle with a wild Pokemon to attempt to catch it. It is easier to capture");
            Console.WriteLine(" wild Pokemon if you weaken them a bit first, though.\"");
            Console.WriteLine("");

            ItemsList.pokeball.Add(5, "obtain");
            Console.WriteLine("");

            AnyKey();

            Console.WriteLine("\"Oh, and take these too. They are potions - you can use them during and outside");
            Console.WriteLine(" of combat to heal your injured Pokemon. Don't let the poor critters faint!\"");
            Console.WriteLine("");

            ItemsList.potion.Add(5, "obtain");
            Console.WriteLine("");

            AnyKey();

            Console.WriteLine("\"You can also heal your Pokemon at Pokemon Centers by using the \"heal\" command");
            Console.WriteLine(" in all cities. It's free, so please visit a Pokemon Center often in order to");
            Console.WriteLine(" keep your Pokemon healthy. Good luck on your journey, darling!\"");
            Console.WriteLine("");

            Console.WriteLine("She waves you goodbye one last time before heading back home. You are finally");
            Console.WriteLine("ready to head out on your own Pokemon adventure with your new buddy, {0}!", Overworld.player.Starter);

            AnyKeyLoadingArea();

            //The user's Pokemon is then healed and the game moves on to the Overworld, where the player can navigate and perform additional actions.
            Overworld.player.party.ElementAt(0).currentHP = Overworld.player.party.ElementAt(0).maxHP;
            Overworld.player.party.ElementAt(0).status = "";
            Overworld.LoadLocation("pallet");
        }

        public static void AnyKey()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine("");
        }

        public static void AnyKeyLoadingArea()
        {
            Console.WriteLine("");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(); 

        }
    }
}
