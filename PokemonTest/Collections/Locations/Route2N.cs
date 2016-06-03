using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class Route2N : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        public Route2N()
            : base()
        {
            Name = "Route 2";
            Type = LocationType.Route;
            Tag = LocationTag.Route2North;

            North = LocationTag.PewterCity;
            South = LocationTag.ViridianForestNorth;

            FlavorMessage = "the forest's entrance";

            Description = "This is the northern side of Route 2. Viridian Forest's northern entrance\n" + 
                          "is located here. Many trainers choose to train their Pokemon here to prepare\n" + 
                          "for their battle with the Gym Leader in Pewter City, Brock.";

            ConnectionsMessage = "A brief walk south leads to the northern end of the Viridian Forest, while a\n" + 
                                 "briefer one due north would take you to Pewter City.";
                        
            HelpMessage = "\"north\" or \"go north\" - moves you to Pewter City.\n" + 
                          "\"south\" or \"go south\" - moves you to the Viridian Forest.\n" + 
                          "\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range of all Pokemon in this area.
            int level = Program.random.Next(3, 5); 

            Pokemon pokemon;

            //35% probability of a Rattata.
            if (species <= 35)
                pokemon = generator.Create("Rattata", level);

            //35% probability of a Pidgey.
            else if (species <= 70)
                pokemon = generator.Create("Pidgey", level);

            //15% probability of a Weedle.
            else if (species <= 85)
                pokemon = generator.Create("Weedle", level);

            //15% probability of a Caterpie.
            else
                 pokemon = generator.Create("Caterpie", level);

            Battle battle = new Battle(pokemon);
        }

        public override void GoNorth()
        {
            UI.WriteLine("You choose to stay to the path and head straight for Pewter City.\n");
        }

        public override void GoSouth()
        {
            UI.WriteLine("You avoid the tall grass and follow the road to the Viridian Forest.\n");
        }
    }
}
