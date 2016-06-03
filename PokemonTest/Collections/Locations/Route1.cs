using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class Route1 : Location
    {
        PokemonGenerator generator = new PokemonGenerator();

        public Route1()
            : base()
        {
            Name = "Route 1";
            Type = LocationType.Route;
            Tag = LocationTag.Route1;

            North = LocationTag.ViridianCity;
            South = LocationTag.Pallet;

            FlavorMessage = "your first trial";

            Description = "The sound of rustling grass can alarm even the most experienced trainers.\n" + 
                          "Watch your step - you could be ambushed by wild Pokemon in the tall grass!";

            ConnectionsMessage = "A pleasant downhill walk due south leads to Pallet Town, and Viridian City is\n" + 
                                 "is located a short distance off to the north.";

            HelpMessage = "\"north\" or \"go north\" - moves you to Viridian City.\n" +
                          "\"south\" or \"go south\" - moves you to Pallet Town.\n" + 
                          "\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range for Rattata and Pidgey. 
            int level = Program.random.Next(3, 5);            

            Pokemon pokemon;

            //40% probability of a Rattata.
            if (species <= 40)
                pokemon = generator.Create("Rattata", level);

            //60% probability of a Pidgey.
            else
                pokemon = generator.Create("Pidgey", level);

            Battle battle = new Battle(pokemon);
        }

        public override void GoNorth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this route.
            int encounter = Program.random.Next(1, 11);

            //60% probability that the player will encounter a wild Pokemon.
            if (encounter <= 6)
            {
                UI.WriteLine("You take a shortcut through the tall grass on your way north through a path\n" +
                             "you know. You hear rustling in the grass behind you -- it's a wild Pokemon!\n");

                Encounter();

                UI.WriteLine("You are just about exhausted after dealing with the wild Pokemon. The\n" +
                             "shortcut has paid off though - Viridian City looms over the horizon before you!");                
            }

            //40% probability that the player will make it through the route peacefully.
            else
            {
                UI.WriteLine("You decide to stick to the road, avoiding the tall grass. You walk north\n" +
                             "for a couple of minutes, until you can finally see Viridian City!");
            }

            UI.AnyKey();
        }

        public override void GoSouth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this route.         
            int encounter = Program.random.Next(1, 3);

            //50% probability that the player will encounter a wild Pokemon.
            if (encounter <= 1)
            {
                UI.WriteLine("You decide to take the scenic route to Pallet, straight through the tall\n" +
                             "grass. The beautiful natural scenery all around you is mesmerizing - so\n" +
                             "much so that you don't even notice that you've run into a wild Pokemon!\n");

                Encounter();

                UI.WriteLine("You withdraw your Pokemon with a sigh of relief. It wasn't all bad though,\n" +
                             " -- you had fun, gained some valuable experience and you're nearly home!");
            }

            //50% probability that the player will make it through the route peacefully.
            else
            {
                UI.WriteLine("You skillfully jump from ledge to ledge in order to avoid the tall grass.\n" +
                             "After a few minutes of walking, you can finally see your house in Pallet Town!");                
            }

            UI.AnyKey();
        }
    }
}
