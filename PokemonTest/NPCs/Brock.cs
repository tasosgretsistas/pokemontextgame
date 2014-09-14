using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.NPCs
{
    class Brock : Trainer
    {
        Generator generator = new Generator();

        public Brock()
            : base()
        {
            Name = "Gym Leader Brock";
            Greeting = "\n\"I saw you take on my apprentices, and I must say, I'm impressed. But do you\n reckon you are good enough to take on Brock, the master of Rock-type Pokemon?\n I guess we'll just have to find out the hard way!\"";
            DefeatSpeech = "\n\"Amazing! Your style of battle, your technique - it was as if you are one\n with your Pokemon. Here, take this Boulder Badge - you've earned it. With\n this badge, you can use Cut outside of battle, and you can access Mt. Moon.\n Good luck on your journey, and if you ever want a rematch, I'll be waiting.\"";
            VictorySpeech = "\n\"An honest effort, but not quite good enough yet. Feel free to try again!\"";
            party = new List<Pokemon> { generator.Create("Geodude", 9), generator.Create("Onix", 10) };
            Money = 1000;
            ID = "brock";
        }

        public override void Defeat()
        {
            Overworld.player.Badges++;
            Overworld.player.badgeList.Add("Boulder Badge");
            base.Defeat();            
        }
    }
}
