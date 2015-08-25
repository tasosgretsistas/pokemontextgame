namespace PokemonTextEdition
{
    public class Move
    {
        #region Fields & Properties

        public int ID { get; set; }

        //The move's primary identifiers - its name, elemental type, and attribute (physical, special, status).
        public string Name { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; } 

        //Its unique parameters - damage, accuracy and priority (-6 to 6).
        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public int Priority { get; set; } 

        //This bool determines whether a move can ever miss, regardless of its accuracy.
        public bool PerfectAccuracy { get; set; } 
            
        //These determine if the move has a special effect, its effect's ID (0 if no effect) 
        //and the number coefficient of its effect - i.e. the effect's probability.
        public bool SecondaryEffect { get; set; }
        public int EffectID { get; set; }
        public float EffectN { get; set; }

        //Determines if the move has been disabled from use.
        public bool Disabled { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// The primary constructor for the Move class, representing a Pokemon's potential attack.
        /// </summary>
        /// <param name="moveName">The move's name.</param>
        /// <param name="moveType">The move's elemental type.</param>
        /// <param name="moveDamage">The move's base damage.</param>
        /// <param name="moveAccuracy">The move's base accuracy, expressed in percentage. For perfect accuracy moves, set this to 100.</param>
        /// <param name="moveAttribute">The move's attack attribute - physical, special or status.</param>
        /// <param name="movePriority">The move's priority factor - base being 0. A Pokemon with a higher priority attack will attack first.</param>
        /// <param name="movePerfectAccuracy">A flag to be used for moves that cannot miss.</param>
        /// <param name="moveSecondaryEffect">A flag to be used for moves that have secondary effects.</param>
        /// <param name="moveEffectID">The move's secondary effect ID number. Each unique effect has a different ID.</param>
        /// <param name="moveEffectN">The move's secondary effect and the effect's coefficient - for instance, the effect's probability, or the amount of damage it'd deal.</param>
        public Move(string moveName, string moveType, int moveDamage, int moveAccuracy,  string moveAttribute, int movePriority, bool movePerfectAccuracy, bool moveSecondaryEffect, int moveEffectID, float moveEffectN)
        {
            Name = moveName;
            Type = moveType;
            Attribute = moveAttribute;

            Damage = moveDamage;
            Accuracy = moveAccuracy;            
            Priority = movePriority;

            PerfectAccuracy = movePerfectAccuracy;

            SecondaryEffect = moveSecondaryEffect;
            EffectID = moveEffectID;
            EffectN = moveEffectN;

            Disabled = false;
        }

        public Move()
        {
            Name = "Sample Move";
            Type = "";
        }

        #endregion
    }
}
