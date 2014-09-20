using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Locations
{
    class MtMoonPart1 : Location
    {
        Random rng = new Random();
        Generator generator = new Generator();

        public MtMoonPart1()
            : base()
        {
            Name = "Mt. Moon";
            Type = "cave";
            Tag = "route3e";

            West = "route3w";
            East = "mtmoonpt1";

            Description = "the mountain's plateau";
            LongDescription = "Atop the plateau at the east end of this route lies the entrance to Mt. Moon.\nMost people stop at the Pokemon Center to rest after the long journey uphill\nbefore heading into the perilous cave.";
            Connections = "western end of Route 3 to the west and Mt Moon\nto the east";
            HelpMessage = "\"west\" or \"go west\" - moves you to western Route 3.\n\"east\" or \"go east\" - moves you to Mt. Moon.\n\"fight\" - attempts to start a fight with a wild Pokemon.";
        }

        public override void Encounter()
        {
            int level = rng.Next(7, 10);
            int level2 = rng.Next(6, 9);
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
                battle.Wild(generator.Create("Jigglypuff", 8));
            }
            else
            {
                battle.Wild(generator.Create("Mankey", 8));
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
            Console.WriteLine("Not yet implemented.");
            Overworld.LoadLocation(this.Tag);
        }
    }
}
