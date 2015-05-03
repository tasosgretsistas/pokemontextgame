using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class Route3W: Location
    {
        Random rng = new Random();
        Generator generator = new Generator();

        Trainer lenny = TrainerList.trainers.Find(t => t.ID == "6");
        Trainer timmy = TrainerList.trainers.Find(t => t.ID == "7");
        Trainer mina = TrainerList.trainers.Find(t => t.ID == "8");
        Trainer mandy = TrainerList.trainers.Find(t => t.ID == "9");

        Trainer lenny2 = TrainerList.trainers.Find(t => t.ID == "6r");
        Trainer timmy2 = TrainerList.trainers.Find(t => t.ID == "7r");
        Trainer mina2 = TrainerList.trainers.Find(t => t.ID == "8r");
        Trainer mandy2 = TrainerList.trainers.Find(t => t.ID == "9r");

        public Route3W()
            : base()
        {
            Name = "Route 3";
            Type = "route";
            Tag = "route3w";

            West = "pewter";
            East = "route3e";

            Description = "the rocky road";
            LongDescription = "This rocky path marks the beginning of the long mountain trail that leads to\nMt. Moon. Many strong trainers head this way after emerging victorious from\nthe Pewter City gym. Be careful as you challenge them!";
            ConnectionsMessage = "Pewter City is close by to the west of here, while going east finds one at the\neastern end of Route 3 and the entrance to Mt. Moon.";
            HelpMessage = "\"west\" or \"go west\" - moves you to Pewter City.\n\"east\" or \"go east\" - moves you to eastern Route 2.\n\"fight\" - attempts to start a fight with a wild Pokemon.\n\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        {
            if (mandy.HasBeenDefeated)
            {
                Console.WriteLine("Which trainer would you like to have a rematch with?");
                Console.WriteLine("(Available trainers: Lenny, Timmy, Mina, Mandy)");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "Lenny":
                    case "lenny":

                        lenny2.Encounter();

                        break;

                    case "Timmy":
                    case "timmy":

                        timmy2.Encounter();

                        break;

                    case "Mina":
                    case "mina":

                        mina2.Encounter();

                        break;

                    case "Mandy":
                    case "mandy":

                        mandy2.Encounter();

                        break;
                }
            }

            else
                Console.WriteLine("You need to defeat all of the trainers in this area before using this command!\n");
        }

        public override void Encounter()
        {   
            int level = rng.Next(6, 9);
            int level2 = rng.Next(5, 8);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 75)
            {
                battle.Wild(generator.Create("Spearow", level));
            }
            else if (species > 50)
            {
                battle.Wild(generator.Create("Pidgey", level));
            }
            else if (species > 35)
            {
                battle.Wild(generator.Create("Nidoran♀", level2));
            }
            else if (species > 20)
            {
                battle.Wild(generator.Create("Nidoran♂", level2));
            }
            else if (species > 10)
            {
                battle.Wild(generator.Create("Jigglypuff", 7));
            }
            else
            {
                battle.Wild(generator.Create("Mankey", 7));
            }


            return;
        }

        public override void GoWest()
        {
            Console.WriteLine("You follow the beaten path and eventually find yourself back at Pewter City.\n");
        }

        public override void GoEast()
        {
            Console.WriteLine("As you reach the midway point of the mountain's trail, you see a small part of");
            Console.WriteLine("level land where multiple trainers have congregated. Most of them will probably");
            Console.WriteLine("want to take you on, and you're going to give them a run for their money!");

            Text.AnyKey();

            if (!timmy.HasBeenDefeated)
            {
                if (!lenny.HasBeenDefeated)
                {
                    Console.WriteLine("The first trainer to walk up to you is a little kid who's holding a school bag.");
                    Console.WriteLine("He doesn't look too strong, but if he's here, he's beaten the Pewter Gym, so he");
                    Console.WriteLine("can't be weak either. Better not underestimate him!");

                    lenny.Encounter();

                    Console.WriteLine("");
                    Console.WriteLine("An Ekans, huh? You've never seen that Pokemon before, as it's definitely not");
                    Console.WriteLine("native to Pallet Town. How many more new Pokemon are you going to encounter,");
                    Console.WriteLine("you think to yourself as you move on.");

                    Text.AnyKey();
                }

                if (!timmy.HasBeenDefeated)
                {
                    Console.WriteLine("Another kid with a schoolbag walks up to you, but this one looks less confident");
                    Console.WriteLine("than the previous one. You have learnt not to underestimate anybody though, so");
                    Console.WriteLine("you greet him with a nod and quickly grab your Pokeballs.");

                    timmy.Encounter();

                    Console.WriteLine("");
                    Console.WriteLine("That was no easy fight, and it looks like there's more to go. Your heart is");
                    Console.WriteLine("racing as you start imagining the intense battles that are yet to come.");
                    Console.WriteLine("");

                    Console.WriteLine("You decide to give your Pokemon a small break from the constant battles, but");
                    Console.WriteLine("you can't wait until your next battle.");

                    Text.AnyKey();

                    Overworld.LoadLocation(this.Tag);
                }
            }

            if (!mandy.HasBeenDefeated)
            {
                if (!mina.HasBeenDefeated)
                {
                    Console.WriteLine("A teenage girl walks up to you this time. She has an aura of confidence to her,");
                    Console.WriteLine("one that tells you that you should by no means underestimate her. After all,");
                    Console.WriteLine("this is one of the highest points on the mountain - only the elite are here.");

                    mina.Encounter();

                    Console.WriteLine("");
                    Console.WriteLine("That girl really meant business there. You won, but you can't help having the");
                    Console.WriteLine("impression that you need to step up your Pokemon training game yet. One more");
                    Console.WriteLine("ought to prove invaluable in giving you Pokemon battling experience.");

                    Text.AnyKey();
                }

                if (!mandy.HasBeenDefeated)
                {
                    Console.WriteLine("The last trainer that you can see is a girl who's seemingly having a picnic at");
                    Console.WriteLine("the side of the mountain trail, on a large grass patch. She doesn't seem to");
                    Console.WriteLine("have noticed you, so you approach her instead. You only briefly see her face,");
                    Console.WriteLine("but damn... she's really pretty.");

                    Text.AnyKey();

                    Console.WriteLine("Ugh, this is no time to be gawking at girls. There's a pink, round Pokemon that");
                    Console.WriteLine("you have never seen before next to her, so she must be a trainer - and a good");
                    Console.WriteLine("one at it. You reach out for your Pokeballs and step in front of her.");

                    mandy.Encounter();

                    Console.WriteLine("");
                    Console.WriteLine("Your entire body is shaking with excitement after that one, and you're fairly");
                    Console.WriteLine("sure it's not just because you've fallen in love with that girl back there. But");
                    Console.WriteLine("enough of that, it's time to move on - onward to Mt. Moon!");

                    Text.AnyKey();
                }
            }

            Console.WriteLine("You leave the part where all the trainer battles are happening and head back to");
            Console.WriteLine("the main path. The hike up the mountain is rough as the path is very steep, but");
            Console.WriteLine("with your eagerness to battle driving you forward, you are unstoppable.");
            Console.WriteLine("");

            Console.WriteLine("A good 15 minutes of walking later, you finally arrive at a plateau at the top");
            Console.WriteLine("of the mountain, where you can clearly see the entrance to Mt. Moon!");

            Text.AnyKey();
        }
    }
}
