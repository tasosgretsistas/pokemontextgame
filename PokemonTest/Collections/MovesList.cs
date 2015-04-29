using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class MovesList
    {
        //All of the available moves in the game.

        #region Effect IDs
        /*
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
         * 16 = Pursuit.
         * 17 - Paralysis.
         * 18 - Item steal.
         * 19 - Moves with set damage equal to the Pokemon's level.
         * 20 - Confusion.
         */
        #endregion

        #region Normal Type
        static public Moves Tackle = new Moves("Tackle", "Normal", 50, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Scratch = new Moves("Scratch", "Normal", 40, 100, "Physical", 0, false, false, 0, 0);
        static public Moves QuickAttack = new Moves("Quick Attack", "Normal", 40, 100, "Physical", 1, false, false, 0, 0);
        static public Moves Swift = new Moves("Swift", "Normal", 60, 100, "Special", 0, true, false, 0, 0);
        static public Moves Rage = new Moves("Rage", "Normal", 20, 100, "Physical", 0, false, false, 4, 0);
        static public Moves TakeDown = new Moves("Take Down", "Normal", 90, 85, "Physical", 0, false, false, 8, 0.25f);
        static public Moves RapidSpin = new Moves("Rapid Spin", "Normal", 20, 100, "Physical", 0, false, true, 11, 0);
        static public Moves Protect = new Moves("Protect", "Normal", 0, 100, "Status", 4, true, false, 12, 0);
        static public Moves BodySlam = new Moves("Body Slam", "Normal", 85, 100, "Physical", 0, false, true, 2, 31);
        static public Moves CometPunch = new Moves("Comet Punch", "Normal", 18, 85, "Physical", 0, false, false, 13, 5);
        static public Moves Cut = new Moves("Cut", "Normal", 70, 95, "Physical", 0, false, false, 0, 0);
        static public Moves Disable = new Moves("Disable", "Normal", 0, 100, "Status", 0, false, false, 14, 0);
        static public Moves DizzyPunch = new Moves("Dizzy Punch", "Normal", 70, 100, "Physical", 0, false, true, 15, 21);
        static public Moves DoubleHit = new Moves("Double Hit", "Normal", 35, 90, "Physical", 0, false, false, 13, 2);
        static public Moves DoubleSlap = new Moves("Double Slap", "Normal", 15, 85, "Physical", 0, false, false, 13, 5);
        static public Moves DoubleEdge = new Moves("Double-Edge", "Normal", 120, 100, "Physical", 0, false, false, 8, 0.33f);
        static public Moves Pound = new Moves("Pound", "Normal", 40, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Sing = new Moves("Sing", "Normal", 0, 55, "Status", 0, false, false, 7, 0);
        static public Moves FuryAttack = new Moves("Fury Attack", "Normal", 15, 85, "Physical", 0, false, false, 13, 5);
        static public Moves FurySwipes = new Moves("Fury Swipes", "Normal", 18, 80, "Physical", 0, false, false, 13, 5);
        static public Moves HornAttack = new Moves("Horn Attack", "Normal", 65, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Wrap = new Moves("Wrap", "Normal", 15, 90, "Physical", 0, false, false, 13, 5);
        static public Moves Glare = new Moves("Glare", "Normal", 0, 100, "Status", 0, false, false, 17, 0);
        static public Moves Covet = new Moves("Covet", "Normal", 60, 100, "Physical", 0, false, true, 18, 0);
        static public Moves Supersonic = new Moves("Supersonic", "Normal", 0, 55, "Status", 0, false, false, 20, 0);
        static public Moves HyperFang = new Moves("Hyper Fang", "Normal", 80, 90, "Physical", 0, false, true, 3, 16);
        #endregion

        #region Rock Type
        static public Moves Rollout = new Moves("Rollout", "Rock", 30, 90, "Physical", 0, false, false, 4, 1);
        static public Moves RockThrow = new Moves("Rock Throw", "Rock", 50, 90, "Physical", 0, false, false, 0, 0);
        static public Moves RockTomb = new Moves("Rock Tomb", "Rock", 60, 95, "Physical", 0, false, false, 0, 0);
        #endregion

        #region Ground Type
        static public Moves MudSlap = new Moves("Mud-Slap", "Ground", 20, 100, "Special", 0, false, false, 0, 0);
        #endregion

        #region Ghost Type
        static public Moves Astonish = new Moves("Astonish", "Ghost", 30, 100, "Physical", 0, false, false, 0, 0);
        #endregion

        #region Grass Type
        static public Moves VineWhip = new Moves("Vine Whip", "Grass", 45, 100, "Physical", 0, false, false, 0, 0);
        static public Moves RazorLeaf = new Moves("Razor Leaf", "Grass", 55, 95, "Physical", 0, false, false, 9, 2);
        static public Moves LeechSeed = new Moves("Leech Seed", "Grass", 0, 90, "Status", 0, false, false, 5, 0);
        static public Moves SleepPowder = new Moves("Sleep Powder", "Grass", 0, 75, "Status", 0, false, false, 7, 0);
        static public Moves StunSpore = new Moves("Stun Spore", "Grass", 0, 75, "Status", 0, false, false, 17, 0);
        #endregion

        #region Fire Type
        static public Moves Ember = new Moves("Ember", "Fire", 40, 100, "Special", 0, false, true, 1, 11);
        static public Moves FireFang = new Moves("Fire Fang", "Fire", 65, 95, "Physical", 0, false, true, 1, 11);
        #endregion

        #region Water Type
        static public Moves WaterGun = new Moves("Water Gun", "Water", 40, 100, "Special", 0, false, false, 0, 0);
        static public Moves Bubble = new Moves("Bubble", "Water", 40, 100, "Special", 0, false, true, 2, 11);
        #endregion

        #region Electric Type
        static public Moves ThunderShock = new Moves("Thunder Shock", "Electric", 40, 100, "Special", 0, false, true, 2, 11);
        static public Moves ThunderWave = new Moves("Thunder Wave", "Electric", 0, 100, "Status", 0, false, false, 17, 0);
        static public Moves ElectroBall = new Moves("Electro Ball", "Electric", 90, 100, "Special", 0, false, false, 0, 0);
        #endregion

        #region Fighting Type
        static public Moves DoubleKick = new Moves("Double Kick", "Fighting", 30, 100, "Physical", 0, false, false, 13, 2);
        static public Moves KarateChop = new Moves("Karate Chop", "Fighting", 50, 100, "Physical", 0, false, false, 9, 2);
        static public Moves SeismicToss = new Moves("Seismic Toss", "Fighting", 0, 100, "Physical", 0, false, false, 19, 0);
        static public Moves LowKick = new Moves("Low Kick", "Fighting", 70, 100, "Physical", 0, false, false, 0, 0);
        #endregion

        #region Bug Type
        static public Moves BugBite = new Moves("Bug Bite", "Bug", 60, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Twineedle = new Moves("Twineedle", "Bug", 60, 100, "Physical", 0, false, true, 3, 21);
        #endregion

        #region Poison Type
        static public Moves PoisonSting = new Moves("Poison Sting", "Poison", 15, 100, "Physical", 0, false, true, 3, 31);
        static public Moves Acid = new Moves("Acid", "Poison", 40, 100, "Special", 0, false, true, 3, 16);
        static public Moves PoisonPowder = new Moves("Poison Powder", "Poison", 0, 75, "Status", 0, false, false, 6, 0);
        #endregion

        #region Psychic Type
        static public Moves Confusion = new Moves("Confusion", "Psychic", 50, 100, "Special", 0, false, true, 15, 16);
        #endregion

        #region Flying Type
        static public Moves Gust = new Moves("Gust", "Flying", 40, 100, "Special", 0, false, false, 0, 0);
        static public Moves Peck = new Moves("Peck", "Flying", 35, 100, "Physical", 0, false, false, 0, 0);
        static public Moves WingAttack = new Moves("Wing Attack", "Flying", 60, 100, "Physical", 0, false, false, 0, 0);
        static public Moves AerialAce = new Moves("Aerial Ace", "Flying", 60, 100, "Physical", 0, true, false, 0, 0);
        #endregion

        #region Dragon Type
        static public Moves DragonRage = new Moves("Dragon Rage", "Dragon", 0, 100, "Special", 0, false, false, 10, 40);
        static public Moves Twister = new Moves("Twister", "Dragon", 40, 100, "Special", 0, false, false, 0, 0);
        #endregion

        #region Dark Type
        static public Moves Bite = new Moves("Bite", "Dark", 60, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Crunch = new Moves("Crunch", "Dark", 80, 100, "Physical", 0, false, false, 0, 0);
        static public Moves Pursuit = new Moves("Pursuit", "Dark", 40, 100, "Physical", 0, false, false, 16, 0);
        #endregion

        //DEBUG

        #region Test Type

        static public Moves test1 = new Moves("Test Move - MovesList.cs", "Normal", 60, 100, "Physical", 0, false, false, 0, 0);
        static public Moves test2 = new Moves("Test Move - Generator.cs", "Normal", 60, 100, "Physical", 0, false, false, 0, 0);

        #endregion

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
                    moves.Add(PoisonPowder, 13);
                    moves.Add(SleepPowder, 13);
                    moves.Add(TakeDown, 15);
                    moves.Add(RazorLeaf, 19);
                    break;

                case "Ivysaur":
                    moves.Add(Tackle, 1);
                    moves.Add(LeechSeed, 7);
                    moves.Add(VineWhip, 9);
                    moves.Add(PoisonPowder, 13);
                    moves.Add(SleepPowder, 13);
                    moves.Add(TakeDown, 15);
                    moves.Add(RazorLeaf, 20);
                    break;

                case "Venusaur":
                    moves.Add(Tackle, 1);
                    moves.Add(LeechSeed, 7);
                    moves.Add(VineWhip, 9);
                    moves.Add(PoisonPowder, 13);
                    moves.Add(SleepPowder, 13);
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

                case "Butterfree":
                    moves.Add(Confusion, 10);
                    moves.Add(PoisonPowder, 12);
                    moves.Add(StunSpore, 12);
                    moves.Add(SleepPowder, 12);
                    moves.Add(Gust, 16);
                    moves.Add(Supersonic, 18);
                    break;

                case "Weedle":
                    moves.Add(PoisonSting, 1);
                    moves.Add(BugBite, 15);
                    break;

                case "Kakuna":
                    moves.Add(PoisonSting, 1);
                    moves.Add(BugBite, 15);
                    break;

                case "Beedrill":
                    moves.Add(FuryAttack, 10);
                    moves.Add(Twineedle, 16);
                    moves.Add(Rage, 19);
                    moves.Add(Pursuit, 22);
                    break;

                case "Pidgey":
                    moves.Add(Tackle, 1);
                    moves.Add(Gust, 9);
                    moves.Add(QuickAttack, 17);
                    moves.Add(Twister, 21);
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
                    moves.Add(Bite, 10);
                    moves.Add(Pursuit,13);
                    moves.Add(HyperFang, 16);
                    moves.Add(Crunch,22);
                    break;

                case "Raticate":
                    moves.Add(Tackle, 1);
                    moves.Add(QuickAttack, 4);
                    moves.Add(Bite, 10);
                    moves.Add(Pursuit, 13);
                    moves.Add(HyperFang, 16);
                    moves.Add(Crunch, 24);
                    break;

                case "Spearow":
                    moves.Add(Peck, 1);
                    moves.Add(FuryAttack, 9);
                    moves.Add(Pursuit, 13);
                    moves.Add(AerialAce, 17);
                    break;

                case "Fearow":
                    moves.Add(Peck, 1);
                    moves.Add(FuryAttack, 9);
                    moves.Add(Pursuit, 13);
                    moves.Add(AerialAce, 17);
                    break;

                case "Ekans":
                    moves.Add(Wrap, 1);
                    moves.Add(PoisonSting, 4);
                    moves.Add(Bite, 9);
                    moves.Add(Glare, 12);
                    moves.Add(Acid, 20);
                    break;

                case "Arbok":
                    moves.Add(Wrap, 1);
                    moves.Add(PoisonSting, 4);
                    moves.Add(Bite, 9);
                    moves.Add(Glare, 12);
                    moves.Add(Acid, 20);
                    moves.Add(Crunch, 22);
                    break;

                case "Pikachu":
                    moves.Add(ThunderShock, 1);
                    moves.Add(QuickAttack, 10);
                    moves.Add(ThunderWave, 13);
                    moves.Add(ElectroBall, 18);
                    break;

                case "Sandshrew":
                    moves.Add(Scratch, 1);
                    moves.Add(PoisonSting, 5);
                    moves.Add(Rollout, 7);
                    moves.Add(Swift, 9);
                    moves.Add(FurySwipes, 20);
                    break;

                case "Nidoran♀":
                    moves.Add(Scratch, 1);
                    moves.Add(DoubleKick, 9);
                    moves.Add(PoisonSting, 13);
                    moves.Add(FurySwipes, 19);
                    moves.Add(Bite, 21);
                    break;

                case "Nidorina":
                    moves.Add(Scratch, 1);
                    moves.Add(DoubleKick, 9);
                    moves.Add(PoisonSting, 13);
                    moves.Add(FurySwipes, 20);
                    moves.Add(Bite, 23);
                    break;

                case "Nidoran♂":
                    moves.Add(Peck, 1);
                    moves.Add(DoubleKick, 9);
                    moves.Add(PoisonSting, 13);
                    moves.Add(FuryAttack, 19);
                    moves.Add(HornAttack, 21);
                    break;

                case "Nidorino":
                    moves.Add(Peck, 1);
                    moves.Add(DoubleKick, 9);
                    moves.Add(PoisonSting, 13);
                    moves.Add(FuryAttack, 20);
                    moves.Add(HornAttack, 23);
                    break;

                case "Jigglypuff":
                    moves.Add(Sing, 1);
                    moves.Add(Pound, 7);
                    moves.Add(Disable, 13);
                    break;

                case "Diglett":
                    moves.Add(Scratch, 1);
                    moves.Add(Astonish, 7);
                    moves.Add(MudSlap, 9);
                    break;

                case "Mankey":
                    moves.Add(Scratch, 1);
                    moves.Add(Covet, 1);
                    moves.Add(LowKick, 1);
                    moves.Add(FurySwipes, 9);
                    moves.Add(KarateChop, 13);
                    moves.Add(SeismicToss, 17);
                    break;

                case "Primeape":
                    moves.Add(Scratch, 1);
                    moves.Add(LowKick, 1);
                    moves.Add(FurySwipes, 9);
                    moves.Add(KarateChop, 13);
                    moves.Add(SeismicToss, 17);
                    moves.Add(Rage, 28);
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
                    moves.Add(Swift, 10);
                    moves.Add(QuickAttack, 13);
                    moves.Add(Bite, 17);
                    moves.Add(Covet,23);
                    moves.Add(TakeDown, 25);
                    moves.Add(DoubleEdge, 37);
                    break;

                default:
                    moves.Add(test1, 1); //This is a foolproof in case something goes wrong.
                    break;
            }

            return moves;
        }

    }
}
