using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class MtMoonPart2 : Location
        {
        Generator generator = new Generator();
        Random rng = new Random();        

        Trainer lana = TrainerList.trainers.Find(t => t.ID == 11);
        Trainer lanar = TrainerList.trainers.Find(t => t.ID == -11);

        Trainer simon = TrainerList.trainers.Find(t => t.ID == 12);
        Trainer simonr = TrainerList.trainers.Find(t => t.ID == -12);   

        public MtMoonPart2()
            : base()
        {
            Name = "Mt. Moon";
            Type = LocationType.Cave;
            Tag = "mtmoon2";

            West = "mtmoon1";
            North = "mtmoons";
            East = "mtmoon3";

            Description = "the cave's depths";
            LongDescription = "These are the dark reaches of Mt. Moon's interior. Pitch black darkness and an\nunnerving quiet make this part really difficult to navigate. The only thing\nthat stands out is a peculiar melody emanating from the north...";
            ConnectionsMessage = "Navigating due west would take you closer towards Route 3, and you are roughly\naware that following the cave's downward inclination to the east would take you\nto the eastern side of the cave and hopefully closer to Cerulean City.";
            HelpMessage = "\"west\" or \"go west\" - moves you to the western end of Mt. Moon.\n\"east\" or \"go east\" - moves you further underground in the cave.\n\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            int level = rng.Next(8, 11);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 45)
            {
                battle.Wild(generator.Create("Zubat", level));
            }

            else if (species > 25)
            {
                battle.Wild(generator.Create("Geodude", level));
            }

            else if (species > 5)
            {
                battle.Wild(generator.Create("Paras", 8));
            }

            else
            {
                battle.Wild(generator.Create("Clefairy", 9));
            }

            return;
        }

        public override void GoWest()
        {
            Console.WriteLine("Enjoying a leisurely downhill stroll, you head down the mountain and towards");
            Console.WriteLine("the western end of route 3, where you can see Pewter City from the high ground.");
        }

        public override void GoEast()
        {
            if (rng.Next(1, 11) > 6)
            {
                Console.WriteLine("Naturally following the downward slopes of the cave, you pace ever steadily ");
                Console.WriteLine("eastward. You maintain your calm so as not to lose your sense of direction, and");
                Console.WriteLine("eventually the cave starts getting brighter again - you are on the right path!");

                UI.AnyKey();
            }

            else
            {
                Console.WriteLine("As you aimlessly wander about the cave, you realize that frankly, you have not");
                Console.WriteLine("the foggiest where you are supposed to be going. Eventually you find yourself");
                Console.WriteLine("inside a corridor that's brightly lit by torches, making it easier to navigate");
                Console.WriteLine("about, but at a cost - you appear to have been spotted by wild Pokemon!");
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
