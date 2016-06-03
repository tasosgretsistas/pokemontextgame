using System.Collections.Generic;
using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// The type of location, such as town, city or forest.
    /// </summary>
    enum LocationType
    {
        None,
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
        None,
        Pallet,
        Route1,
        ViridianCity,
        Route2South,
        ViridianForestSouth,
        ViridianForestCenter,
        ViridianForestNorth,
        Route2North,
        PewterCity,
        Route3West,
        Route3East,
        MtMoonWest,
        MtMoonCenter,
        MtMoonSecret,
        MtMoonEast
    }

    /// <summary>
    /// This class represents the various locations and areas within the game. By default, the class represents a generic empty location, 
    /// and should be inherited by each specific location, with its corresponding scripted events. 
    /// </summary>
    class Location
    {
        #region Fields

        //The location's nomimal attributes.

        /// <summary>
        /// The location's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location's type, i.e. city, town, route, cave, forest, etc.
        /// </summary>
        public LocationType Type { get; set; }

        /// <summary>
        /// A useful identifier which is used for saving & loading the game as well as checking the location's connections to other locations.
        /// </summary>
        public LocationTag Tag { get; set; }

        //The location's descriptive elements.

        /// <summary>
        /// A brief flavour message describing the town - i.e. "the town of beginnings". 
        /// </summary>
        public string FlavorMessage { get; set; }

        /// <summary>
        /// A description of the location, which is presented to the player. This should be as helpful to the player as possible.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A message listing which other locations this location is connected to.
        /// </summary>
        public string ConnectionsMessage { get; set; }

        /// <summary>
        /// A message listing the commands available in this specific location.
        /// </summary>
        public string HelpMessage { get; set; }

        //Connected Locations
        
        /// <summary>
        /// The location to the north of this location. Set to null if not applicable.
        /// </summary>
        public LocationTag North { get; set; }

        /// <summary>
        /// The location to the south of this location. Set to null if not applicable.
        /// </summary>
        public LocationTag South { get; set; }

        /// <summary>
        /// The location to the west of this location. Set to null if not applicable..
        /// </summary>
        public LocationTag West { get; set; }

        /// <summary>
        /// The location to the east of this location. Set to null if not applicable.
        /// </summary>
        public LocationTag East { get; set; }

        /// <summary>
        /// The list of items available for purchase at this location's mart.
        /// </summary>
        public List<Item> MartStock { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for blank locations. Creates a location named "Undefined Location" and sets every other attribute set to 0 / empty string.
        /// </summary>
        public Location()
        {
            Name = "Undefined Location";
            Type = LocationType.None;
            Tag = LocationTag.None;

            FlavorMessage = string.Empty;
            Description = string.Empty;
            ConnectionsMessage = string.Empty;
            HelpMessage = string.Empty;

            North = LocationTag.None;
            South = LocationTag.None;
            West = LocationTag.None;
            East = LocationTag.None;

            MartStock = new List<Item> { };
        }

        /// <summary>
        /// Constructor for creating generic locations. This should not be used often as nearly every area has to be unique with its own scripts.
        /// </summary>
        /// <param name="lName">The location's name.</param>
        /// <param name="lType">The location's type, as LocationType.</param>
        /// <param name="lTag">The location's tag, as LocationTag. This is a helpful identifier that is used for checking its connection to other locations.</param>
        /// <param name="lFlavorMessage">A brief flavour message describing the town - i.e. "the town of beginnings". </param>
        /// <param name="lDescription">A description of the location. This should be as helpful to the player as possible.</param>
        /// <param name="lConnectionsMessage">A message detailing which zones the location is connected to.</param>
        /// <param name="lHelpMessage">A message detailing the extra commands available for this location.</param>
        /// <param name="lNorth">The tag of the location to the north of this location.</param>
        /// <param name="lSouth">The tag of the location to the south of this location.</param>
        /// <param name="lWest">The tag of the location to the west of this location.</param>
        /// <param name="lEast">The tag of the location to the east of this location.</param>
        /// <param name="lMartStock">A list of items representing the item stock of this location's Pokemon Mart, where the player can buy items. Should be null if the area does not have a mart.</param>
        public Location(string lName, LocationType lType, LocationTag lTag, string lFlavorMessage, string lDescription, string lConnectionsMessage, string lHelpMessage,
                        LocationTag lNorth, LocationTag lSouth, LocationTag lWest, LocationTag lEast, List<Item> lMartStock)
        {
            Name = lName;
            Type = lType;
            Tag = lTag;

            FlavorMessage = lFlavorMessage;
            Description = lDescription;
            ConnectionsMessage = lConnectionsMessage;

            North = lNorth;
            South = lSouth;
            West = lWest;
            East = lEast;

            MartStock = lMartStock;
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Displays all of the location's information in its entirety.
        /// </summary>
        /// <returns>A formatted string listing the location's information.
        /// Example: "Pallet Town, the White City of Beginnings. [Description] [Connections]"</returns>
        public string PrintInfo()
        {
             return "You are now in " + Name + ", " + FlavorMessage + 
                    ".\n\n" + Description + "\n\n" + ConnectionsMessage + "\n";
        }

        #endregion

        #region Generic Location Functions

        /*Since the following methods should produce unique results for each specific location, the ones listed here are the default "error" messages.
         *They are tagged as "virtual" so that they will be overriden by each specific location subsequently created, should that area need a different method.
         *For instance, if a trainer should try to use the command for encountering wild Pokemon in a city, the following method's "error" text will display.
         *However, should he try to do so in a route area, that area's specific encounter code will trigger, starting a fight with a wild Pokemon specific to that area.
         */

        public virtual void Encounter()
        {
            UI.WriteLine("There are no wild Pokemon in " + Name + ".\n");
        }

        public virtual void Trainer()
        {
            UI.WriteLine("There are no trainers to battle in " + Name + ".\n");
        }

        public virtual void GoNorth()
        {
            UI.WriteLine("There's nothing due north of " + Name + ".\n");
        }

        public virtual void GoSouth()
        {
            UI.WriteLine("There's nothing due south of " + Name + ".\n");
        }

        public virtual void GoWest()
        {
            UI.WriteLine("There's nothing due west of " + Name + ".\n");
        }

        public virtual void GoEast()
        {
            UI.WriteLine("There's nothing due east of " + Name + ".\n");
        }

        #endregion

        #region Special Facilities

        /// <summary>
        /// This method represents visiting a Pokemon Center within a town or city, which heals a player's Pokemon party.
        /// </summary>
        public virtual void Center()
        {
            //If the player is in a city, town, or a route with a Pokemon Center...
            if (Type == LocationType.City || Type == LocationType.Town || Tag == LocationTag.Route3East)
            {
                //All of his Pokemon are healed.
                Game.PartyHeal(true);

                //Then, the player's "lastHeal" tag is updated to the current location.
                Game.LastHealLocation = Tag.ToString();
            }

            //If he is not in a city, an error message is displayed.
            else
                UI.WriteLine("There is no Pokemon Center in " + Name + ".\n");
        }

        /// <summary>
        /// This method represents visiting a Pokemon Mart within a city, which allows the player to buy and sell items.
        /// </summary>
        public virtual void Mart()
        {
            //If the player is in a city, an instance of the Mart class is created with the item stock list provided by this object's mart stock list.
            if (Type == LocationType.City)            
                new Mart(MartStock).Welcome();
            
            //If the player is not in a city, an error message is displayed.
            else            
                UI.WriteLine("There is no Pokemon Mart in " + Name + ".\n");
        }

        /// <summary>
        /// This method represents visiting a Pokemon Gym within a city, which allows the player to take on a Gym Leader.
        /// This method should be overriden as there is no default behaviour for visiting a gym and as such only produces an error message.
        /// </summary>
        public virtual void Gym()
        {
            UI.WriteLine("There is no Pokemon Gym in " + Name + ".\n");
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Checks if this location is the same as another.
        /// </summary>
        /// <param name="obj">Returns true if the tag of both locations is the same.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Location l = obj as Location;

            if ((object)l == null)
                return false;

            return (Tag == l.Tag);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
