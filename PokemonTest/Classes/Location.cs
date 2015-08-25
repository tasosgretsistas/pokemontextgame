using System;
using System.Collections.Generic;

namespace PokemonTextEdition
{
    /// <summary>
    /// The type of location, such as town, city or forest.
    /// </summary>
    enum LocationType
    {
        City,
        Town,
        Route,
        Cave,
        Forest
    }

    /// <summary>
    /// The list of location tags currently in the game.
    /// </summary>
    enum LocationTag
    {

    }

    class Location
    {
        #region Declarations & Constructors

        //The location's primary attributes.
        //The tag is a helpful identification string for the location, used mostly for checking its connection to other locations.
        public string Name { get; set; }
        public LocationType Type { get; set; }
        public string Tag { get; set; }

        //The location's descriptive elements.
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string ConnectionsMessage { get; set; }
        public string HelpMessage { get; set; }

        //A list of tags representing this location's connected locations.
        public string North { get; set; }
        public string South { get; set; }
        public string West { get; set; }
        public string East { get; set; }

        //The items available at this location's mart.
        public List<Item> martStock = new List<Item>();

        public Location(string n, string t, string d)
        {
        }

        public Location()
        {
            Name = "Unnamed Location";
            Type = LocationType.Route;

        }

        #endregion

        public void PrintLocation()
        {
            Console.Write("You are now in {0}, {1}.\n\n{2}\n\n{3}\n", Name, Description, LongDescription, ConnectionsMessage);
        }

        public void Help()
        {
            Console.WriteLine(HelpMessage);
        }

        /*Since the following methods should produce unique results for each specific location, the ones listed here are the default "error" messages.
         *They are tagged as "virtual" so that they will be overriden by each specific location subsequently created, should that area need a different method.
         *For instance, if a trainer should try to use the command for encountering wild Pokemon in a city, the following method's "error" text will display.
         *However, should he try to do so in a route area, that area's specific encounter code will trigger, starting a fight with a wild Pokemon specific to that area.
         */

        #region Generic Location Functions

        public virtual void Encounter()
        {
            Console.WriteLine("There are no wild Pokemon in {0}.\n", Name);
        }

        public virtual void Trainer()
        {
            Console.WriteLine("There are no trainers to battle in {0}.\n", Name);
        }

        public virtual void GoNorth()
        {
            Console.WriteLine("There's nothing due north of {0}.\n", Name);
        }

        public virtual void GoSouth()
        {
            Console.WriteLine("There's nothing due south of {0}.\n", Name);
        }

        public virtual void GoWest()
        {
            Console.WriteLine("There's nothing due west of {0}.\n", Name);
        }

        public virtual void GoEast()
        {
            Console.WriteLine("There's nothing due east of {0}.\n", Name);
        }

        #endregion

        #region Special Facilities

        /// <summary>
        /// This method represents visiting a Pokemon Center within a town or city, which heals a player's Pokemon party.
        /// </summary>
        public virtual void Center()
        {
            //If the player is in a city, town, or a route with a Pokemon Center...
            if (Type == LocationType.City || Type == LocationType.Town || Tag == "route3e")
            {
                //All of his Pokemon are healed.

                foreach (Pokemon p in Overworld.player.party)
                {
                    p.CurrentHP = p.MaxHP;
                    p.Status = PokemonStatus.None;
                }
                Console.WriteLine("Your Pokemon are now fully healed!\n");

                //Then, the player's "lastHeal" tag is updated to the current location.
                Overworld.player.LastHealLocation = this.Tag;
            }

            //If he is not in a city, an error message is displayed.
            else
                Console.WriteLine("There is no Pokemon Center in {0}.\n", Name);
        }

        /// <summary>
        /// This method represents visiting a Pokemon Mart within a city, which allows the player to buy and sell items.
        /// </summary>
        public virtual void Mart()
        {
            //If the player is in a city, an instance of the Mart class is created with the item stock list provided by this object's mart stock list.
            if (Type == LocationType.City)            
                new Mart().Welcome(this.martStock);
            
            //If the player is not in a city, an error message is displayed.
            else            
                Console.WriteLine("There is no Pokemon Mart in {0}.\n", Name);
        }

        /// <summary>
        /// This method represents visiting a Pokemon Gym within a city, which allows the player to take on a Gym Leader.
        /// This method should be overriden as there is no default behaviour for visiting a gym and as such only produces an error message.
        /// </summary>
        public virtual void Gym()
        {
            Console.WriteLine("There is no Pokemon Gym in {0}.\n", Name);
        }

        #endregion
    }
}
