using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    public class Location
    {
        //The location's primary attributes.
        //The tag is a helpful identification string for the location, used mostly for checking its connection to other locations.
        public string Name { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }

        //The location's descriptive elements.
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Connections { get; set; }
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
        }

        public void PrintLocation()
        {
            Console.Write("You are now in {0}, {1}.\n\n{2}\n\nThis {3} is connected to {4}.\n", Name, Description, LongDescription, Type, Connections);
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

        public virtual void Encounter()
        {
            Console.WriteLine("There are no wild Pokemon in {0}.\n", Name);
        }

        public virtual void Trainer()
        {
            Console.WriteLine("There are no trainers in {0}.\n", Name);
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

        public virtual void Heal()
        {
            //Simple code for healing the player's party.
            //If the player is inside a city, his Pokemon will get healed,  and the "lastHeal" tag which specifies what city the player last healed in will update. 
            //If the player is not within a city, an error message will be displayed.
           
            if (Type == "city" || Type == "town")
            {
                foreach (Pokemon p in Overworld.player.party)
                {
                    p.currentHP = p.maxHP;
                    p.status = "";
                }
                Console.WriteLine("Your Pokemon are now fully healed!\n");
                Overworld.player.LastHealLocation = this.Tag;
            }

            else
            {
                Console.WriteLine("There is no Pokemon Center in {0}.\n", Name);
            }
        }

        public virtual void Shop()
        {
            if (Type == "city")
            {
                new Mart().Welcome(martStock);
            }

            else
            {
                Console.WriteLine("There is no Pokemon Mart in {0}\n.", Name);
            }
        }

        public virtual void Gym()
        {
            Console.WriteLine("There is no Pokemon Gym in {0}.\n", Name);
        }
    }
}
