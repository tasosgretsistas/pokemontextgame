using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;
using System.Linq;

namespace PokemonTextEdition.Locations
{
    class Route3W : Location
    {
        Random random = new Random();
        PokemonGenerator generator = new PokemonGenerator();

        Trainer lenny = TrainerList.AllTrainers.Find(t => t.TrainerID == 8);
        Trainer timmy = TrainerList.AllTrainers.Find(t => t.TrainerID == 9);
        Trainer mina = TrainerList.AllTrainers.Find(t => t.TrainerID == 10);
        Trainer mandy = TrainerList.AllTrainers.Find(t => t.TrainerID == 11);

        Trainer lenny2 = TrainerList.AllTrainers.Find(t => t.TrainerID == -8);
        Trainer timmy2 = TrainerList.AllTrainers.Find(t => t.TrainerID == -9);
        Trainer mina2 = TrainerList.AllTrainers.Find(t => t.TrainerID == -10);
        Trainer mandy2 = TrainerList.AllTrainers.Find(t => t.TrainerID == -11);

        public Route3W()
            : base()
        {
            Name = "Route 3";
            Type = LocationType.Route;
            Tag = LocationTag.Route3West;

            West = LocationTag.PewterCity;
            East = LocationTag.Route3East;

            FlavorMessage = "the rocky road";

            Description = "This rocky path marks the beginning of the long mountain trail that leads to\n" +
                          "Mt. Moon. Many strong trainers head this way after emerging victorious from\n" +
                          "the Pewter City gym. Be careful as you challenge them!";

            ConnectionsMessage = "Pewter City is close by to the west of here, while going east finds one at the\n" +
                                 "eastern end of Route 3 and the entrance to Mt. Moon.";

            HelpMessage = "\"west\" or \"go west\" - moves you to Pewter City.\n" +
                          "\"east\" or \"go east\" - moves you to eastern Route 2.\n" +
                          "\"fight\" - attempts to start a fight with a wild Pokemon.\n" +
                          "\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        {
            if (mandy.HasBeenDefeated(Overworld.Player))
            {
                UI.WriteLine("Which trainer would you like to have a rematch with?\n" +
                             "(Available trainers: Lenny, Timmy, Mina, Mandy)");

                string input = UI.ReceiveInput();

                switch (input.ToLower())
                {
                    case "lenny":

                        lenny2.Encounter();

                        break;

                    case "timmy":

                        timmy2.Encounter();

                        break;

                    case "mina":

                        mina2.Encounter();

                        break;

                    case "mandy":

                        mandy2.Encounter();

                        break;
                }
            }

            else
                UI.WriteLine("You need to defeat all of the trainers in this area before using this command!\n");
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = random.Next(1, 101);

            //The level range for Nidoran♀ and Nidoran♂.
            int level = random.Next(5, 8);

            //The level range for Spearow and Pidgey.
            int level2 = random.Next(6, 9);

            Pokemon pokemon;

            //25% probability of a Spearow.
            if (species <= 25)
                pokemon = generator.Create("Spearow", level);

            //25% probability of a Pidgey.
            else if (species <= 50)
                pokemon = generator.Create("Pidgey", level);

            //15% probability of a Nidoran♀.
            else if (species <= 65)
                pokemon = generator.Create("Nidoran♀", level2);

            //15% probability of a Nidoran♂.
            else if (species <= 70)
                pokemon = generator.Create("Nidoran♂", level2);

            //20% probability of a Mankey.
            else if (species <= 90)
                pokemon = generator.Create("Mankey", 7);

            //10% probability of a Jigglypuff.
            else
                pokemon = generator.Create("Jigglypuff", 7);
        }

        public override void GoWest()
        {
            UI.WriteLine("You follow the beaten path and eventually find yourself back at Pewter City.\n");
        }

        public override void GoEast()
        {
            UI.WriteLine("As you reach the midway point of the mountain's trail, you see a small part of\n" +
                         "level land where multiple trainers have congregated. Most of them will probably\n" +
                         "want to take you on, and you're going to give them a run for their money!");

            UI.AnyKey();            

            //First set of trainers.
            if (!timmy.HasBeenDefeated(Overworld.Player))
            {
                //If the player has not defeated Lenny before, he has to battle him.
                if (!lenny.HasBeenDefeated(Overworld.Player))
                {
                    UI.WriteLine("The first trainer to walk up to you is a little kid who's holding a school bag.\n" +
                                 "He doesn't look too strong, but if he's here, he's beaten the Pewter Gym, so he\n" +
                                 "can't be weak either. Better not underestimate him!\n");

                    lenny.Encounter();

                    UI.WriteLine("An Ekans, huh? You've never seen that Pokemon before, as it's definitely not\n" +
                                 "native to Pallet Town. How many more new Pokemon are you going to encounter,\n" +
                                 "you think to yourself as you move on.");

                    UI.AnyKey();
                }

                UI.WriteLine("Another kid with a schoolbag walks up to you, but this one looks less confident\n" +
                             "than the previous one. You have learnt not to underestimate anybody though, so\n" +
                             "you greet him with a nod and quickly grab the Pokeball containing " + Overworld.Player.party.ElementAt(0) + ".\n");

                //Then, he has to battle Timmy.
                timmy.Encounter();

                UI.WriteLine("That was no easy fight, and it looks like there's more to go. Your heart is\n" +
                             "racing as you start imagining the intense battles that are yet to come.\n\n " +

                             "You decide to give your Pokemon a small break from the constant battles, but\n" +
                             "you can't wait until your next battle.");

                UI.AnyKey();

                Overworld.LoadLocation(LocationTag.Route3West);
            }

            //Second set of trainers.
            if (!mandy.HasBeenDefeated(Overworld.Player))
            {
                //If the player has not defeated Mina before, he has to battle her.
                if (!mina.HasBeenDefeated(Overworld.Player))
                {
                    UI.WriteLine("A teenage girl walks up to you this time. She has an aura of confidence to her,\n" +
                                 "one that tells you that you should by no means underestimate her. After all,\n" +
                                 "this is one of the highest points on the mountain - only the elite are here.\n");

                    mina.Encounter();

                    UI.WriteLine("That girl really meant business there. You won, but you can't help having the\n" +
                                 "impression that you need to step up your Pokemon training game yet. One more\n" +
                                 "ought to prove invaluable in giving you Pokemon battling experience.");

                    UI.AnyKey();
                }


                UI.WriteLine("The last trainer that you can see is a girl who's seemingly having a picnic at\n" +
                             "the side of the mountain trail, on a large grass patch. She doesn't seem to\n" +
                             "have noticed you, so you approach her instead. You only briefly see her face,\n" +
                             "but damn... she's really pretty.");

                UI.AnyKey();

                UI.WriteLine("Ugh, this is no time to be gawking at girls. There's a pink, round Pokemon that\n" +
                             "you have never seen before next to her, so she must be a trainer - and a good\n" +
                             "one at it. You reach out for your Pokeballs and step in front of her.\n");

                //Then, he has to battle Mandy.
                mandy.Encounter();

                UI.WriteLine("Your entire body is shaking with excitement after that one, and you're fairly\n" +
                             "sure it's not just because you've fallen in love with that girl back there. But\n" +
                             "enough of that, it's time to move on - onward to Mt. Moon!\n");

                UI.AnyKey();

            }

            UI.WriteLine("You leave the part where all the trainer battles are happening and head back to\n" +
                         "the main path. The hike up the mountain is rough as the path is very steep, but\n" +
                         "with your eagerness to battle driving you forward, you are unstoppable.\n\n" +

                         "A good 15 minutes of walking later, you finally arrive at a plateau at the top\n" +
                         "of the mountain, where you can clearly see the entrance to Mt. Moon!");

            UI.AnyKey();
        }
    }
}
