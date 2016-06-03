using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class Route2S : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        public Route2S()
            : base()
        {
            Name = "Route 2";
            Type = LocationType.Route;
            Tag = LocationTag.Route2South;

            North = LocationTag.ViridianForestSouth;
            South = LocationTag.ViridianCity;

            FlavorMessage = "the forest's entrance";

            Description = "This long trail eventually leads to the southern entrance of the Viridian\n" +
                          "Forest. Many trainers choose to train their Pokemon here before venturing\n" + 
                          "into the perilous forest for the first time.";

            ConnectionsMessage = "This route connects Viridian City from the south to the Viridian Forest to the\n" + 
                                 "north, all within walking distance.";    
                    
            HelpMessage = "\"north\" or \"go north\" - moves you to Viridian Forest.\n" +
                          "\"south\" or \"go south\" - moves you to Viridian City.\n" + 
                          "\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range of all Pokemon in this area.
            int level = Program.random.Next(3, 5); 

            Pokemon pokemon;            

            //40% probability of a Rattata.
            if (species < 41)
                pokemon = generator.Create("Rattata", level);

            //40% probability of a Pidgey.
            else if (species < 81)
                pokemon = generator.Create("Pidgey", level);
            
            //10% probability of a Weedle.
            else if (species < 91)
                pokemon = generator.Create("Weedle", level);

            //10% probability of a Caterpie.
            else
                pokemon = generator.Create("Caterpie", level);

            Battle battle = new Battle(pokemon);
        }

        public override void GoNorth()
        {
            UI.WriteLine("You choose to stay to the path and head straight for the Viridian Forest.\n");
        }

        public override void GoSouth()
        {
            UI.WriteLine("You avoid the tall grass and follow the road to Viridian City.\n");
        }
    }
}
