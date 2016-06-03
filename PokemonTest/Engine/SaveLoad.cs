using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace PokemonTextEdition.Engine
{
    /// <summary>
    /// This class is dedicated to saving and loading the game by utilizing the SaveState class.
    /// </summary>
    class SaveLoad
    {
        /// <summary>
        /// The name of the save game file to be used.
        /// </summary>
        private const string SaveGame = "pokemontext.sav";

        #region General

        /// <summary>
        /// This code handles saving the game. It is wrapped in a try-catch statement to facilitate for the amount of things that can go wrong during the read/write operation.
        /// </summary>
        public static void Save()
        {
            try
            {
                bool operation = true;

                //If a save file already exists, the user is asked whether he wants to overwrite this save file before the operation goes on.
                if (File.Exists(SaveGame))
                {
                    operation = false;

                    UI.WriteLine("A save file already exists:\n");

                    //This quickly loads the save file so that it can show the player the save file's data.
                    using (Stream stream = File.Open(SaveGame, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        SaveState savestate = (SaveState)formatter.Deserialize(stream);

                        stream.Close();

                        UI.WriteLine(SaveInformation(savestate, Settings.GameVersion));
                    }

                    UI.WriteLine("Do you want to overwrite it?\nType (y)es to overwrite or press Enter to cancel");

                    string confirmation = UI.ReceiveInput();

                    switch (confirmation)
                    {
                        case "yes":
                        case "y":

                            operation = true;

                            break;
                    }
                }

                //If there was no save file, or if the player chose to overwrite it, the operation continues.
                if (operation)
                    PackSave();
            }

            //If the operation is unsuccesful, a relevant message is displayed. Then, program flow resumes as usual.
            catch (Exception ex)
            {
                UI.Error("There was a problem with saving the game.\n", "There was a problem with saving the game: " + ex.Message, 2);

                UI.WriteLine("The game will now return to what was happening.");
                UI.AnyKey();
            }
        }

        /// <summary>
        /// This code handles loading the game. It is wrapped in a try-catch statement to facilitate for the amount of things that can go wrong during the read/write operation.
        /// </summary>
        public static void Load()
        {
            //This object will represent the player that'll be loaded from the save file.
            //It starts at null as a player has not yet been loaded.
            Player player = null;

            try
            {
                //First, the program checks whether a correctly named save game file exists.               
                if (File.Exists(SaveGame))
                {
                    //If it does exist, it then tries to open the file and display the player's information.
                    using (Stream stream = File.Open(SaveGame, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        SaveState savestate = (SaveState)formatter.Deserialize(stream);

                        stream.Close();

                        UI.WriteLine(SaveInformation(savestate, Settings.GameVersion));

                        UI.WriteLine("Would you like to load this game?");

                        string confirmation = UI.ReceiveInput();

                        //If the player chooses to load the file, the player object becomes equal to the player from the save file.
                        if (confirmation == "yes" || confirmation == "y")
                        {
                            UnpackGameState(savestate.GameState);
                            player = UnpackSave(savestate);
                        }                    
                    }
                }

                //If no correctly named save game file exists, an error is displayed and the game starts from the beginning.
                else
                    UI.WriteLine("No save file could be found!\n" + 
                                 "Please verify that your save game file is correctly named \"" + SaveGame + "\".\n");
            }

            //If the operation is unsuccesful, a relevant message is displayed and the game goes back to the main menu.
            catch (Exception ex)
            {
                UI.Error("There was a problem with loading the game.\n", "There was a problem with loading the game: " + ex.Message, 2);
            }

            //If the operation from the loading process was succesful, the player object will no longer be null and the game can go on.
            if (player != null)
            {
                Game.Player = player;

                Program.Log("The game loaded successfully.", 1);

                UI.AnyKey();

                //The game loads the player's location.
                Overworld.LoadLocationString(Game.Location);
            }

            //Otherwise, the game goes back to the main menu.
            else
            {
                UI.WriteLine("Returning to the main menu.");
                UI.AnyKey();

                Program.MainMenu();
            }
        }

        /// <summary>
        /// Briefly displays the player's information stored in this particular save state object.
        /// </summary>
        /// <param name="save">The save file to examine.</param>
        /// <param name="currentVersion">The current game version this save file is being accessed from.</param>
        /// <returns>A formatted string containing the save state's information.</returns>
        static string SaveInformation(SaveState save, string currentVersion)
        {
            string gameVersion = "";

            if (currentVersion == save.GameVersion)
                gameVersion = " (current)";

            return "This save file was created with game version " + save.GameVersion + gameVersion + ".\n" +
                   "Player name: " + save.Player.PlayerName + ". Pokemon seen: " + save.Player.SeenPokemon.Length + ", caught: " + save.Player.CaughtPokemon.Length + ".\n" +
                   "Last save date was on " + save.SaveDate + ".\n";
        }

        #endregion

        #region Packing

        /// <summary>
        /// Compresses the player's information into a save state object and then writes it into the savegame file.
        /// </summary>
        static void PackSave()
        {
            UI.Write("Saving game... ");

            //First, the game tries to open a stream for the save file. If it succeeds, all is well, and the operation continues as normal.
            using (Stream stream = File.Open(SaveGame, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                Game.Location = Overworld.CurrentLocation.Tag.ToString();

                // A savestate object is created, containing the current game and player information...
                SaveState save = new SaveState(Settings.GameVersion, Story.beginDate, DateTime.Now, PackGameState(), PackPlayer(Game.Player));

                //... which is then serialized into the "saveGame" file, and the operation terminates with program flow resuming as usual.
                formatter.Serialize(stream, save);

                stream.Close();

                Program.Log("The game saved successfully.", 1);

                UI.WriteLine("Saved!\n");
            }
        }
        
        static CompactGameState PackGameState()
        {
            return new CompactGameState(Game.RivalName, Game.StarterPokemon, Game.Location, Game.LastHealLocation, Game.DefeatedTrainers.ToArray(), null);
        }

        /// <summary>
        /// Compresses a player object along with its information to their compact forms, used for saving the game.
        /// </summary>
        /// <param name="player">The player to compress.</param>
        /// <returns>The resulting CompactPlayer object.</returns>
        static CompactPlayer PackPlayer(Player player)
        {
            CompactItem[] items = new CompactItem[player.Bag.Count];

            for (int i = 0; i < player.Bag.Count; i++)
            {
                items[i] = ItemToCompact(player.Bag.ElementAt(i));
            }

            CompactPokemon[] partyPokemon = new CompactPokemon[player.Party.Count];

            for (int i = 0; i < player.Party.Count; i++)
            {
                partyPokemon[i] = PokemonToCompact(player.Party.ElementAt(i));
            }

            CompactPokemon[] boxPokemon = new CompactPokemon[player.Box.Count];

            for (int i = 0; i < player.Box.Count; i++)
            {
                boxPokemon[i] = PokemonToCompact(player.Box.ElementAt(i));
            }

            return new CompactPlayer(player.PlayerID, player.Name, player.Money, items, player.Badges.ToArray(), 
                                            player.SeenPokemon.ToArray(), player.CaughtPokemon.ToArray(),
                                            partyPokemon, boxPokemon);
        }

        /// <summary>
        /// Compresses a Pokemon object into a compact Pokemon object, used for saving the game.
        /// </summary>
        /// <param name="pokemon">The Pokemon object to convert.</param>
        /// <returns>The resulting CompactPokemon object.</returns>
        static CompactPokemon PokemonToCompact(Pokemon pokemon)
        {
            return new CompactPokemon(pokemon.species.PokedexNumber, 0, pokemon.Nickname, 0, 0, pokemon.Level, pokemon.Experience,
                                pokemon.IndividualValues, pokemon.CurrentHP, pokemon.Status.ToString(), 0, MovesToCompact(pokemon.knownMoves));
        }

        /// <summary>
        /// Compresses an Item object into a compact Item object, used for saving the game.
        /// </summary>
        /// <param name="item">The Item object to convert.</param>
        /// <returns>The resulting CompactItem object.</returns>
        static CompactItem ItemToCompact(ItemInstance item)
        {
            return new CompactItem(item.BaseItem.ItemID, item.Count);
        }

        /// <summary>
        /// Compresses a list of moves into an array of integers corresponding to each move's MoveID, used for saving the game.
        /// </summary>
        /// <param name="pokemon">The Pokemon object to convert.</param>
        /// <returns>The resulting array of integers.</returns>
        static int[] MovesToCompact(List<Move> moves)
        {
            int[] Moves = new int[moves.Count];

            for (int i = 0; i < moves.Count; i++)
            {
                Moves[i] = moves.ElementAt(i).MoveID;
            }

            return Moves;
        }

        #endregion

        #region Unpacking

        /// <summary>
        /// Unpacks the information from a save state object to the game by converting the information from the save state 
        /// and its assorted CompactPokemon and CompactItem objects to Pokemon and Items respectively.
        /// </summary>
        /// <param name="save">The save state object to unpack.</param>
        static Player UnpackSave(SaveState save)
        {
            return UnpackPlayer(save.Player);
        }
        
        static void UnpackGameState(CompactGameState compactState)
        {
            UI.Write("Loading game information... ");            

            Game.RivalName = compactState.RivalName;
            Game.StarterPokemon = compactState.StartingPokemon;            

            Game.Location = compactState.CurrentLocation;
            Game.LastHealLocation = compactState.LastHealLocation;

            Game.DefeatedTrainers = new List<int> { };

            foreach (int trainer in compactState.DefeatedTrainers)
                if (!Game.DefeatedTrainers.Contains(trainer))
                    Game.DefeatedTrainers.Add(trainer);

            UI.WriteLine("Loaded!");
        }     

        /// <summary>
        /// Decompresses a compact player object along with its information to their respective actual forms. Used while loading from a save file.
        /// </summary>
        /// <param name="compactPlayer">The compact player to decompress.</param>
        /// <returns>The resulting Player object.</returns>
        static Player UnpackPlayer(CompactPlayer compactPlayer)
        {
            Player player = new Player();

            PokemonGenerator generator = new PokemonGenerator();

            UI.Write("Loading the player's info... ");

            //Loading the player's information.
            player.Name = compactPlayer.PlayerName;           

            player.Money = compactPlayer.Money;

            foreach (string badge in compactPlayer.Badges)
                if (!player.Badges.Contains(badge))            

            foreach (int pokemon in compactPlayer.SeenPokemon)
                if (!player.CaughtPokemon.Contains(pokemon))
                    player.CaughtPokemon.Add(pokemon);

            foreach (int pokemon in compactPlayer.SeenPokemon)
                if (!player.CaughtPokemon.Contains(pokemon))
                    player.CaughtPokemon.Add(pokemon);

            UI.WriteLine("Loaded!");

            UI.Write("Loading items... ");

            //Loading the player's items.

            ItemInstance[] items = new ItemInstance[compactPlayer.Items.Count()];

            for (int i = 0; i < compactPlayer.Items.Count(); i++)
            {
                items[i] = CompactToItem(compactPlayer.Items[i]);
            }

            player.Bag = items.ToList();

            UI.WriteLine("Loaded!");

            UI.Write("Loading Pokemon... ");

            //Loading the player's Pokemon.
            foreach (CompactPokemon compactPokemon in compactPlayer.PartyPokemon)            
                player.Party.Add(CompactToPokemon(compactPokemon));            

            foreach (CompactPokemon compactPokemon in compactPlayer.BoxPokemon)
                player.Box.Add(CompactToPokemon(compactPokemon));

            UI.WriteLine("Loaded!");

            return player;
        }        

        /// <summary>
        /// Decompresses a compact Pokemon object to its actual form. Used while loading from a save file.
        /// </summary>
        /// <param name="compactPokemon">The compact Pokemon to decompress.</param>
        /// <returns>The resulting Pokemon object.</returns>
        static Pokemon CompactToPokemon(CompactPokemon compactPokemon)
        {
            StatusCondition status = StatusCondition.None;

            Enum.TryParse(compactPokemon.Status, out status);

            return new Pokemon(compactPokemon.PokedexNumber, compactPokemon.Nickname, compactPokemon.Level, compactPokemon.Experience,
                               compactPokemon.IndividualValues, compactPokemon.CurrentHP, status, CompactToMoves(compactPokemon.Moves));
        }        

        /// <summary>
        /// Decompresses a compact item object to its actual form. Used while loading from a save file.
        /// </summary>
        /// <param name="compactItem">The compact item to decompress.</param>
        /// <returns>The resulting ItemInstance object.</returns>
        static ItemInstance CompactToItem(CompactItem compactItem)
        {
            return new ItemInstance(ItemList.AllItems.Find(i => i.ItemID == compactItem.ItemID), compactItem.Count);
        }        

        /// <summary>
        /// Converts an array of integers to the resulting list of moves corresponding to each MoveID. Used while loading from a save file.
        /// </summary>
        /// <param name="moves">The array of integers to convert.</param>
        /// <returns>The resulting list of Move objects.</returns>
        static List<Move> CompactToMoves(int[] moves)
        {
            List<Move> knownMoves = new List<Move> { };

            for (int i = 0; i < moves.Length; i++)
            { 
                Move move = MoveList.AllMoves.Find(m => m.MoveID == moves[i]);

                knownMoves.Add(move);               
            }

            return knownMoves;
        }

        #endregion
    }
}
