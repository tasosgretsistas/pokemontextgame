using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using PokemonTextEdition.NPCs;

namespace PokemonTextEdition.Locations
{
    class PewterCity : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        Trainer fred = TrainerList.AllTrainers.Find(t => t.TrainerID == 5);

        Trainer dave = TrainerList.AllTrainers.Find(t => t.TrainerID == 6);

        Trainer brock = new Brock();

        Trainer brock2 = TrainerList.AllTrainers.Find(t => t.TrainerID == -7);

        public PewterCity()
        {
            Name = "Pewter City";
            Type = LocationType.City;
            Tag = LocationTag.PewterCity;

            South = LocationTag.Route2North;
            East = LocationTag.Route3West;

            FlavorMessage = "the city between rugged mountains";

            Description = "This quiet city atop the mountainside offers a much needed rest for trainers\n" + 
                          "who just crossed the Viridian Forest. Rest not for too long, however --\n" + 
                          "your first battle with a Gym Leader awaits you here, so be prepared!";

            ConnectionsMessage = "On the southern outskirts of this city lies the northern side of Route 2, and\n" + 
                                 "to the east starts the rocky trail of Route 3.";

            HelpMessage = "\"south\" or \"go south\" - moves you to Route 2.\n" + 
                          "\"east\" or \"go east\" - moves you to Route 3.\n" + 
                          "\"center\" or \"heal\" - takes you to a Pokemon center to heal your Pokemon.\n" + 
                          "\"mart\" - takes you to a Pokemon mart where you can buy and sell items.\n" + 
                          "\"gym\" - takes you to the Pewter City gym.";
        }

        public override void GoSouth()
        {
        }

        public override void GoEast()
        {
            //The player may not progress until Brock has been defeated.
            if (!Game.DefeatedTrainers.Contains(brock.TrainerID))
            {
                UI.WriteLine("Just as you start walking towards the rocky path that leads eastward, a man\n" +
                             "equipped in mountain gear comes running your way.\n\n" +

                             "\"Oi there! The road east is dangerous and many trainers who underestimate it\n" +
                             " quickly find themselves overwhelmed! I suggest that you become at least strong\n" +
                             " enough to defeat Brock at the Pewter Gym before venturing this way.\"\n\n" +

                            "The traveler looks like he knows what he's talking about. You heed his advice\n" +
                            "and head back to Pewter City -- there is a gym waiting to be challenged!");

                Overworld.LoadLocation(LocationTag.PewterCity);
            } 

            else
                UI.WriteLine("Having mastered the Pewter City Gym, you decide to move on eastward, to the" +
                             "perilous Mt. Moon!\n");
        }

        public override void Gym()
        {
            //If Brock hasn't been defeated already, the Gym challenge begins.
            if (!brock.HasBeenDefeated(Game.Player))
            {
                UI.WriteLine("You're standing before a 5-meter tall door - the entrance to the Pewter Gym.\n" + 
                             "You can't help but feel a bit uneasy at the thought of what challenges await.\n\n" +
                             
                             "Are you ready to start your gym challenge? It is recommended that you save.\n" + 
                             "(Valid input: yes to begin, enter to return)");

                string decision = UI.ReceiveInput();

                switch (decision.ToLower())
                {
                    case "yes":
                    case "y":

                        //If the player has not defeated Fred before, he has to battle him.
                        if (!fred.HasBeenDefeated(Game.Player))
                        {
                            fred.Encounter();

                            UI.WriteLine("\nYou decide to take a short break after that difficult fight.\n");
                        }

                        //If the player has not defeated Dave before, he has to battle him.
                        else if (!dave.HasBeenDefeated(Game.Player))
                        {
                            dave.Encounter();

                            UI.WriteLine("\nYou decide to take a short break after that difficult fight.\n");
                        }

                        //Finally, if the player has defeated both Fred and Dave, he may challenge Brock.
                        else if (!brock.HasBeenDefeated(Game.Player))
                        {
                            brock.Encounter();

                            UI.WriteLine("You've done it! You've defeated the first Gym Leader! You can't help feeling" +
                                         "hyped after that one. Next stop, the Cerulean City gym!\n");
                        }

                        break;

                    default:
                        
                        break;
                }
            }

            //Else, if the player has already defeated Brock, he gets to fight him again.
            else
            {
                UI.WriteLine("");
                brock2.Encounter();
            }
        }
    }
}
