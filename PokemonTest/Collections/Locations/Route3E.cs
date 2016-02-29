using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class Route3E : Location
    {
        Random random = new Random();
        PokemonGenerator generator = new PokemonGenerator();

        public Route3E()
            : base()
        {
            Name = "Route 3";
            Type = LocationType.Route;
            Tag = LocationTag.Route3East;

            West = LocationTag.Route3West;
            East = LocationTag.MtMoonWest;

            FlavorMessage = "the mountain's plateau";

            Description = "Atop the plateau at the east end of this route lies the entrance to Mt. Moon.\n" + 
                          "Most people stop at the Pokemon Center here to rest after that long uphill\n" + 
                          "journey before heading into the pitch-black cave.";

            ConnectionsMessage = "Following the mountain trail down to the west leads to the west end of Route 3\n" + 
                                 "and to the east lies a cave - the entrance to Mt. Moon's interior.";

            HelpMessage = "\"west\" or \"go west\" - moves you to western Route 3.\n" + 
                          "\"east\" or \"go east\" - moves you to Mt. Moon.\n" + 
                          "\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = random.Next(1, 101);

            //The level range for Nidoran♀ and Nidoran♂.
            int level = random.Next(6, 9);

            //The level range for Spearow and Pidgey.
            int level2 = random.Next(7, 10); 

            Pokemon pokemon;

            //25% probability of a Spearow.
            if (species < 26)
                pokemon = generator.Create("Spearow", level);

            //25% probability of a Pidgey.
            else if (species < 51)
                pokemon = generator.Create("Pidgey", level);

            //15% probability of a Nidoran♀.
            else if (species < 66)
                pokemon = generator.Create("Nidoran♀", level2);

            //15% probability of a Nidoran♂.
            else if (species < 71)
                pokemon = generator.Create("Nidoran♂", level2);

            //15% probability of a Mankey.
            else if (species < 86)
                pokemon = generator.Create("Mankey", 8);

            //15% probability of a Jigglypuff.
            else
                pokemon = generator.Create("Jigglypuff", 8);


            Battle battle = new Battle(pokemon);
        }

        public override void GoWest()
        {
            UI.WriteLine("Enjoying a leisurely downhill stroll, you head down the mountain and towards\n" +
                         "the western end of route 3, where you can see Pewter City from the high ground.\n");
        }

        public override void GoEast()
        {
            UI.WriteLine("Feeling confident in your ability to take on whatever challenges await inside\n" +
                         "the cave, you turn on your flashlight as you head inside with a sure step.\n");
        }
    }
}
