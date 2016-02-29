using PokemonTextEdition.Classes;
using System.Collections.Generic;

namespace PokemonTextEdition.Collections
{
    /// <summary>
    /// //A list of all the Pokemon species currently in the game.
    /// </summary>
    class MoveList
    {
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

        //Remember to add new moves added here to the allMoves list below.

        #region Normal Type

        static public Move Pound = new Move(1, "Pound", Type.Normal, 40, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.None, 0);
        static public Move DoubleSlap = new Move(3, "Double Slap", Type.Normal, 15, 85, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 5);
        static public Move CometPunch = new Move(4, "Comet Punch", Type.Normal, 18, 85, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 5);
        static public Move Scratch = new Move(10, "Scratch", Type.Normal, 40, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.None, 0);
        static public Move Cut = new Move(15, "Cut", Type.Normal, 70, 95, MoveAttribute.Physical, 0, false, false, MoveEffect.None, 0);
        static public Move HornAttack = new Move(30, "Horn Attack", Type.Normal, 65, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.None, 0);
        static public Move FuryAttack = new Move(31, "Fury Attack", Type.Normal, 15, 85, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 5);
        static public Move Tackle = new Move(33, "Tackle", Type.Normal, 50, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.None, 0);
        static public Move BodySlam = new Move(34, "Body Slam", Type.Normal, 85, 100, MoveAttribute.Physical, 0, false, true, MoveEffect.Paralysis, 30);
        static public Move Wrap = new Move(35, "Wrap", Type.Normal, 15, 90, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 5);
        static public Move TakeDown = new Move(36, "Take Down", Type.Normal, 90, 85, MoveAttribute.Physical, 0, false, false, MoveEffect.Recoil, 0.25f);
        static public Move DoubleEdge = new Move(38, "Double-Edge", Type.Normal, 120, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.Recoil, 0.33f);
        static public Move Sing = new Move(47, "Sing", Type.Normal, 0, 55, MoveAttribute.Status, 0, false, false, MoveEffect.Sleep, 0);
        static public Move Supersonic = new Move(48, "Supersonic", Type.Normal, 0, 55, MoveAttribute.Status, 0, false, false, MoveEffect.Confusion, 0);
        static public Move Disable = new Move(50, "Disable", Type.Normal, 0, 100, MoveAttribute.Status, 0, false, false, MoveEffect.Disable, 0);
        static public Move QuickAttack = new Move(98, "Quick Attack", Type.Normal, 40, 100, MoveAttribute.Physical, 1, false, false, 0, 0);    
        static public Move Rage = new Move(99, "Rage", Type.Normal, 20, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.ConsecutiveDamage, 0);
        static public Move Swift = new Move(129, "Swift", Type.Normal, 60, 100, MoveAttribute.Special, 0, true, false, 0, 0);
        static public Move Glare = new Move(137, "Glare", Type.Normal, 0, 100, MoveAttribute.Status, 0, false, false, MoveEffect.Paralysis, 0);
        static public Move DizzyPunch = new Move(146, "Dizzy Punch", Type.Normal, 70, 100, MoveAttribute.Physical, 0, false, true, MoveEffect.Confusion, 20);
        static public Move FurySwipes = new Move(154, "Fury Swipes", Type.Normal, 18, 80, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 5);
        static public Move HyperFang = new Move(158, "Hyper Fang", Type.Normal, 80, 90, MoveAttribute.Physical, 0, false, true, MoveEffect.Poison, 15);
        static public Move Protect = new Move(182, "Protect", Type.Normal, 0, 100, MoveAttribute.Status, 4, true, false, MoveEffect.Protect, 0);        
        static public Move RapidSpin = new Move(229, "Rapid Spin", Type.Normal, 20, 100, MoveAttribute.Physical, 0, false, true, MoveEffect.ClearHazards, 0);
        static public Move Covet = new Move(343, "Covet", Type.Normal, 60, 100, MoveAttribute.Physical, 0, false, true, MoveEffect.ItemSteal, 0);
        static public Move DoubleHit = new Move(458, "Double Hit", Type.Normal, 35, 90, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 2);

        #endregion

        #region Rock Type

        static public Move RockThrow = new Move(88, "Rock Throw", Type.Rock, 50, 90, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move Rollout = new Move(205, "Rollout", Type.Rock, 30, 90, MoveAttribute.Physical, 0, false, false, MoveEffect.ConsecutiveDamage, 2);
        static public Move RockTomb = new Move(317, "Rock Tomb", Type.Rock, 60, 95, MoveAttribute.Physical, 0, false, false, 0, 0);

        #endregion

        #region Ground Type

        static public Move MudSlap = new Move(189, "Mud-Slap", Type.Ground, 20, 100, MoveAttribute.Special, 0, false, false, 0, 0);

        #endregion

        #region Ghost Type

        static public Move Astonish = new Move(310, "Astonish", Type.Ghost, 30, 100, MoveAttribute.Physical, 0, false, false, 0, 0);

        #endregion

        #region Grass Type

        static public Move VineWhip = new Move(22, "Vine Whip", Type.Grass, 45, 100, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move LeechSeed = new Move(73, "Leech Seed", Type.Grass, 0, 90, MoveAttribute.Status, 0, false, false, MoveEffect.LeechSeed, 0);
        static public Move RazorLeaf = new Move(75, "Razor Leaf", Type.Grass, 55, 95, MoveAttribute.Physical, 0, false, false, MoveEffect.IncreasedCritChance, 2);
        static public Move StunSpore = new Move(78, "Stun Spore", Type.Grass, 0, 75, MoveAttribute.Status, 0, false, false, MoveEffect.Paralysis, 0);
        static public Move SleepPowder = new Move(79, "Sleep Powder", Type.Grass, 0, 75, MoveAttribute.Status, 0, false, false, MoveEffect.Sleep, 0);

        #endregion

        #region Fire Type

        static public Move Ember = new Move(52, "Ember", Type.Fire, 40, 100, MoveAttribute.Special, 0, false, true, MoveEffect.Burn, 10);
        static public Move FireFang = new Move(424, "Fire Fang", Type.Fire, 65, 95, MoveAttribute.Physical, 0, false, true, MoveEffect.Burn, 10);

        #endregion

        #region Water Type

        static public Move WaterGun = new Move(55, "Water Gun", Type.Water, 40, 100, MoveAttribute.Special, 0, false, false, 0, 0);
        static public Move Bubble = new Move(145, "Bubble", Type.Water, 40, 100, MoveAttribute.Special, 0, false, true, MoveEffect.Paralysis, 10);

        #endregion

        #region Electric Type

        static public Move ThunderShock = new Move(84, "Thunder Shock", Type.Electric, 40, 100, MoveAttribute.Special, 0, false, true, MoveEffect.Paralysis, 10);
        static public Move ThunderWave = new Move(86, "Thunder Wave", Type.Electric, 0, 100, MoveAttribute.Status, 0, false, false, MoveEffect.Paralysis, 0);
        static public Move ElectroBall = new Move(486, "Electro Ball", Type.Electric, 90, 100, MoveAttribute.Special, 0, false, false, 0, 0);

        #endregion

        #region Fighting Type

        static public Move KarateChop = new Move(2, "Karate Chop", Type.Fighting, 50, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.IncreasedCritChance, 2);
        static public Move DoubleKick = new Move(24, "Double Kick", Type.Fighting, 30, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.MultipleHits, 2);
        static public Move LowKick = new Move(67, "Low Kick", Type.Fighting, 70, 100, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move SeismicToss = new Move(69, "Seismic Toss", Type.Fighting, 0, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.SetDamagePerLevel, 0);

        #endregion

        #region Bug Type

        static public Move Twineedle = new Move(41, "Twineedle", Type.Bug, 60, 100, MoveAttribute.Physical, 0, false, true, MoveEffect.Poison, 20);
        static public Move BugBite = new Move(450, "Bug Bite", Type.Bug, 60, 100, MoveAttribute.Physical, 0, false, false, 0, 0);

        #endregion

        #region Poison Type

        static public Move PoisonSting = new Move(40, "Poison Sting", Type.Poison, 15, 100, MoveAttribute.Physical, 0, false, true, MoveEffect.Poison, 30);
        static public Move Acid = new Move(51, "Acid", Type.Poison, 40, 100, MoveAttribute.Special, 0, false, true, MoveEffect.Poison, 15);
        static public Move PoisonPowder = new Move(77, "Poison Powder", Type.Poison, 0, 75, MoveAttribute.Status, 0, false, false, MoveEffect.Poison, 0);

        #endregion

        #region Psychic Type

        static public Move Confusion = new Move(93, "Confusion", Type.Psychic, 50, 100, MoveAttribute.Special, 0, false, true, MoveEffect.Confusion, 15);

        #endregion

        #region Flying Type

        static public Move Gust = new Move(16, "Gust", Type.Flying, 40, 100, MoveAttribute.Special, 0, false, false, 0, 0);
        static public Move WingAttack = new Move(17, "Wing Attack", Type.Flying, 60, 100, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move Peck = new Move(64, "Peck", Type.Flying, 35, 100, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move AerialAce = new Move(332, "Aerial Ace", Type.Flying, 60, 100, MoveAttribute.Physical, 0, true, false, 0, 0);

        #endregion

        #region Dragon Type

        static public Move DragonRage = new Move(82, "Dragon Rage", Type.Dragon, 0, 100, MoveAttribute.Special, 0, false, false, MoveEffect.SetDamage, 40);
        static public Move Twister = new Move(239, "Twister", Type.Dragon, 40, 100, MoveAttribute.Special, 0, false, false, 0, 0);

        #endregion

        #region Dark Type

        static public Move Bite = new Move(44, "Bite", Type.Dark, 60, 100, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move Pursuit = new Move(228, "Pursuit", Type.Dark, 40, 100, MoveAttribute.Physical, 0, false, false, MoveEffect.Pursuit, 0);
        static public Move Crunch = new Move(242, "Crunch", Type.Dark, 80, 100, MoveAttribute.Physical, 0, false, false, 0, 0);

        #endregion

        //DEBUG
        #region Test Type

        static public Move test1 = new Move(-1, "Test Move 1", Type.Normal, 60, 100, MoveAttribute.Physical, 0, false, false, 0, 0);
        static public Move test2 = new Move(-2, "Test Move 2", Type.Normal, 60, 100, MoveAttribute.Physical, 0, false, false, 0, 0);

        #endregion

        public static List<Move> AllMoves = new List<Move> 
        {
            
            Pound, DoubleSlap, CometPunch, Scratch, Cut, Gust, WingAttack, VineWhip, DoubleKick, HornAttack, FuryAttack, Tackle, BodySlam, Wrap, TakeDown,
            DoubleEdge, PoisonSting, Twineedle, Bite, Sing, Supersonic, Disable, Acid, Ember, WaterGun, Peck, LowKick, SeismicToss, LeechSeed, RazorLeaf,
            PoisonPowder, StunSpore, DragonRage, ThunderShock, ThunderWave, RockThrow, Confusion, QuickAttack, Rage, Swift, Glare, Bubble, DizzyPunch,
            FurySwipes, HyperFang, Protect, MudSlap, Rollout, Pursuit, RapidSpin, Twister, Crunch, Astonish, RockTomb, AerialAce, Covet, FireFang, 
            BugBite, DoubleHit, ElectroBall,
            test1, test2
        };

        /// <summary>
        /// This method returns the available moves for every Pokemon, plus what level they learn them at.
        /// </summary>
        /// <param name="name">The name of the Pokemon whose moves are to be returned.</param>
        /// <returns>Returns a dictionary of Move, int symbolizing what move the Pokemon learns at what level.</returns>
        public static Dictionary<Move, int> PokemonAvailableMoves(string name)
        {
            Dictionary<Move, int> moves = new Dictionary<Move, int>();

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
                    moves.Add(Pursuit, 13);
                    moves.Add(HyperFang, 16);
                    moves.Add(Crunch, 22);
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
                    moves.Add(Covet, 23);
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
