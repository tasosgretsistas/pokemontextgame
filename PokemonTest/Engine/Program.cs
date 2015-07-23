using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PokemonTextEdition
{
    public class Program
    {
        #region Constants & Variables

        //Current game version.
        public const string version = "v0.2 [BETA]";

        //The platform the program is running on.
        public const string platform = "console";

        //This parameter determines how important a message needs to be in order to get logged.
        //0 = trivial, 1 = important, 2 = vital.
        private static int logLevel = 1;

        //The name of the save game file to be used.
        private const string saveGame = "savegame.sav";

        //Developer mode.
        public static bool GodMode { get; set; }

        //The font colour for each kind of message. (If I ever decide to implement this)
        public static ConsoleColor eventColor = ConsoleColor.Yellow;

        #endregion

        #region Main Menu

        static void Main(string[] args)
        {
            //Introduction code. Keep this updated!

            Console.WriteLine("Welcome to Pokemon Red/Blue: Text Edition, by Tasos Gretsistas! " + version);
            Console.WriteLine("All 151 Pokemon are now in the game! Hurray!");
            Console.WriteLine("Pokemon can now evolve and you can use items! :-)");
            Console.WriteLine("");

            Console.WriteLine("A letter in parentheses represents a command shortcut. For instance, (f)ight");
            Console.WriteLine("means that you only need to type \"f\" to input this particular command.");
            Console.WriteLine("");

            MainMenu();
        }

        /// <summary>
        /// The game's main menu.
        /// </summary>
        static void MainMenu()
        {
            Console.WriteLine("Type \"(l)oad\" to load game, \"(s)kip\" to skip the intro or press enter to begin.");

            string input = Console.ReadLine();

            if (input != "")
                Console.WriteLine();

            switch (input.ToLower())
            {
                case "skip":
                case "s":
                    SkipIntro();

                    break;

                case "load":
                case "l":
                    Load();

                    break;

                case "testbattle":
                    Cheats.TestBattle();

                    break;

                case "list items":
                    ItemList.ListAllItems();
                    MainMenu();
                    break;

                case "list pokemon":
                    PokemonList.ListAllPokemon();
                    MainMenu();
                    break;

                case "list moves":
                    MovesList.ListAllMoves();
                    MainMenu();
                    break;

                default:
                    Story.Introduction();

                    break;
            }
        }

        /// <summary>
        /// Skips the story's introduction segment and automatically instantiates the Overworld.player and Overworld.rival objects with their default names.
        /// </summary>
        static void SkipIntro()
        {
            Console.WriteLine("By default the player will be named \"{0}\" and the rival \"{1}\".\n", Overworld.player.Name, Overworld.player.RivalName);

            Story.SelectPokemon();
        }

        #endregion

        #region Game Functionality

        /// <summary>
        /// Logging method that writes a given message with a given importance into a file called "log.txt".
        /// Messages with a lower importance than the threshold set at Program.cs will not be logged.
        /// </summary>
        /// <param name="message">The string of the actual message to be written.</param>
        /// <param name="messageLevel">The importance of the message. 0 = trivial, 1 = important, 2 = vital.</param>
        public static void Log(string message, int messageLevel)
        {
            //Simple log method that writes things into "log.txt".

            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                //If the message's level of importance is higher than the current logLevel, it gets written. Otherwise, it gets ignored.

                if (messageLevel >= logLevel)
                    writer.WriteLine("{0} - {1}", DateTime.Now, message);
            }
        }

        /// <summary>
        /// This code handles saving the game. It is wrapped in a try-catch statement to facilitate for the amount of things that can go wrong during the read/write operation.
        /// </summary>
        public static void Save()
        {
            try
            {
                bool operation = true;

                //If a save file already exists, the user is asked whether he wants to overwrite this save file before the operation goes on.
                if (File.Exists(saveGame))
                {
                    operation = false;

                    Console.WriteLine("A save file already exists:\n");

                    //This quickly loads the save file so that it can show the player the save file's data.
                    using (Stream stream = File.Open(saveGame, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        SaveState savestate = (SaveState)formatter.Deserialize(stream);

                        stream.Close();

                        Console.WriteLine(savestate.SaveInfo(version));
                    }

                    Console.WriteLine("Do you want to overwrite it?\nType (y)es to overwrite or press Enter to cancel");

                    string confirmation = Console.ReadLine();

                    if (confirmation != "")
                        Console.WriteLine("");

                    switch (confirmation)
                    {
                        case "yes":
                        case "y":

                            operation = true;

                            break;
                    }
                }

                //If there was no save file, or if the player chose to overwrite it, the operation continues.
                if (operation)
                {
                    Console.Write("Saving game... ");

                    //First, the game tries to open a stream for the save file. If it succeeds, all is well, and the operation continues as normal.    

                    using (Stream stream = File.Open(saveGame, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        Overworld.player.Location = Overworld.currentLocation.Tag;

                        //The player's information is compressed into the save file...
                        SaveState save = new SaveState(version, Story.beginDate, DateTime.Now,
                                                        Overworld.player.Name, Overworld.player.RivalName, Overworld.player.StartingPokemon,
                                                        Overworld.player.Location, Overworld.player.LastHealLocation,
                                                        null, Overworld.player.Money, Overworld.player.items,
                                                        Overworld.player.badgeList, Overworld.player.defeatedTrainers,
                                                        Overworld.player.seenPokemon, Overworld.player.caughtPokemon,
                                                        Overworld.player.party, Overworld.player.box);

                        //... which is then serialized into the "saveGame" file, and the operation terminates with program flow resuming as usual.
                        formatter.Serialize(stream, save);

                        stream.Close();

                        Log("The game saved successfully.", 1);

                        Console.WriteLine("Saved!\n");
                    }
                }
            }

            //If the operation is unsuccesful, a relevant message is displayed. Then, program flow resumes as usual.
            catch (Exception ex)
            {
                Log("Failed to save the game. Error: " + ex.Message, 2);

                Console.WriteLine("\n\nThere was a problem with saving the game.");
                Console.WriteLine("Error: " + ex.Message);

                Console.WriteLine("\nPlease contact the author about this issue.\n");

                Console.WriteLine("The game will now return to what was happening.");
                Text.AnyKey();
            }
        }

        /// <summary>
        /// This code handles loading the game. It is wrapped in a try-catch statement to facilitate for the amount of things that can go wrong during the read/write operation.
        /// </summary>
        public static void Load()
        {
            //First, the program checks whether a correctly named save game file exists.
            try
            {
                //If it does exist, it then tries to open the file and load the player's information.
                if (File.Exists(saveGame))
                {
                    using (Stream stream = File.Open(saveGame, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        SaveState savestate = (SaveState)formatter.Deserialize(stream);

                        stream.Close();

                        Console.WriteLine(savestate.SaveInfo(version));

                        Console.WriteLine("Would you like to load this game?");

                        string confirmation = Console.ReadLine();

                        if (confirmation != "")
                            Console.WriteLine("");

                        switch (confirmation)
                        {
                            case "yes":
                            case "y":

                                UnpackSaveState(savestate);

                                break;

                            default:

                                MainMenu();

                                break;
                        }
                    }
                }

                //If no correctly named save game file exists, an error is displayed and the game starts from the beginning.
                else
                {
                    Console.WriteLine("No save file could be found!");
                    Console.WriteLine("Please verify that your save game file is correctly named \"" + saveGame + "\".\n");

                    Program.MainMenu();
                }
            }

            //If the operation is unsuccesful, a relevant message is displayed and the game goes back to the main menu.
            catch (Exception ex)
            {
                Log("Failed to load the game. Error: " + ex.Message, 2);

                Console.WriteLine("\n\nThere was a problem with loading the game.");
                Console.WriteLine("Error: " + ex.Message);

                Console.WriteLine("\nPlease contact the author about this issue.");

                Console.WriteLine("\nReturning to the main menu.");
                Text.AnyKey();

                Program.MainMenu();
            }
        }

        /// <summary>
        /// Unpacks the information from a save state object to the game by converting the information from the save state and its assorted CompactPokemon and CompactItem objects to Pokemon and Items respectively.
        /// </summary>
        /// <param name="save">The save state object to unpack.</param>
        static void UnpackSaveState(SaveState save)
        {
            Generator generator = new Generator();

            Console.Write("Loading the player's info... ");

            //Loading the player's information.
            Overworld.player.Name = save.PlayerName;
            Overworld.player.RivalName = save.RivalName;
            Overworld.player.StartingPokemon = save.StartingPokemon;

            Overworld.player.Location = save.CurrentLocation;
            Overworld.player.LastHealLocation = save.LastHealLocation;

            Overworld.player.Money = save.Money;

            foreach (string badge in save.Badges)
                if (!Overworld.player.badgeList.Contains(badge))
                    Overworld.player.badgeList.Add(badge);

            foreach (string trainer in save.DefeatedTrainers)
                if (!Overworld.player.defeatedTrainers.Contains(trainer))
                    Overworld.player.defeatedTrainers.Add(trainer);

            foreach (string pokemon in save.SeenPokemon)
                if (!Overworld.player.caughtPokemon.Contains(pokemon))
                    Overworld.player.caughtPokemon.Add(pokemon);

            foreach (string pokemon in save.SeenPokemon)
                if (!Overworld.player.caughtPokemon.Contains(pokemon))
                    Overworld.player.caughtPokemon.Add(pokemon);

            Console.WriteLine("Loaded!");

            Console.Write("Loading items... ");

            //Loading the player's items.
            foreach (CompactItem item in save.Items)
                if (!Overworld.player.items.Exists(i => i.Name == item.Name))
                    ItemList.allItems.Find(i => i.Name == item.Name).Add(item.Count, "");

            Console.WriteLine("Loaded!");

            Console.Write("Loading Pokemon... ");

            //Loading the player's Pokemon.
            foreach (CompactPokemon pokemon in save.PartyPokemon)
            {
                Overworld.player.party.Add(new Pokemon(pokemon.PokedexNumber, pokemon.Nickname, pokemon.Level, pokemon.Experience, pokemon.IndividualValues, pokemon.CurrentHP, pokemon.Status, pokemon.Moves));
            }

            foreach (CompactPokemon pokemon in save.BoxPokemon)
                Overworld.player.box.Add(new Pokemon(pokemon.PokedexNumber, pokemon.Nickname, pokemon.Level, pokemon.Experience, pokemon.IndividualValues, pokemon.CurrentHP, pokemon.Status, pokemon.Moves));

            Console.WriteLine("Loaded!");

            Log("The game loaded successfully.", 1);

            Text.AnyKey();

            //The game finally loads the player's location.
            Overworld.LoadLocation(Overworld.player.Location);
        }



        #endregion

    }
}

