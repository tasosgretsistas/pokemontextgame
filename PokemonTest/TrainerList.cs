using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class TrainerList
    {
        static public List<Trainer> trainers = new List<Trainer>()
       {
           new Trainer("Bug Catcher", "Nick", "Bug-type Pokemon are the best! Don't underestimate them!", "What?! How could my Bug-type Pokemon be defeated...",
                        "Ha ha ha! That's what you get for trying to take on a Bug-type expert!", new List<Pokemon> { new Generator().Create("Caterpie", 5), new Generator().Create("Caterpie", 5) }, 95, "1"),

           new Trainer("Bug Catcher", "Nick", "I'm stronger than the last time we fought! You'll see!", "No!!! Why did it turn out the same way again...",
                        "I told you!! You've got to practice lot more to beat me!", new List<Pokemon> { new Generator().Create("Caterpie", 7), new Generator().Create("Caterpie", 8) }, 125, "1r"),

           new Trainer("Bug Catcher", "Eric", "I want to become a Bug-type Gym Leader one day!", "But, my dream of becoming a Gym Leader...",
                        "I knew I was cut out to become a Gym Leader!", new List<Pokemon> { new Generator().Create("Kakuna", 6), new Generator().Create("Metapod", 6) }, 85, "2"),

           new Trainer("Bug Catcher", "Eric", "My dream of becoming a Gym Leader is stronger than before - as am I!", "Still not good enough to become a Gym Leader, I guess...",
                       "See! I am ready!! My Gym awaits me!", new List<Pokemon> { new Generator().Create("Kakuna", 8), new Generator().Create("Metapod", 8) }, 100, "2r"),

           new Trainer("Bug Catcher", "Michael", "I really love the forest... I wish my parents would let me stay here.", "Gah, I guess I'll head back home.",
                        "I guess this means I can stay in the forest some more!", new List<Pokemon> { new Generator().Create("Weedle", 9) }, 110, "3"),

           new Trainer("Bug Catcher", "Michael", "My parents let me play here again. I'm so happy!", "I played long enough I guess.",
                        "I can play for a while more, yay!", new List<Pokemon> { new Generator().Create("Weedle", 11) }, 140, "3r"),

           new Trainer("Camper", "Fred", "Welcome to the Pewter Gym! If you wish to challenge Brock, you will have\n to go through 2 of his apprentices first - and I'm the first one!", 
               "I can tell you're going to give Brock a run for his money. Good luck!", "You need to practice some more before you can seriously take on Brock!",            
           new List<Pokemon> { new Generator().Create("Sandshrew", 9) }, 180, "4"),

           new Trainer("Hiker", "Dave", "You must be pretty good to have defeated Fred. Let's see what you got, then!", "Whoa, no joke, you're good -- but you'll need guts to take on Brock!", 
                       "Huh, I guess you weren't that tough after all! But keep trying, kid.", new List<Pokemon> { new Generator().Create("Diglett", 10) }, 180, "5"),

           new Trainer("", "Brock", "Here for a rematch? Very well - let's see how far you've come!", "You never fail to impress me! Keep it up and you'll become a star trainer!", 
                        "Aha! It seems you've still got a long way to go. But don't stop trying!", new List<Pokemon> { new Generator().Create("Geodude", 11), new Generator().Create("Onix", 12) }, 250, "brockr"),

           new Trainer("Youngster", "Lenny", "Alright, I just beat the Pewter Gym! I'm so ready for anything, bring it on!", "Oh man! And I was feeling so confident... You're even stronger than Brock!", 
                        "I told you! Nobody can defeat me, I'm on fire!", new List<Pokemon> { new Generator().Create("Rattata", 11), new Generator().Create("Ekans", 11) }, 165, "6"),

           new Trainer("Youngster", "Lenny", "I'm warning you, I've gotten a lot stronger since last time! I trained hard!", "I guess you trained harder! You have to show me how you train someday!", 
                     "I guess all my training paid off, alright!! I'll keep training like this!", new List<Pokemon> { new Generator().Create("Rattata", 12), new Generator().Create("Ekans", 12) }, 185, "6r"),

           new Trainer("Youngster", "Timmy", "I thought I was good for beating Brock, but these trainers are tough...", "Am I really cut out for this... ?", 
                       "Oh... I guess I was stronger than I thought.", new List<Pokemon> { new Generator().Create("Spearow", 13) }, 200, "7"),

           new Trainer("Youngster", "Timmy", "I've trained a bit since last time... I think I can win this time...", "I did a bit better this time though, didn't I... ?", 
                   "See, I'm totally stronger than last time... I'm so happy.", new List<Pokemon> { new Generator().Create("Spearow", 15) }, 230, "7r"),

           new Trainer("Lass", "Mina", "I'm taking a break from hiking this mountain. A battle is just what I need!", "Wow, I can't believe you took on my Nidoran. Oh well, time to move on.", 
                       "That was a good battle, thank you. I'd better get back to hiking now.", new List<Pokemon> { new Generator().Create("Rattata", 12), new Generator().Create("Nidoran♂", 12)  }, 150, "8"),

           new Trainer("Lass", "Mina", "I decided to stay on the mountain and train a bit more. Show me what you got!", "You're just as strong as last time, impressive. I'll have to train some more.", 
                       "I think I'm ready for Mt. Moon now. Thanks for the battle!", new List<Pokemon> { new Generator().Create("Rattata", 13), new Generator().Create("Nidoran♂", 13)  }, 175, "8r"),

           new Trainer("Lass", "Mandy", "Excuse me, can't you see I'm having a picnic? Oh, you're a trainer? It's on!", "I guess it was about time to leave anyway. Picnics are the best, though.", 
                       "You interrupted my picnic for this... ? Sigh. Come, Jigglypuff, let's go.", new List<Pokemon> { new Generator().Create("Jigglypuff", 14)}, 220, "9"),

           new Trainer("Lass", "Mandy", "Picnics are so fun that I'm having another one. A battle? Fine by me.", "Owie. Never mind the battle, my stomach is starting to hurt - I ate too much!", 
                       "Yup, nothing more refreshing than a battle after a picnic in the morning!", new List<Pokemon> { new Generator().Create("Jigglypuff", 16)}, 240, "9r"),

       };
    }
}
