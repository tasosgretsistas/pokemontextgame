using PokemonTextEdition.Classes;
using System.Collections.Generic;

namespace PokemonTextEdition.NPCs
{
    class Brock : Trainer
    {
        PokemonGenerator generator = new PokemonGenerator();

        public Brock()
            : base()
        {
            Name = "Gym Leader Brock";

            Greeting = "\n\"I saw you take on my apprentices, and I must say, I'm impressed. But do you\n reckon you are good enough to take on Brock, the master of Rock-type Pokemon?\n I guess we'll just have to find out the hard way!\"";

            DefeatSpeech = "\n\"Amazing! Your style of battle, your technique - it was as if you are one\n with your Pokemon.\n\n Here, take this Boulder Badge - you've earned it. With this badge, you can\n use Cut outside of battle, and you can access Mt. Moon to the east. You'll\n want to head that way in order to get to Cerulean City and challenge the gym.\n\n Good luck on your journey, and if you ever want a rematch, I'll be waiting.\"\n";
            VictorySpeech = "\n\"An honest effort, but not quite good enough yet. Feel free to try again!\"";

            Party = new List<Pokemon> { generator.Create("Geodude", 9), generator.Create("Onix", 10) };

            Money = 1200;
            TrainerID = 7;
        }

        public override void Defeat(Player player)
        {
            player.Badges.Add("Boulder Badge");
            base.Defeat(player);            
        }
    }
}
