using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class Route2N : Location
    {
        Random rng = new Random();
        Generator generator = new Generator();

        public Route2N()
            : base()
        {
            Name = "Route 2";
            Type = "route";
            Tag = "route2n";

            North = "pewter";
            South = "forest3";

            Description = "the forest's entrance";
            LongDescription = "This is the northern side of Route 2. Viridian Forest's northern entrance\nis located here. Many trainers choose to train their Pokemon here to prepare\nfor their battle with the Gym Leader in Pewter City, Brock.";
            Connections = "Pewter City to the north and the Viridian Forest\nto the south";            
            HelpMessage = "\n\"north\" or \"go north\" - moves you to Pewter City.\n\"south\" or \"go south\" - moves you to the Viridian Forest.\n\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {   
            int level = rng.Next(3, 5);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 65)
            {
                battle.Wild(generator.Create("Rattata", level));
            }
            else if (species > 30)
            {
                battle.Wild(generator.Create("Pidgey", level));
            }
            else if (species > 15)
            {
                battle.Wild(generator.Create("Weedle", level));
            }
            else
            {
                battle.Wild(generator.Create("Caterpie", level));
            }

            return;
        }

        public override void GoNorth()
        {
            Console.WriteLine("\nYou choose to stay to the path and head straight for Pewter City.");
        }

        public override void GoSouth()
        {
            Console.WriteLine("\nYou avoid the tall grass and follow the road to the Viridian Forest.");
        }
    }
}
