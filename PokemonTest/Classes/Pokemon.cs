using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// The various status conditions that a Pokemon may be afflicted by, such as sleep and paralysis.
    /// </summary>
    enum StatusCondition
    {
        None,
        Burn,
        Freeze,
        Paralysis,
        Poison,
        Sleep,
        FullHeal //Used for items that can cure any status
    }

    /// <summary>
    /// This class represents the actual instances of a Pokemon, such as a specific Pikachu owned by a trainer or in the wild.
    /// Objects of this class should be created by using the PokemonGenerator class.
    /// </summary>
    class Pokemon
    {
        #region Fields

        #region Primary Attributes

        //The Pokemon's Globally Unique ID. NYI
        //public Guid guid = new Guid();

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
                if (value != null)
                    nickname = value;
            }
        }

        //The Pokemon's experience and level - currently completely linear.
        protected int level, experience;

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
                    UI.Error("The game tried to set " + Name + "'s level to " + value + ", which is unintended behaviour.",
                             "Level property error. Pokemon: " + species.Name + ". Current level: " + Level + ". New level: " + value, 2);
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

                //This checks if the Pokemon will level up from gaining experience.
                //It is a while loop to facilitate for the event that a Pokemon could level up multiple times.
                while (experience >= species.ExperienceToLevel)
                {
                    experience -= species.ExperienceToLevel;
                    LevelUp();
                }
            }
        }

        #endregion

        #region Individual Values

        // The Pokemon's Individual Values - the individual increase per stat for each unique Pokemon.
        // These are randomly generated with the PokemonGenerator class and cannot normally be altered in-game.

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

        //The Pokemon's resulting stats after being adjusted for the Pokemon's level method. 
        //These are the Pokemon's active stats during any one fight.

        public int MaxHP
        {
            get
            {
                return ((HPIV + (2 * species.BaseHP) + 100) * Level / 100) + 10;
            }
        }

        public int Attack
        {
            get
            {
                return ((AttackIV + (2 * species.BaseAttack)) * Level / 100) + 5;
            }
        }

        public int Defense
        {
            get
            {
                return ((DefenseIV + (2 * species.BaseDefense)) * Level / 100) + 5;
            }
        }


        public int SpecialAttack
        {
            get
            {
                return ((SpecialAttackIV + (2 * species.BaseSpecialAttack)) * Level / 100) + 5;
            }
        }

        public int SpecialDefense
        {
            get
            {
                return ((SpecialDefenseIV + (2 * species.BaseSpecialDefense)) * Level / 100) + 5;
            }
        }

        public int Speed
        {
            get
            {
                return ((SpeedIV + (2 * species.BaseSpeed)) * Level / 100) + 5;
            }
        }

        #endregion

        #region Combat Properties

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

        /// <summary>
        /// Determines whether the Pokemon has fainted. Triggers Faint() if the Pokemon would faint.
        /// </summary>
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
        protected StatusCondition status;

        public StatusCondition Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        //The Pokemon's effective Speed during combat, which facilitates for the event that it might be Paralyzed, which slows it to 25% of its maximum speed.
        public int EffectiveSpeed
        {
            get
            {
                if (this.Status == StatusCondition.Paralysis)
                    return (int)(Speed * 0.25f);

                else
                    return Speed;
            }
        }

        //Temporary combat effects a Pokemon might be afflicted with.

        public int SleepCounter { get; set; }//Determines how long a Pokemon will be asleep for.
        public bool MoveLock { get; set; } //Determines if the Pokemon is locked into a move.   
        public bool LeechSeed { get; set; } //Determines if the Pokemon is afflicted by Leech Seed.        
        public bool Protect { get; set; } //Determines if a Pokemon is under the effect of Protect.
        public bool Confused { get; set; }  //Determines if a Pokemon is suffering from confusion.

        #endregion

        //A list that contains all the moves it currently knows.
        public List<Move> knownMoves = new List<Move>();

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructor for blank Pokemon. Creates a Pokemon of species MissingNo at level 1 with all of its other attributes set to 0, false and empty strings.
        /// </summary>
        public Pokemon()
        {
            species = PokemonList.AllPokemon.ElementAt(0);

            Nickname = string.Empty;

            Level = 1;
            Experience = 0;

            CurrentHP = 0;

            Status = StatusCondition.None;

            ResetTemporaryEffects(true);
        }

        /// <summary>
        /// Constructs a Pokemon with a species based on a species object.
        /// This constructor should only be used while loading the game or through the PokemonGenerator class, which is used for creating Pokemon.
        /// </summary>
        /// <param name="pokemonSpecies">The species object that will be the Pokemon's species.</param>
        public Pokemon(PokemonSpecies pokemonSpecies)
        {
            species = pokemonSpecies;
        }

        /// <summary>
        /// Constructs a Pokemon with a species based on a species name.
        /// This constructor should only be used while loading the game or through the PokemonGenerator class, which is used for creating Pokemon.
        /// </summary>
        /// <param name="speciesName">The name of the species that will be the Pokemon's species.</param>
        public Pokemon(string speciesName)
        {
            species = PokemonList.AllPokemon.Find(p => p.Name == speciesName);
        }

        /// <summary>
        /// Constructs a Pokemon with a species based on a Pokedex number.
        /// This constructor should only be used while loading the game or through the PokemonGenerator class, which is used for creating Pokemon.
        /// </summary>
        /// <param name="speciesName">The Pokedex number of the species that will be the Pokemon's species.</param>
        public Pokemon(int pokedexNumber)
        {
            species = PokemonList.AllPokemon.Find(p => p.PokedexNumber == pokedexNumber);
        }

        /// <summary>
        /// Special constructor for deconstructing Pokemon from the CompactPokemon class, for use while loading the game.
        /// Chains to the Pokemon(int) constructor.
        /// </summary>
        /// <param name="species">The Pokedex number of the Pokemon's species.</param>
        /// <param name="nickname">The Pokemon's nickname</param>
        /// <param name="level">The Pokemon's level.</param>
        /// <param name="experience">The Pokemon's experience.</param>
        /// <param name="ivs">The Pokemon's IVs, as an array of integers.</param>
        /// <param name="currentHP">The Pokemon's current HP.</param>
        /// <param name="status">The Pokemon's status condition, as StatusCondition enum.</param>
        /// <param name="moves">The Pokemon's known moves, as a list of moves.</param>
        public Pokemon(int species, string nickname, int level, int experience, int[] ivs, int currentHP, StatusCondition status, List<Move> moves)
            : this(species)
        {
            Nickname = nickname;

            Level = level;

            Experience = experience;

            for (int i = 0; i < ivs.Length; i++)
            {
                IndividualValues[i] = ivs[i];
            }

            CurrentHP = currentHP;

            Status = status;

            knownMoves = moves;
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Displays all of the Pokemon's current stats, including its level, species and its currently known moves. 
        /// This is used to show the player his Pokemon's status.
        /// </summary>
        /// <returns>A formatted string listing the Pokemon's information.
        /// Example: "Level 5 Pikachu, HP: 10/10. Type: Electric, Status: Normal. [Stats] [Known Moves]</returns>
        public string PrintInfo()
        {
            return "Level " + Level + " " + Name + ". HP: " + CurrentHP + "/" + MaxHP + ". Type: " + TypeMessage() + ". Status: " + StatusMessage() +
                   ".\nAttack: " + Attack + ". Defense: " + Defense + ". Sp. Attack: " + SpecialAttack + ". Sp Defense: " + SpecialDefense + ". Speed: " + Speed +
                   ".\nKnown moves: " + PrintMoves();
        }

        /// <summary>
        /// Displays all of the Pokemon's stats in a brief format with HP only shown in percentage.
        /// This is used for Pokemon whose stats are to remain hidden. such as enemy Pokemon.
        /// </summary>
        /// <returns>A formatted string listing the Pokemon's information in brief.
        /// Example: "Level 5 Pikachu. HP: 100%. Type: Electric. Status: Normal"</returns>
        public string PrintInfoBrief()
        {
            return "\nLevel " + Level + " " + Name + ". HP: " + PercentLife() + "%. Type: " + TypeMessage() + ". Status: " + StatusMessage() + ".";
        }

        /// <summary>
        /// Displays all of the Pokemon's known moves, formatted by the amount of moves it knows.
        /// </summary>
        /// <returns>The Pokemon's moves as string.
        /// Example: "Tackle, Scratch, Thunder Shock, Thunder Wave"</returns>
        public string PrintMoves()
        {
            string moves = "";

            for (int i = 0; i < knownMoves.Count; i++)
            {
                moves += knownMoves.ElementAt(i).Name;

                if (i + 1 < knownMoves.Count)
                    moves += ", ";

                else
                    moves += ".";
            }

            return moves;            
        }

        /// <summary>
        /// Displays all of the Pokemon's IVs. 
        /// As it is not intended for IVs to be visible to the player, this method should not be included in any list of commands.
        /// </summary>
        public string PrintIVs()
        {
            return Name + "'s IVs: HP " + HPIV + ", ATK " + AttackIV + ", DEF " + DefenseIV +
                   ", SPA " + SpecialAttackIV + ", SPD " + SpecialDefenseIV + ", SPE " + SpeedIV + ".";
        }

        /// <summary>
        /// Displays the Pokemon's remaining life expressed as a percent of its maximum life.
        /// </summary>
        /// <returns>The Pokemon's remaining life percentage as a double.</returns>
        public double PercentLife()
        {
            return Math.Round((100.0 * CurrentHP / MaxHP), 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Displays the Pokemon's status condition formatted to be displayed to the user.
        /// </summary>
        /// <returns>The Pokemon's status condition as string. Example: "Normal", "Poisoned", "Burnt", etc.</returns>
        protected string StatusMessage()
        {
            switch (Status)
            {
                case StatusCondition.None:
                    return "Normal";

                case StatusCondition.Poison:
                    return "Poisoned";

                case StatusCondition.Burn:
                    return "Burnt";

                case StatusCondition.Paralysis:
                    return "Paralyzed";

                case StatusCondition.Sleep:
                    return "Asleep";

                case StatusCondition.Freeze:
                    return "Frozen";

                default:
                    return "Condition not specified";
            }
        }

        /// <summary>
        /// Displays the Pokemon's type formatted based on whether it is dual-typed. Used in displaying the Pokemon's information.
        /// </summary>
        /// <returns>The Pokemon's type(s) as string. Example: "Normal" or "Normal\Flying"</returns>
        protected string TypeMessage()
        {
            if (species.Type2 == Type.None)
                return species.Type1.ToString();

            else
                return species.Type1 + "/" + species.Type2;
        }

        /// <summary>
        /// Checks whether a Pokemon is of a particular type.
        /// </summary>
        /// <param name="type">The type to check for.</param>
        /// <returns>True if the Pokemon is of the given type, or false if it is not.</returns>
        public bool TypeCheck(Type type)
        {
            if (species.Type1 == type || species.Type2 == type)
                return true;

            else
                return false;
        }

        #endregion

        #region Health & Status

        /// <summary>
        /// Handles the events that need to take place when a Pokemon faints, such as setting its life to 0, curing status ailments and restoring all temporary status conditions.
        /// </summary>
        protected void Faint()
        {
            CurrentHP = 0;
            CureStatus(false);
            ResetTemporaryEffects(true);
        }

        /// <summary>
        /// Heals a Pokemon to full HP.
        /// </summary>
        /// <param name="displayMessage">Determines whether a message will be displayed to the player.</param>
        public void HealFull(bool displayMessage)
        {
            if (displayMessage)
                UI.WriteLine(Name + " was restored to full health.");

            CurrentHP = MaxHP;
        }

        /// <summary>
        /// Cures a Pokemon of all its status ailments.
        /// </summary>
        /// <param name="displayMessage">Determines whether a message will be displayed to the player.</param>
        public void CureStatus(bool displayMessage)
        {
            if (displayMessage)
                UI.WriteLine(Name + " was cured of its " + Status.ToString().ToLower() + ".");

            Status = StatusCondition.None;
        }

        /// <summary>
        /// Resets every temporary status effect on the Pokemon to their default state.
        /// </summary>
        /// <param name="includeSwitchPersistentStatus">Set to true to reset effects that do not fade upon switching out.</param>
        public void ResetTemporaryEffects(bool includeSwitchPersistentStatus)
        {
            if (includeSwitchPersistentStatus)
            {
                SleepCounter = 0;
            }

            MoveLock = false;
            LeechSeed = false;
            Protect = false;
            Confused = false;
        }

        public void Paralyze()
        {
            UI.WriteLine(Name + " got paralyzed by the attack! It may be unable to move.");

            Status = StatusCondition.Paralysis;
        }

        public void Sleep(int turns)
        {
            UI.WriteLine(Name + " fell asleep!");

            Status = StatusCondition.Sleep;

            SleepCounter = turns;
        }

        public void Burn()
        {
            UI.WriteLine(Name + " got burnt by the attack!");

            Status = StatusCondition.Burn;
        }
        

        public void Poison()
        {
            UI.WriteLine(Name + " got poisoned by the attack!");

            Status = StatusCondition.Poison;
        }

        #endregion

        #region Moves

        /// <summary>
        /// Asks the player to select one of the Pokemon's moves, for example for attacking or for forgetting a move.
        /// </summary>
        /// <param name="mandatorySelection">Determines if it is mandatory for the player to make a selection.</param>
        /// <returns>The move object that the player selected if his selection was valid, or null if it was not.</returns>
        public Move SelectMove(bool mandatorySelection)
        {
            //First, all of the moves that the Pokemon knows are listed.
            for (int i = 0; i < knownMoves.Count; i++)
            {
                UI.WriteLine((i + 1) + " - " + knownMoves.ElementAt(i).Name);
            }

            //First, input is taken from the player. 
            string input = UI.ReceiveInput();

            int index;
            bool validInput = Int32.TryParse(input, out index);

            //If the input is a number corresponding to a move in the Pokemon's knownMoves list, it gets selected.
            if (validInput && index > 0 && index < (knownMoves.Count + 1))
                return knownMoves.ElementAt(index - 1);

            //If the player hit enter and the selection wasn't mandatory, he is returned back to whatever was happening.
            else if (input == "" && !mandatorySelection)
            {
                return null;
            }

            //If the input was smaller than 1, bigger than the player's party size or not a number, an error message is shown.
            else
            {
                UI.InvalidInput();

                return null;
            }
        }

        /// <summary>
        /// Checks whether the Pokemon learns any moves at its current level and if so, proceeds to attempt to learn it.
        /// This method should only be invoked when the Pokemon levels up or evolves.
        /// </summary>
        protected void NewMovesCheck()
        {
            //This returns a list of all the moves the Pokemon has access to.
            Dictionary<Move, int> availableMoves = MoveList.PokemonAvailableMoves(species.Name);

            //If the Pokemon learns a new move at its current level, then the procedure starts.
            if (availableMoves.ContainsValue(Level))
            {
                //This is a list that will contain all of the moves the Pokemon can learn at this level.
                //This facilitates for the event that the Pokemon can learn more than one move at the current level.
                List<Move> moves = new List<Move>();

                foreach (KeyValuePair<Move, int> move in availableMoves)
                {
                    if (move.Value == this.Level)
                        moves.Add(move.Key);
                }

                foreach (Move move in moves)
                {
                    LearnMoveDialogue(move);
                }
            }
        }

        /// <summary>
        /// Attempts to teach a move to the Pokemon.
        /// <para>Pokemon should preferably only learn new moves through this method, as it contains the necessary checks for adding and removing moves from the knownMoves list.</para>
        /// </summary>
        /// <param name="move">The move to teach to the Pokemon.</param>
        public void LearnMoveDialogue(Move move)
        {
            //First, the game checks whether the Pokemon already knows that move, as Pokemon cannot know a move twice.
            if (!knownMoves.Exists(m => m.MoveID == move.MoveID))
            {
                //Then, the game checks whether the Pokemon already knows 4 moves. If it doesn't, it learns the move immediately.
                if (knownMoves.Count < 4)
                    LearnMove(move);

                //If the Pokemon does know 4 moves, the user is asked if the Pokemon should forget a move in order to learn the new move.
                else
                {
                    bool stop = false; //Determines if the operation should end in the following do-while loop

                    do
                    {
                        UI.WriteLine(Name + " wants to learn the move " + move.Name + ", but it already knows 4 moves.\n\n" +
                                    "Should a move be forgotten in order to learn " + move.Name + "?\n(\"(y)es\" to forget a move, or enter to cancel)");

                        //Input is taken from the player.
                        string decision = UI.ReceiveInput();

                        switch (decision.ToLower())
                        {
                            case "yes":
                            case "y":

                                //If the player typed "yes", he is asked to give input as to which move should be forgotten.
                                //The && part here is necessary because otherwise it is possible for some freak infinite loop to occur. It's just a precaution.
                                if (ForgetMoveDialogue(false) && knownMoves.Count > 1) 
                                {
                                    //If he selected a valid move to forget, that move is forgotten and this operation is now finished.

                                    UI.Write("And... ");

                                    LearnMove(move);

                                    stop = true;
                                }                                

                                break;

                            //If the user's answer was not "yes", the program asks to verify that the user does not want to teach the Pokemon this move.
                            default:

                                UI.WriteLine("Are you sure that " + Name + " should not learn " + move.Name + "?\n" +
                                            "(\"(y)es\" to cancel completely, or enter to re-try)");

                                //Input is taken from the player.
                                string decision2 = UI.ReceiveInput();

                                switch (decision2.ToLower())
                                {
                                    case "yes":
                                    case "y":

                                        //If the player typed "yes", the operation is now finished.

                                        stop = true;

                                        break;
                                }

                                break;
                        }
                    }

                    while (!stop);
                }
            }            
        }

        /// <summary>
        /// Attempts to have the Pokemon forget a move.
        /// Pokemon should preferably only forget moves through this method, as it contains the necessary checks for adding and removing moves from the knownMoves list.
        /// </summary>
        /// <param name="newLine">Determines if the game will add a line break after the displayed message.</param>
        /// <returns>True if the Pokemon forgot a move in this manner, or false if it did not.</returns>
        public bool ForgetMoveDialogue(bool newLine)
        {
            //First, the game checks if the Pokemon knows more than 1 move, as Pokemon have to know at least 1 move to be able to fight.
            if (knownMoves.Count > 1)
            {
                UI.WriteLine("Which move should be forgotten?");

                //Then, the player is asked to select a move to forget from the Pokemon's known moves.
                Move tempMove = SelectMove(false);

                //If the player didn't select a valid move, the operation is cancelled and this method returns false.
                if (tempMove == null)
                    return false;

                //If the player did select a valid move, that move is forgotten and this method returns true.
                else
                {
                    ForgetMove(tempMove, newLine);

                    return true;
                }
            }

            //If the Pokemon only knew one move, the operation is cancelled and this method returns false.
            else
            {
                UI.WriteLine(Name + " has to know at least 1 move!");

                return false;
            }
        }

        /// <summary>
        /// Has the Pokemon learn a new move.
        /// Note that it is prefered that Pokemon only learn new moves through the LearnMoveDialogue method 
        /// as it contains the necessary checks that need to be made before a Pokemon can learn a move.
        /// </summary>
        /// <param name="move">The move to learn.</param>
        protected void LearnMove(Move move)
        {
            knownMoves.Add(move);

            UI.WriteLine(Name + " learned the move " + move.Name + "!\n");
        }

        /// <summary>
        /// Has the Pokemon forget a move it knows.
        /// Note that it is prefered that Pokemon only forget moves through the ForgetMoveDialogue method 
        /// as it contains the necessary checks that need to be made before a Pokemon can learn a move.
        /// </summary>
        /// <param name="move">The move to forget.</param>
        /// <param name="newLine">Determines if the game will add a line break after the displayed message.</param>
        protected void ForgetMove(Move move, bool newLine)
        {
            if (knownMoves.Contains(move))
            {
                knownMoves.Remove(knownMoves.Find(moves => moves.Name == move.Name));

                string message = "1, 2 and poof! " + Name + " forgot " + move.Name + "!";

                if (newLine)
                    message += "\n";

                UI.WriteLine(message);
            }

            else
                UI.Error("The game attempted to have " + Name + " forget a move that it does not know.",
                         Name + " attempted to forget " + move.Name + " at level " + Level + ".", 2);
        }

        #endregion

        #region Level Up & Evolution

        /// <summary>
        /// Handles the events that need to take place when a Pokemon gains a level, such as increasing its level by 1, checking if it can learn new moves and checking if it will evolve.
        /// <para>This method is automatically invoked by the Experience property when the Pokemon would level up by gaining experience, and should also be invoked by other methods of increasing the Pokemon's level, such as using a Rare Candy.</para>
        /// </summary>
        public void LevelUp()
        {
            Program.Log(Name + " levelled up.", 0);

            //Temporary values which help show the relative increase in stats after levelling up.
            int tempHP = MaxHP, tempAtk = Attack, tempDef = Defense, tempSpa = SpecialAttack, tempSpd = SpecialDefense, tempSpe = Speed;

            //Obviously with levelling up, the Pokemon's level increases by 1.
            Level++;

            //The specific increase in HP the Pokemon gained by levelling up.
            //This is necessary in particular so that the Pokemon will still have the same amount of HP as a percentage after levelling up.
            int differenceHP = MaxHP - tempHP;

            UI.WriteLine(Name + " levelled up to " + Level + "!\n" +
                         "HP +" + (differenceHP) + ", Attack +" + (Attack - tempAtk) + ", Defense +" + (Defense - tempDef) +
                         ", Sp. Attack +" + (SpecialAttack - tempSpa) + ", Sp. Defense +" + (SpecialDefense - tempSpd) + ", Speed +" + (Speed - tempSpe) + ".\n");

            //Only non-fainted Pokemon should be healed when levelling up, otherwise a Rare Candy would also function as a Revive in that it 
            //would also revive and heal the Pokemon. Therefore, the game checks that the Pokemon is not fainted before attempting to heal it.
            if (!Fainted) 
            {
                //If the difference between the Pokemon's new max HP and its old max HP added to its current HP is smaller than its max HP, it gets healed for the difference.
                if (MaxHP > CurrentHP + differenceHP)
                    CurrentHP += differenceHP;

                //Else it gets simply healed to full.
                else
                    CurrentHP = MaxHP;
            }

            //The game then checks if the Pokemon can learn new moves at its level.
            NewMovesCheck();

            //Finally, the game checks if the Pokemon will evolve because it levelled up.
            EvolutionCheck(EvolutionType.Level);
        }

        /// <summary>
        /// Checks whether the Pokemon should evolve if so, proceeds to attempt to evolve.
        /// </summary>
        /// <param name="evolutionType">The type of evolution that is to be triggered.
        /// For example, if the Pokemon is checking whether it evolves after levelling up, this should be EvolutionType.Level.</param>
        protected void EvolutionCheck(EvolutionType evolutionType)
        {
            //If the Pokemon's species can evolve and its evolution type is the same as triggering evolution type, the operation carries on.
            if (species.Evolves && species.EvolutionType == evolutionType)
            {
                //If the Pokemon's species evolves via levelling up and its current level is higher than or equal to the level at which it evolves, it evolves.
                if (species.EvolutionType == EvolutionType.Level && species.EvolutionLevel <= Level)
                {
                    EvolutionDialogue(species.EvolvesInto);
                }

                //IMPLEMENT: Add other evolution types.
            }
        }

        /// <summary>
        /// Attempts to have a Pokemon evolve.
        /// <para>Pokemon should preferably only evolve through this method, as it presents the player with the option to cancel the evolution process.</para>
        /// </summary>
        /// <param name="evolvedPokemon">The name of the Pokemon to evolve into. (Not in use - all Pokemon currently evolve using their "species.EvolvesInto" property)</param>
        /// <returns></returns>
        public bool EvolutionDialogue(string evolvedPokemon)
        {
            Program.Log(Name + " is evolving.", 0);

            UI.WriteLine("\nWhat? " + Name + " is evolving! (Type \"(c)ancel\" to stop evolving)");

            //As is traditional in the games, the player has the option to cancel evolving so the game asks for input.
            string decision = UI.ReceiveInput();
            
            switch (decision.ToLower())
            {
                case "cancel":
                case "c":

                    //If the player chose to cancel, the operation ends.
                    Program.Log("The player chose to cancel evolving.", 0);

                    return false;

                default:

                    //Otherwise, if the player did not choose to cancel, the Pokemon evolves.
                    Evolve(PokemonList.AllPokemon.Find(p => p.Name == species.EvolvesInto));

                    return true;
            }
        }

        /// <summary>
        /// Has the Pokemon evolve into another Pokemon, effectively changing its species object.
        /// <para>Note that it is prefered that Pokemon only evolve through the EvolutionDialogue method as it contains the necessary checks that need to be made before a Pokemon can evolve.</para>
        /// </summary>
        /// <param name="evolutionSpecies">This Pokemon's evolved species. For instance, for Charmander this would be Charmeleon.</param>
        protected void Evolve(PokemonSpecies evolutionSpecies)
        {
            //The Pokemon's previous name -- which would be its species name if it is not nicknamed -- saved here so that it will be displayed to the user.
            string oldPokemon = Name;

            //The Pokemon's previous maximum HP. Used for calculating the relative increase in max HP, useful later.            
            int oldMaxHP = MaxHP;

            species = evolutionSpecies;

            //The specific increase in HP the Pokemon gained by evolving.
            //This is necessary in particular so that the Pokemon will still have the same amount of HP as a percentage after evolving.
            int differenceHP = MaxHP - oldMaxHP;

            //Only non-fainted Pokemon should be healed when evolving, otherwise an evolution stone would also function as a Revive in that it 
            //would revive and heal the Pokemon. Therefore, the game checks that the Pokemon is not fainted before attempting to heal it.
            if (!Fainted) //This is needed as fainted Pokemon can be evolved by using an evolution stone.
            {
                //If the difference between the Pokemon's new max HP and its old max HP added to its current HP is smaller than its max HP, it gets healed for the difference.
                if (MaxHP > CurrentHP + differenceHP)
                    CurrentHP += differenceHP;

                //Else it gets simply healed to full.
                else
                    CurrentHP = MaxHP;
            }

            Program.Log(oldPokemon + " evolved into " + species.Name + ".", 1);

            UI.Write("Congratulations! Your " + oldPokemon + " evolved into " + species.Name + "!\n");

            //Finally, the game then checks if the Pokemon can learn new moves at its level.
            NewMovesCheck();
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Checks if this Pokemon is the same as another.
        /// </summary>
        /// <param name="obj">Returns true if the two Pokemon are equal through reference equality.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Pokemon p = obj as Pokemon;

            if ((object)p == null)
                return false;

            return (p == this);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
