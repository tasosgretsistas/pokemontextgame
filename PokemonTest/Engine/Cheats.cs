using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion
    }
}
