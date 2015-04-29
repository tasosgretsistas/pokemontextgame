using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    public class TypeChart
    {
        //The code for determining type multipliers during combat.

        public static float Check(string attackType, string defenderType1, string defenderType2)
        {
            //The main checking method. It takes 3 arguments - a move, and the defending Pokemon's 2 types.
            //Then, it runs Calculate method - twice if the defending Pokemon has two types - and returns the result of the calculation.
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

        public static float Calculate(string attackType, string defenderType)
        {
            //The actual calculation method. It checks whether the defender is weak, resistant or immune to the attack.
            //If it is, it returns the corresponding multiplier. Otherwise, it returns 1, for normal effectiveness.

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
