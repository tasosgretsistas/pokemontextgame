﻿using System;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// This class deals with saving the game. It essentially compresses all of the player's information as well
    /// as his Pokemon to a bare minimum in order to create compact save files that are completely future-proof.
    /// </summary>
    [Serializable]
    class SaveState
    {
        public string GameVersion;
        public DateTime BeginDate;
        public DateTime SaveDate;

        public CompactGameState GameState;
        public CompactPlayer Player;

        /// <summary>
        /// This is the default constructor of the SaveState class for the Windows Console platform.
        /// </summary>
        /// <param name="version">The version of the game.</param>
        /// <param name="beginDate">The date when the user selected his first Pokemon.</param>
        /// <param name="date">The date of the save state's creation.</param>        
        /// <param name="player">The CompactPlayer object representing the player.</param>
        public SaveState(string version, DateTime beginDate, DateTime date, CompactGameState game, CompactPlayer player)
        {
            GameVersion = version;
            BeginDate = beginDate;
            SaveDate = date;

            GameState = game;
            Player = player;
        }
    }

    /// <summary>
    /// A compact version of the Game class that stores only the absolute minimum to describe the class in int and string form.
    /// </summary>
    [Serializable]
    class CompactGameState
    {
        public string RivalName;
        public string StartingPokemon;

        public string CurrentLocation;
        public string LastHealLocation;

        public int[] Flags;

        public int[] DefeatedTrainers;


        /// <summary>
        /// The default constructor for the CompactGameState class. It receives input in the form of int and string only.
        /// </summary>
        /// <param name="rivalName">The name of the player's rival.</param>
        /// <param name="startingPokemon">The name of the player's starting Pokemon.</param>
        /// <param name="currentLocation">The player's current location within the game.</param>
        /// <param name="lastHeal">The location where the player last had his Pokemon healed.</param>
        /// <param name="defeatedTrainers">The trainers that the player has defeated.</param>
        /// <param name="flags">The various event flags raised throughout the game. (NYI)</param>
        public CompactGameState(string rivalName, string startingPokemon,
                                string currentLocation, string lastHeal, int[] defeatedTrainers, int[] flags)
        {
            this.RivalName = rivalName;
            this.StartingPokemon = startingPokemon;

            this.CurrentLocation = currentLocation;
            this.LastHealLocation = lastHeal;

            this.DefeatedTrainers = defeatedTrainers;

            this.Flags = flags;
        }
    }

    /// <summary>
    /// A compact version of the Player class that stores only the absolute minimum to describe the class in int and string form.
    /// </summary>
    [Serializable]
    class CompactPlayer
    {
        public int PlayerID;

        public string PlayerName;

        public int Money;

        public CompactItem[] Items;

        public string[] Badges;
        
        public int[] SeenPokemon;
        public int[] CaughtPokemon;

        public CompactPokemon[] PartyPokemon;
        public CompactPokemon[] BoxPokemon;

        /// <summary>
        /// The default constructor for the CompactPlayer class. It receives input in the form of int, strings, and arrays of CompactItem and CompactPokemon type objects only.
        /// </summary>
        /// <param name="playerID">The player's unique ID number.</param>
        /// <param name="playerName">The player's name.</param>
        /// <param name="money">The player's money on hand.</param>
        /// <param name="items">The player's inventory of items.</param>
        /// <param name="badges">The player's badges.</param>
        /// <param name="seenPokemon">The Pokemon that the player has seen.</param>
        /// <param name="caughtPokemon">The Pokemon that the player has captured.</param>
        /// <param name="partyPokemon">The Pokemon that the player has in his party.</param>
        /// <param name="boxPokemon">The Pokemon that the player has in storage.</param>
        public CompactPlayer(int playerID, string playerName,  int money,
                             CompactItem[] items, string[] badges, 
                             int[] seenPokemon, int[] caughtPokemon,
                             CompactPokemon[] partyPokemon, CompactPokemon[] boxPokemon)
        {
            this.PlayerID = playerID;
            this.PlayerName = playerName;      

            this.Money = money;

            this.Items = items;

            this.Badges = badges;

            this.SeenPokemon = seenPokemon;
            this.CaughtPokemon = caughtPokemon;

            this.PartyPokemon = partyPokemon;
            this.BoxPokemon = boxPokemon;
        }
    }

    /// <summary>
    /// A compact version of the Pokemon class that stores only the absolute minimum to describe the class in int and string form.
    /// </summary>
    [Serializable]
    class CompactPokemon
    {
        public int PokedexNumber;

        public int UniqueID;

        public string Nickname;

        public int Ability;
        public int Gender;

        public int Level;
        public int Experience;

        public int[] IndividualValues;

        public int CurrentHP;
        public string Status;

        public int HeldItem;

        public int[] Moves;

        public CompactPokemon(int pokedexNumber, int uniqueID, string nickname, int ability, int gender, int level, int experience, 
                              int[] individualValues, int currentHP, string status, int helditem, int[] moves)
        {
            PokedexNumber = pokedexNumber;
            UniqueID = uniqueID;
            Nickname = nickname;
            Ability = ability;
            Gender = gender;
            Level = level;
            Experience = experience;
            IndividualValues = individualValues;
            CurrentHP = currentHP;
            Status = status;
            HeldItem = helditem;
            Moves = moves;
        }
    }

    /// <summary>
    /// A compact version of the Item class that stores only the absolute minimum to describe the class in int and string form.
    /// </summary>
    [Serializable]
    class CompactItem
    {
        public int ItemID;
        public int Count;

        public CompactItem(int id, int count)
        {
            ItemID = id;
            Count = count;
        }
    }

    
}
