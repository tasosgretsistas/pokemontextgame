using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;

namespace PokemonTextEdition.Classes
{
    class PokemonGenerator
    {
        /// <summary>
        /// Creates a Pokemon with no IVs. This method should only be used by other create methods as it is mandatory for Pokemon to have IVs.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to create.</param>
        /// <param name="level">The level of the Pokemon to create.</param>
        /// <returns>The non-finalized Pokemon object.</returns>
        public Pokemon BaseCreate(string name, int level)
        {
            //First, the generator has to find the species of the Pokemon in the AllPokemon list.
            PokemonSpecies species = PokemonList.AllPokemon.Find(p => p.Name == name);

            //Then, it constructs the Pokemon using the Pokemon(PokemonSpecies) constructor, which only loads the species.
            Pokemon pokemon = new Pokemon(species);

            //The available moves are acquired from the PokemonAvailableMoves method in the MoveList class.
            Dictionary<Move, int> availableMoves = MoveList.PokemonAvailableMoves(name);

            //Its level is set to the given level afterwards so that its moves and stats can be set.
            pokemon.Level = level; 

            //This loop basically adds every move that the Pokemon can learn to its knownMoves list.
            foreach (KeyValuePair<Move, int> move in availableMoves)
            {                
                if (move.Value <= pokemon.Level)                    
                    pokemon.knownMoves.Add(move.Key);
            }

            //If the Pokemon knows more than 4 moves, it loses its first move constantly until it knows 4.
            while (pokemon.knownMoves.Count > 4)                            
                pokemon.knownMoves.RemoveAt(0);
            

            //If something goes wrong and the Pokemon does not know any moves, it automatically learns a test move.
            if (pokemon.knownMoves.Count < 1)
                pokemon.knownMoves.Add(MoveList.test2);

            return pokemon;
        }

        /// <summary>
        /// Creates a Pokemon with stats and moves determined by the defined level. Its IVs are randomly set between 0 and 31.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to create.</param>
        /// <param name="level">The level of the Pokemon to create.</param>
        /// <returns>The finalized Pokemon object.</returns>
        public Pokemon Create(string name, int level)
        {
            //The BaseCreate method is run first so as to normally create a Pokemon, and then the Pokemon's IVs become randomized.
            Pokemon pokemon = BaseCreate(name, level);

            //This code gives the Pokemon random Individual Values, ranging from 0 to 31.  
            //These are later factored into the equation that determines the Pokemon's stats.
            pokemon.HPIV = Program.random.Next(0, 32);
            pokemon.AttackIV = Program.random.Next(0, 32);
            pokemon.DefenseIV = Program.random.Next(0, 32);
            pokemon.SpecialAttackIV = Program.random.Next(0, 32);
            pokemon.SpecialDefenseIV = Program.random.Next(0, 32);
            pokemon.SpeedIV = Program.random.Next(0, 32);
            
            //Finally, the Pokemon gets healed to full life, and is returned to the calling method.
            pokemon.CurrentHP = pokemon.MaxHP;

            return pokemon;
        }

        /// <summary>
        /// Creates a Pokemon with specific IVs for each stat. Its stats and moves determined by the defined level.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to create.</param>
        /// <param name="level">The level of the Pokemon to create.</param>
        /// <param name="hp">The Pokemon's HP IV.</param>
        /// <param name="atk">The Pokemon's Attack IV.</param>
        /// <param name="def">The Pokemon's Defense IV.</param>
        /// <param name="spa">The Pokemon's Special Attack IV.</param>
        /// <param name="spd">The Pokemon's Special Defense IV.</param>
        /// <param name="spe">The Pokemon's Speed IV.</param>
        /// <returns>The finalized Pokemon object.</returns>
        public Pokemon CreateSetIVs(string name, int level, int hp, int atk, int def, int spa, int spd, int spe)
        {
            //The BaseCreate method is run first so as to normally create a Pokemon, and then the Pokemon's IVs are overriden by the given ones.            
            Pokemon pokemon = BaseCreate(name, level);

            pokemon.HPIV = hp;
            pokemon.AttackIV = atk;
            pokemon.DefenseIV = def;
            pokemon.SpecialAttackIV = spa;
            pokemon.SpecialDefenseIV = spd;
            pokemon.SpeedIV = spe;

            //Finally, the Pokemon gets healed to full life, and is returned to the calling method.
            pokemon.CurrentHP = pokemon.MaxHP;

            return pokemon;
        }

        /// <summary>
        /// Creates a Pokemon with a perfect 31 IV for every stat. Its stats and moves determined by the defined level.
        /// </summary>
        /// <param name="name">The name of the species of Pokemon to create.</param>
        /// <param name="level">The level of the Pokemon to create.</param>
        /// <returns>The finalized Pokemon object.</returns>
        public Pokemon CreatePerfect(string name, int level)
        {
            //The BaseCreate method is run first so as to normally create a Pokemon, and then all of the Pokemon's IVs become 31.
            Pokemon pokemon = BaseCreate(name, level);

            pokemon.HPIV = 31;
            pokemon.AttackIV = 31;
            pokemon.DefenseIV = 31;
            pokemon.SpecialAttackIV = 31;
            pokemon.SpecialDefenseIV = 31;
            pokemon.SpeedIV = 31;

            //Finally, the Pokemon gets healed to full life, and is returned to the calling method.
            pokemon.CurrentHP = pokemon.MaxHP;

            return pokemon;
        }

    }
}
