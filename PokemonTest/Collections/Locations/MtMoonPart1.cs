using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class MtMoonPart1 : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        Trainer lana = TrainerList.AllTrainers.Find(t => t.TrainerID == 12);
        Trainer lanar = TrainerList.AllTrainers.Find(t => t.TrainerID == -12);

        Trainer simon = TrainerList.AllTrainers.Find(t => t.TrainerID == 13);
        Trainer simonr = TrainerList.AllTrainers.Find(t => t.TrainerID == -13);

        public MtMoonPart1()
            : base()
        {
            Name = "Mt. Moon";
            Type = LocationType.Cave;
            Tag = LocationTag.MtMoonWest;

            West = LocationTag.Route3East;
            East = LocationTag.MtMoonCenter;

            FlavorMessage = "the cave's entrance";

            Description = "This is the entrance of the complex cave within Mt. Moon. There is only dim\n" +
                          "light reflecting off the cave's floor to guide your way, and the cool, damp\n" +
                          "sensation one gets can send chills down anybody's spine. Proceed with caution.";

            ConnectionsMessage = "The bright light to the west marks the exit of the cave towards Route 2, while\n" +
                                 "going east would only take you deeper into the cave.";

            HelpMessage = "\"west\" or \"go west\" - moves you to eastern Route 3.\n" +
                          "\"east\" or \"go east\" - moves you deeper into Mt. Moon.\n" +
                          "\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range for Zubat and Geodude.
            int level = Program.random.Next(7, 11);

            Pokemon pokemon;

            //49% probability of a Zubat.
            if (species <= 49)
                pokemon = generator.Create("Zubat", level);

            //25% probability of a Geodude.
            else if (species <= 74)
                pokemon = generator.Create("Geodude", level);

            //25% probability of a Paras.
            else if (species <= 99)
                pokemon = generator.Create("Paras", 8);

            //1% probability of a Clefairy.
            else
                pokemon = generator.Create("Clefairy", 8);

            Battle battle = new Battle(pokemon);
        }

        public override void GoWest()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //50% probability that the player will encounter a wild Pokemon.
            if (encounter <= 5)
            {
                UI.WriteLine("You get absent-minded on your way out of the cave and forget to take a left\n" +
                             "turn where you were supposed to. You only realize it when you get to a part of\n" +
                             "the cave you have no recollection of seeing before, and by then it's too late.\n" +
                             "Or so the angered wild Pokemon running your way would suggest!");

                Encounter();

                UI.WriteLine("Yikes - everything fine, which is a relief, but you remind yourself that you\n" +
                             "ought to be more careful if you head back into the cave again. Thankfully, you\n" +
                             "are now back out of the cave, where no wild Pokemon will be attacking you.");

                UI.AnyKey();
            }

            //50% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("You carefully retrace your steps as you backtrack towards the exit of the cave.\n" +
                             "Being cautious pays off, as soon enough you find yourself before a bright light\n" +
                             "-- the light of day back in Route 3!");

                UI.AnyKey();
            }
        }

        public override void GoEast()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //60% probability that the player will encounter a wild Pokemon.
            if (encounter <= 6)
            {
                UI.WriteLine("You clumsily step about inside the cave, tip-toeing so as to not make any noise\n" +
                            "that could potentially stir the attention of wild Pokemon. Your attempts all go\n" +
                            "to waste though, as you trip on a rock of some kind protruding off the floor,\n" +
                            "and with a loud screech a startled Pokemon comes running your way!");

                Encounter();

                UI.WriteLine("Phew, all good, thankfully. You kick the rock that caused you to trip in anger\n" +
                             "and yell out a few curses. Wiping the sweat off your forehead, you swear to be\n" +
                             "more careful from now on, for the sake of your Pokemon.");

                UI.AnyKey();
            }

            //40% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("Holding your trusty flashlight firmly in your hand, you proceed carefully into\n" +
                             "the cave's darker reaches. You steer clear off particularly dangerous-looking\n" +
                             "paths and hallways as you navigate further into the cave.");

                UI.AnyKey();                
            }

            //Enemy trainer routine.
            if (!simon.HasBeenDefeated(Game.Player))
            {
                //If the player has not defeated Lana before, he has to battle her.
                if (!lana.HasBeenDefeated(Game.Player))
                {
                    UI.WriteLine("The light in the cave is ever dimming, and you eventually find yourself before\n" +
                                 "a round hole in the wall, big enough for you to walk through. Just as you are\n" +
                                 "about to go inside it, something bumps into you. Upon looking more closely, it\n" +
                                 "appears that you have walked into a girl - a Pokemon trainer at it!");

                    lana.Encounter();
                    
                    UI.WriteLine("You have definitely not seen that Pokemon before, and it looked really strong.\n" +
                                 "Your heart is pumping with excitement as you pick up your pace going onward.");

                    UI.AnyKey();
                }


                UI.WriteLine("Visibility is constantly becoming lower as you go further into the hole in the\n" +
                             "wall, so out of necessity you also begin to move slower. You keep shining your\n" +
                             "flashlight about to check for wild Pokemon, until your light lands on a person.\n" +
                             "It's a man in a lab coat, and he grins when he sees you - a Pokemon trainer!");

                //Then, he has to battle Simon.
                simon.Encounter();

                UI.WriteLine("So all sorts of people are interested in this cave, huh... Well, no matter. You\n" +
                             "still have the journey ahead to think about, so once more you start walking\n" +
                             "towards the unknown with a confident stride.");

                UI.AnyKey();
            }
        }
    }
}
