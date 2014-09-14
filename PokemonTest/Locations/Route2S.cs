using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class Route2S : Location
    {
        Random rng = new Random();
        Generator generator = new Generator();

        public Route2S()
            : base()
        {
            Name = "Route 2";
            Type = "route";
            Tag = "route2s";

            North = "forest1";
            South = "viridian";

            Description = "the forest's entrance";
            LongDescription = "This long trail eventually leads to the southern entrance of the Viridian\nForest. Many trainers choose to train their Pokemon here before venturing\ninto the forest for the first time.";
            Connections = "Viridian City to the south and the Viridian\nForest to the north";            
            HelpMessage = "\n\"north\" or \"go north\" - moves you to Viridian Forest.\n\"south\" or \"go south\" - moves you to Viridian City.\n\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {   
            int level = rng.Next(3, 5);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 60)
            {
                battle.Wild(generator.Create("Rattata", level));
            }
            else if (species > 20)
            {
                battle.Wild(generator.Create("Pidgey", level));
            }
            else if (species > 10)
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
            Console.WriteLine("\nYou choose to stay to the path and head straight for the Viridian Forest.");
        }

        public override void GoSouth()
        {
            Console.WriteLine("\nYou avoid the tall grass and follow the road to Viridian City.");
        }
    }
}
