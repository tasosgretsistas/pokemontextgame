using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    [Serializable]
    public class Pokemon
    {
        //The Pokemon's primary attributes.
        public string name, type, type2;

        //The Pokemon's Pokedex information - its type of "species" and number.
        public string pokedexSpecies;
        public int pokedexNumber;

        //This determines how difficult a Pokemon is to catch. (NYI)
        public double catchRate;


        //The Pokemon's experience and level - currently completely linear.
        public int level, experience;

        //The Pokemon's evolution properties - what Pokemon it evolves into, how it evolves, and at what level it evolves.
        public string evolution, evolutionType;
        public int evolutionLevel;


        //The Pokemon's base stats that determine its overall potential.
        public int baseHP, baseAttack, baseDefense, baseSpecialAttack, baseSpecialDefense, baseSpeed;

        //The Pokemon's Individual Values - the individual increase per stat for each unique Pokemon.
        //These are randomly generated with the Generator class and cannot be altered normally in-game.
        private int hpIV, attackIV, defenseIV, specialAttackIV, specialDefenseIV, speedIV;

        //The Pokemon's resulting stats, after being calculated by the StatAdjust method. 
        //These are the Pokemon's active stats during any one fight.
        public int currentHP, maxHP, attack, defense, specialAttack, specialDefense, speed;


        //This is used to determine whether the Pokemon is currently suffering from a status ailment.
        public string status;

        //Temporary combat effects a Pokemon might be afflicted with.
        
        public bool moveLocked = false; //Determines if the Pokemon is locked into a move.   
        public bool leechSeed = false; //Determines if the Pokemon is afflicted by Leech Seed.
        public int sleepCounter = 0; //Determines how long a Pokemon will be asleep for.
        public bool protect = false; //Determines if a Pokemon is under the effect of Protect.
        public bool confused = false; //Determines if a Pokemon is suffering from confusion.

        //A dictionary that contains all the moves the Pokemon can learn, and a list that contains all the moves it currently knows.
        public Dictionary<Moves, int> availableMoves = new Dictionary<Moves, int>();
        public List<Moves> knownMoves = new List<Moves>();

        //Properties for all of the IVs so as to limit them between 1 and 31 - their valid range.
        public int HPIV
        {
            get { return hpIV; }
            set { if (value > 31) hpIV = 31; else hpIV = value; }
        }

        public int AttackIV
        {
            get { return attackIV; }
            set { if (value > 31) attackIV = 31; else attackIV = value; }
        }

        public int DefenseIV
        {
            get { return defenseIV; }
            set { if (value > 31) defenseIV = 31; else defenseIV = value; }
        }

        public int SpecialAttackIV
        {
            get { return specialAttackIV; }
            set { if (value > 31) specialAttackIV = 31; else specialAttackIV = value; }
        }

        public int SpecialDefenseIV
        {
            get { return specialDefenseIV; }
            set { if (value > 31) specialDefenseIV = 31; else specialDefenseIV = value; }
        }

        public int SpeedIV
        {
            get { return speedIV; }
            set { if (value > 31) speedIV = 31; else speedIV = value; }
        }

        public Pokemon()
        {
            name = "Blank";
        }

        /// <summary>
        /// The main constructor for initializing Pokemon. Note that IVs and currently known moves are not assigned here, but are rather handled by the Generator class.
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
        /// <param name="pEvolution">The Pokemon that this Pokemon evolves into.</param>
        /// <param name="pEvolutionLevel">The level at which this Pokemon evolves.</param>
        public Pokemon(string pName, string pType, string pType2, string pSpecies, int pNumber, double pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, string pEvolution, int pEvolutionLevel)
        {
            name = pName;

            type = pType;
            type2 = pType2;

            pokedexSpecies = pSpecies;
            pokedexNumber = pNumber;

            catchRate = pCatchRate;

            baseHP = pBaseHP;
            baseAttack = pBaseAtk;
            baseDefense = pBaseDef;
            baseSpecialAttack = pBaseSpa;
            baseSpecialDefense = pBaseSpd;
            baseSpeed = pBaseSpe;

            evolution = pEvolution;
            evolutionLevel = pEvolutionLevel;

            status = "";
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
        /// <param name="pEvolution">The Pokemon that this Pokemon evolves into.</param>
        /// <param name="pEvolutionType">The particular type of evolution for this Pokemon. I.E. Water Stone, trade, happiness</param>
        public Pokemon(string pName, string pType, string pType2, string pSpecies, int pNumber, double pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, string pEvolution, string pEvolutionType)
        {
            name = pName;

            type = pType;
            type2 = pType2;

            pokedexSpecies = pSpecies;
            pokedexNumber = pNumber;

            catchRate = pCatchRate;

            baseHP = pBaseHP;
            baseAttack = pBaseAtk;
            baseDefense = pBaseDef;
            baseSpecialAttack = pBaseSpa;
            baseSpecialDefense = pBaseSpd;
            baseSpeed = pBaseSpe;

            evolution = pEvolution;
            evolutionType = pEvolutionType;

            status = "";
        }

        /// <summary>
        /// This constructor generates Pokemon that strictly do not evolve. Note that IVs and currently known moves are not assigned here, but are rather handled by the Generator class.
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
        public Pokemon(string pName, string pType, string pType2, string pSpecies, int pNumber, double pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe)
        {
            name = pName;

            type = pType;
            type2 = pType2;

            pokedexSpecies = pSpecies;
            pokedexNumber = pNumber;

            catchRate = pCatchRate;

            baseHP = pBaseHP;
            baseAttack = pBaseAtk;
            baseDefense = pBaseDef;
            baseSpecialAttack = pBaseSpa;
            baseSpecialDefense = pBaseSpd;
            baseSpeed = pBaseSpe;

            evolution = "doesNotEvolve";
            evolutionLevel = 0;

            status = "";
        }

        public string TypeMessage()
        //This method simply returns the Pokemon's type formatted based on whether it is dual-typed.
        //Used in printing the Pokemon's information.
        {
            if (type2 == "")
            {
                return type;
            }
            else
            {
                return type + "/" + type2;
            }
        }

        public double PercentLife()
        //This method returns the Pokemon's remaining life expressed as a percent of its maximum life.
        {
            return Math.Round(((double)currentHP / (double)maxHP * 100), 2, MidpointRounding.AwayFromZero);
        }

        public void PrintStatus()
        //This method displays all of a Pokemon's current stats, including its level, species and its currently known moves.
        {
            Console.WriteLine("Level {0} {1}. HP: {2}/{3}. Type: {4}. Pokedex #: {5}.\nAttack: {6}. Defense: {7}. Sp. Attack: {8}. Sp Defense: {9} Speed: {10}.\nKnown moves: {11}",
                level, name, currentHP, maxHP, TypeMessage(), pokedexNumber, attack, defense, specialAttack, specialDefense, speed, PrintMoves());
        }

        public void BriefStatus()
        //This is a brief version of the Status() method, used for Pokemon whose stats are to remain hidden - i.e., enemy Pokemon.
        {
            Console.WriteLine("\nLevel {0} {1}. HP: {2}%. Type: {3}. Pokedex #: {4}. ", level, name, PercentLife(), TypeMessage(), pokedexNumber);
        }

        public void PrintIVs()
        //This method prints the Pokemon's IVs. As this is not intended to be viewed by the player, it is not included in the list of commands.
        {
            Console.WriteLine("{0}'s IVs: HP {1}, ATK {2}, DEF {3}, SPA {4}, SPD {5}, SPE {6}", name, hpIV, attackIV, defenseIV, specialAttackIV, specialDefenseIV, speedIV);
        }

        public string PrintMoves()
        //A multi-purpose method that returns the Pokemon's known moves, formatted by the amount of moves it knows.
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
                    return "Error";

            }
        }

        public void Faint()
        {
            //This code runs when a Pokemon would faint, and it basically restores all of a Pokemon's temporary and non-temporary effects back to default.

            if (currentHP < 0)
                currentHP = 0;

            status = "";
            sleepCounter = 0;
            moveLocked = false;
            leechSeed = false;
            protect = false;

        }

        public void StatAdjust()
        //This method adjusts a Pokemon's stats based on its level.
        {
            maxHP = (((hpIV + (2 * baseHP + 100)) * level) / 100) + 10;
            attack = (((attackIV + (2 * baseAttack)) * level) / 100) + 5;
            defense = (((defenseIV + (2 * baseDefense)) * level) / 100) + 5;
            specialAttack = (((specialAttackIV + (2 * baseSpecialAttack)) * level) / 100) + 5;
            specialDefense = (((specialDefenseIV + (2 * baseSpecialDefense)) * level) / 100) + 5;
            speed = (((speedIV + (2 * baseSpeed)) * level) / 100) + 5;
        }

        public void LevelUp()
        //This code handles levelling up. It is currently completely linear, as the level-up threshold is set to 300 for all levels.
        {
            //Some temporary values to show the increase in stats after levelling up.
            int tempHP = maxHP, tempAtk = attack, tempDef = defense, tempSpa = specialAttack, tempSpd = specialDefense, tempSpe = speed;

            level++;
            experience -= 300;
            Console.WriteLine("\n{0} levelled up to {1}!", name, level);

            StatAdjust();
            Console.WriteLine("HP +{0}, Attack +{1}, Defense +{2}, Sp. Attack +{3}, Sp. Defense +{4}, Speed +{5}.",
                maxHP - tempHP, attack - tempAtk, defense - tempDef, specialAttack - tempSpa, specialDefense - tempSpd, speed - tempSpe);

            if (currentHP != 0)
            {
                currentHP += (maxHP - tempHP);
            }

            CheckNewMoves();

            if (evolution != "doesNotEvolve" && evolutionLevel <= level)
            {
                Evolve(evolution);
            }
        }

        public void Evolve(string evolvedPokemon)
        {
            //Code that handles a Pokemon's evolution.

            Program.Log(name + " is evolving.", 0);

            Console.WriteLine("\nWhat? {0} is evolving! (Type \"(c)ancel\" to stop evolving)", this.name);

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

                    Pokemon evolution = PokemonList.allPokemon.Find(p => p.name == this.evolution);

                    string oldPokemon = this.name;
                    int oldMaxHP = this.maxHP;

                    this.name = evolution.name;
                    this.type = evolution.type;
                    this.type2 = evolution.type2;

                    this.pokedexSpecies = evolution.pokedexSpecies;
                    this.pokedexNumber = evolution.pokedexNumber;

                    this.catchRate = evolution.catchRate;

                    this.evolution = evolution.evolution;
                    this.evolutionType = evolution.evolutionType;
                    this.evolutionLevel = evolution.evolutionLevel;

                    this.baseHP = evolution.baseHP;
                    this.baseAttack = evolution.baseAttack;
                    this.baseDefense = evolution.baseDefense;
                    this.baseSpecialAttack = evolution.baseSpecialAttack;
                    this.baseSpecialDefense = evolution.baseSpecialDefense;
                    this.baseSpeed = evolution.baseSpeed;

                    this.availableMoves = MovesList.PokemonAvailableMoves(name);

                    //Then, the Pokemon's stats get re-adjusted, it gets healed by the difference between its old max HP and its new max HP, and it learns new moves, if any.
                    StatAdjust();

                    this.currentHP += (this.maxHP - oldMaxHP);

                    Program.Log(oldPokemon + " evolved into " + name + ".", 1);
                    Console.Write("Congratulations! Your {0} evolved into {1}!", oldPokemon, name);
                    Console.WriteLine("");

                    CheckNewMoves();

                    break;
            }

        }

        public Moves SelectMove(bool mandatory)
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
            else if (input == "" && !mandatory)
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

        public void CheckNewMoves()
        //This code checks if the Pokemon learns any new moves at its level.
        {
            foreach (KeyValuePair<Moves, int> move in availableMoves)
            {
                //First, the game checks whether any move in the Pokemon's available moves list matches its current level and if the Pokemon doesn't already know that move.
                if (move.Value == level && !knownMoves.Contains(move.Key))
                {
                    //If so, and if the Pokemon currently knows less than 4 moves, it learns the new move.
                    if (knownMoves.Count < 4)
                    {
                        knownMoves.Add(move.Key);
                        Console.WriteLine("\n{0} learned the move {1}!", name, move.Key.Name);
                    }

                    //Otherwise, the user is asked if the Pokemon should forget a move in order to learn the new move.
                    else
                    {
                        Console.WriteLine("\n{0} wants to learn the move {1}, but it already knows 4 moves.", name, move.Key.Name);
                        Console.WriteLine("Should a move be forgotten in order to learn {1}?\n(\"(y)es\" to forget a move, or enter to cancel)", move.Key.Name);

                        string decision = Console.ReadLine();

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
                                    Console.WriteLine("1, 2 and poof!\n{0} forgot {1} and learned {2} instead!", name, tempMove.Name, move.Key.Name);
                                    knownMoves.Remove(knownMoves.Find(moves => moves.Name == tempMove.Name));
                                    knownMoves.Add(move.Key);

                                }

                                //Else, if the input was not valid, he will be asked whether the Pokemon should forget a move again.
                                else
                                {
                                    CheckNewMoves();
                                }

                                break;

                            //If the user's answer was not "yes", the program asks to verify that the user does not want to teach the Pokemon this move.
                            default:

                                Console.WriteLine("Are you sure that {0} should not learn {1}?", name, move.Key.Name);
                                Console.WriteLine("(\"(y)es\" to cancel completely, or enter to re-try)");

                                //A second switch to verify the user's answer.
                                switch (Console.ReadLine())
                                {
                                    case "Yes":
                                    case "yes":
                                    case "y":

                                        //If his answer is yes again, the game simply returns to whatever was happening.
                                        return;

                                    default:

                                        //Any other input will start this process all over again.
                                        CheckNewMoves();
                                        break;
                                }

                                break;

                        }
                    }
                }
            }
        }
    }
}
