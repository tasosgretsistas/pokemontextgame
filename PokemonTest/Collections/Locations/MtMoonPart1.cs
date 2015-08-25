using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class MtMoonPart1 : Location
    {
        Generator generator = new Generator();
        Random rng = new Random();        

        Trainer lana = TrainerList.trainers.Find(t => t.ID == 10);
        Trainer lanar = TrainerList.trainers.Find(t => t.ID == -10);

        Trainer simon = TrainerList.trainers.Find(t => t.ID == 11);
        Trainer simonr = TrainerList.trainers.Find(t => t.ID == -11);   

        public MtMoonPart1()
            : base()
        {
            Name = "Mt. Moon";
            Type = LocationType.Cave;
            Tag = "mtmoon1";

            West = "route3e";
            East = "mtmoon2";

            Description = "the cave's entrance";
            LongDescription = "This is the entrance of the complex cave within Mt. Moon. There is only dim\nlight reflecting off the cave's floor to guide your way, and the cool, damp\nsensation one gets can send chills down anybody's spine. Proceed with caution.";
            ConnectionsMessage = "The bright light to the west marks the exit of the cave towards Route 2, while\ngoing east would only take you deeper into the cave.";
            HelpMessage = "\"west\" or \"go west\" - moves you to eastern Route 3.\n\"east\" or \"go east\" - moves you deeper into Mt. Moon.\n\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            int level = rng.Next(7, 11);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 50)
            {
                battle.Wild(generator.Create("Zubat", level));
            }

            else if (species > 26)
            {
                battle.Wild(generator.Create("Geodude", level));
            }

            else if (species > 2)
            {
                battle.Wild(generator.Create("Paras", 8));
            }

            else
            {
                battle.Wild(generator.Create("Clefairy", 8));
            }

            return;
        }

        public override void GoWest()
        {
            if (rng.Next(1, 11) > 5)
            {
                Console.WriteLine("You carefully retrace your steps as you backtrack towards the exit of the cave.");
                Console.WriteLine("Being cautious pays off, as soon enough you find yourself before a bright light");
                Console.WriteLine("-- the light of day back in Route 3!");

                UI.AnyKey();
            }

            else
            {
                Console.WriteLine("You get absent-minded on your way out of the cave and forget to take a left");
                Console.WriteLine("turn where you were supposed to. You only realize it when you get to a part of");
                Console.WriteLine("the cave you have no recollection of seeing before, and by then it's too late.");
                Console.WriteLine("Or so the angered wild Pokemon running your way would suggest!");
                Console.WriteLine("");

                Encounter();

                Console.WriteLine("Yikes - everything fine, which is a relief, but you remind yourself that you");
                Console.WriteLine("ought to be more careful if you head back into the cave again. Thankfully, you");
                Console.WriteLine("are now back out of the cave, where no wild Pokemon will be attacking you.");

                UI.AnyKey();
            }
        }

        public override void GoEast()
        {
            if (rng.Next(1, 11) > 6)
            {
                Console.WriteLine("Holding your trusty flashlight firmly in your hand, you proceed carefully into");
                Console.WriteLine("the cave's darker reaches. You steer clear off particularly dangerous-looking");
                Console.WriteLine("paths and hallways as you navigate further into the cave.");

                UI.AnyKey();
            }

            else
            {
                Console.WriteLine("You clumsily step about inside the cave, tip-toeing so as to not make any noise");
                Console.WriteLine("that could potentially stir the attention of wild Pokemon. Your attempts all go");
                Console.WriteLine("to waste though, as you trip on a rock of some kind protruding off the floor,");
                Console.WriteLine("and with a loud screech a startled Pokemon comes running your way!");
                Console.WriteLine("");

                Encounter();

                Console.WriteLine("Phew, all good, thankfully. You kick the rock that caused you to trip in anger");
                Console.WriteLine("and yell out a few curses. Wiping the sweat off your forehead, you swear to be");
                Console.WriteLine("more careful from now on, for the sake of your Pokemon.");

                UI.AnyKey();
            }

            if (!simon.HasBeenDefeated)
            {
                if (!lana.HasBeenDefeated)
                {
                    Console.WriteLine("The light in the cave is ever dimming, and you eventually find yourself before");
                    Console.WriteLine("a round hole in the wall, big enough for you to walk through. Just as you are");
                    Console.WriteLine("about to go inside it, something bumps into you. Upon looking more closely, it");
                    Console.WriteLine("appears that you have walked into a girl - a Pokemon trainer at it!");

                    lana.Encounter();

                    Console.WriteLine("");
                    Console.WriteLine("You have definitely not seen that Pokemon before, and it looked really strong.");
                    Console.WriteLine("Your heart is pumping with excitement as you pick up your pace going onward.");

                    UI.AnyKey();
                }

                if (!simon.HasBeenDefeated)
                {
                    Console.WriteLine("Visibility is constantly becoming lower as you go further into the hole in the");
                    Console.WriteLine("wall, so out of necessity you also begin to move slower. You keep shining your");
                    Console.WriteLine("flashlight about to check for wild Pokemon, until your light lands on a person.");
                    Console.WriteLine("It's a man in a lab coat, and he grins when he sees you - a Pokemon trainer!");

                    simon.Encounter();

                    Console.WriteLine("");
                    Console.WriteLine("So all sorts of people are interested in this cave, huh... Well, no matter. You");
                    Console.WriteLine("still have the journey ahead to think about, so once more you start walking");
                    Console.WriteLine("towards the unknown with a confident stride.");

                    UI.AnyKey();
                }
            }
        }
    }
}
