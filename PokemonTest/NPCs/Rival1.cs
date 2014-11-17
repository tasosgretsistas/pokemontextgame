using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.NPCs
{
    class Rival1 : Trainer
    {
        public Rival1() :base()
        {
            Name = "Gary";
            Type = "Pokemon Trainer";
            Greeting = "You shouldn't be seeing this! if you do, contact the creator - (Ref: Rival1)";
            DefeatSpeech = "You shouldn't be seeing this! if you do, contact the creator - (Ref: Rival1)";
            VictorySpeech = "You shouldn't be seeing this! if you do, contact the creator - (Ref: Rival1)";
            party = new List<Pokemon> {  };
            Money = 500;
            ID = "rival1";
        }

        //Narration after the rival battle, with different dialogue based on the result of the battle.

        public override void Defeat()
        {
            Console.WriteLine("{0} looks devastated by his defeat.", Name);
            Console.WriteLine("He quickly withdraws {0} back into its Pokeball.", party.ElementAt(0).Name);
            Console.WriteLine("");

            Console.WriteLine("\"This can't be happening! Did I make the wrong choice, picking {0}?", party.ElementAt(0).Name);
            Console.WriteLine(" Gah...\"");
        }

        public override void Victory()
        {
            Console.WriteLine("{0} has a smug look on his face as he looks at you.", Name);
            Console.WriteLine("");

            Console.WriteLine("\"Ha ha ha! I told you {0} is a weak Pokemon! ", Overworld.player.party.ElementAt(0).Name);
            Console.WriteLine("\n My {0} was clearly stronger!\"", party.ElementAt(0).Name);
        }

    }
}
