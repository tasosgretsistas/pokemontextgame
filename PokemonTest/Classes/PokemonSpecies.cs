namespace PokemonTextEdition.Classes
{
    public class PokemonSpecies
    {
        #region Fields & Properties

        //The Pokemon's primary attributes.
        public string Name { get; set; }

        //The Pokemon's elemental types.
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public Types type = Types.Water;

        //The Pokemon's Pokedex information - its species name and number.
        public string PokedexSpecies { get; set; }
        public int PokedexNumber { get; set; }

        //This determines how difficult a Pokemon is to catch. (NYI)
        public float CatchRate { get; set; }

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
        

        #endregion

        #region Constructors

        /// <summary>
        /// Blank Pokemon constructor.
        /// </summary>
        public PokemonSpecies()
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
        public PokemonSpecies(int pNumber, string pName, string pType, string pType2, string pSpecies, float pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe)
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
        public PokemonSpecies(int pNumber, string pName, string pType, string pType2, string pSpecies, float pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, string pEvolvesInto, int pEvolutionLevel)
            : this(pNumber, pName, pType, pType2, pSpecies, pCatchRate, pBaseHP, pBaseAtk, pBaseDef, pBaseSpa, pBaseSpd, pBaseSpe)
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
        public PokemonSpecies(int pNumber, string pName, string pType, string pType2, string pSpecies, float pCatchRate, int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, string pEvolvesInto, string pEvolutionType, int pEvolutionLevel)
            : this(pNumber, pName, pType, pType2, pSpecies, pCatchRate, pBaseHP, pBaseAtk, pBaseDef, pBaseSpa, pBaseSpd, pBaseSpe)
        {
            //Constructor for Pokemon with irregular evolution.

            EvolvesInto = pEvolvesInto;
            EvolutionType = pEvolutionType;
            EvolutionLevel = pEvolutionLevel;
        }

        #endregion
    }
}
