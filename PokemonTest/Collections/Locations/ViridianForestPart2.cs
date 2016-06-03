using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class ViridianForestPart2 : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        Trainer eric = TrainerList.AllTrainers.Find(t => t.TrainerID == 3);
        Trainer ericr = TrainerList.AllTrainers.Find(t => t.TrainerID == -3);

        public ViridianForestPart2()
        {
            Name = "Viridian Forest's center";
            Type = LocationType.Forest;
            Tag = LocationTag.ViridianForestCenter;

            South = LocationTag.ViridianForestSouth;
            North = LocationTag.ViridianForestNorth;

            FlavorMessage = "the forest's grove";

            Description = "The deepest part of the Viridian Forest. A big grove lies in the middle.\n" +
                          "Careful - you never know what kind of danger could lurk around the corner.";

            ConnectionsMessage = "The edges of the forest are located in each direction. The Viridian City end\n" + 
                                 "to the south, and the Pewter City end to the north.";

            HelpMessage = "\"north\" or \"go north\" - moves you to the north part of the forest.\n" + 
                          "\"south\" or \"go south\" - moves you to the south part of the forest.\n" +
                          "\"fight\" - attempts to start a fight with a wild Pokemon.\n" +
                          "\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        {
            if (eric.HasBeenDefeated(Game.Player))
                ericr.Encounter();

            else
                UI.WriteLine("You need to defeat all of the trainers in this area before using this command!\n");
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range for Metapod, Kakuna and Pikachu.
            int level = Program.random.Next(3, 6);

            Pokemon pokemon;

            //10% probability of a Pidgeotto.
            if (species <= 10)
                pokemon = generator.Create("Pidgeotto", 7);

            //10% probability of a Pikachu.
            else if (species <= 20)
                pokemon = generator.Create("Pikachu", level);

            //40% probability of a Metapod.
            else if (species <= 60)
                pokemon = generator.Create("Metapod", level);

            //40% probability of a Kakuna.
            else
                pokemon = generator.Create("Kakuna", level);

            Battle battle = new Battle(pokemon);
        }

        public override void GoNorth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //60% probability that the player will encounter a wild Pokemon.
            if (encounter <= 6)
            {
                UI.WriteLine("Fascinated by the gorgeous view, you run into the wide grove - at the cost\n" +
                             "of your sense of direction. You decide it's time to look at the map when a\n" +
                             "wild Pokemon, attracted by the smell of the food in your bag, attacks you!\n");

                Encounter();

                UI.WriteLine("Both you and your food are safe - for now. You go back to reading the map,\n" +
                             "which points you in the way north and once again back into the forest.");                
            }

            //40% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("You decide to stay away from the big grove, where you would be a sitting\n" +
                                  "duck for wild Pokemon. Instead, you move through the less dense parts of\n" +
                                  "the forest until you find the path that leads to the northern side.");            
            }

            UI.AnyKey();

            //If the player has not defeated Nick before, he has to battle him.
            if (!eric.HasBeenDefeated(Game.Player))
            {
                UI.WriteLine("On your way further north, you run into another kid with a net -- is it\n" +
                             "a new fashion or something? Either way, you already know what this means!\n");

                eric.Encounter();
            }
        }

        public override void GoSouth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //50% probability that the player will encounter a wild Pokemon.
            if (Program.random.Next(1, 11) <= 5)
            {
                UI.WriteLine("It seems you've become a bit more confident in how well you know the forest\n" +
                             "than you should be - you've gotten lost. Luckily, you can see the glade in\n" +
                             "the distance - but it turns out that it was the wrong glade!\n");

                Encounter();

                UI.WriteLine("Looking at the map, you realize that you're at the completely wrong place.\n" +
                                  "Heading due east, you reach the big glade in the middle with the beautiful\n" +
                                  "view, and then the path that leads to the southern side of the forest.");
            }

            //50% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("You know this path fairly well by now, so you feel confident in your ability\n" +
                             "to get around this part with no hiccups. You even stop by the glade for a\n"+
                             "moment to see the view before you're on your way to the southern side.");
            }

            UI.AnyKey();
        }
    }
}
