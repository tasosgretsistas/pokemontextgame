using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// The move's attribute - physical, special or status.
    /// </summary>
    enum MoveAttribute
    {
        None,
        Physical,
        Special,
        Status
    }

    enum MoveEffect
    {
        None,
        Burn,
        Poison,
        Paralysis,
        Sleep,
        Confusion,
        LeechSeed,
        Recoil,
        MoveLock,
        IncreasedCritChance,
        SetDamage,
        SetDamagePerLevel,
        ConsecutiveDamage,
        ClearHazards,
        Protect,
        MultipleHits,
        Disable,
        Pursuit,
        ItemSteal,

    }

    /// <summary>
    /// This class represents the various moves, or the attacks that Pokemon can use.
    /// All moves available in the game are instantiated in the MoveList.cs file and are accessible through the allMoves list.
    /// </summary>
    class Move
    {
        #region Fields

        //The move's primary identifiers.

        /// <summary>
        /// The move's unique ID number.
        /// </summary>
        public int MoveID { get; set; }        

        /// <summary>
        /// The move's name, explicitly in string form as it will be displayed to the player.
        /// Example: "Tackle" or "Scratch"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The move's elemental type. Example: Type.Grass, Type.Fire, Type.Water
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The move's attribute: MoveAttribute.Physical for physical attacks, MoveAttribute.Special for special attacks,
        /// and MoveAttribute.Status for moves that do not do damage and only have an effect that needs resolving.
        /// </summary>
        public MoveAttribute Attribute { get; set; } 

        //The move's parameters.

        /// <summary>
        /// The base amount of damage that would be dealt to a Pokemon by this move.
        /// This gets plugged into a more complicated formula during battle that calculates the exact amount of damage a move would deal.
        /// </summary>
        public int Damage { get; set; }

        private int accuracy;

        /// <summary>
        /// The move's accuracy - effectively its chance to hit the enemy Pokemon expressed in %. 
        /// Therefore, this should be in the range of 0 to 100.
        /// Example: 50 would mean the move will hit 1 in 2 times, and 100 would mean the move will hit every time.
        /// </summary>
        public int Accuracy
        {
            get
            {
                return accuracy;
            }

            set
            {
                if (value < 0)
                {
                    UI.Error("The game tried to assign an invalid accuracy value to a move.",
                             "The game tried to assign a value of " + value + " to " + Name + "'s accuracy value.", 2);

                    accuracy = 0;
                }

                else if (value > 100)
                {
                    UI.Error("The game tried to assign an invalid accuracy value to a move.",
                             "The game tried to assign a value of " + value + " to " + Name + "'s accuracy value.", 2);

                    accuracy = 100;
                }

                else
                    accuracy = value;
            }
        }

        protected int priority;

        /// <summary>
        /// The move's priority. A move with a higher priority will go first, regardless of the Pokemon's speed parameter.
        /// This should be in the range of -7 to 7.
        /// </summary>
        public int Priority
        {
            get
            {
                return priority;
            }

            set
            {
                if (value < -7)
                {
                    UI.Error("The game tried to assign an invalid priority value to a move.",
                             "The game tried to assign a value of " + value + " to " + Name + "'s priority value.", 2);

                    priority = -7;
                }

                else if (value > 7)
                {
                    UI.Error("The game tried to assign an invalid priority value to a move.",
                             "The game tried to assign a value of " + value + " to " + Name + "'s priority value.", 2);

                    priority = 7;
                }

                else
                    priority = value;
            }
        } 

        /// <summary>
        /// Determines if a move will ever miss, regardless of its accuracy.
        /// </summary>
        public bool PerfectAccuracy { get; set; }

        //The move's effect parameters. 

        /// <summary>
        /// Determines if the move has a secondary effect that needs to be resolved after it deals damage.
        /// </summary>
        public bool SecondaryEffect { get; set; }

        /// <summary>
        /// The move's effect, if it has any. 
        /// </summary>
        public MoveEffect Effect { get; set; }

        /// <summary>
        /// The coefficient of the move's effect, which can stand for one of several things, such as:
        /// <para>The probability of the effect occurring, Range 0-100.</para>
        /// <para>The amount of hits a multi-hit move would do. Int ranging 2-5.</para>
        /// <para>The amount of damage a move with set damage will deal. Range 0-Any.</para>
        /// <para>The amount of damage the Pokemon would suffer as recoil. Range 0-1.0f.</para>
        /// <para>The relative increase in the move's critical strike chance. Range 2-Any.</para>
        /// </summary>
        public float EffectCoefficient { get; set; }        

        /// <summary>
        /// Determines if the move has been disabled from use.
        /// </summary>
        public bool Disabled { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Blank move constructor. Creates a move named "Undefined Move" and sets every other attribute set to 0 / false.
        /// </summary>
        public Move()
        {
            MoveID = 0;

            Name = "Undefined Move";
            Type = Type.None;
            Attribute = MoveAttribute.None;

            Damage = 0;
            Accuracy = 0;
            Priority = 0;

            PerfectAccuracy = false;
            
            Effect = MoveEffect.None;
            EffectCoefficient = 0;
            SecondaryEffect = false;

            Disabled = false;
        }

        /// <summary>
        /// Constructor for creating moves, which are the various attacks that Pokemon can use.
        /// </summary>
        /// <param name="moveID">The move's unique ID number.</param>
        /// <param name="moveName">The move's name.</param>
        /// <param name="moveType">The move's elemental type.</param>
        /// <param name="moveDamage">The move's base damage.</param>
        /// <param name="moveAccuracy">The move's base accuracy, expressed in percentage.</param>
        /// <param name="moveAttribute">The move's attribute - physical, special or status.</param>
        /// <param name="movePriority">The move's priority factor - ranging -7 to 7, base being 0. 
        /// A Pokemon whose move has a higher priority will attack first, regardless of speed</param>
        /// <param name="movePerfectAccuracy">Determines if the attack can ever miss, regardless of its accuracy.</param>
        /// <param name="moveSecondaryEffect">Determines if the move has a secondary effect that it needs to be resolved after it does damage.</param>
        /// <param name="moveEffect">The move's effect. Set to MoveEffect.None for moves that do not have an effect.</param>
        /// <param name="moveEffectCoefficient">The coefficient of the move's effect, i.e. its probability of occuring (expressed in %).
        /// See the the EffectCoefficient property for more information.</param>
        public Move(int moveID, string moveName, Type moveType, 
                    int moveDamage, int moveAccuracy,  MoveAttribute moveAttribute, int movePriority, bool movePerfectAccuracy,
                    bool moveSecondaryEffect, MoveEffect moveEffect, float moveEffectCoefficient)
        {
            MoveID = moveID;

            Name = moveName;
            Type = moveType;
            Attribute = moveAttribute;

            Damage = moveDamage;
            Accuracy = moveAccuracy;            
            Priority = movePriority;

            PerfectAccuracy = movePerfectAccuracy;

            SecondaryEffect = moveSecondaryEffect;
            Effect = moveEffect;
            EffectCoefficient = moveEffectCoefficient;

            Disabled = false;
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Checks if this move is the same as another.
        /// </summary>
        /// <param name="obj">Returns true if the ID of both moves is the same.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Move m = obj as Move;

            if ((object)m == null)
                return false;

            return (MoveID == m.MoveID);
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
