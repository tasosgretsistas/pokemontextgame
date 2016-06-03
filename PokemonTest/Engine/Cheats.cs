using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using System;
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

        public static void CheatListener()
        {
            UI.WriteLine("I'm listening...");

            string input = UI.ReceiveInput().ToLower();

            switch (input)
            {
                case "god mode":
                    GodMode();
                    CheatListener();
                    break;

                case "testbattle":
                case "list pokemon":
                case "list pokemon bst":
                case "list pokemon evolution":
                case "list moves":
                case "list items":
                    Authentication(input.ToLower());
                    break;
            }
        }

        public static void Authentication(string command)
        {
            if (Settings.GodMode)
            {
                switch (command)
                {
                    case "testbattle":
                        TestBattle();
                        break;

                    case "list pokemon":
                        ListAllPokemon();
                        break;

                    case "list pokemon bst":
                        DisplayBSTs();
                        break;

                    case "list pokemon evolution":
                        DisplayEvolutions();
                        break;

                    case "list moves":
                        ListAllMoves();
                        break;

                    case "list items":
                        ListAllItems();
                        break;
                }
            }

            else
                UI.InvalidInput();

        }

        /// <summary>
        /// Lists the Individual Values of each Pokemon in the player's party, which are normally hidden from the player.
        /// </summary>
        public static void TellMe()
        {
            foreach (Pokemon p in Game.Player.Party)
                UI.WriteLine(p.PrintIVs());

            UI.WriteLine("");
        }

        /// <summary>
        /// Sets the Individual Values of every Pokemon in the player's party to 31, effectively making them as strong as they can be, and then heals them to full.
        /// </summary>
        public static void ScrewTheRules()
        {
            UI.WriteLine("I have money!\n");

            foreach (Pokemon p in Game.Player.Party)
            {
                p.HPIV = 31;
                p.AttackIV = 31;
                p.DefenseIV = 31;
                p.SpecialAttackIV = 31;
                p.SpecialDefenseIV = 31;
                p.SpeedIV = 31;
            }

            Game.PartyHeal(true);
        }

        #endregion

        #region Developer Tools

        public static void GodMode()
        {
            if (Settings.GodMode == false)
            {
                Settings.GodMode = true;

                UI.WriteLine("Yes! I am a god!\n");
            }

            else
            {
                Settings.GodMode = false;

                UI.WriteLine("A god no longer.\n");
            }
        }

        /// <summary>
        /// Starts a test battle between 2 Pokemon of the player's choice at a specified level.
        /// </summary>
        public static void TestBattle()
        {
            if (Settings.GodMode)
            {
                PokemonGenerator generator = new PokemonGenerator();

                try
                {
                    Game.Player = new Player();

                    UI.Write("Enter your Pokemon's name: ");

                    string playerPokemon = UI.ReceiveInput();

                    UI.Write("Enter your Pokemon's level: ");

                    int playerLevel = Convert.ToInt32(UI.ReceiveInput());

                    Game.Player.Party.Add(generator.Create(playerPokemon, playerLevel));

                    UI.Write("Enemy Pokemon name: ");

                    string enemyPokemon = UI.ReceiveInput();

                    UI.Write("Enemy Pokemon level: ");

                    int enemyLevel = Convert.ToInt32(UI.ReceiveInput());

                    UI.WriteLine("");

                    Battle battle = new Battle(generator.Create(enemyPokemon, enemyLevel));
                }

                catch (Exception ex)
                {
                    UI.Error(ex.Message, "", 0);

                    TestBattle();
                }
            }

            else
                UI.InvalidInput();
        }

        /// <summary>
        /// Lists every Pokemon currently available in the game.
        /// </summary>
        public static void ListAllPokemon()
        {
            if (Settings.GodMode)
            {
                foreach (PokemonSpecies p in PokemonList.AllPokemon)
                {
                    string evolutionMessage = "";

                    if (p.Evolves)
                        evolutionMessage = ", evolves into " + p.EvolvesInto;

                    UI.WriteLine(p.PokedexNumber.ToString().PadLeft(3, '0') + " - " + p.Name + evolutionMessage);
                }

                UI.WriteLine("");
            }
        }

        /// <summary>
        /// Lists every Pokemon currently available in the game sorted by Base Stat Total (the sum of all its base stats) - for error checking purposes.
        /// </summary>
        public static void DisplayBSTs()
        {
            if (Settings.GodMode)
            {

                Dictionary<string, int> totals = new Dictionary<string, int>();

                foreach (PokemonSpecies pokemon in PokemonList.AllPokemon)
                {
                    int BST = pokemon.BaseHP + pokemon.BaseAttack + pokemon.BaseDefense + pokemon.BaseSpecialAttack + pokemon.BaseSpecialDefense + pokemon.BaseSpeed;
                    totals.Add(pokemon.Name, BST);
                }

                totals = totals.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                foreach (var i in totals)
                {
                    UI.WriteLine(i.Key.PadRight(11, ' ') + " - " + i.Value);
                }

                UI.WriteLine("");
            }
        }

        /// <summary>
        ///  Lists every Pokemon that can evolve currently available in the game as well as its respective evolution - for error checking purposes.
        /// </summary>
        public static void DisplayEvolutions()
        {
            if (Settings.GodMode)
            {

                foreach (PokemonSpecies pokemon in PokemonList.AllPokemon)
                {
                    if (pokemon.Evolves)
                        UI.WriteLine(pokemon.Name + " evolves into " + pokemon.EvolvesInto + ".");
                }
            }
        }

        /// <summary>
        /// Lists every move currently available in the game.
        /// </summary>
        public static void ListAllMoves()
        {
            if (Settings.GodMode)
            {
                UI.WriteLine(MoveList.AllMoves.Count + " moves found.\n");


                UI.WriteLine("Move Name".PadLeft(11) + "Type".PadLeft(9) + "Power".PadLeft(9) + "Effect".PadLeft(12) + "Coefficient".PadLeft(15));
                UI.WriteLine("".PadRight(60, '-'));

                foreach (Move move in MoveList.AllMoves)
                {
                    UI.WriteLine(move.Name.PadRight(15) + move.Type.ToString().PadRight(10) + move.Damage.ToString().PadRight(5) +
                                 move.Effect.ToString().PadRight(20) + move.EffectCoefficient.ToString().PadRight(3));
                }

                UI.WriteLine("");
            }
        }

        /// <summary>
        /// Lists every item currently available in the game.
        /// </summary>
        public static void ListAllItems()
        {
            foreach (Item i in ItemList.AllItems)
            {
                UI.WriteLine(i.Name);
            }

            UI.WriteLine("");
        }

        #endregion
    }
}
