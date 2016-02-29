using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class MtMoonPart2 : Location
    {
        Random random = new Random();
        PokemonGenerator generator = new PokemonGenerator();

        public MtMoonPart2()
            : base()
        {
            Name = "Mt. Moon";
            Type = LocationType.Cave;
            Tag = LocationTag.MtMoonCenter;

            West = LocationTag.MtMoonWest;

            FlavorMessage = "the cave's depths";

            Description = "These are the dark reaches of Mt. Moon's interior. Pitch black darkness and an\n" + 
                          "unnerving quiet make this part really difficult to navigate. The only thing\n" + 
                          "that stands out is a peculiar melody emanating from the north...";

            ConnectionsMessage = "Navigating due west would take you closer towards Route 3, and you are roughly\n" + 
                                 "aware that following the cave's downward inclination to the east would take you\n" + 
                                 "to the eastern side of the cave and hopefully closer to Cerulean City.";

            HelpMessage = "\"west\" or \"go west\" - moves you to the western end of Mt. Moon.\n" + 
                          "\"east\" or \"go east\" - moves you further underground in the cave.\n" + 
                          "\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = random.Next(1, 101);

            //The level range for Zubat and Geodude.
            int level = random.Next(8, 11);

            Pokemon pokemon;

            //55% probability of a Zubat.
            if (species <= 55)
                pokemon = generator.Create("Zubat", level);

            //20% probability of a Geodude.
            else if (species <= 75)
                pokemon = generator.Create("Geodude", level);

            //20% probability of a Paras.
            else if (species <= 95)
                pokemon = generator.Create("Paras", 8);

            //5% probability of a Clefairy.
            else
                pokemon = generator.Create("Clefairy", 9);

            Battle battle = new Battle(pokemon);
        }

        public override void GoWest()
        {
            
        }

        public override void GoEast()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = random.Next(1, 11);

            //70% probability that the player will encounter a wild Pokemon.
            if (encounter <= 7)
            {
                UI.WriteLine("As you aimlessly wander about the cave, you realize that frankly, you have not\n" +
                             "the foggiest where you are supposed to be going. Eventually you find yourself\n" +
                             "inside a corridor that's brightly lit by torches, making it easier to navigate\n" +
                             "about, but at a cost - you appear to have been spotted by wild Pokemon!");

                Encounter();

                UI.WriteLine("Phew, all good, thankfully. You kick the rock that caused you to trip in anger\n" +
                             "and yell out a few curses. Wiping the sweat off your forehead, you swear to be\n" +
                             "more careful from now on, for the sake of your Pokemon.");

                UI.AnyKey();
            }

            //30% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("Naturally following the downward slopes of the cave, you pace ever steadily\n" +
                             "eastward. You maintain your calm so as not to lose your sense of direction, and\n" +
                             "eventually the cave starts getting brighter again - you are on the right path!");

                UI.AnyKey();

            }

            //Add trainer encounter logic.
            
        }
    }
}
