﻿using System.Collections.Generic;

namespace PokemonTextEdition
{
    /// <summary>
    /// A list of all the generic trainer NPCs species currently in the game.
    /// </summary>
    class TrainerList
    {
        static public List<Trainer> trainers = new List<Trainer>()
       {
            // 1 - Rival1.cs

           new Trainer(2, "Bug Catcher", "Nick", 95, 
                        "Bug-type Pokemon are the best! Don't underestimate them!", 
                        "What?! How could my Bug-type Pokemon be defeated...",
                        "Ha ha ha! That's what you get for trying to take on a Bug-type expert!", 
                        new List<Pokemon> { new Generator().Create("Caterpie", 5), new Generator().Create("Caterpie", 5) }),

           new Trainer(-2, "Bug Catcher", "Nick", 125, 
                        "I'm stronger than the last time we fought! You'll see!", 
                        "No!!! Why did it turn out the same way again...",
                        "I told you!! You've got to practice lot more to beat me!", 
                        new List<Pokemon> { new Generator().Create("Caterpie", 7), new Generator().Create("Caterpie", 8) }),

           new Trainer(3, "Bug Catcher", "Eric", 85, 
                        "I want to become a Bug-type Gym Leader one day!", 
                        "But, my dream of becoming a Gym Leader...",
                        "I knew I was cut out to become a Gym Leader!", 
                        new List<Pokemon> { new Generator().Create("Kakuna", 6), new Generator().Create("Metapod", 6) }),

           new Trainer(-3, "Bug Catcher", "Eric", 100, 
                        "My dream of becoming a Gym Leader is stronger than before - as am I!", 
                        "Still not good enough to become a Gym Leader, I guess...",
                        "See! I am ready!! My Gym awaits me!", 
                        new List<Pokemon> { new Generator().Create("Kakuna", 8), new Generator().Create("Metapod", 8) }),

           new Trainer(4, "Bug Catcher", "Michael", 110, 
                        "I really love the forest... I wish my parents would let me stay here.", 
                        "Gah, I guess I'll head back home.",
                        "I guess this means I can stay in the forest some more!", 
                        new List<Pokemon> { new Generator().Create("Weedle", 9) }),

           new Trainer(-4, "Bug Catcher", "Michael", 140, 
                        "My parents let me play here again. I'm so happy!", 
                        "I played long enough I guess.",
                        "I can play for a while more, yay!", 
                        new List<Pokemon> { new Generator().Create("Weedle", 11) }),

           new Trainer(5, "Camper", "Fred", 180, 
                        "Welcome to the Pewter Gym! If you wish to challenge Brock, you will have\n to go through 2 of his apprentices first - and I'm the first one!", 
                        "I can tell you're going to give Brock a run for his money. Good luck!", 
                        "You need to practice some more before you can seriously take on Brock!",            
                        new List<Pokemon> { new Generator().Create("Sandshrew", 9) }),

           new Trainer(6, "Hiker", "Dave", 180, 
                        "You must be pretty good to have defeated Fred. Let's see what you got, then!", 
                        "Whoa, no joke, you're good -- but you'll need guts to take on Brock!", 
                        "Huh, I guess you weren't that tough after all! But keep trying, kid.", 
                        new List<Pokemon> { new Generator().Create("Diglett", 10) }),

           // 7 - Brock.cs

           new Trainer(-7, "", "Brock", 250, 
                        "Here for a rematch? Very well - let's see how far you've come!", 
                        "You never fail to impress me! Keep it up and you'll become a star trainer!", 
                        "Aha! It seems you've still got a long way to go. But don't stop trying!", 
                        new List<Pokemon> { new Generator().Create("Geodude", 11), new Generator().Create("Onix", 12) }),

           new Trainer(8, "Youngster", "Lenny", 165, 
                        "Alright, I just beat the Pewter Gym! I'm so ready for anything, bring it on!", 
                        "Oh man! And I was feeling so confident... You're even stronger than Brock!", 
                        "I told you! Nobody can defeat me, I'm on fire!", 
                        new List<Pokemon> { new Generator().Create("Rattata", 11), new Generator().Create("Ekans", 11) }),

           new Trainer(-8, "Youngster", "Lenny", 185, 
                        "I'm warning you, I've gotten a lot stronger since last time! I trained hard!", 
                        "I guess you trained harder! You have to show me how you train someday!", 
                        "I guess all my training paid off, alright!! I'll keep training like this!", 
                        new List<Pokemon> { new Generator().Create("Rattata", 12), new Generator().Create("Ekans", 12) }),

           new Trainer(9, "Youngster", "Timmy", 200, 
                        "I thought I was good for beating Brock, but these trainers are tough...", 
                        "Am I really cut out for this... ?",
                        "Oh... I guess I was stronger than I thought.", 
                        new List<Pokemon> { new Generator().Create("Spearow", 13) }),

           new Trainer(-9, "Youngster", "Timmy", 230, 
                        "I've trained a bit since last time... I think I can win this time...", 
                        "I did a bit better this time though, didn't I... ?",
                        "See, I'm totally stronger than last time... I'm so happy.", 
                        new List<Pokemon> { new Generator().Create("Spearow", 15) }),

           new Trainer(10, "Lass", "Mina", 150, 
                        "I'm taking a break from hiking this mountain. A battle is just what I need!", 
                        "Wow, I can't believe you took on my Nidoran. Oh well, time to move on.",
                        "That was a good battle, thank you. I'd better get back to hiking now.", 
                        new List<Pokemon> { new Generator().Create("Rattata", 12), new Generator().Create("Nidoran♂", 12)  }),

           new Trainer(-10, "Lass", "Mina", 175, 
                        "I decided to stay on the mountain and train a bit more. Show me what you got!", 
                        "You're just as strong as last time, impressive. I'll have to train some more.",
                        "I think I'm ready for Mt. Moon now. Thanks for the battle!", 
                        new List<Pokemon> { new Generator().Create("Rattata", 13), new Generator().Create("Nidoran♂", 13)  }),

           new Trainer(11, "Lass", "Mandy", 220, 
                        "Excuse me, can't you see I'm having a picnic? Oh, you're a trainer? It's on!", 
                        "I guess it was about time to leave anyway. Picnics are the best, though.", 
                        "You interrupted my picnic for this... ? Sigh. Come, Jigglypuff, let's go.", 
                        new List<Pokemon> { new Generator().Create("Jigglypuff", 14)}),

           new Trainer(-11, "Lass", "Mandy", 240, 
                        "Picnics are so fun that I'm having another one. A battle? Fine by me.", 
                        "Owie. Never mind the battle, my stomach is starting to hurt - I ate too much!", 
                        "Yup, nothing more refreshing than a battle after a picnic in the morning!", 
                        new List<Pokemon> { new Generator().Create("Jigglypuff", 16)}),

           new Trainer(11,"Lass", "Lana", 230, 
                        "After searching for so long, I finally ran into a Clefairy, and of course I\n caught it! And now I get to test it out in a battle, it's my lucky day!", 
                        "Oh no! I guess I need to raise my Clefairy first and gain some experience with\n it before it is really fighting-fit... ", 
                        "Haha! Did you see that?! My Clefairy is so strong, it was well worth searching\n for it for all of those long hours!", 
                        new List<Pokemon> { new Generator().Create("Clefairy", 15)}),
            
           new Trainer(-11, "Lass", "Lana", 270, 
                        "I've been training with my Clefairy here at Mt. Moon, its natural habitat.\n This time we certainly won't lose! Let's do it, Clefairy!", 
                        "Ahhh, we lost again! Let's train some more, Clefairy!", 
                        "I told you we'd gotten stronger! Me and Clefairy, we're the strongest team!", 
                        new List<Pokemon> { new Generator().Create("Clefairy", 17)}),

           new Trainer(12, "Super Nerd", "Simon", 250, 
                        "There are strong magnetic forces at work inside this cave, and a lot of static\n electricity stored inside the granite. My Electric-type Pokemon love it!", 
                        "Hold on, let me record the data of our battle on my computer.", 
                        "It was foolish of you to challenge me while my power level is so much higher.", 
                        new List<Pokemon> { new Generator().Create("Magnemite", 11), new Generator().Create("Voltorb", 11)}),

           new Trainer(-12, "Super Nerd", "Simon", 270, 
                        "My Electric-type Pokemon have absorbed a lot of static electricity from these\n halls, and they have become super-charged! We won't lose!", 
                        "Y-Your data, it's on a completely different level than before...", 
                        "I have become stronger thanks to the power of science!", 
                        new List<Pokemon> { new Generator().Create("Magnemite", 13), new Generator().Create("Voltorb", 13)}),
       };
    }
}
