using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class ViridianCity : Location
    {
        public ViridianCity()
        {
            Name = "Viridian City";
            Type = "city";
            Tag = "viridian";

            South = "route1";
            North = "route2s";
            West = "indigo";

            Description = "the evergreen city";
            LongDescription = "This bustling city is most young trainers' first stop. There is a Pokemon\nCenter to heal your Pokemon, as well as a Mart where you can buy items.\nThe Viridian Gym is also located here, but it is currently closed.";
            Connections = "Route 1 to the south, Route 2 to the north and\nthe Indigo Plateau to the west";         
            HelpMessage = "\n\"north\" or \"go north\" - moves you to Route 2.\n\"south\" or \"go south\" - moves you to Route 1.\n\"center\" or \"heal\" - takes you to a Pokemon center to heal your Pokemon.\n\"mart\" - takes you to a Pokemon mart where you can buy and sell items.";

            martStock.Add(ItemsList.pokeball);
            martStock.Add(ItemsList.potion);            
            martStock.Add(ItemsList.antidote);
            martStock.Add(ItemsList.awakening);
        }

        public override void GoNorth()
        {
        }

        public override void GoSouth()
        {
        }

        public override void GoWest()
        {
            Console.WriteLine("This location is not currently in the game.");
            Overworld.LoadLocation("viridian");
        }
    }
}
