﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Properties;

namespace PokemonTextEdition
{
    public class Program
    {
        //This parameter determines how severe a message needs to be in order to get logged.
        //0 = trivial, 1 = important, 2 = vital.
        private const int logLevel = 0;

        private const string saveGame = "savegame.sav"; //The name of the save game file to be used.
        
        //The font colour for each kind of message. (NYI)
        public static ConsoleColor quotesColor = ConsoleColor.Green;
        public static ConsoleColor battleColor = ConsoleColor.Red;

        static void Main(string[] args)
        {

            //Introduction code.
            //TODO: Keep this updated!
            Console.WriteLine("Welcome to Pokemon Red/Blue: Text Edition, by Tasos Gretsistas! v0.2 [BETA]");
            Console.WriteLine("All 151 Pokemon are now in the game! Hurray!");
            Console.WriteLine("Pokemon can now evolve and you can use items! :-)");

            Console.WriteLine("\nA letter in parentheses represents a command shortcut. For instance, (f)ight");
            Console.WriteLine("means that you only need to type \"f\" to input this particular command.");

            Console.WriteLine("\nType \"(l)oad\" to load game, \"(s)kip\" to skip the intro or press enter to begin.");

            string command = Console.ReadLine();

            switch (command)
            {
                case "skip": case "S": case "s":

                    Console.WriteLine("");
                    Console.WriteLine("By default the player will be named \"{0}\" and the rival \"{1}\".", Overworld.player.Name, Overworld.player.RivalName);
                    Console.WriteLine("");

                    Story.SelectPokemon();

                    break;

                case "load": case "L": case "l":

                    Load();

                    break;

                default:

                    Story.Introduction();

                    break;
            }
        }

        public static void Save()
        {
            //Code for saving the game.

            //First, the game tries to open a file with the saveGame name.
            //If it succeeds, all is well, and the game continues as normal.
            try
            {
                Stream stream = File.Open(saveGame, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();

                Console.Write("\nSaving game... ");

                Overworld.player.Location = Overworld.currentLocation.Tag;
                formatter.Serialize(stream, Overworld.player);

                Log("The game saved successfully.", 1);

                Console.WriteLine("Saved!");

                stream.Close();
            }

            //If the operation is unsuccesful, a relevant message is displayed.
            //Then, program flow resumes as usual.
            catch (Exception ex)
            {
                Log("Failed to save the game. Error: " + ex.Message, 2);

                Console.WriteLine("\nThere was a problem trying to save the game.");
                Console.WriteLine("Error: " + ex.Message);

                Console.WriteLine("\nPlease contact the author about this issue.");

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        public static void Load()
        {
            //Code for loading the game.

            //First, the program checks whether a correctly named save game file exists.
            if (File.Exists(saveGame))
            {
                //If it does exist, it then tries to open the file and load the player's information.
                //If this operation is succesful, the player is taken to the corresponding Overworld menu for his saved location.
                try
                {
                    Stream stream = File.Open(saveGame, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();

                    Console.WriteLine("Loading game...");

                    Overworld.player = (Player)formatter.Deserialize(stream);

                    Log("The game loaded successfully.", 1);

                    stream.Close();

                    Overworld.LoadLocation(Overworld.player.Location);
                }

                //If the operation is unsuccesful, a relevant message is displayed.
                //Then, the game starts from the beginning.
                catch (Exception ex)
                {
                    Log("Failed to load the game. Error: " + ex.Message, 2);

                    Console.WriteLine("\nThere was a problem trying to load the game.");
                    Console.WriteLine("Error: " + ex.Message);

                    Console.WriteLine("\nPlease contact the author about this issue.");

                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();

                    Environment.Exit(0);
                }
            }

            //If no correctly named save game file exists, an error is displayed and the game starts from the beginning.
            else
            {
                Console.WriteLine("No save file could be found!");
                Console.WriteLine("Please verify that your save game file is correctly named \"" + saveGame + "\".");
                Story.Introduction();
            }
        }
        /// <summary>
        /// Logging method that writes a given message into a file called "log.txt".
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

    }
}

