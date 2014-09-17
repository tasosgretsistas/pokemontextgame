using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class ViridianForestPart2 : Location
        {
        Generator generator = new Generator();
        Random rng = new Random();

        Trainer eric = TrainerList.trainers.Find(t => t.ID == "2");

        Trainer ericr = TrainerList.trainers.Find(t => t.ID == "2r");

        public ViridianForestPart2()
        {
            Name = "Viridian Forest's center";
            Type = "forest";
            Tag = "forest2";

            South = "forest1";
            North = "forest3";

            Description = "the forest's grove";
            LongDescription = "The deepest part of the Viridian Forest. A big grove lies in the middle.\nCareful - you never know what kind of danger could lurk around the corner.";
            Connections = "the southern Forest to the south and the northern\nforest to the north";
            HelpMessage = "\"north\" or \"go north\" - moves you to the north part of the forest.\n\"south\" or \"go south\" - moves you to the south part of the forest.\n\"fight\" - attempts to start a fight with a wild Pokemon.\n\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        {
            

            if (eric.Defeated())
            {
                ericr.Encounter();
            }

            else
            {
                Console.WriteLine("\nYou need to defeat all of the trainers in an area before using this command!");
            }
        }

        public override void Encounter()
        {   
            int level = rng.Next(3, 6);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 92)
            {
                battle.Wild(generator.Create("Pidgeotto", 7));
            }            
            else if (species > 84)
            {
                battle.Wild(generator.Create("Pikachu", level));
            }
            else if (species > 42)
            {
                battle.Wild(generator.Create("Metapod", level));
            }
            else
            {
                battle.Wild(generator.Create("Kakuna", level));
            }
        }

        public override void GoNorth()
        {
            if (rng.Next(1, 11) > 6)
            {
                Console.WriteLine("You decide to stay away from the big grove, where you would be a sitting");
                Console.WriteLine("duck for wild Pokemon. Instead, you move through the less dense parts of");
                Console.WriteLine("the forest until you find the path that leads to the northern side.");

                Story.AnyKey();
            }

            else
            {
                Console.WriteLine("Fascinated by the gorgeous view, you run into the wide grove - at the cost");
                Console.WriteLine("of your sense of direction. You decide it's time to look at the map when a");
                Console.WriteLine("wild Pokemon, attracted by the smell of the food in your bag, attacks you!");
                Console.WriteLine("");
                
                Encounter();

                Console.WriteLine("");

                Console.WriteLine("Both you and your food are safe - for now. You go back to reading the map,");
                Console.WriteLine("which points you in the way north and once again back into the forest.");

                Story.AnyKey();
            }

            if (!eric.Defeated())
            {
                Console.WriteLine("On your way further north, you run into another kid with a net -- is it");
                Console.WriteLine("a new fashion or something? Either way, you already know what this means!");

                eric.Encounter();
            }
        }

        public override void GoSouth()
        {
            if (rng.Next(1, 11) > 5)
            {
                Console.WriteLine("You know this path fairly well by now, so you feel confident in your ability");
                Console.WriteLine("to get around this part with no hiccups. You even stop by the glade for a");
                Console.WriteLine("moment to see the view before you're on your way to the southern side.");

                Story.AnyKey();
            }

            else
            {
                Console.WriteLine("It seems you've become a bit more confident in how well you know the forest");
                Console.WriteLine("than you should be - you've gotten lost. Luckily, you can see the glade in");
                Console.WriteLine("the distance - but it turns out that it was the wrong glade!");
                Console.WriteLine("");
                
                Encounter();

                Console.WriteLine("");

                Console.WriteLine("Looking at the map, you realize that you're at the completely wrong place.");
                Console.WriteLine("Heading due east, you reach the big glade in the middle with the beautiful");
                Console.WriteLine("view, and then the path that leads to the southern side of the forest.");

                Story.AnyKey();
            }
        }
    }
}
