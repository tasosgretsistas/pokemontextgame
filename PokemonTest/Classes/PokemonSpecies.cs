namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// The elemental type of a Pokemon or a Pokemon's attack - i.e. Fire, Water, Grass, Dragon, etc.
    /// </summary>
    enum Type
    {
        None,
        Normal,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Fire,
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark,
        Fairy,
        Debug
    }

    /// <summary>
    /// The Pokemon's type of evolution, such as levelling up, special stones or being traded.
    /// </summary>
    enum EvolutionType
    {
        None,
        Level,
        Trade,
        FireStone,
        LeafStone,
        MoonStone,
        ThunderStone,
        WaterStone,
        Eevee
    }

    /// <summary>
    /// This class represents the different species of Pokemon, such as Pikachu, Bulbasaur, Charizard, etc.
    /// All species available in the game are instantiated in the PokemonList.cs file and are accessible through the allPokemon list.
    /// </summary>
    class PokemonSpecies
    {
        #region Fields

        //The Pokemon's number in the Pokedex - effectively also its unique ID number.
        public int PokedexNumber { get; set; }

        //The Pokemon's name.
        public string Name { get; set; }

        //The Pokemon's elemental types - i.e. Fire, Grass, Water, etc.
        public Type Type1 { get; set; }
        public Type Type2 { get; set; }

        //The Pokemon's Pokedex species listing - just for flavour.
        public string PokedexSpecies { get; set; }        

        //This determines how difficult a Pokemon is to catch. (NYI)
        public float CatchRate { get; set; }

        //This determines how much experience is required for a given species of Pokemon to gain a level.
        //Currently linearly set to 300 across the board.
        public int ExperienceToLevel { get; set; }

        //The Pokemon's evolution properties - what Pokemon it evolves into, how it evolves, and at what level it evolves.
        public EvolutionType EvolutionType { get; set; }
        public string EvolvesInto { get; set; }
        public string EvolvesFrom { get; set; }
        public int EvolutionLevel { get; set; }

        //Quickly determines if a Pokemon evolves.
        public bool Evolves
        {
            get
            {
                if (EvolutionType == EvolutionType.None)
                    return false;

                else
                    return true;
            }
        }

        //The base stats for the particular species of Pokemon. These determine its overall potential.
        #region Base Stats

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
        ///  Constructor for blank Pokemon species. Creates a Pokemon species named "Unnamed Species" and sets every other attribute set to 0 / false.
        /// </summary>
        public PokemonSpecies()
        {
            PokedexNumber = 0;

            Name = "Undefined Pokemon Species";

            Type1 = Type.Debug;
            Type2 = Type.None;

            PokedexSpecies = string.Empty;

            CatchRate = 0;

            ExperienceToLevel = 300;

            EvolutionType = EvolutionType.None;
            EvolvesInto = string.Empty;
            EvolvesFrom = string.Empty;
            EvolutionLevel = 0;
        }

        /// <summary>
        /// Main Pokemon species constructor, that all of the other constructors chain from. By default, Pokemon are generated as that they do not evolve. 
        /// Note that Pokemon species are only a template contained inside the actual Pokemon objects.
        /// </summary>
        /// <param name="pName">The name of the Pokemon's species.</param>
        /// <param name="pType">The Pokemon's primary type.</param>
        /// <param name="pType2">The Pokemon's secondary type, if applicable. If not, set to Type.None.</param>
        /// <param name="pSpecies">The Pokemon's Pokedex entry species descrription. This is flavor only.</param>
        /// <param name="pNumber">The Pokemon's Pokedex number.</param>
        /// <param name="pCatchRate">The Pokemon's catch rate. Base: 1. (NYI)</param>
        /// <param name="pBaseHP">The Pokemon's base HP stat, used for calculating maximum HP.</param>
        /// <param name="pBaseAtk">The Pokemon's base Attack stat, used for calculating current Attack.</param>
        /// <param name="pBaseDef">The Pokemon's base Defense stat, used for calculating current Defense.</param>
        /// <param name="pBaseSpa">The Pokemon's base Special Attack stat, used for calculating current Special Attack.</param>
        /// <param name="pBaseSpd">The Pokemon's base Special Defense stat, used for calculating current Special Defense.</param>
        /// <param name="pBaseSpe">The Pokemon's base Speed stat, used for calculating current Speed.</param>
        public PokemonSpecies(int pNumber, string pName, Type pType, Type pType2, string pSpecies, float pCatchRate, 
                              int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe)
        {
            Name = pName;

            Type1 = pType;
            Type2 = pType2;

            PokedexSpecies = pSpecies;
            PokedexNumber = pNumber;

            CatchRate = pCatchRate;

            ExperienceToLevel = 300; //Change this eventually

            BaseHP = pBaseHP;
            BaseAttack = pBaseAtk;
            BaseDefense = pBaseDef;
            BaseSpecialAttack = pBaseSpa;
            BaseSpecialDefense = pBaseSpd;
            BaseSpeed = pBaseSpe;

            EvolvesInto = "";
            EvolutionType = EvolutionType.None;
            EvolutionLevel = 0;
        }

        /// <summary>
        /// Constructor for creating Pokemon species with regular evolution via levelling up.
        /// Note that Pokemon species are only a template contained inside the actual Pokemon objects.
        /// </summary>
        /// <param name="pName">The name of the Pokemon's species.</param>
        /// <param name="pType">The Pokemon's primary type.</param>
        /// <param name="pType2">The Pokemon's secondary type, if applicable. If not, set to Type.None.</param>
        /// <param name="pSpecies">The Pokemon's Pokedex entry species descrription. This is flavor only.</param>
        /// <param name="pNumber">The Pokemon's Pokedex number.</param>
        /// <param name="pCatchRate">The Pokemon's catch rate. Base: 1. (NYI)</param>
        /// <param name="pBaseHP">The Pokemon's base HP stat, used for calculating maximum HP.</param>
        /// <param name="pBaseAtk">The Pokemon's base Attack stat, used for calculating current Attack.</param>
        /// <param name="pBaseDef">The Pokemon's base Defense stat, used for calculating current Defense.</param>
        /// <param name="pBaseSpa">The Pokemon's base Special Attack stat, used for calculating current Special Attack.</param>
        /// <param name="pBaseSpd">The Pokemon's base Special Defense stat, used for calculating current Special Defense.</param>
        /// <param name="pBaseSpe">The Pokemon's base Speed stat, used for calculating current Speed.</param>
        /// <param name="pEvolvesInto">The Pokemon that this Pokemon evolves into.</param>
        /// <param name="pEvolutionLevel">The level at which this Pokemon evolves.</param>
        public PokemonSpecies(int pNumber, string pName, Type pType, Type pType2, string pSpecies, float pCatchRate, 
                              int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, 
                              string pEvolvesInto, int pEvolutionLevel)
            : this(pNumber, pName, pType, pType2, pSpecies, pCatchRate, pBaseHP, pBaseAtk, pBaseDef, pBaseSpa, pBaseSpd, pBaseSpe)
        {
            EvolvesInto = pEvolvesInto;
            EvolutionType = EvolutionType.Level;
            EvolutionLevel = pEvolutionLevel;
        }

        /// <summary>
        /// Constructor for creating Pokemon species with irregular evolution, such as evolving with special stones.
        /// Note that Pokemon species are only a template contained inside the actual Pokemon objects.
        /// </summary>
        /// <param name="pName">The name of the Pokemon's species.</param>
        /// <param name="pType">The Pokemon's primary type.</param>
        /// <param name="pType2">The Pokemon's secondary type, if applicable. If not, set to Type.None.</param>
        /// <param name="pSpecies">The Pokemon's Pokedex entry species descrription. This is flavor only.</param>
        /// <param name="pNumber">The Pokemon's Pokedex number.</param>
        /// <param name="pCatchRate">The Pokemon's catch rate. Base: 1. (NYI)</param>
        /// <param name="pBaseHP">The Pokemon's base HP stat, used for calculating maximum HP.</param>
        /// <param name="pBaseAtk">The Pokemon's base Attack stat, used for calculating current Attack.</param>
        /// <param name="pBaseDef">The Pokemon's base Defense stat, used for calculating current Defense.</param>
        /// <param name="pBaseSpa">The Pokemon's base Special Attack stat, used for calculating current Special Attack.</param>
        /// <param name="pBaseSpd">The Pokemon's base Special Defense stat, used for calculating current Special Defense.</param>
        /// <param name="pBaseSpe">The Pokemon's base Speed stat, used for calculating current Speed.</param>
        /// <param name="pEvolvesInto">The Pokemon that this Pokemon evolves into.</param>
        /// <param name="pEvolutionType">The particular type of evolution for this Pokemon. I.E. Water Stone, trade, happiness</param>
        public PokemonSpecies(int pNumber, string pName, Type pType, Type pType2, string pSpecies, float pCatchRate, 
                              int pBaseHP, int pBaseAtk, int pBaseDef, int pBaseSpa, int pBaseSpd, int pBaseSpe, 
                              string pEvolvesInto, EvolutionType pEvolutionType, int pEvolutionLevel)
            : this(pNumber, pName, pType, pType2, pSpecies, pCatchRate, pBaseHP, pBaseAtk, pBaseDef, pBaseSpa, pBaseSpd, pBaseSpe)
        {
            EvolvesInto = pEvolvesInto;
            EvolutionType = pEvolutionType;
            EvolutionLevel = pEvolutionLevel;
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Checks if this Pokemon species is the same as another.
        /// </summary>
        /// <param name="obj">Returns true if the Pokedex number of both species is the same.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            PokemonSpecies p = obj as PokemonSpecies;

            if ((object)p == null)
                return false;

            return (PokedexNumber == p.PokedexNumber);
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
