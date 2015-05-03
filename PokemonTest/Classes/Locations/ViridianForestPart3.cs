using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class ViridianForestPart3 : Location
    {
        Generator generator = new Generator();
        Random rng = new Random();

        Trainer michael = TrainerList.trainers.Find(t => t.ID == "3");
        Trainer michaelr = TrainerList.trainers.Find(t => t.ID == "3r");    
 
        public ViridianForestPart3()
        {
            Name = "Viridian Forest";
            Type = "forest";
            Tag = "forest3";

            South = "forest2";
            North = "route2n";

            Description = "the forest's end";
            LongDescription = "The far north part of the forest, bordering Pewter City's outskirts. This\npart of the forest is just thin enough to be able to make out Pewter City\nin the north and an odd cave that you don't see on the map in the east.";
            ConnectionsMessage = "Going due south would take you to the deepest part of the forest, while going\nnorth leads to the northern side of Route 2 and the outskirts of Pewter City.";
            HelpMessage = "\"north\" or \"go north\" - moves you to Pewter City.\n\"south\" or \"go south\" - moves you deeper in the Viridian Forest.\n\"fight\" - attempts to start a fight with a wild Pokemon.\n\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        { 
            if (michael.HasBeenDefeated)            
                michaelr.Encounter();           

            else
                Console.WriteLine("You need to defeat all of the trainers in this area before using this command!\n");
        }

        public override void Encounter()
        {
            int level = rng.Next(3, 6);
            int level2 = rng.Next(4, 7);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 75)
            {
                battle.Wild(generator.Create("Caterpie", level));
            }
            else if (species > 50)
            {
                battle.Wild(generator.Create("Weedle", level));
            }
            else if (species > 30)
            {
                battle.Wild(generator.Create("Pidgey", level));
            }
            else if (species > 15)
            {
                battle.Wild(generator.Create("Metapod", level2));
            }
            else
            {
                battle.Wild(generator.Create("Kakuna", level2));
            }
        }

        public override void GoNorth()
        {
            if (rng.Next(1, 11) > 6)
            {
                Console.WriteLine("You can definitely see Pewter City ahead of you, but that doesn't mean you");
                Console.WriteLine("should stop looking at the map, either. You follow the path that you know");
                Console.WriteLine("is safe enough to tread on, and it pays off - you can see the northern exit.");

                Text.AnyKey();
            }

            else
            {
                Console.WriteLine("You're ecstatic that you can see Pewter and can finally leave the forest.");
                Console.WriteLine("Sadly, you have also lost focus and wandered off the main path, right");
                Console.WriteLine("into wild Pokemon territory!");
                Console.WriteLine("");
                
                Encounter();

                Console.WriteLine("You sigh in relief that nobody got harmed due to your inattentiveness. No");
                Console.WriteLine("distractions from now on - you follow the path towards the northern exit.");

                Text.AnyKey();
            }

            if (!michael.HasBeenDefeated)
            {
                Console.WriteLine("You are almost out of the forest when you run into another kid with a net.");
                Console.WriteLine("This one doesn't seem as eager to fight you, but you're ready either way!");

                michael.Encounter();
            }
        }

        public override void GoSouth()
        {
            if (rng.Next(1, 11) > 5)
            {
                Console.WriteLine("You know for certain that you can handle wild Pokemon here, but you still");
                Console.WriteLine("decide to play it safe. Taking advantage of the higher visibility in this");
                Console.WriteLine("part of the forest, you head straight for the glade in the forest's center.");
            }

            else
            {
                Console.WriteLine("Feeling very confident in your ability to take any wild Pokemon on, you");
                Console.WriteLine("casually stroll through the forest. Not completely unexpectedly, a wild");
                Console.WriteLine("Pokemon decides to take you up on your challenge!");
                Console.WriteLine("");
                
                Encounter();

                Console.WriteLine("Hah, no problem. You got some valuable battle experience AND you're almost");
                Console.WriteLine("where you wanted to go - the forest's center. Score 1 for you and your party!");
            }

            Text.AnyKey();
        }
    }
}
