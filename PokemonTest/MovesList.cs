using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class MovesList
    {
        /*
         * EFFECT IDs
         * 1 = % based burn.
         * 2 = % based paralysis.
         * 3 = % based poison.
         * 4 = Move-lock, increased consecutive damage.
         * 5 = Leech Seed.
         * 6 = Poison.
         * 7 = Sleep.
         * 8 = % of damage dealt as recoil.
         * 9 = Multiplier based increased crit chance.
         * 10 = Moves with set damage.
         * 11 = Rapid Spin.
         * 12 = Protect.
         * 13 = Multi-hit moves, with a maximum of N hits.
         * 14 = Disable.
         * 15 = % based confusion.
         */

        //All of the available moves in the game.

        //Normal type.
        static public Moves Tackle = new Moves("Tackle", "Normal", 50, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Scratch = new Moves("Scratch", "Normal", 40, 100, "Physical", 0, false, false, 0, 0);
        static public Moves QuickAttack = new Moves("Quick Attack", "Normal", 40, 100, "Physical", 1, false, false, 0, 0);
        static public Moves Swift = new Moves("Swift", "Normal", 60, 100, "Special", 0, true, false, 0, 0);
        static public Moves Rage = new Moves("Rage", "Normal", 20, 100, "Physical", 0, false, false, 4, 0);
        static public Moves TakeDown = new Moves("Take Down", "Normal", 90, 85, "Physical", 0, false, false, 8, 0.25);
        static public Moves RapidSpin = new Moves("Rapid Spin", "Normal", 20, 100, "Physical", 0, false, true, 11, 0);
        static public Moves Protect = new Moves("Protect", "Normal", 0, 100, "Status", 4, true, false, 12, 0);
        static public Moves BodySlam = new Moves("Body Slam", "Normal", 85, 100, "Physical", 0, false, true, 2, 31);
        static public Moves CometPunch = new Moves("Comet Punch", "Normal", 18, 85, "Physical", 0, false, false, 13, 5);
        static public Moves Cut = new Moves("Cut", "Normal", 70, 95, "Physical", 0, false, false, 0, 0);
        static public Moves Disable = new Moves("Disable", "Normal", 0, 100, "Status", 0, false, false, 14, 0);
        static public Moves DizzyPunch = new Moves("Dizzy Punch", "Normal", 70, 100, "Physical", 0, false, true, 15, 21);
        static public Moves DoubleHit = new Moves("Double Hit", "Normal", 35, 90, "Physical", 0, false, false, 13, 2);
        static public Moves DoubleSlap = new Moves("Double Slap", "Normal", 15, 85, "Physical", 0, false, false, 13, 5);
        static public Moves DoubleEdge = new Moves("Double-Edge", "Normal", 120, 100, "Physical", 0, false, false, 8, 0.33);                

        //Rock type.
        static public Moves Rollout = new Moves("Rollout", "Rock", 30, 90, "Physical", 0, false, false, 4, 1);
        static public Moves RockThrow = new Moves("Rock Throw", "Rock", 50, 90, "Physical", 0, false, false, 0, 0);
        static public Moves RockTomb = new Moves("Rock Tomb", "Rock", 60, 95, "Physical", 0, false, false, 0, 0);

        //Ground type.
        static public Moves MudSlap = new Moves("Mud-Slap", "Ground", 20, 100, "Special", 0, false, false, 0, 0);

        //Ghost type.
        static public Moves Astonish = new Moves("Astonish", "Ghost", 30, 100, "Physical", 0, false, false, 0, 0);

        //Grass type.
        static public Moves VineWhip = new Moves("Vine Whip", "Grass", 45, 100, "Physical", 0, false, false, 0, 0);
        static public Moves RazorLeaf = new Moves("Razor Leaf", "Grass", 55, 95, "Physical", 0, false, false, 9, 2);
        static public Moves LeechSeed = new Moves("Leech Seed", "Grass", 0, 90, "Status", 0, false, false, 5, 0);
        static public Moves SleepPoweder = new Moves("Sleep Powder", "Grass", 0, 75, "Status", 0, false, false, 7, 0);

        //Fire type.
        static public Moves Ember = new Moves("Ember", "Fire", 40, 100, "Special", 0, false, true, 1, 11);
        static public Moves FireFang = new Moves("Fire Fang", "Fire", 65, 95, "Physical", 0, false, true, 1, 11);

        //Water type.
        static public Moves WaterGun = new Moves("Water Gun", "Water", 40, 100, "Special", 0, false, false, 0, 0);
        static public Moves Bubble = new Moves("Bubble", "Water", 40, 100, "Special", 0, false, true, 2, 11);

        //Electric type.
        static public Moves ThunderShock = new Moves("Thunder Shock", "Electric", 40, 100, "Special", 0, false, true, 2, 11);

        //Bug type.
        static public Moves BugBite = new Moves("Bug Bite", "Bug", 60, 100, "Physical", 0, false, false, 0, 0);

        //Poison type.
        static public Moves PoisonSting = new Moves("Poison Sting", "Poison", 15, 100, "Physical", 0, false, true, 3, 31);
        static public Moves PoisonPoweder = new Moves("Poison Powder", "Poison", 0, 75, "Status", 0, false, false, 6, 0);

        //Flying type.
        static public Moves Gust = new Moves("Gust", "Flying", 40, 100, "Special", 0, false, false, 0, 0);
        static public Moves WingAttack = new Moves("Wing Attack", "Flying", 60, 100, "Physical", 0, false, false, 0, 0);

        //Dragon type.
        static public Moves DragonRage = new Moves("Dragon Rage", "Dragon", 0, 100, "Special", 0, false, false, 10, 40);

        //Dark type.
        static public Moves Bite = new Moves("Bite", "Dark", 60, 100, "Physical", 0, false, false, 0, 0);


        /// <summary>
        /// This method simply returns the available moves for every Pokemon, plus what level they learn them at.
        /// </summary>
        /// <param name="name">The name of the Pokemon whose moves are to be returned.</param>
        /// <returns></returns>
        public static Dictionary<Moves, int> PokemonAvailableMoves(string name)
        {            
            Dictionary<Moves, int> moves = new Dictionary<Moves, int>();

            switch (name)
            {
                case "Bulbasaur":
                    moves.Add(Tackle, 1);
                    moves.Add(LeechSeed, 7);
                    moves.Add(VineWhip, 9);
                    moves.Add(PoisonPoweder, 13);
                    moves.Add(SleepPoweder, 13);
                    moves.Add(TakeDown, 15);
                    moves.Add(RazorLeaf, 19);
                    break;

                case "Ivysaur":
                    moves.Add(Tackle, 1);
                    moves.Add(LeechSeed, 7);
                    moves.Add(VineWhip, 9);
                    moves.Add(PoisonPoweder, 13);
                    moves.Add(SleepPoweder, 13);
                    moves.Add(TakeDown, 15);
                    moves.Add(RazorLeaf, 20);
                    break;

                case "Venusaur":
                    moves.Add(Tackle, 1);
                    moves.Add(LeechSeed, 7);
                    moves.Add(VineWhip, 9);
                    moves.Add(PoisonPoweder, 13);
                    moves.Add(SleepPoweder, 13);
                    moves.Add(TakeDown, 15);
                    moves.Add(RazorLeaf, 20);
                    break;

                case "Charmander":
                    moves.Add(Scratch, 1);
                    moves.Add(Ember, 7);
                    moves.Add(DragonRage, 16);
                    moves.Add(FireFang, 25);
                    break;

                case "Charmeleon":
                    moves.Add(Scratch, 1);
                    moves.Add(Ember, 1);
                    moves.Add(DragonRage, 17);
                    moves.Add(FireFang, 28);
                    break;

                case "Charizard":
                    moves.Add(Scratch, 1);
                    moves.Add(Ember, 1);
                    moves.Add(DragonRage, 17);
                    moves.Add(FireFang, 28);
                    break;

                case "Squirtle":
                    moves.Add(Tackle, 1);
                    moves.Add(WaterGun, 7);
                    moves.Add(Bubble, 13);
                    moves.Add(Bite, 16);
                    moves.Add(RapidSpin, 19);
                    moves.Add(Protect, 22);
                    break;

                case "Wartortle":
                    moves.Add(Tackle, 1);
                    moves.Add(WaterGun, 1);
                    moves.Add(Bubble, 13);
                    moves.Add(Bite, 16);
                    moves.Add(RapidSpin, 20);
                    moves.Add(Protect, 24);
                    break;

                case "Blastoise":
                    moves.Add(Tackle, 1);
                    moves.Add(WaterGun, 1);
                    moves.Add(Bubble, 13);
                    moves.Add(Bite, 16);
                    moves.Add(RapidSpin, 20);
                    moves.Add(Protect, 24);
                    break;

                case "Caterpie":
                    moves.Add(Tackle, 1);
                    moves.Add(BugBite, 15);
                    break;

                case "Metapod":
                    moves.Add(Tackle, 1);
                    moves.Add(BugBite, 15);
                    break;

                case "Weedle":
                    moves.Add(PoisonSting, 1);
                    moves.Add(BugBite, 15);
                    break;

                case "Kakuna":
                    moves.Add(PoisonSting, 1);
                    moves.Add(BugBite, 15);
                    break;

                case "Pidgey":
                    moves.Add(Tackle, 1);
                    moves.Add(Gust, 9);
                    break;

                case "Pidgeotto":
                    moves.Add(Gust, 1);
                    moves.Add(Tackle, 1);
                    moves.Add(WingAttack, 7);
                    moves.Add(QuickAttack, 13);
                    break;

                case "Rattata":
                    moves.Add(Tackle, 1);
                    moves.Add(QuickAttack, 4);
                    break;

                case "Pikachu":
                    moves.Add(ThunderShock, 1);
                    moves.Add(QuickAttack, 7);
                    moves.Add(VineWhip, 9);
                    moves.Add(PoisonPoweder, 13);
                    break;

                case "Sandshrew":
                    moves.Add(Scratch, 1);
                    moves.Add(PoisonSting, 5);
                    moves.Add(Rollout, 7);
                    moves.Add(Swift, 9);
                    break;

                case "Diglett":
                    moves.Add(Scratch, 1);
                    moves.Add(Astonish, 7);
                    moves.Add(MudSlap, 9);
                    break;

                case "Geodude":
                    moves.Add(Tackle, 1);
                    moves.Add(Rollout, 9);
                    break;

                case "Onix":
                    moves.Add(Tackle, 1);
                    moves.Add(RockThrow, 7);
                    moves.Add(RockTomb, 10);
                    moves.Add(Rage, 11);
                    break;

                case "Eevee":
                    moves.Add(Tackle, 1);
                    break;
            }

            return moves;
        }

    }
}
