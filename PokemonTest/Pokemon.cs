using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    [Serializable]
    public class Pokemon
    {
        #region Declarations

        //The Pokemon's primary attributes.
        public string Name { get; set; }

        //The Pokemon's elemental types.
        public string Type1 { get; set; }
        public string Type2 { get; set; }

        //The Pokemon's Pokedex information - its species name and number.
        public string PokedexSpecies { get; set; }
        public int PokedexNumber { get; set; }

        //This determines how difficult a Pokemon is to catch. (NYI)
        public float CatchRate { get; set; }

        //The Pokemon's experience and level - currently completely linear.
        private int level = 1, experience = 0;

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                if (value > 0 && value <= 100)
                    level = value;

                else
                {
                    Console.WriteLine("The game tried to set " + Name + "'s level to " + value + ", which is unintended behaviour.");
                    Console.WriteLine("Please report this to the author, including the log.txt file.");

                    Program.Log("Level property error. Current: " + Level + ". New level: " + value, 2);
                }

            }
        }

        public int Experience
        {
            get
            {
                return experience;
            }

            set
            {
                if (value >= 0)
                    experience = value;

                else
                    experience = 0;
            }
        }

        //The Pokemon's evolution properties - what Pokemon it evolves into, how it evolves, and at what level it evolves.
        public string EvolvesInto { get; set; }
        public string EvolvesFrom { get; set; }
        public string EvolutionType { get; set; }

        public int EvolutionLevel { get; set; }

        //Quickly determines if a Pokemon evolves.
        public bool Evolves
        {
            get
            {
                if (EvolvesInto != "DoesNotEvolve")
                    return true;
                else
                    return false;
            }
        }

        #region Base Stats

        //The base stats for the particular species of Pokemon. These determine its overall potential.
        private int[] baseStats = new int[6];

        public int BaseHP
        {
            get { return baseStats[0]; }
            set { if (value < 0) baseStats[0] = 0; else baseStats[0] = value; }
        }

        public int BaseAttack
        {
            get { return baseStats[1]; }
            set { if (value < 0) baseStats[1] = 0; else baseStats[1] = value; }
        }

        public int BaseDefense
        {
            get { return baseStats[2]; }
            set { if (value < 0) baseStats[2] = 0; else baseStats[2] = value; }
        }

        public int BaseSpecialAttack
        {
            get { return baseStats[3]; }
            set { if (value < 0) baseStats[3] = 0; else baseStats[3] = value; }
        }

        public int BaseSpecialDefense
        {
            get { return baseStats[4]; }
            set { if (value < 0) baseStats[4] = 0; else baseStats[4] = value; }
        }

        public int BaseSpeed
        {
            get { return baseStats[5]; }
            set { if (value < 0) baseStats[5] = 0; else baseStats[5] = value; }
        }

        #endregion

        #region Individual Values

        //The Pokemon's Individual Values - the individual increase per stat for each unique Pokemon.
        //These are randomly generated with the Generator class and cannot be altered normally in-game.
        private int[] individualValues = new int[6];

        //Properties for all of the IVs so as to limit them between 0 and 31 - their valid range.
        public int HPIV
        {
            get { return individualValues[0]; }
            set { if (value > 31) individualValues[0] = 31; else if (value < 0) individualValues[0] = 0; else individualValues[0] = value; }
        }

        public int AttackIV
        {
            get { return individualValues[1]; }
            set { if (value > 31) individualValues[1] = 31; else if (value < 0) individualValues[1] = 0; else individualValues[1] = value; }
        }

        public int DefenseIV
        {
            get { return individualValues[2]; }
            set { if (value > 31) individualValues[2] = 31; else if (value < 0) individualValues[2] = 0; else individualValues[2] = value; }
        }

        public int SpecialAttackIV
        {
            get { return individualValues[3]; }
            set { if (value > 31) individualValues[3] = 31; else if (value < 0) individualValues[3] = 0; else individualValues[3] = value; }
        }

        public int SpecialDefenseIV
        {
            get { return individualValues[4]; }
            set { if (value > 31) individualValues[4] = 31; else if (value < 0) individualValues[4] = 0; else individualValues[4] = value; }
        }

        public int SpeedIV
        {
            get { return individualValues[5]; }
            set { if (value > 31) individualValues[5] = 31; else if (value < 0) individualValues[5] = 0; else individualValues[5] = value; }
        }

        #endregion

        #region Actual Stats

        //The Pokemon's resulting stats, after being calculated by the StatAdjust method. 
        //These are the Pokemon's active stats during any one fight.

        public int MaxHP
        {
            get
            {
                int value = ((HPIV + (2 * BaseHP) + 100) * Level / 100) + 10;

                return value;
            }
        }

        public int Attack
        {
            get
            {
                int value = ((AttackIV + (2 * BaseAttack)) * Level / 100) + 5;

                return value;
            }
        }

        public int Defense
        {
            get
            {
                int value = ((DefenseIV + (2 * BaseDefense)) * Level / 100) + 5;

                return value;
            }
        }


        public int SpecialAttack
        {
            get
            {
                int value = ((SpecialAttackIV + (2 * BaseSpecialAttack)) * Level / 100) + 5;

                return value;
            }
        }

        public int SpecialDefense
        {
            get
            {
                int value = ((SpecialDefenseIV + (2 * BaseSpecialDefense)) * Level / 100) + 5;

                return value;
            }
        }

        public int Speed
        {
            get
            {
                int value = ((SpeedIV + (2 * BaseSpeed)) * Level / 100) + 5;

                return value;
            }
        }

        #endregion

        //The Pokemon's battle-related properties, starting with its current HP.
        private int currentHP;

        public int CurrentHP
        {
            get
            {
                return currentHP;
            }

            set
            {
                if (value > MaxHP)
                    currentHP = MaxHP;
                else if (value < 0)
                    currentHP = 0;
                else
                    currentHP = value;
            }
        }

        //Determines whether the Pokemon has fainted. Adjusted using the FaintCheck() method.
        public bool Fainted
        {
            get
            {
                if (CurrentHP > 0)
                    return false;

                else
                {
                    Faint();

                    return true;
                }
            }
        }

        //This is used to determine whether the Pokemon is currently suffering from a status ailment.
        private string status = "";

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value == "" || value == "burn" || value == "poison" || value == "paralysis" || value == "sleep")
                    status = value;

                else
                {
                    Console.WriteLine("The game tried to set " + Name + "'s status to " + value + ", which is unintended behaviour.");
                    Console.WriteLine("Please report this to the author, including the log.txt file.");

                    Program.Log("Status property error. Current: " + status + ". New status: " + value, 2);
                }
            }
        }

        //Temporary combat effects a Pokemon might be afflicted with.

        public bool moveLocked = false; //Determines if the Pokemon is locked into a move.   
        public bool leechSeed = false; //Determines if the Pokemon is afflicted by Leech Seed.
        public int sleepCounter = 0; //Determines how long a Pokemon will be asleep for.
        public bool protect = false; //Determines if a Pokemon is under the effect of Protect.
        public bool confused = false; //Determines if a Pokemon is suffering from confusion.

        //A dictionary that contains all the moves the Pokemon can learn, and a list that contains all the moves it currently knows.
        public Dictionary<Moves, int> availableMoves = new Dictionary<Moves, int>();
        public List<Moves> knownMoves = new List<Moves>();

        #endregion

        #region Constructors

        /// <summary>
        /// Blank Pokemon constructor.
        /// </summary>
        public Pokemon()
        {
            Name = "Blank";
        }

        /// <summary>
        /// The main Pokemon constructor, that all of the other constructors chain from. By default, Pokemon are generated as that they do not evolve. 
        /// Note that IVs and currently known moves are not assigned here, but are rather handled by the Generator class.
        /// </summary>
        /// <param name="pName">The name of the Pokemon's species.</param>
        /// <param name="pType">The Pokemon's primary type.</param>
        /// <param name="pType2">The Pokemon's secondary type, if applicable. If not, set to blank.</param>
        /// <param name="pSpecies">The Pokemon's Pokedex entry species descrription. This is flavor only.</param>
        /// <param name="pNumber">The Pokemon's Pokedex number.</param>
        /// <param name="pCatchRate">The Pokemon's catch rate. Base: 1. (NYI)</param>
        /// <param name="pBaseHP">The Pokemon's base HP stat, used for calculating current HP.</param>
        /// <param name="pBaseAtk">The Pokemon's base Attack stat, used for calculating current Attack.</param>
        /// <param name="pBaseDef">The Pokemon's base Defense stat, used for calculating current Defense.</param>
        /// <param name="pBaseSpa">The Pokemon's base Special Attack stat, used for calculating current Special Attack.</param>
        /// <param name="pBaseSpd">The Pokemon's base Special Defense stat, used for calculating current Special Defense.</param>
        /// <param name="pBaseSpe">The Pokemon's base Speed stat, used for calculating current Speed.</param>
        public Pokemon(string pName, string pType, string pType2, string pSpecies, int pNumber, float pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe)
        {
            //Main constructor.

            Name = pName;

            Type1 = pType;
            Type2 = pType2;

            PokedexSpecies = pSpecies;
            PokedexNumber = pNumber;

            CatchRate = pCatchRate;

            BaseHP = pBaseHP;
            BaseAttack = pBaseAtk;
            BaseDefense = pBaseDef;
            BaseSpecialAttack = pBaseSpa;
            BaseSpecialDefense = pBaseSpd;
            BaseSpeed = pBaseSpe;

            EvolvesInto = "DoesNotEvolve";
            EvolutionType = "DoesNotEvolve";
            EvolutionLevel = 0;
        }

        /// <summary>
        /// The main constructor for initializing Pokemon with regular evolution. Note that IVs and currently known moves are not assigned here, but are rather handled by the Generator class.
        /// </summary>
        /// <param name="pName">The name of the Pokemon's species.</param>
        /// <param name="pType">The Pokemon's primary type.</param>
        /// <param name="pType2">The Pokemon's secondary type, if applicable. If not, set to blank.</param>
        /// <param name="pSpecies">The Pokemon's Pokedex entry species descrription. This is flavor only.</param>
        /// <param name="pNumber">The Pokemon's Pokedex number.</param>
        /// <param name="pCatchRate">The Pokemon's catch rate. Base: 1. (NYI)</param>
        /// <param name="pBaseHP">The Pokemon's base HP stat, used for calculating current HP.</param>
        /// <param name="pBaseAtk">The Pokemon's base Attack stat, used for calculating current Attack.</param>
        /// <param name="pBaseDef">The Pokemon's base Defense stat, used for calculating current Defense.</param>
        /// <param name="pBaseSpa">The Pokemon's base Special Attack stat, used for calculating current Special Attack.</param>
        /// <param name="pBaseSpd">The Pokemon's base Special Defense stat, used for calculating current Special Defense.</param>
        /// <param name="pBaseSpe">The Pokemon's base Speed stat, used for calculating current Speed.</param>
        /// <param name="pEvolvesInto">The Pokemon that this Pokemon evolves into.</param>
        /// <param name="pEvolutionLevel">The level at which this Pokemon evolves.</param>
        public Pokemon(string pName, string pType, string pType2, string pSpecies, int pNumber, float pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, string pEvolvesInto, int pEvolutionLevel)
            : this(pName, pType, pType2, pSpecies, pNumber, pCatchRate, pBaseHP, pBaseAtk, pBaseDef, pBaseSpa, pBaseSpd, pBaseSpe)
        {
            //Constructor for Pokemon with regular evolution.

            EvolvesInto = pEvolvesInto;
            EvolutionType = "LevelUp";
            EvolutionLevel = pEvolutionLevel;
        }

        /// <summary>
        /// This is the constructor for Pokemon with irregular evolution. Note that IVs and currently known moves are not assigned here, but are rather handled by the Generator class.
        /// </summary>
        /// <param name="pName">The name of the Pokemon's species.</param>
        /// <param name="pType">The Pokemon's primary type.</param>
        /// <param name="pType2">The Pokemon's secondary type, if applicable. If not, set to blank.</param>
        /// <param name="pSpecies">The Pokemon's Pokedex entry species descrription. This is flavor only.</param>
        /// <param name="pNumber">The Pokemon's Pokedex number.</param>
        /// <param name="pCatchRate">The Pokemon's catch rate. Base: 1. (NYI)</param>
        /// <param name="pBaseHP">The Pokemon's base HP stat, used for calculating current HP.</param>
        /// <param name="pBaseAtk">The Pokemon's base Attack stat, used for calculating current Attack.</param>
        /// <param name="pBaseDef">The Pokemon's base Defense stat, used for calculating current Defense.</param>
        /// <param name="pBaseSpa">The Pokemon's base Special Attack stat, used for calculating current Special Attack.</param>
        /// <param name="pBaseSpd">The Pokemon's base Special Defense stat, used for calculating current Special Defense.</param>
        /// <param name="pBaseSpe">The Pokemon's base Speed stat, used for calculating current Speed.</param>
        /// <param name="pEvolvesInto">The Pokemon that this Pokemon evolves into.</param>
        /// <param name="pEvolutionType">The particular type of evolution for this Pokemon. I.E. Water Stone, trade, happiness</param>
        public Pokemon(string pName, string pType, string pType2, string pSpecies, int pNumber, float pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, string pEvolvesInto, string pEvolutionType, int pEvolutionLevel)
            : this(pName, pType, pType2, pSpecies, pNumber, pCatchRate, pBaseHP, pBaseAtk, pBaseDef, pBaseSpa, pBaseSpd, pBaseSpe)
        {
            //Constructor for Pokemon with irregular evolution.

            EvolvesInto = pEvolvesInto;
            EvolutionType = pEvolutionType;
            EvolutionLevel = pEvolutionLevel;
        }

        #endregion

        #region Displaying Information

        public string TypeMessage()
        //This method simply returns the Pokemon's type formatted based on whether it is dual-typed.
        //Used in printing the Pokemon's information.
        {
            string type = Type1;

            if (Type2 != "")
            {
                type += "/" + Type2;
            }

            return type;
        }

        public string StatusMessage()
        {
            switch (Status){
                case "":
                    return "Normal";

                case "poison":
                    return "Poisoned";

                case "burn":
                    return "Burnt";

                case "paralysis":
                    return "Paralyzed";

                case "sleep":
                    return "Asleep";

                default:
                    return "Condition not specified";
            }
        }

        public double PercentLife()
        //This method returns the Pokemon's remaining life expressed as a percent of its maximum life.
        {
            return Math.Round((100.0 * CurrentHP / MaxHP), 2, MidpointRounding.AwayFromZero);
        }

        public void PrintStatus()
        //This method displays all of a Pokemon's current stats, including its level, species and its currently known moves.
        {
            Console.WriteLine("Level {0} {1}. HP: {2}/{3}. Type: {4}. Status: {5}.\nAttack: {6}. Defense: {7}. Sp. Attack: {8}. Sp Defense: {9} Speed: {10}.\nKnown moves: {11}",
                Level, Name, CurrentHP, MaxHP, TypeMessage(), StatusMessage(), Attack, Defense, SpecialAttack, SpecialDefense, Speed, PrintMoves());
        }

        public void BriefStatus()
        //This is a brief version of the Status() method, used for Pokemon whose stats are to remain hidden - i.e., enemy Pokemon.
        {
            Console.WriteLine("\nLevel {0} {1}. HP: {2}%. Type: {3}. Status: {4}. ", Level, Name, PercentLife(), TypeMessage(), StatusMessage());
        }

        public void PrintIVs()
        //This method prints the Pokemon's IVs. As this is not intended to be viewed by the player, it is not included in the list of commands.
        {
            Console.WriteLine("{0}'s IVs: HP {1}, ATK {2}, DEF {3}, SPA {4}, SPD {5}, SPE {6}", Name, HPIV, AttackIV, DefenseIV, SpecialAttackIV, SpecialDefenseIV, SpeedIV);
        }

        public string PrintMoves()
        //This method returns the Pokemon's known moves, formatted by the amount of moves it knows.
        {
            switch (knownMoves.Count)
            {
                case 1:
                    return knownMoves.ElementAt(0).Name;

                case 2:
                    return knownMoves.ElementAt(0).Name + ", " + knownMoves.ElementAt(1).Name;

                case 3:
                    return knownMoves.ElementAt(0).Name + ", " + knownMoves.ElementAt(1).Name + ", " + knownMoves.ElementAt(2).Name;

                case 4:
                    return knownMoves.ElementAt(0).Name + ", " + knownMoves.ElementAt(1).Name + ", " + knownMoves.ElementAt(2).Name + ", " + knownMoves.ElementAt(3).Name;

                default:
                    return "PrintMoves() error.";
            }
        }

        #endregion

        #region Supplementary Methods

        /// <summary>
        /// Checks whether a Pokemon is of "type" in either of its types.
        /// </summary>
        /// <param name="type">The type to check for.</param>
        /// <returns></returns>
        public bool TypeCheck(string type)
        {
            //This method quickly determines whether a Pokemon is of a particular type.

            if (Type1 == type)
                return true;

            else if (Type2 == type)
                return true;

            else
                return false;
        }

        public void Faint()
        {
            //This code runs when a Pokemon would faint, setting its life to 0, curing it of status ailments and restoring all temporary status conditions.

            CurrentHP = 0;
            Status = "";

            RestoreTemporaryStatus(true);
        }

        /// <summary>
        /// Restores every temporary status effect on the Pokemon.
        /// </summary>
        /// <param name="includeSwitchPersistentStatus">Set to true to reset effects that do not fade upon switching out.</param>
        public void RestoreTemporaryStatus(bool includeSwitchPersistentStatus)
        {
            //This method restores all of a Pokemon's temporary status effects.
            //Optionally, it also resets effects that persist through switching out.

            if (includeSwitchPersistentStatus)
            {
                sleepCounter = 0;
            }

            moveLocked = false;
            leechSeed = false;
            protect = false;
        }

        #endregion

        #region Moves

        public Moves SelectMove(bool mandatorySelection)
        {
            //This code is used when the player is asked to select one of a Pokemon's moves.

            //First, all of the moves that the Pokemon knows are listed.
            for (int i = 0; i < this.knownMoves.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, this.knownMoves.ElementAt(i).Name);
            }

            string input = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            //First, input is taken from the player. If the input is a number corresponding to a move in the Pokemon's knownMoves list, it gets selected.
            if (validInput && index > 0 && index < (this.knownMoves.Count + 1))
                return this.knownMoves.ElementAt(index - 1);

            //If the player hit enter and the selection wasn't mandatory, he is returned back to whatever was happening.
            else if (input == "" && !mandatorySelection)
            {
                Program.Log("The player chose to cancel the operation.", 0);

                return new Moves();
            }

            //If the input was smaller than 1, bigger than the player's party size or not a number, an error message is shown.
            else
            {
                Program.Log("The player gave invalid input. Returning to what was previously happening.", 0);
                Console.WriteLine("\nInvalid input.\n");

                return new Moves();
            }
        }

        public bool NewMovesAvailable()
        {
            if (availableMoves.ContainsValue(Level))
            {
                return true;
            }

            else
                return false;
        }

        public void LearnNewMoves()
        {
            //This code handles learning new moves.

            foreach (KeyValuePair<Moves, int> move in availableMoves)
            {
                //First, the game checks whether the Pokemon already knows the move being learned.
                if (knownMoves.Exists(m => m.Name == move.Key.Name))
                {
                    //If it doesn't, and if the Pokemon currently knows less than 4 moves, it learns the new move.
                    if (knownMoves.Count < 4)
                    {
                        knownMoves.Add(move.Key);
                        Console.WriteLine("{0} learned the move {1}!", Name, move.Key.Name);
                    }

                    //Otherwise, the user is asked if the Pokemon should forget a move in order to learn the new move.
                    else
                    {
                        Console.WriteLine("{0} wants to learn the move {1}, but it already knows 4 moves.\n", Name, move.Key.Name);
                        Console.WriteLine("Should a move be forgotten in order to learn {0}?\n(\"(y)es\" to forget a move, or enter to cancel)", move.Key.Name);

                        string decision = Console.ReadLine();

                        if (decision != "")
                            Console.WriteLine("");

                        switch (decision)
                        {
                            case "Yes":
                            case "yes":
                            case "y":

                                //If he types "yes", he is asked to give input as to which move should be forgotten.
                                Console.WriteLine("What move should be forgotten?");

                                Moves tempMove = SelectMove(false);

                                //If the input was a number larger than 0 and equal to or smaller than the amount of moves the Pokemon currently knows,
                                //the user the move selected is forgotten, and the new move is learned in its stead.
                                if (tempMove.Name != "Blank")
                                {
                                    Console.WriteLine("\n1, 2 and poof!\n{0} forgot {1} and learned {2} instead!", Name, tempMove.Name, move.Key.Name);
                                    knownMoves.Remove(knownMoves.Find(moves => moves.Name == tempMove.Name));
                                    knownMoves.Add(move.Key);

                                }

                                //Else, if the input was not valid, he will be asked whether the Pokemon should forget a move again.
                                else
                                {
                                    LearnNewMoves();
                                }

                                break;

                            //If the user's answer was not "yes", the program asks to verify that the user does not want to teach the Pokemon this move.
                            default:

                                Console.WriteLine("Are you sure that {0} should not learn {1}?", Name, move.Key.Name);
                                Console.WriteLine("(\"(y)es\" to cancel completely, or enter to re-try)");

                                string decision2 = Console.ReadLine();

                                //A second switch to verify the user's answer.
                                switch (decision2)
                                {
                                    case "Yes":
                                    case "yes":
                                    case "y":

                                        //If his answer is yes again, the game simply returns to whatever was happening.
                                        return;

                                    default:

                                        //Any other input will start this process all over again.

                                        if (decision2 != "")
                                            Console.WriteLine("");

                                        LearnNewMoves();
                                        break;
                                }

                                break;

                        }
                    }
                }
            }
        }

        #endregion

        #region Level Up & Evolution

        public void LevelUp()
        {
            //This code handles levelling up. It is currently completely linear, as the level-up threshold is set to 300 for all levels.

            Program.Log(this.Name + " levelled up.", 0);

            //Some temporary values to show the increase in stats after levelling up.
            int tempHP = MaxHP, tempAtk = Attack, tempDef = Defense, tempSpa = SpecialAttack, tempSpd = SpecialDefense, tempSpe = Speed;

            Level++;
            Experience -= 300;

            Console.WriteLine("\n{0} levelled up to {1}!", Name, Level);

            Console.WriteLine("HP +{0}, Attack +{1}, Defense +{2}, Sp. Attack +{3}, Sp. Defense +{4}, Speed +{5}.",
                MaxHP - tempHP, Attack - tempAtk, Defense - tempDef, SpecialAttack - tempSpa, SpecialDefense - tempSpd, Speed - tempSpe);

            int differenceHP = MaxHP - tempHP;

            if (!Fainted) //This is not needed as fainted Pokemon can't level up by default, but it's here as foolproof.
            {
                //If the difference between the Pokemon's new max HP and its old max HP added to its current HP is smaller than its max HP, it gets healed for the difference.
                if (MaxHP > CurrentHP + differenceHP)
                    CurrentHP += differenceHP;

                //Else it gets simply healed to full.
                else
                    CurrentHP = MaxHP;
            }

            if (NewMovesAvailable())
            {
                Console.WriteLine("");
                LearnNewMoves();
            }

            if (Evolves && EvolutionLevel <= Level)
            {
                Evolve(EvolvesInto);
            }
        }

        public void Evolve(string evolvedPokemon)
        {
            //Code that handles a Pokemon's evolution.

            Program.Log(Name + " is evolving.", 0);

            Console.WriteLine("\nWhat? {0} is evolving! (Type \"(c)ancel\" to stop evolving)", this.Name);

            string decision = Console.ReadLine();

            //As is traditional in the games, the player has the option to cancel evolving.
            switch (decision)
            {
                case "Cancel":
                case "cancel":
                case "C":
                case "c":
                    Program.Log("The player chose to cancel evolving.", 0);

                    return;

                default:

                    //The actual evolution code. First, the game finds which Pokemon this Pokemon evolves into.
                    //Afterwards, the evolved Pokemon's parameters are copied to this Pokemon, bar for the parameters unique to this specific Pokemon.

                    Pokemon evolution = PokemonList.allPokemon.Find(p => p.Name == this.EvolvesInto);

                    string oldPokemon = this.Name;
                    int oldMaxHP = this.MaxHP;

                    this.Name = evolution.Name;
                    this.Type1 = evolution.Type1;
                    this.Type2 = evolution.Type2;

                    this.PokedexSpecies = evolution.PokedexSpecies;
                    this.PokedexNumber = evolution.PokedexNumber;

                    this.CatchRate = evolution.CatchRate;

                    this.EvolvesInto = evolution.EvolvesInto;
                    this.EvolutionType = evolution.EvolutionType;
                    this.EvolutionLevel = evolution.EvolutionLevel;

                    this.BaseHP = evolution.BaseHP;
                    this.BaseAttack = evolution.BaseAttack;
                    this.BaseDefense = evolution.BaseDefense;
                    this.BaseSpecialAttack = evolution.BaseSpecialAttack;
                    this.BaseSpecialDefense = evolution.BaseSpecialDefense;
                    this.BaseSpeed = evolution.BaseSpeed;

                    this.availableMoves = MovesList.PokemonAvailableMoves(Name);

                    //Then, the Pokemon gets healed by the difference between its old max HP and its new max HP, and it learns new moves, if any.

                    int differenceHP = MaxHP - oldMaxHP;

                    if (!Fainted) //This is needed as fainted Pokemon can be evolved by using an evolution stone.
                    {
                        //If the difference between the Pokemon's new max HP and its old max HP added to its current HP is smaller than its max HP, it gets healed for the difference.
                        if (MaxHP > CurrentHP + differenceHP)
                            CurrentHP += differenceHP;

                        //Else it gets simply healed to full.
                        else
                            CurrentHP = MaxHP;
                    }

                    Program.Log(oldPokemon + " evolved into " + Name + ".", 1);
                    Console.Write("Congratulations! Your {0} evolved into {1}!", oldPokemon, Name);
                    Console.WriteLine("");

                    if (NewMovesAvailable())
                    {
                        Console.WriteLine("");
                        LearnNewMoves();
                    }

                    break;
            }
        }

        #endregion
    }
}
