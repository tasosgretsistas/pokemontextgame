using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;

namespace PokemonTextEdition.Locations
{
    class ViridianForestPart1 : Location
    {
        PokemonGenerator generator = new PokemonGenerator();
        

        Trainer nick = TrainerList.AllTrainers.Find(t => t.TrainerID == 2);
        Trainer nickr = TrainerList.AllTrainers.Find(t => t.TrainerID == -2);               

        public ViridianForestPart1()
        {
            Name = "Viridian Forest";
            Type = LocationType.Forest;
            Tag = LocationTag.ViridianForestSouth;

            South = LocationTag.Route2South;
            North = LocationTag.ViridianForestCenter;

            FlavorMessage = "the natural maze";

            Description = "This deep forest is the first major obstacle for most aspiring trainers.\n" + 
                        "Bug-type Pokemon abode, it is very easy to get lost in the thick forest.";

            ConnectionsMessage = "Going south of here would lead you to southern Route 2, while going north\n" + 
                                 "would take you right into the heart of the forest.";

            HelpMessage = "\"north\" or \"go north\" - moves you deeper in the Viridian Forest.\n" +
                          "\"south\" or \"go south\" - moves you to Viridian City.\n" +
                          "\"fight\" - attempts to start a fight with a wild Pokemon.\n" +
                          "\"battle\" - attempts to start a battle with a previously defeated trainer.";
        }

        public override void Trainer()
        { 
            if (nick.HasBeenDefeated(Game.Player))      
                nickr.Encounter();            

            else  
                UI.WriteLine("You need to defeat all of the trainers in this area before using this command!\n");            
        }

        public override void Encounter()
        {
            //Determines which Pokemon the player will encounter.
            int species = Program.random.Next(1, 101);

            //The level range for Caterpie, Weedle and Pidgey.
            int level = Program.random.Next(3, 6);

            //The level range for Metapod and Kakuna.
            int level2 = Program.random.Next(4, 7);             

            Pokemon pokemon;

            //25% probability of a Caterpie.
            if (species <= 25)
                 pokemon = generator.Create("Caterpie", level);

            //25% probability of a Weedle.
            else if (species <= 50)
                 pokemon = generator.Create("Weedle", level);

            //20% probability of a Pidgey.
            else if (species <= 70)
                 pokemon = generator.Create("Pidgey", level);

            //15% probability of a Metapod.
            else if (species <= 85)
                 pokemon = generator.Create("Metapod", level2);

            //15% probability of a Kakuna.
            else
                pokemon = generator.Create("Kakuna", level2);

            Battle battle = new Battle(pokemon);
        }

        public override void GoNorth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //60% probability that the player will encounter a wild Pokemon.
            if (encounter <= 6)
            {
                UI.WriteLine("You decide to trust your direction sense as you go deeper into the forest.\n" +
                             "It seems to go okay for a while, but soon enough you realize that you are\n" +
                             "not where you're supposed to be - you get attacked by a wild Pokemon!\n");

                Encounter();

                UI.WriteLine("Phew, that was a close one back there, but at least both you and your Pokemon\n" +
                             "are doing okay. You decide to be a bit more careful from now on, as you go even\n" +
                             "deeper into this dangerous forest.");                
            }

            //40% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("Using your trusty compass, you carefully maneuver around the dark forest.\n" +
                             "You stay clear off thick foliage and tall grass, as you move due north, in\n" +
                             "the direction of Pewter City. Few minutes later, you find yourself in the\n" +
                             "middle of the forest, just as planned!");
            }

            UI.AnyKey();

            //If the player has not defeated Nick before, he has to battle him.
            if (!nick.HasBeenDefeated(Game.Player))
            {
                UI.WriteLine("Right as you take your first step into the innermost part of the forest,\n" +
                             "you hear something behind you. It's a little kid with a bug-catching net.\n\n" +

                             "\"Hold it right there! I'm a trainer too, and when two trainers spot each\n" +
                              " other, they have to battle! Those are the rules!\"\n");
                
                nick.Encounter();
            }
        }

        public override void GoSouth()
        {
            //Determines if the player will encounter a wild Pokemon while traversing this zone.
            int encounter = Program.random.Next(1, 11);

            //50% probability that the player will encounter a wild Pokemon.
            if (encounter <= 5)
            {
                UI.WriteLine("You get to a crossroads on your way out of the forest, which you don't\n" +
                             "remember seeing before - you're probably lost. As if it couldn't get any\n" + 
                             "worse, a wild Pokemon decides to jump you!\n");

                Encounter();

                UI.WriteLine("Thankfully you weren't fully unprepared for that. Collecting yourself, you\n" +
                             "follow the signs around the forest until you are finally back at Route 2.");
            }

            //50% probability that the player will make it through the zone peacefully.
            else
            {
                UI.WriteLine("Carefully following the path you took when you first went into the forest,\n" +
                             "you navigate towards the southern exit. Not long later, you reach Route 2.");
            }            

            UI.AnyKey();
        }
    }
}
