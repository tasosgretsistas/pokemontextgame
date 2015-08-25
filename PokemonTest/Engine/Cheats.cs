using System;
using PokemonTextEdition.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Engine
{
    /// <summary>
    /// This class holds the various cheats and developer tools of the game.
    /// </summary>
    class Cheats
    {
        #region Cheats 

        /// <summary>
        /// Lists the Individual Values of each Pokemon in the player's party, which are normally hidden from the player.
        /// </summary>
        public static void TellMe()
        {
            foreach (Pokemon p in Overworld.player.party)
                p.PrintIVs();

            Console.WriteLine("");
        }

        /// <summary>
        /// Sets the Individual Values of every Pokemon in the player's party to 31, effectively making them as strong as they can be, and then heals them to full.
        /// </summary>
        public static void ScrewTheRules()
        {
            Console.WriteLine("I have money!\n");

            foreach (Pokemon p in Overworld.player.party)
            {
                p.HPIV = 31;
                p.AttackIV = 31;
                p.DefenseIV = 31;
                p.SpecialAttackIV = 31;
                p.SpecialDefenseIV = 31;
                p.SpeedIV = 31;
            }

            Overworld.player.PartyHeal();
        }

        #endregion

        #region Developer Tools
        
        /// <summary>
        /// Starts a test battle between 2 Pokemon of the player's choice, including their current level.
        /// </summary>
        public static void TestBattle()
        {
            Generator gen = new Generator();
            Battle battle = new Battle();

            try
            {
                Console.Write("Your Pokemon name: ");

                string playerPokemon = Console.ReadLine();

                Console.Write("Your Pokemon level: ");

                int playerLevel = Convert.ToInt32(Console.ReadLine());

                Pokemon player = gen.Create(playerPokemon, playerLevel);

                Overworld.player.party.Add(player);

                Console.Write("Enemy Pokemon name: ");

                string enemyPokemon = Console.ReadLine();

                Console.Write("Enemy Pokemon level: ");

                int enemyLevel = Convert.ToInt32(Console.ReadLine());

                Pokemon enemy = gen.Create(enemyPokemon, enemyLevel);

                Console.WriteLine("");

                battle.Wild(enemy);
            }

            catch (Exception ex)
            {
                Console.WriteLine("You fucked up. Reason: " + ex.Message);
                Console.WriteLine("Try again.\n");

                TestBattle();
            }
        }

        /// <summary>
        /// Lists every Pokemon currently available in the game.
        /// </summary>
        public static void ListAllPokemon()
        {
            foreach (PokemonSpecies p in PokemonList.allPokemon)
            {
                string evolutionMessage = "";

                if (p.Evolves)
                    evolutionMessage = ", evolves into " + p.EvolvesInto;

                Console.WriteLine(p.Name + evolutionMessage);
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// Lists every Pokemon currently available in the game sorted by Base Stat Total (the sum of all its base stats) - for error checking purposes.
        /// </summary>
        public static void DisplayBSTs()
        {
            Dictionary<string, int> totals = new Dictionary<string, int>();

            foreach (PokemonSpecies p in PokemonList.allPokemon)
            {
                int BST = p.BaseHP + p.BaseAttack + p.BaseDefense + p.BaseSpecialAttack + p.BaseSpecialDefense + p.BaseSpeed;
                totals.Add(p.Name, BST);
            }

            totals = totals.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var i in totals)
            {
                Console.WriteLine("{0} - {1}", i.Key, i.Value);
            }
        }

        /// <summary>
        ///  Lists every Pokemon that can evolve currently available in the game as well as its respective evolution - for error checking purposes.
        /// </summary>
        public static void DisplayEvolutions()
        {
            foreach (PokemonSpecies p in PokemonList.allPokemon)
            {
                if (p.Evolves)
                    Console.WriteLine("{0} evolves into {1}.", p.Name, p.EvolvesInto);
            }
        }

        /// <summary>
        /// Lists every move currently available in the game.
        /// </summary>
        public static void ListAllMoves()
        {
            Console.WriteLine(MoveList.allMoves.Count + " moves found.\n");

            foreach (Move move in MoveList.allMoves)
            {
                Console.WriteLine(move.Name.PadRight(15) + ": Type: " + move.Type + ", Power: " + move.Damage + ", Effect ID: " + move.EffectID);
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// Lists every item currently available in the game.
        /// </summary>
        public static void ListAllItems()
        {
            foreach (Item i in ItemList.allItems)
            {
                Console.WriteLine(i.Name);
            }

            Console.WriteLine("");
        }

        #endregion
    }
}
