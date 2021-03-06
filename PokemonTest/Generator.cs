﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class Generator
    {
        Random rng = new Random();


        public Pokemon BaseCreate(string name, int level)
        {
            //The code that generates a base Pokemon with no IVs. 2 parameters are needed - a valid name, and a level.            

            //First, two Pokemon objects are declared - one for the "original" Pokemon, as retrieved from the PokemonList class, and one for the "result" Pokemon to be output.
            //The result Pokemon's parameters are simply copied from the original.

            Pokemon original = PokemonList.allPokemon.Find(p => p.name == name);

            Pokemon pokemon = new Pokemon(original.name, original.type, original.type2, original.pokedexSpecies, original.pokedexNumber, original.catchRate,
                               original.baseHP, original.baseAttack, original.baseDefense, original.baseSpecialAttack, original.baseSpecialDefense, 
                               original.baseSpeed, original.evolution, original.evolutionLevel);

            pokemon.availableMoves = MovesList.PokemonAvailableMoves(name); //Then, it acquires its available moves from the MoveList method in the MovesList class.
            pokemon.level = level; //Its level is set to the given level afterwards so that its moves and stats can be set.

            foreach (KeyValuePair<Moves, int> move in pokemon.availableMoves)
            {
                if (move.Value <= pokemon.level)
                {
                    //This loop basically adds every move that the Pokemon can learn to its knownMoves list.
                    pokemon.knownMoves.Add(move.Key);
                }
            }

            while (pokemon.knownMoves.Count > 4)
            {
                //If the Pokemon knows more than 4 moves, it loses its first move constantly until it knows 4.
                pokemon.knownMoves.RemoveAt(0);
            }

            

            return pokemon;
        }

        /// <summary>
        /// This method generates Pokemon with stats and moves determined by the defined level. Its IVs are randomly set between 1 and 31.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to generate.</param>
        /// <param name="level">The level of the generated Pokemon.</param>
        /// <returns></returns>
        public Pokemon Create(string name, int level)
        {
            //This method generates Pokemon with random IVs in the 1-31 range.
            //The BaseCreate method is run first so as to normally create a Pokemon, and then the Pokemon's IVs become randomized.

            Pokemon pokemon = BaseCreate(name, level);

            //This code gives the Pokemon random Individual Values, ranging from 0 to 31.  
            //These are later factored into the equation that determines the Pokemon's stats.
            pokemon.HPIV = rng.Next(0, 32);
            pokemon.AttackIV = rng.Next(0, 32);
            pokemon.DefenseIV = rng.Next(0, 32);
            pokemon.SpecialAttackIV = rng.Next(0, 32);
            pokemon.SpecialDefenseIV = rng.Next(0, 32);
            pokemon.SpeedIV = rng.Next(0, 32);

            //Finally, the Pokemon's stats become adjusted to its level with the StatAdjust() method.
            //Then, the Pokemon gets healed to full life, and is output back to the calling method.
            pokemon.StatAdjust();
            pokemon.currentHP = pokemon.maxHP;

            return pokemon;
        }

        /// <summary>
        /// This method generates a Pokemon with specific IVs for each stat. Its stats and moves determined by the defined level.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to generate.</param>
        /// <param name="level">The level of the generated Pokemon.</param>
        /// <param name="hp">Its HP IV.</param>
        /// <param name="atk">Its Attack IV.</param>
        /// <param name="def">Its Defense IV.</param>
        /// <param name="spa">Its Special Attack IV.</param>
        /// <param name="spd">Its Special Defense IV.</param>
        /// <param name="spe">Its Speed IV.</param>
        /// <returns></returns>
        public Pokemon CreateSetIVs(string name, int level, int hp, int atk, int def, int spa, int spd, int spe)
        {
            //This method generates Pokemon with specific IVs. 
            //The BaseCreate method is run first so as to normally create a Pokemon, and then the Pokemon's IVs are overriden by the given ones.
            
            Pokemon pokemon = BaseCreate(name, level);

            pokemon.HPIV = hp;
            pokemon.AttackIV = atk;
            pokemon.DefenseIV = def;
            pokemon.SpecialAttackIV = spa;
            pokemon.SpecialDefenseIV = spd;
            pokemon.SpeedIV = spe;

            pokemon.StatAdjust();
            pokemon.currentHP = pokemon.maxHP;

            return pokemon;
        }

        /// <summary>
        /// This method generates a Pokemon with perfect 31 IVs for every stat. Its stats and moves determined by the defined level.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to generate.</param>
        /// <param name="level">The level of the generated Pokemon.</param>
        /// <returns></returns>
        public Pokemon CreatePerfect(string name, int level)
        {
            //This method generates Pokemon with perfect IVs.
            //The BaseCreate method is run first so as to normally create a Pokemon, and then all of the Pokemon's IVs become 31.

            Pokemon pokemon = BaseCreate(name, level);

            pokemon.HPIV = 31;
            pokemon.AttackIV = 31;
            pokemon.DefenseIV = 31;
            pokemon.SpecialAttackIV = 31;
            pokemon.SpecialDefenseIV = 31;
            pokemon.SpeedIV = 31;

            pokemon.StatAdjust();
            pokemon.currentHP = pokemon.maxHP;

            return pokemon;
        }
        


    }
}
