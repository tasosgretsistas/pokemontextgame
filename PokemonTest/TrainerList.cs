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
           new Trainer("Bug Catcher Nick", "Bug-type Pokemon are the best! Don't underestimate them!", "What?! How could my Bug-type Pokemon be defeated...",
                        "Ha ha ha! That's what you get for trying to take on a Bug-type expert!", new List<Pokemon> { new Generator().Create("Caterpie", 5), new Generator().Create("Caterpie", 5) }, 150, "1"),

           new Trainer("Bug Catcher Nick", "I'm stronger than the last time we fought! You'll see!", "No!!! Why did it turn out the same way again...",
                        "I told you!! You've got to practice lot more to beat me!", new List<Pokemon> { new Generator().Create("Caterpie", 7), new Generator().Create("Caterpie", 8) }, 200, "1r"),

           new Trainer("Bug Catcher Eric", "I want to become a Bug-type Gym Leader one day!", "But, my dream of becoming a Gym Leader...",
                        "I knew I was cut out to become a Gym Leader!", new List<Pokemon> { new Generator().Create("Kakuna", 6), new Generator().Create("Metapod", 6) }, 150, "2"),

           new Trainer("Bug Catcher Eric", "My dream of becoming a Gym Leader is stronger than before - as am I!", "Still not good enough to become a Gym Leader, I guess...",
                       "See! I am ready!! My Gym awaits me!", new List<Pokemon> { new Generator().Create("Kakuna", 8), new Generator().Create("Metapod", 8) }, 200, "2r"),

           new Trainer("Bug Catcher Michael", "I really love the forest... I wish my parents would let me stay here.", "Gah, I guess I'll head back home.",
                        "I guess this means I can stay in the forest some more!", new List<Pokemon> { new Generator().Create("Weedle", 9) }, 200, "3"),

           new Trainer("Bug Catcher Michael", "My parents let me play here again. I'm so happy!", "I played long enough I guess.",
                        "I can play for a while more, yay!", new List<Pokemon> { new Generator().Create("Weedle", 11) }, 250, "3r"),

           new Trainer("Camper Fred", "Welcome to the Pewter Gym! If you wish to challenge Brock, you will have\n to go through 2 of his apprentices first - and I'm the first one!", 
               "I can tell you're going to give Brock a run for his money. Good luck!", "You need to practice some more before you can seriously take on Brock!",            
           new List<Pokemon> { new Generator().Create("Sandshrew", 9) }, 150, "4"),

           new Trainer("Hiker Dave", "You must be pretty good to have defeated Fred. Let's see what you got, then!", "Whoa, no joke, you're good -- but you'll need guts to take on Brock!", 
        "Huh, I guess you weren't that tough after all! But keep trying, kid.", new List<Pokemon> { new Generator().Create("Diglett", 10) }, 200, "5"),

           new Trainer("Brock", "Here for a rematch? Very well - let's see how far you've come!", "You never fail to impress me! Keep it up and you'll become a star trainer!", 
    "Aha! It seems you've still got a long way to go. But don't stop trying!", new List<Pokemon> { new Generator().Create("Geodude", 11), new Generator().Create("Onix", 12) }, 200, "brockr"),

       };
    }
}
