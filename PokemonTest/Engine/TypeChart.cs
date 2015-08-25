using PokemonTextEdition.Classes;

namespace PokemonTextEdition
{
    

    /// <summary>
    /// This class determines the damage type multiplers of an attack during combat.
    /// </summary>
    class TypeChart
    {
        /// <summary>
        /// This method checks the effectiveness of a move used against a Pokemon with specific types, and returns the result.
        /// It accomplishes this by running the Calculate() method - twice, if the defending Pokemon has two types.
        /// </summary>
        /// <param name="attackType">The attacking move's Types.</param>
        /// <param name="defenderType1">The defending Pokemon's primary Types.</param>
        /// <param name="defenderType2">The defending Pokemon's secondary Types. By default this will be an empty string if the Pokemon doesn't have a secondary Types.</param>
        /// <returns>Returns a float:
        /// 1 if the move has normal effectiveness,
        /// 0.5f or 0.25f if the move would be half effective or quarter effective respectively (not very effective), 
        /// 2 or 4 if the move would be double or quadruple effective (super effective)
        /// 0 if the move would have no effect (immune)</returns>
        public static float Check(Type attackType, Type defenderType1, Type defenderType2)
        {
            float multiplier = 1;

            if (defenderType2 == Type.None)
            {
                multiplier = Calculate(attackType, defenderType1);
            }

            else
            {
                multiplier = (Calculate(attackType, defenderType1) * Calculate(attackType, defenderType2));
            }

            return multiplier;
        }

        /// <summary>
        /// This move calculates the effectiveness of an attack against a specific type of defending Pokemon. Refer to the documentation for the Check() method for more info.
        /// </summary>
        /// <param name="attackType">The attack's Types.</param>
        /// <param name="defenderType">The defending Pokemon's Types.</param>
        /// <returns></returns>
        public static float Calculate(Type attackType, Type defenderType)
        {
            float mult = 1;

            if (attackType == Type.Normal)
            {
                switch (defenderType)
                {
                    case Type.Rock:
                    case Type.Steel:
                        mult = 0.5f;
                        break;
                    case Type.Ghost:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Fire)
            {
                switch (defenderType)
                {
                    case Type.Water:
                    case Type.Rock:
                    case Type.Fire:
                    case Type.Dragon:
                        mult = 0.5f;
                        break;
                    case Type.Grass:
                    case Type.Ice:
                    case Type.Steel:
                    case Type.Bug:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Water)
            {
                switch (defenderType)
                {
                    case Type.Water:
                    case Type.Grass:
                    case Type.Dragon:
                        mult = 0.5f;
                        break;
                    case Type.Ground:
                    case Type.Rock:
                    case Type.Fire:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Grass)
            {
                switch (defenderType)
                {
                    case Type.Flying:
                    case Type.Poison:
                    case Type.Fire:
                    case Type.Bug:
                    case Type.Grass:
                    case Type.Dragon:
                    case Type.Steel:
                        mult = 0.5f;
                        break;
                    case Type.Water:
                    case Type.Ground:
                    case Type.Rock:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Electric)
            {
                switch (defenderType)
                {
                    case Type.Electric:
                    case Type.Grass:
                    case Type.Dragon:
                        mult = 0.5f;
                        break;
                    case Type.Flying:
                    case Type.Water:
                        mult = 2;
                        break;
                    case Type.Ground:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Ice)
            {
                switch (defenderType)
                {
                    case Type.Water:
                    case Type.Ice:
                    case Type.Steel:
                    case Type.Fire:
                        mult = 0.5f;
                        break;
                    case Type.Flying:
                    case Type.Ground:
                    case Type.Grass:
                    case Type.Dragon:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Fighting)
            {
                switch (defenderType)
                {
                    case Type.Flying:
                    case Type.Poison:
                    case Type.Bug:
                    case Type.Psychic:
                    case Type.Fairy:
                        mult = 0.5f;
                        break;
                    case Type.Normal:
                    case Type.Ice:
                    case Type.Rock:
                    case Type.Dark:
                    case Type.Steel:
                        mult = 2;
                        break;
                    case Type.Ghost:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Poison)
            {
                switch (defenderType)
                {

                    case Type.Poison:
                    case Type.Ground:
                    case Type.Rock:
                    case Type.Ghost:
                        mult = 0.5f;
                        break;
                    case Type.Grass:
                    case Type.Fairy:
                        mult = 2;
                        break;
                    case Type.Steel:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Ground)
            {
                switch (defenderType)
                {
                    case Type.Bug:
                    case Type.Grass:
                        mult = 0.5f;
                        break;
                    case Type.Poison:
                    case Type.Fire:
                    case Type.Rock:
                    case Type.Electric:
                    case Type.Steel:
                        mult = 2;
                        break;
                    case Type.Flying:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Flying)
            {
                switch (defenderType)
                {
                    case Type.Rock:
                    case Type.Electric:
                    case Type.Steel:
                        mult = 0.5f;
                        break;
                    case Type.Fighting:
                    case Type.Bug:
                    case Type.Grass:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Psychic)
            {
                switch (defenderType)
                {
                    case Type.Psychic:
                    case Type.Steel:
                        mult = 0.5f;
                        break;
                    case Type.Fighting:
                    case Type.Poison:
                        mult = 2;
                        break;
                    case Type.Dark:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Bug)
            {
                switch (defenderType)
                {
                    case Type.Flying:
                    case Type.Fighting:
                    case Type.Fire:
                    case Type.Ghost:
                    case Type.Poison:
                    case Type.Steel:
                    case Type.Fairy:
                        mult = 0.5f;
                        break;
                    case Type.Grass:
                    case Type.Psychic:
                    case Type.Dark:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Rock)
            {
                switch (defenderType)
                {
                    case Type.Fighting:
                    case Type.Ground:
                    case Type.Steel:
                        mult = 0.5f;
                        break;
                    case Type.Flying:
                    case Type.Bug:
                    case Type.Fire:
                    case Type.Ice:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Ghost)
            {
                switch (defenderType)
                {
                    case Type.Dark:
                        mult = 0.5f;
                        break;
                    case Type.Ghost:
                    case Type.Psychic:
                        mult = 2;
                        break;                   
                    case Type.Normal:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Dragon)
            {
                switch (defenderType)
                {
                    case Type.Steel:
                        mult = 0.5f;
                        break;
                    case Type.Dragon:
                        mult = 2;
                        break;                    
                    case Type.Fairy:
                        mult = 0;
                        break;
                }
            }

            else if (attackType == Type.Dark)
            {
                switch (defenderType)
                {
                    case Type.Dark:
                    case Type.Fairy:
                    case Type.Ice:
                        mult = 0.5f;
                        break;
                    case Type.Psychic:
                    case Type.Ghost:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Steel)
            {
                switch (defenderType)
                {
                    case Type.Steel:
                    case Type.Fire:
                    case Type.Water:
                    case Type.Electric:
                        mult = 0.5f;
                        break;
                    case Type.Fairy:
                    case Type.Rock:
                    case Type.Ice:
                        mult = 2;
                        break;
                }
            }

            else if (attackType == Type.Fairy)
            {
                switch (defenderType)
                {
                    case Type.Steel:
                    case Type.Fire:
                    case Type.Poison:
                        mult = 0.5f;
                        break;
                    case Type.Dark:
                    case Type.Fighting:
                    case Type.Dragon:
                        mult = 2;
                        break;                    
                }
            }

            else //This doesn't need to exist, but I've included it as a failproof in case I've assigned an incorrect type to an attack.
            {
                Program.Log("The attack used did not have a correctly assigned type, so TypeChart.Check returned 1. (Type = " + attackType + ")", 2);
                mult = 1;
            }

            return mult;
        }
    }
}
