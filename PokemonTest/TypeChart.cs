using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    public class TypeChart
    {
        //The code for determining type multipliers during combat.

        public static double Check(Moves attack, string defender, string defender2)
        {
            //The main checking method. It takes 3 arguments - a move, and the defending Pokemon's 2 types.
            //Then, it runs Calculate method - twice if the defending Pokemon has two types - and returns the result of the calculation.
            double mult = 1;

            if (defender2 == "")
            {
                mult = Calculate(attack, defender);
            }

            else
            {                
                mult = (Calculate(attack, defender) * Calculate(attack, defender2));
            }

            return mult;
        }

        public static double Calculate(Moves attack, string defender)
        {
            //The actual calculation method. It checks whether the defender is weak, resistant or immune to the attack.
            //If it is, it returns the corresponding multiplier. Otherwise, it returns 1, for normal effectiveness.
            double mult = 1;
            string attacker = attack.Type;

            if (attacker == "Normal")
            {
                switch (defender)
                {
                    case "Rock":
                    case "Steel":
                        mult = 0.5;
                        break;
                    case "Ghost":
                        mult = 0;
                        break;
                }
            }

            else if (attacker == "Fire")
            {
                switch (defender)
                {
                    case "Water":
                    case "Rock":
                    case "Fire":
                    case "Dragon":
                        mult = 0.5;
                        break;
                    case "Grass":
                    case "Ice":
                    case "Steel":
                    case "Bug":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Water")
            {
                switch (defender)
                {
                    case "Water":
                    case "Grass":
                    case "Dragon":
                        mult = 0.5;
                        break;
                    case "Ground":
                    case "Rock":
                    case "Fire":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Grass")
            {
                switch (defender)
                {
                    case "Flying":
                    case "Poison":
                    case "Fire":
                    case "Bug":
                    case "Grass":
                    case "Dragon":
                    case "Steel":
                        mult = 0.5;
                        break;
                    case "Water":
                    case "Ground":
                    case "Rock":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Electric")
            {
                switch (defender)
                {
                    case "Electric":
                    case "Grass":
                    case "Dragon":
                        mult = 0.5;
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

            else if (attacker == "Ice")
            {
                switch (defender)
                {
                    case "Water":
                    case "Ice":
                    case "Steel":
                    case "Fire":
                        mult = 0.5;
                        break;
                    case "Flying":
                    case "Ground":
                    case "Grass":
                    case "Dragon":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Fighting")
            {
                switch (defender)
                {
                    case "Flying":
                    case "Poison":
                    case "Bug":
                    case "Psychic":
                    case "Fairy":
                        mult = 0.5;
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

            else if (attacker == "Poison")
            {
                switch (defender)
                {

                    case "Poison":
                    case "Ground":
                    case "Rock":
                    case "Ghost":
                        mult = 0.5;
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

            else if (attacker == "Ground")
            {
                switch (defender)
                {
                    case "Bug":
                    case "Grass":
                        mult = 0.5;
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

            else if (attacker == "Flying")
            {
                switch (defender)
                {
                    case "Rock":
                    case "Electric":
                    case "Steel":
                        mult = 0.5;
                        break;
                    case "Fighting":
                    case "Bug":
                    case "Grass":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Psychic")
            {
                switch (defender)
                {
                    case "Psychic":
                    case "Steel":
                        mult = 0.5;
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

            else if (attacker == "Bug")
            {
                switch (defender)
                {
                    case "Flying":
                    case "Fighting":
                    case "Fire":
                    case "Ghost":
                    case "Poison":
                    case "Steel":
                    case "Fairy":
                        mult = 0.5;
                        break;
                    case "Grass":
                    case "Psychic":
                    case "Dark":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Rock")
            {
                switch (defender)
                {
                    case "Fighting":
                    case "Ground":
                    case "Steel":
                        mult = 0.5;
                        break;
                    case "Flying":
                    case "Bug":
                    case "Fire":
                    case "Ice":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Ghost")
            {
                switch (defender)
                {
                    case "Dark":
                        mult = 0.5;
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

            else if (attacker == "Dragon")
            {
                switch (defender)
                {
                    case "Steel":
                        mult = 0.5;
                        break;
                    case "Dragon":
                        mult = 2;
                        break;                    
                    case "Fairy":
                        mult = 0;
                        break;
                }
            }

            else if (attacker == "Dark")
            {
                switch (defender)
                {
                    case "Dark":
                    case "Fairy":
                    case "Ice":
                        mult = 0.5;
                        break;
                    case "Psychic":
                    case "Ghost":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Steel")
            {
                switch (defender)
                {
                    case "Steel":
                    case "Fire":
                    case "Water":
                    case "Electric":
                        mult = 0.5;
                        break;
                    case "Fairy":
                    case "Rock":
                    case "Ice":
                        mult = 2;
                        break;
                }
            }

            else if (attacker == "Fairy")
            {
                switch (defender)
                {
                    case "Steel":
                    case "Fire":
                    case "Poison":
                        mult = 0.5;
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
                Program.Log(attack.Name + " did not have a correctly assigned type, so TypeChart.Check returned 1. (Type = " + attack.Type + ")", 2);
                mult = 1;
            }

            return mult;
        }
    }
}
