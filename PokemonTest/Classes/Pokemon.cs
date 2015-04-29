using PokemonTextEdition.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    [Serializable]
    public class Pokemon
    {
        #region Fields & Properties

        //The Pokemon's Globally Unique ID.
        public Guid guid = new Guid();

        //The Pokemon's species.
        public PokemonSpecies species;

        //The Pokemon's nominal attributes.
        public string Name
        {
            get
            {
                if (Nickname == null || Nickname == "")
                    return species.Name;

                else
                    return Nickname;
            }
        }

        protected string nickname;

        public string Nickname
        {
            get
            {
                return nickname;
            }

            set
            {
                if (value != "" && value != null)
                    nickname = value;
            }
        }

        //The Pokemon's experience and level - currently completely linear.
        protected int level = 1, experience = 0;

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

        #region Individual Values

        //The Pokemon's Individual Values - the individual increase per stat for each unique Pokemon.
        //These are randomly generated with the Generator class and cannot be altered normally in-game.
        private int[] individualValues = new int[6];

        public int[] IndividualValues
        {
            get
            {
                return individualValues;
            }
        }

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
                int value = ((HPIV + (2 * species.BaseHP) + 100) * Level / 100) + 10;

                return value;
            }
        }

        public int Attack
        {
            get
            {
                int value = ((AttackIV + (2 * species.BaseAttack)) * Level / 100) + 5;

                return value;
            }
        }

        public int Defense
        {
            get
            {
                int value = ((DefenseIV + (2 * species.BaseDefense)) * Level / 100) + 5;

                return value;
            }
        }


        public int SpecialAttack
        {
            get
            {
                int value = ((SpecialAttackIV + (2 * species.BaseSpecialAttack)) * Level / 100) + 5;

                return value;
            }
        }

        public int SpecialDefense
        {
            get
            {
                int value = ((SpecialDefenseIV + (2 * species.BaseSpecialDefense)) * Level / 100) + 5;

                return value;
            }
        }

        public int Speed
        {
            get
            {
                int value = ((SpeedIV + (2 * species.BaseSpeed)) * Level / 100) + 5;

                return value;
            }
        }

        #endregion

        //The Pokemon's battle-related properties, starting with its current HP.

        protected int currentHP;

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

        //Determines whether the Pokemon has fainted. Triggers Faint() if the Pokemon would faint.
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
        protected string status = "";

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

        //A list that contains all the moves it currently knows.
        public List<Moves> knownMoves = new List<Moves>();

        #endregion

        #region Constructors

        /// <summary>
        /// Blank Pokemon constructor.
        /// </summary>
        public Pokemon()
        {
            species = PokemonList.allPokemon.ElementAt(0);
        }

        /// <summary>
        /// Constructs a Pokemon with a species based on a species object.
        /// </summary>
        /// <param name="speciesName">The name of the species.</param>
        public Pokemon(PokemonSpecies pokemonSpecies)
        {
            species = pokemonSpecies;
        }

        /// <summary>
        /// Constructs a Pokemon with a species based on a species name.
        /// </summary>
        /// <param name="speciesName">The name of the species.</param>
        public Pokemon(string speciesName)
        {
            species = PokemonList.allPokemon.Find(p => p.Name == speciesName);
        }

        /// <summary>
        /// Constructs a Pokemon with a species based on a Pokedex number.
        /// </summary>
        /// <param name="speciesName">The Pokedex number of the species.</param>
        public Pokemon(int pokedexNumber)
        {
            species = PokemonList.allPokemon.Find(p => p.PokedexNumber == pokedexNumber);
        }

        #endregion

        #region Displaying Information

        public string TypeMessage()
        //This method simply returns the Pokemon's type formatted based on whether it is dual-typed.
        //Used in printing the Pokemon's information.
        {
            string type = species.Type1;

            if (species.Type2 != "")
            {
                type += "/" + species.Type2;
            }

            return type;
        }

        public string StatusMessage()
        {
            switch (Status)
            {
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

            if (species.Type1 == type)
                return true;

            else if (species.Type2 == type)
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

        public void CheckForNewMoves()
        {
            //This code checks whether the Pokemon learns a new move at its level and if so, proceeds to learn it.

            //This returns a list of all the moves the Pokemon has access to.
            Dictionary<Moves, int> availableMoves = MovesList.PokemonAvailableMoves(species.Name);

            //If the Pokemon learns a new move at its current level, then the procedure starts.
            if (availableMoves.ContainsValue(Level))
            {
                Console.WriteLine("");

                //This is a list that will contain all of the moves the Pokemon can learn at this level, to facilitate for the
                //event that the Pokemon can learn more than one move at the current level.
                List<Moves> moves = new List<Moves>();

                foreach (KeyValuePair<Moves, int> move in availableMoves)
                {
                    if (move.Value == this.Level)
                        moves.Add(move.Key);
                }

                foreach (Moves move in moves)
                {
                    //The game checks whether the Pokemon already knows the move being learned before trying to make the Pokemon learn the move.
                    if (!this.knownMoves.Exists(m => m.Name == move.Name))
                        LearnMove(move);
                }
            }
        }

        public void LearnMove(Moves move)
        {
            //First, the game checks whether the Pokemon already knows 4 moves. If it doesn't, it learns the move.
            if (knownMoves.Count < 4)
            {
                knownMoves.Add(move);
                Console.WriteLine("{0} learned the move {1}!", Name, move.Name);
            }

            //If the Pokemon does know 4 moves, the user is asked if the Pokemon should forget a move in order to learn the new move.
            else
            {
                Console.WriteLine("{0} wants to learn the move {1}, but it already knows 4 moves.\n", Name, move.Name);
                Console.WriteLine("Should a move be forgotten in order to learn {0}?\n(\"(y)es\" to forget a move, or enter to cancel)", move.Name);

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
                            Console.WriteLine("\n1, 2 and poof!\n{0} forgot {1} and learned {2} instead!", Name, tempMove.Name, move.Name);
                            knownMoves.Remove(knownMoves.Find(moves => moves.Name == tempMove.Name));
                            knownMoves.Add(move);

                        }

                        //Else, if the input was not valid, he will be asked whether the Pokemon should forget a move again.
                        else
                        {
                            LearnMove(move);
                        }

                        break;

                    //If the user's answer was not "yes", the program asks to verify that the user does not want to teach the Pokemon this move.
                    default:

                        Console.WriteLine("Are you sure that {0} should not learn {1}?", Name, move.Name);
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

                                LearnMove(move);
                                break;
                        }

                        break;
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

            if (!Fainted) //This is needed as fainted Pokemon can level up with Rare Candies, which shouldn't also function as a revive.
            {
                //If the difference between the Pokemon's new max HP and its old max HP added to its current HP is smaller than its max HP, it gets healed for the difference.
                if (MaxHP > CurrentHP + differenceHP)
                    CurrentHP += differenceHP;

                //Else it gets simply healed to full.
                else
                    CurrentHP = MaxHP;
            }

            CheckForNewMoves();

            if (species.Evolves && species.EvolutionLevel <= Level)
            {
                Evolve(species.EvolvesInto);
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

                    PokemonSpecies evolution = PokemonList.allPokemon.Find(p => p.Name == this.species.EvolvesInto);

                    string oldPokemon = this.Name;
                    int oldMaxHP = this.MaxHP;

                    this.species = evolution;

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

                    CheckForNewMoves();

                    break;
            }
        }

        #endregion
    }
}
