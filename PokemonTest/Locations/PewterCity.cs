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
            East = "route3";

            Description = "the city between rugged mountains";
            LongDescription = "This quiet city atop the mountainside offers a much needed rest for trainers\nwho just crossed the Viridian Forest. Rest not for too long, however --\nyour first battle with a Gym Leader awaits you here, so be prepared!";
            Connections = "Route 2 to the south and Route 3 to the east";
            HelpMessage = "\"south\" or \"go south\" - moves you to Route 2.\n\"east\" or \"go east\" - moves you to Route 3.\n\"center\" or \"heal\" - takes you to a Pokemon center to heal your Pokemon.\n\"mart\" - takes you to a Pokemon mart where you can buy and sell items.\n\"gym\" - takes you to the Pewter City gym.";
        }

        public override void GoSouth()
        {
        }

        public override void GoEast()
        {
            Console.WriteLine("This location is not currently in the game.");
            Overworld.LoadLocation("pewter");
        }

        public override void Gym()
        {
            if (!brock.Defeated())
            {
                Console.WriteLine("\nYou're standing before a 5-meter tall door - the entrance to the Pewter Gym.\nYou can't help but feel a bit uneasy at the thought of what challenges await.");

                Console.WriteLine("\nAre you ready to start your gym challenge? It is recommended that you save.\n(Valid input: yes to begin, enter to return)");

                string decision = Console.ReadLine();

                if (decision == "yes")
                {
                    if (!fred.Defeated())
                    {
                        fred.Encounter();

                        Console.WriteLine("\nYou decide to take a short break after that difficult fight.");
                    }

                    else if (!dave.Defeated())
                    {
                        dave.Encounter();

                        Console.WriteLine("\nYou decide to take a short break after that difficult fight.");
                    }

                    else if (!brock.Defeated())
                    {
                        brock.Encounter();

                        Console.WriteLine("\nWhoa - you've really done it, you've defeated the first Gym Leader.\nOnly 7 more to go!");
                        Console.WriteLine("\nThe current version of the game only goes up to this point. Thanks for playing!");
                    }
                }

                else if (decision == "no" || decision == "")
                {
                }

                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            else
            {
                brock2.Encounter();
            }
        }
    }
}
