using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Properties;
using PokemonTextEdition.Locations;
using PokemonTextEdition.Classes;

namespace PokemonTextEdition
{
    class Overworld
    {
        //The overworld class, used to allow the player to move within the world.
        //For all intents and purposes, this is the main part of the game, where all other classes will return.
        //This is enforced by the fact that every option in the Options() screen redirects back to Options().

        //This is the main object of the player - most classes and methods will call this player.
        public static Player player = new Player();

        //An object that represents the current location.
        public static Location currentLocation = new Location();        

        //A list of every available location.
        static List<Location> Locations = new List<Location>() { new PalletTown(), new Route1(), new ViridianCity(), new Route2S(), new ViridianForestPart1(), 
        new ViridianForestPart2(), new ViridianForestPart3(), new Route2N(), new PewterCity(), new Route3W(), new Route3E() };

        public static void LoadLocation(string l)
        {
            //This method simply loads a location, prints its information and then moves on to the options menu.

            currentLocation = Locations.Find(location => location.Tag == l);
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

            switch (action)
            {
                case "go north":
                case "north":

                    currentLocation.GoNorth();

                    if (Locations.Exists(location => location.Tag == currentLocation.North))                    
                        LoadLocation(currentLocation.North);
                    
                    break;

                case "go south":
                case "south":

                    currentLocation.GoSouth();

                    if (Locations.Exists(location => location.Tag == currentLocation.South))                    
                        LoadLocation(currentLocation.South);
                    
                    break;

                case "go east":
                case "east":

                    currentLocation.GoEast();

                    if (Locations.Exists(location => location.Tag == currentLocation.East))
                        LoadLocation(currentLocation.East);

                    break;

                case "go west":
                case "west":

                    currentLocation.GoWest();

                    if (Locations.Exists(location => location.Tag == currentLocation.West))
                        LoadLocation(currentLocation.West);

                    break;
                    
                case "Fight":
                case "fight":
                case "f":

                    currentLocation.Encounter();

                    Options();

                    break;

                case "Battle":
                case "battle":
                case "b":

                    currentLocation.Trainer();

                    Options();

                    break;

                case "Center":
                case "center":
                case "Heal":
                case "heal":
                case "c":

                    currentLocation.Center();

                    Options();

                    break;

                case "Mart":
                case "mart":
                case "m":

                    currentLocation.Mart();

                    Options();

                    break;

                case "Gym":
                case "gym":
                case "g":

                    currentLocation.Gym();

                    Options();

                    break;

                case "Help":
                case "help":
                case "h":

                    currentLocation.Help();

                    Console.WriteLine("");
                    Console.WriteLine("\"(p)layer\" - displays your player information, such as name and money on hand.");
                    Console.WriteLine("\"(s)tatus\" - displays the current status for each Pokemon in your party.");
                    Console.WriteLine("\"s(w)itch\" - allows you to change the order of the Pokemon in your party.");
                    Console.WriteLine("\"(i)tems\" - displays the contents of your bag and allows you to use items.");
                    Console.WriteLine("\"s(a)ve\" - saves your progress in the game.");
                    Console.WriteLine("");

                    Options();

                    break;

                case "Player":
                case "player":
                case "p":

                    player.PlayerInfo();

                    Options();

                    break;

                case "Status":
                case "status":
                case "s":

                    player.PartyStatus();

                    Options();

                    break;

                case "Switch":
                case "switch":
                case "w":

                    player.SwitchAround();

                    Options();

                    break;

                case "Items":
                case "items":
                case "i":

                    player.ItemsMain();

                    Options();

                    break;

                case "Save":
                case "save":
                case "a":

                    Program.Save();

                    Options();

                    break;

                case "tellme":

                    TellMe();
                    
                    Options();

                    break;

                case "screwtherules":                   

                    ScrewTheRules();

                    Options();

                    break;

                default:
                    
                    Console.WriteLine("Invalid command.\n");

                    Options();

                    break;
            }
        }

        #region Cheats

        static void TellMe()
        {
            foreach (Pokemon p in player.party)
                p.PrintIVs();

            Console.WriteLine("");
        }

        static void ScrewTheRules()
        {
            Console.WriteLine("I have money!\n");

            foreach (Pokemon p in player.party)
            {
                p.HPIV = 31;
                p.AttackIV = 31;
                p.DefenseIV = 31;
                p.SpecialAttackIV = 31;
                p.SpecialDefenseIV = 31;
                p.SpeedIV = 31;
            }

            Overworld.player.PartyHeal();
        }

        #endregion
    }
}
