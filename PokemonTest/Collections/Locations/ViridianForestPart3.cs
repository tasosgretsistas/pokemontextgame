using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class ViridianForestPart3 : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        Trainer michael = TrainerList.AllTrainers.Find(t => t.TrainerID == 4);
        Trainer michaelr = TrainerList.AllTrainers.Find(t => t.TrainerID == -4);    
 
        public ViridianForestPart3()
        {
            Name = "Viridian Forest";
            Type = LocationType.Forest;
            Tag = LocationTag.ViridianForestNorth;

            South = LocationTag.ViridianForestCenter;
            North = LocationTag.Route2North;

            FlavorMessage = "the forest's end";

            Description = "The far north part of the forest, bordering Pewter City's outskirts. This\n" + 
                          "part of the forest is just thin enough to be able to make out Pewter City\n" + 
                          "in the north and an odd cave that you don't see on the map in the east.";

            ConnectionsMessage = "Going due south would take you to the deepest part of the forest, while going\n" + 
                                 "north leads to the northern side of Route 2 and the outskirts of Pewter City.";

            HelpMessage = "\"north\" or \"go north\" - moves you to Pewter City.\n" + 
                          "\"south\" or \"go south\" - moves you deeper in the Viridian Forest.\n" + 
                          "\"fight\" - attempts to start a fight with a wild Pokemon.\n" + 
                          "\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        { 
            if (michael.HasBeenDefeated(Game.Player))            
                michaelr.Encounter();           

            else
                UI.WriteLine("You need to defeat all of the trainers in this area before using this command!\n");
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range for Caterpie, Weedle and Pidgey.
            int level = Program.random.Next(3, 6);

            //The level range for Metapod and Kakuna.
            int level2 = Program.random.Next(4, 7);

            Pokemon pokemon;

            //25% probability of a Caterpie.
            if (species < 26)
                 pokemon = generator.Create("Caterpie", level);

            //25% probability of a Weedle.
            else if (species < 51)
                 pokemon = generator.Create("Weedle", level);

            //20% probability of a Pidgey.
            else if (species < 71)
                 pokemon = generator.Create("Pidgey", level);

            //15% probability of a Metapod.
            else if (species < 86)
                 pokemon = generator.Create("Metapod", level2);

            //15% probability of a Kakuna.
            else
                pokemon = generator.Create("Kakuna", level2);

            Battle battle = new Battle(pokemon);
        }

        public override void GoNorth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //60% probability that the player will encounter a wild Pokemon.
            if (encounter <= 6)
            {
                UI.WriteLine("You're ecstatic that you can see Pewter and can finally leave the forest.\n" +
                             "Sadly, you have also lost focus and wandered off the main path, right\n" +
                             "into wild Pokemon territory!\n");

                Encounter();

                UI.WriteLine("You sigh in relief that nobody got harmed due to your inattentiveness. No\n" +
                             "distractions from now on - you follow the path towards the northern exit.");
            }

            //40% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("You can definitely see Pewter City ahead of you, but that doesn't mean you\n" +
                             "should stop looking at the map, either. You follow the path that you know\n" +
                             "is safe enough to tread on, and it pays off - you can see the northern exit.");
            }

            UI.AnyKey();

            //If the player has not defeated Michael before, he has to battle him.
            if (!michael.HasBeenDefeated(Game.Player))
            {
                UI.WriteLine("You are almost out of the forest when you run into another kid with a net.\n" +
                             "This one doesn't seem as eager to fight you, but you're ready either way!\n");

                michael.Encounter();
            }
        }

        public override void GoSouth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //50% probability that the player will encounter a wild Pokemon.
            if (encounter <= 5)
            {
                UI.WriteLine("Feeling very confident in your ability to take any wild Pokemon on, you\n" +
                                  "casually stroll through the forest. Not completely unexpectedly, a wild\n" +
                                  "Pokemon decides to take you up on your challenge!\n");

                Encounter();

                UI.WriteLine("Hah, no problem. You got some valuable battle experience AND you're almost\n" +
                             "where you wanted to go - the forest's center. Score 1 for you and your party!");
            }


            //40% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("You know for certain that you can handle wild Pokemon here, but you still\n" +
                             "decide to play it safe. Taking advantage of the higher visibility in this\n" +
                             "part of the forest, you head straight for the glade in the forest's center.");                
            }

            UI.AnyKey();
        }
    }
}
