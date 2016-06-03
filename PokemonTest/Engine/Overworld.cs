using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;

namespace PokemonTextEdition.Engine
{
    /// <summary>
    /// This class allows the player to move and act within the world.
    /// <para>For all intents and purposes, this is the main part of the game when the player is not battling, where all other classes will return.
    /// This is enforced by the fact that every option in the <see cref="Options()"/> screen redirects back to itself.</para>
    /// </summary>
    class Overworld
    {
        //An object that represents the player's current location.
        public static Location CurrentLocation; 

        public static void LoadLocation(LocationTag location)
        {
            Location tempLocation = LocationList.AllLocations.Find(l => l.Tag == location);

            ChangeLocation(tempLocation);
        }

        public static void LoadLocationString(string location)
        {
            Location tempLocation = LocationList.AllLocations.Find(l => l.Tag.ToString() == location);

            ChangeLocation(tempLocation);
        }

        /// <summary>
        /// Changes the current location, effectively moving the player to a different location within the game.
        /// </summary>
        /// <param name="location">The tag of the location to load.</param>
        public static void ChangeLocation(Location location)
        { 
            if (location != null)
            {
                CurrentLocation = location;

                UI.WriteLine(CurrentLocation.PrintInfo());

                Program.Log("The player moved to " + CurrentLocation.Name + " (" + CurrentLocation.Tag + ").", 1);
            }

            Options();
        }

        public static void Options()
        {
            UI.WriteLine("What will you do?\n(Type \"(h)elp\" for a list of commands for your current location.)");

            string action = UI.ReceiveInput();

            switch (action.ToLower())
            {
                case "go north":
                case "north":
                    CurrentLocation.GoNorth();

                    LoadLocation(CurrentLocation.North);
                    
                    break;

                case "go south":
                case "south":
                    CurrentLocation.GoSouth();

                    LoadLocation(CurrentLocation.South);
                    
                    break;

                case "go east":
                case "east":
                    CurrentLocation.GoEast();

                    LoadLocation(CurrentLocation.East);

                    break;

                case "go west":
                case "west":
                    CurrentLocation.GoWest();

                    LoadLocation(CurrentLocation.West);

                    break;

                case "fight":
                case "f":
                    CurrentLocation.Encounter();

                    Options();

                    break;

                case "battle":
                case "b":
                    CurrentLocation.Trainer();

                    Options();

                    break;

                case "center":
                case "heal":
                case "c":
                    CurrentLocation.Center();

                    Options();

                    break;

                case "mart":
                case "m":
                    CurrentLocation.Mart();

                    Options();

                    break;

                case "gym":
                case "g":
                    CurrentLocation.Gym();

                    Options();

                    break;

                case "help":
                case "h":
                    ShowHelpMenu();

                    Options();

                    break;

                case "player":
                case "p":
                    ShowPlayerInfo();

                    Options();

                    break;

                case "status":
                case "s":
                    ShowPartyInfo();

                    Options();

                    break;

                case "switch":
                case "w":
                    Game.Player.SwitchAround();

                    Options();

                    break;

                case "items":
                case "i":
                    Game.Player.ItemsMain();

                    Options();

                    break;

                case "save":
                case "a":
                    SaveLoad.Save();

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
                    UI.InvalidInput();

                    Options();

                    break;
            }
        }


        static void ShowHelpMenu()
        {
            UI.WriteLine(CurrentLocation.HelpMessage);

            UI.WriteLine("");
            UI.WriteLine("\"(p)layer\" - displays your player information, such as name and money on hand.");
            UI.WriteLine("\"(s)tatus\" - displays the current status for each Pokemon in your party.");
            UI.WriteLine("\"s(w)itch\" - allows you to change the order of the Pokemon in your party.");
            UI.WriteLine("\"(i)tems\" - displays the contents of your bag and allows you to use items.");
            UI.WriteLine("\"s(a)ve\" - saves your progress in the game.");
            UI.WriteLine("");
        }

        static void ShowPlayerInfo()
        {
            UI.WriteLine("Your information:\n" + Game.Player.PrintInfo());            
        }

        static void ShowPartyInfo()
        {
            UI.WriteLine("Your party's status:\n" + Game.Player.PrintPartyStatus());
        }
    
    }
}
