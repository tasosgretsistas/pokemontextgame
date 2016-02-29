using PokemonTextEdition.Classes;
using PokemonTextEdition.Engine;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.NPCs
{
    class Rival1 : Trainer
    {
        public Rival1() :base()
        {
            Name = Overworld.Player.RivalName;
            Type = "Pokemon Trainer";

            Party = new List<Pokemon> {  };

            Money = 500;
            TrainerID = 1;
        }

        //Narration after the rival battle, with different dialogue based on the result of the battle.

        public override void Defeat(Player player)
        {
            UI.WriteLine(Name + " looks devastated by his defeat.");
            UI.WriteLine("He quickly withdraws " + Party.ElementAt(0).Name + " back into its Pokeball.\n");

            UI.WriteLine("\"This can't be happening! Did I make the wrong choice, picking " + Party.ElementAt(0).Name  + "? ");
            UI.WriteLine(" Gah...\"");
        }

        public override void Victory(Player player)
        {
            UI.WriteLine(Name + " has a smug look on his face as he looks at you.\n");

            UI.WriteLine("\"Ha ha ha! I told you " + Overworld.Player.StartingPokemon + " is a weak Pokemon! ");
            UI.WriteLine(" My " + Party.ElementAt(0).Name + " was clearly stronger!\"");
        }

    }
}
