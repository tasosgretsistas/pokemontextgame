using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class PewterCity : Location
    {
        Generator generator = new Generator();

        Trainer fred = TrainerList.trainers.Find(t => t.ID == "4");

        Trainer dave = TrainerList.trainers.Find(t => t.ID == "5");

        Trainer brock = new NPCs.Brock();

        Trainer brock2 = TrainerList.trainers.Find(t => t.ID == "brockr");

        public PewterCity()
        {
            Name = "Pewter City";
            Type = "city";
            Tag = "pewter";

            South = "route2n";
            East = "route3w";

            Description = "the city between rugged mountains";
            LongDescription = "This quiet city atop the mountainside offers a much needed rest for trainers\nwho just crossed the Viridian Forest. Rest not for too long, however --\nyour first battle with a Gym Leader awaits you here, so be prepared!";
            ConnectionsMessage = "On the southern outskirts of this city lies the northern side of Route 2, and\nto the east starts the rocky trail of Route 3.";
            HelpMessage = "\"south\" or \"go south\" - moves you to Route 2.\n\"east\" or \"go east\" - moves you to Route 3.\n\"center\" or \"heal\" - takes you to a Pokemon center to heal your Pokemon.\n\"mart\" - takes you to a Pokemon mart where you can buy and sell items.\n\"gym\" - takes you to the Pewter City gym.";
        }

        public override void GoSouth()
        {
        }

        public override void GoEast()
        {
            if (!Overworld.player.defeatedTrainers.Contains("brock"))
            {
                Console.WriteLine("Just as you start walking towards the rocky path that leads eastward, a man");
                Console.WriteLine("equipped in mountain gear comes running at you.");
                Console.WriteLine("");

                Console.WriteLine("\"Oi there! The road east is dangerous and many trainers who underestimate it");
                Console.WriteLine(" quickly find themselves overwhelmed! I suggest you at least become strong");
                Console.WriteLine(" enough to defeat Brock at the Pewter Gym before venturing this way.\"");

                Console.WriteLine("The traveler looks like he knows what he's talking about. You heed his advice");
                Console.WriteLine("and head back to Pewter City -- there is a gym waiting to be challenged!");

                Overworld.LoadLocation("pewter");
            } 

            else
            {
            }
        }

        public override void Gym()
        {
            if (!brock.HasBeenDefeated)
            {
                Console.WriteLine("You're standing before a 5-meter tall door - the entrance to the Pewter Gym.\nYou can't help but feel a bit uneasy at the thought of what challenges await.\n");

                Console.WriteLine("Are you ready to start your gym challenge? It is recommended that you save.\n(Valid input: yes to begin, enter to return)");

                string decision = Console.ReadLine();

                switch (decision)
                {
                    case "Yes":
                    case "yes":
                    case "y":

                        if (!fred.HasBeenDefeated)
                        {
                            fred.Encounter();

                            Console.WriteLine("\nYou decide to take a short break after that difficult fight.\n");
                        }

                        else if (!dave.HasBeenDefeated)
                        {
                            dave.Encounter();

                            Console.WriteLine("\nYou decide to take a short break after that difficult fight.\n");
                        }

                        else if (!brock.HasBeenDefeated)
                        {
                            brock.Encounter();

                            Console.WriteLine("You've done it! You've defeated the first Gym Leader! You can't help feeling");
                            Console.WriteLine("hyped after that one. Next stop, the Cerulean City gym!\n");
                        }

                        break;

                    default:

                        if (decision != "")
                            Console.WriteLine("");
                        break;
                }
            }

            else
            {
                brock2.Encounter();
            }
        }
    }
}
