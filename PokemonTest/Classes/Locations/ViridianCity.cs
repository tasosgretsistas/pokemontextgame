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
            ConnectionsMessage = "Route 1 is just south off here, and Route 2 within a few minutes to the north.\nTo the west lies Route 22, the gateway to Indigo Plateau.";         
            HelpMessage = "\"north\" or \"go north\" - moves you to Route 2.\n\"south\" or \"go south\" - moves you to Route 1.\n\"center\" or \"heal\" - takes you to a Pokemon center to heal your Pokemon.\n\"mart\" - takes you to a Pokemon mart where you can buy and sell items.";

            martStock.Add(ItemList.pokeball);
            martStock.Add(ItemList.potion);            
            martStock.Add(ItemList.antidote);
            martStock.Add(ItemList.awakening);
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
