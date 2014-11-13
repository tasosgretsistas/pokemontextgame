using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class ViridianForestPart1 : Location
    {
        Generator generator = new Generator();
        Random rng = new Random();

        Trainer nickr = TrainerList.trainers.Find(t => t.ID == "1r");
        Trainer nick = TrainerList.trainers.Find(t => t.ID == "1");          

        public ViridianForestPart1()
        {
            Name = "Viridian Forest";
            Type = "forest";
            Tag = "forest1";

            South = "viridian";
            North = "forest2";

            Description = "the natural maze";
            LongDescription = "This deep forest is the first major obstacle for most aspiring trainers.\nBug-type Pokemon abode, it is very easy to get lost in the thick forest.";
            ConnectionsMessage = "Going south of here would lead you to southern Route 2, while going north\nwould take you right into the heart of the forest.";
            HelpMessage = "\"north\" or \"go north\" - moves you deeper in the Viridian Forest.\n\"south\" or \"go south\" - moves you to Viridian City.\n\"fight\" - attempts to start a fight with a wild Pokemon.\n\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        { 
            if (nick.Defeated())      
                nickr.Encounter();            

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
                Console.WriteLine("Using your trusty compass, you carefully maneuver around the dark forest.");
                Console.WriteLine("You stay clear off thick foliage and tall grass, as you move due north, in");
                Console.WriteLine("the direction of Pewter City. Few minutes later, you find yourself in the");
                Console.WriteLine("middle of the forest, just as planned!");

                Story.AnyKey();
            }

            else
            {
                Console.WriteLine("You decide to trust your direction sense as you go deeper into the forest.");
                Console.WriteLine("It seems to go okay for a while, but soon enough you realize that you are");
                Console.WriteLine("not where you're supposed to be - you get attacked by a wild Pokemon!");
                Console.WriteLine("");

                Encounter();

                Console.WriteLine("Phew, that was a close one back there, but both you and your Pokemon are");
                Console.WriteLine("okay at least. You decide to be a bit more careful from now on, as you go");
                Console.WriteLine("even deeper into this dangerous forest.");

                Story.AnyKey();
            }

            if (!nick.Defeated())
            {
                Console.WriteLine("");

                Console.WriteLine("Right as you take your first step into the innermost part of the forest,");
                Console.WriteLine("you hear something behind you. It's a little kid with a bug-catching net.");
                Console.WriteLine("");

                Console.WriteLine("\"Hold it right there! I'm a trainer too, and when two trainers spot each");
                Console.WriteLine(" other, they have to battle! Those are the rules!\"");                
                
                nick.Encounter();
            }
        }

        public override void GoSouth()
        {
            if (rng.Next(1, 11) > 5)
            {
                Console.WriteLine("Carefully following the path you took when you first went into the forest,");
                Console.WriteLine("you navigate towards the southern exit. Not long later, you reach Route 2.");               
            }

            else
            {
                Console.WriteLine("You get to a crossroads on your way out of the forest, which you don't\n\n");
                Console.WriteLine("remember seeing before - you're probably lost. As if it couldn't get any");
                Console.WriteLine("worse, a wild Pokemon decides to jump you!");
                Console.WriteLine("");
                
                Encounter();

                Console.WriteLine("Thankfully you weren't fully unprepared for that. Collecting yourself, you");
                Console.WriteLine("follow the signs around the forest until you are finally back at Route 2.");
            }

            Story.AnyKey();
        }
    }
}
