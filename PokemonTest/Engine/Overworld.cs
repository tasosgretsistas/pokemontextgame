using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Locations;
using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;

namespace PokemonTextEdition
{
    /// <summary>
    /// The overworld class, used to allow the player to move within the world.
    /// For all intents and purposes, this is the main part of the game, where all other classes will return.
    /// This is enforced by the fact that every option in the Options() screen redirects back to Options().
    /// </summary>
    class Overworld
    {       
        //This object represents the player globally - most classes and methods will call this player.
        public static Player player = new Player();

        //An object that represents the player's current location.
        public static Location currentLocation = new Location();        

        //A list of every available location.
        static List<Location> allLocations = new List<Location>() 
        { 
            new PalletTown(), 
            new Route1(), 
            new ViridianCity(), 
            new Route2S(), 
            new ViridianForestPart1(), 
            new ViridianForestPart2(), 
            new ViridianForestPart3(), 
            new Route2N(), 
            new PewterCity(), 
            new Route3W(), 
            new Route3E() 
        };

        public static void LoadLocation(string l)
        {
            //This method simply loads a location, prints its information and then moves on to the options menu.

            currentLocation = allLocations.Find(location => location.Tag == l);
            currentLocation.PrintLocation();

            Program.Log("The player moved to " + currentLocation.Name + " (" + currentLocation.Tag + ").", 1);

            Console.WriteLine("");
            Options();
        }

        public static void Options()
        {
            Console.WriteLine("What will you do?\n(Type \"(h)elp\" for a list of commands for your current location.)");

            string action = Console.ReadLine();

            if (action != "")
                Console.WriteLine("");

            switch (action.ToLower())
            {
                case "go north":
                case "north":
                    currentLocation.GoNorth();

                    if (allLocations.Exists(location => location.Tag == currentLocation.North))                    
                        LoadLocation(currentLocation.North);
                    
                    break;

                case "go south":
                case "south":
                    currentLocation.GoSouth();

                    if (allLocations.Exists(location => location.Tag == currentLocation.South))                    
                        LoadLocation(currentLocation.South);
                    
                    break;

                case "go east":
                case "east":
                    currentLocation.GoEast();

                    if (allLocations.Exists(location => location.Tag == currentLocation.East))
                        LoadLocation(currentLocation.East);

                    break;

                case "go west":
                case "west":
                    currentLocation.GoWest();

                    if (allLocations.Exists(location => location.Tag == currentLocation.West))
                        LoadLocation(currentLocation.West);

                    break;

                case "fight":
                case "f":
                    currentLocation.Encounter();

                    Options();

                    break;

                case "battle":
                case "b":
                    currentLocation.Trainer();

                    Options();

                    break;

                case "center":
                case "heal":
                case "c":
                    currentLocation.Center();

                    Options();

                    break;

                case "mart":
                case "m":
                    currentLocation.Mart();

                    Options();

                    break;

                case "gym":
                case "g":
                    currentLocation.Gym();

                    Options();

                    break;

                case "help":
                case "h":
                    ShowHelpMenu();

                    Options();

                    break;

                case "player":
                case "p":
                    Overworld.player.PlayerInfo();

                    Options();

                    break;

                case "status":
                case "s":
                    Overworld.player.PartyStatus();

                    Options();

                    break;

                case "switch":
                case "w":
                    player.SwitchAround();

                    Options();

                    break;

                case "items":
                case "i":
                    player.ItemsMain();

                    Options();

                    break;

                case "save":
                case "a":
                    Program.Save();

                    Options();

                    break;

                case "tellme":
                    Cheats.TellMe();
                    
                    Options();

                    break;

                case "screwtherules": 
                    Cheats.ScrewTheRules();
                    Options();
                    break;

                default:                    
                    Console.WriteLine("Invalid command.\n");

                    Options();

                    break;
            }
        }


        static void ShowHelpMenu()
        {
            currentLocation.Help();

            Console.WriteLine("");
            Console.WriteLine("\"(p)layer\" - displays your player information, such as name and money on hand.");
            Console.WriteLine("\"(s)tatus\" - displays the current status for each Pokemon in your party.");
            Console.WriteLine("\"s(w)itch\" - allows you to change the order of the Pokemon in your party.");
            Console.WriteLine("\"(i)tems\" - displays the contents of your bag and allows you to use items.");
            Console.WriteLine("\"s(a)ve\" - saves your progress in the game.");
            Console.WriteLine("");
        }
    
    }
}
