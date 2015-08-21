namespace PokemonTextEdition
{
    /// <summary>
    /// This class determines the damage type multiplers of an attack during combat.
    /// </summary>
    public class TypeChart
    {
        /// <summary>
        /// This method checks the effectiveness of a move used against a Pokemon with specific types, and returns the result.
        /// It accomplishes this by running the Calculate() method - twice, if the defending Pokemon has two types.
        /// </summary>
        /// <param name="attackType">The attacking move's type.</param>
        /// <param name="defenderType1">The defending Pokemon's primary type.</param>
        /// <param name="defenderType2">The defending Pokemon's secondary type. By default this will be an empty string if the Pokemon doesn't have a secondary type.</param>
        /// <returns>Returns a float:
        /// 1 if the move has normal effectiveness,
        /// 0.5f or 0.25f if the move would be half effective or quarter effective respectively (not very effective), 
        /// 2 or 4 if the move would be double or quadruple effective (super effective)
        /// 0 if the move would have no effect (immune)</returns>
        public static float Check(string attackType, string defenderType1, string defenderType2)
        {
            float multiplier = 1;

            if (defenderType2 == "")
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
        /// <param name="attackType">The attack's type.</param>
        /// <param name="defenderType">The defending Pokemon's type.</param>
        /// <returns></returns>
        public static float Calculate(string attackType, string defenderType)
        {
            float mult = 1;

            if (attackType == "Normal")
            {
                switch (defenderType)
                {
                    case "Rock":
                    case "Steel":
                        mult = 0.5f;
                        break;
                    case "Ghost":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Fire")
            {
                switch (defenderType)
                {
                    case "Water":
                    case "Rock":
                    case "Fire":
                    case "Dragon":
                        mult = 0.5f;
                        break;
                    case "Grass":
                    case "Ice":
                    case "Steel":
                    case "Bug":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Water")
            {
                switch (defenderType)
                {
                    case "Water":
                    case "Grass":
                    case "Dragon":
                        mult = 0.5f;
                        break;
                    case "Ground":
                    case "Rock":
                    case "Fire":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Grass")
            {
                switch (defenderType)
                {
                    case "Flying":
                    case "Poison":
                    case "Fire":
                    case "Bug":
                    case "Grass":
                    case "Dragon":
                    case "Steel":
                        mult = 0.5f;
                        break;
                    case "Water":
                    case "Ground":
                    case "Rock":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Electric")
            {
                switch (defenderType)
                {
                    case "Electric":
                    case "Grass":
                    case "Dragon":
                        mult = 0.5f;
                        break;
                    case "Flying":
                    case "Water":
                        mult = 2;
                        break;
                    case "Ground":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Ice")
            {
                switch (defenderType)
                {
                    case "Water":
                    case "Ice":
                    case "Steel":
                    case "Fire":
                        mult = 0.5f;
                        break;
                    case "Flying":
                    case "Ground":
                    case "Grass":
                    case "Dragon":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Fighting")
            {
                switch (defenderType)
                {
                    case "Flying":
                    case "Poison":
                    case "Bug":
                    case "Psychic":
                    case "Fairy":
                        mult = 0.5f;
                        break;
                    case "Normal":
                    case "Ice":
                    case "Rock":
                    case "Dark":
                    case "Steel":
                        mult = 2;
                        break;
                    case "Ghost":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Poison")
            {
                switch (defenderType)
                {

                    case "Poison":
                    case "Ground":
                    case "Rock":
                    case "Ghost":
                        mult = 0.5f;
                        break;
                    case "Grass":
                    case "Fairy":
                        mult = 2;
                        break;
                    case "Steel":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Ground")
            {
                switch (defenderType)
                {
                    case "Bug":
                    case "Grass":
                        mult = 0.5f;
                        break;
                    case "Poison":
                    case "Fire":
                    case "Rock":
                    case "Electric":
                    case "Steel":
                        mult = 2;
                        break;
                    case "Flying":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Flying")
            {
                switch (defenderType)
                {
                    case "Rock":
                    case "Electric":
                    case "Steel":
                        mult = 0.5f;
                        break;
                    case "Fighting":
                    case "Bug":
                    case "Grass":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Psychic")
            {
                switch (defenderType)
                {
                    case "Psychic":
                    case "Steel":
                        mult = 0.5f;
                        break;
                    case "Fighting":
                    case "Poison":
                        mult = 2;
                        break;
                    case "Dark":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Bug")
            {
                switch (defenderType)
                {
                    case "Flying":
                    case "Fighting":
                    case "Fire":
                    case "Ghost":
                    case "Poison":
                    case "Steel":
                    case "Fairy":
                        mult = 0.5f;
                        break;
                    case "Grass":
                    case "Psychic":
                    case "Dark":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Rock")
            {
                switch (defenderType)
                {
                    case "Fighting":
                    case "Ground":
                    case "Steel":
                        mult = 0.5f;
                        break;
                    case "Flying":
                    case "Bug":
                    case "Fire":
                    case "Ice":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Ghost")
            {
                switch (defenderType)
                {
                    case "Dark":
                        mult = 0.5f;
                        break;
                    case "Ghost":
                    case "Psychic":
                        mult = 2;
                        break;                   
                    case "Normal":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Dragon")
            {
                switch (defenderType)
                {
                    case "Steel":
                        mult = 0.5f;
                        break;
                    case "Dragon":
                        mult = 2;
                        break;                    
                    case "Fairy":
                        mult = 0;
                        break;
                }
            }

            else if (attackType == "Dark")
            {
                switch (defenderType)
                {
                    case "Dark":
                    case "Fairy":
                    case "Ice":
                        mult = 0.5f;
                        break;
                    case "Psychic":
                    case "Ghost":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Steel")
            {
                switch (defenderType)
                {
                    case "Steel":
                    case "Fire":
                    case "Water":
                    case "Electric":
                        mult = 0.5f;
                        break;
                    case "Fairy":
                    case "Rock":
                    case "Ice":
                        mult = 2;
                        break;
                }
            }

            else if (attackType == "Fairy")
            {
                switch (defenderType)
                {
                    case "Steel":
                    case "Fire":
                    case "Poison":
                        mult = 0.5f;
                        break;
                    case "Dark":
                    case "Fighting":
                    case "Dragon":
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
