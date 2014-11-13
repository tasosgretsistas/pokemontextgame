using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class Route1 : Location
    {
        Random rng = new Random();
        Generator generator = new Generator();

        public Route1()
            : base()
        {
            Name = "Route 1";
            Type = "route";
            Tag = "route1";

            North = "viridian";
            South = "pallet";

            Description = "your first trial";
            LongDescription = "The sound of rustling grass can alarm even the most experienced trainers.\nWatch your step - you could be ambushed by wild Pokemon in the tall grass!";
            ConnectionsMessage = "A pleasant downhill walk due south leads to Pallet Town, and Viridian City is\nis located a short distance off to the north.";            
            HelpMessage = "\"north\" or \"go north\" - moves you to Viridian City.\n\"south\" or \"go south\" - moves you to Pallet Town.\n\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            int level = rng.Next(3, 5);
            int species = rng.Next(1, 101);

            Battle battle = new Battle();

            if (species > 70)
            {
                battle.Wild(generator.Create("Rattata", level));
            }
            else
            {
                battle.Wild(generator.Create("Pidgey", level));
            }
        }

        public override void GoNorth()
        {
            int encounter = rng.Next(1, 11);

            if (encounter > 6)
            {
                Console.WriteLine("You decide to stick to the road, avoiding the tall grass. You walk north");
                Console.WriteLine("for a couple of minutes, until you can finally see Viridian City!");
            }

            else
            {
                Console.WriteLine("You take a shortcut through the tall grass on your way north through a path");
                Console.WriteLine("you know. You hear rustling in the grass behind you -- it's a wild Pokemon!");
                Console.WriteLine("");

                Encounter();

                Console.WriteLine("You are just about exhausted after dealing with the wild Pokemon. The");
                Console.WriteLine("shortcut has paid off though - in the horizon before you looms Viridian City!");
            }

            Story.AnyKey();
        }

        public override void GoSouth()
        {            
            int encounter = rng.Next(1, 11);

            if (encounter > 5)
            {
                Console.WriteLine("You skillfully jump from ledge to ledge in order to avoid the tall grass.");
                Console.WriteLine("After a few minutes of walking, you can finally see your house in Pallet!");
            }

            else
            {
                Console.WriteLine("You decide to take the scenic route to Pallet, straight through the tall");
                Console.WriteLine("grass. The beautiful natural scenery all around you is mesmerizing - so");
                Console.WriteLine("much so that you don't even notice that you've run into a wild Pokemon!");
                Console.WriteLine("");
                
                Encounter();

                Console.WriteLine("You withdraw your Pokemon with a sigh of relief. It wasn't all bad though,");
                Console.WriteLine("-- you had fun, gained some valuable experience and you're nearly home!");
            }

            Story.AnyKey();
        }
    }
}
