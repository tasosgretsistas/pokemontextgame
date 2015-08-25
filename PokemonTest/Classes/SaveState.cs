using System;
using System.Collections.Generic;
using System.Linq;
using PokemonTextEdition.Engine;

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

        public CompactPlayer Player;

        /// <summary>
        /// This is the default constructor of the SaveState class for the Windows Console platform. It receives input in the form of strings, int, DateTime and Lists of objects.
        /// </summary>
        /// <param name="version">The version of the game.</param>
        /// <param name="beginDate">The date when the user selected his first Pokemon.</param>
        /// <param name="date">The date of the save state's creation.</param>        
        public SaveState(string version, DateTime beginDate, DateTime date, CompactPlayer player)
        {
            this.GameVersion = version;
            this.BeginDate = beginDate;
            this.SaveDate = date;

            this.Player = player;
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

        public string HeldItem;

        public string[] Moves;

        public CompactPokemon(int pokedexNumber, int uniqueID, string nickname, int ability, int gender, int level, int experience, 
                              int[] individualValues, int currentHP, string status, string helditem, string[] moves)
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
        public string Name;
        public int Count;

        public CompactItem(int id, string name, int count)
        {
            ItemID = id;
            Name = name;
            Count = count;
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
        public string RivalName;
        public string StartingPokemon;

        public string CurrentLocation;
        public string LastHealLocation;

        public int[] Flags;

        public int Money;

        public CompactItem[] Items;

        public string[] Badges;
        public int[] DefeatedTrainers;
        public int[] SeenPokemon;
        public int[] CaughtPokemon;

        public CompactPokemon[] PartyPokemon;
        public CompactPokemon[] BoxPokemon;

        /// <summary>
        /// The default constructor for the CompactPlayer class. It receives input in the form of int, strings, and arrays of CompactItem and CompactPokemon type objects only.
        /// </summary>
        /// <param name="playerName">The player's name.</param>
        /// <param name="rivalName">The player rival's name.</param>
        /// <param name="startingPokemon">The name of the player's starting Pokemon.</param>
        /// <param name="money">The player's money on hand.</param>
        /// <param name="currentLocation">The player's current location.</param>
        /// <param name="lastHeal">The player's last healed location.</param>
        /// <param name="flags">The various event flags throughout the game. (NYI)</param>
        /// <param name="seenPokemon">The list of Pokemon the player has encountered.</param>
        /// <param name="caughtPokemon">The list of Pokemon the player has caught.</param>
        /// <param name="partyPokemon">The list of Pokemon the player has in his party.</param>
        /// <param name="boxPokemon">The list of Pokemon the player has in storage.</param>
        public CompactPlayer(int playerID, string playerName, string rivalName, string startingPokemon, 
                             string currentLocation, string lastHeal, int[] flags, int money, 
                             CompactItem[] items, string[] badges, int[] defeatedTrainers, 
                             int[] seenPokemon, int[] caughtPokemon, 
                             CompactPokemon[] partyPokemon, CompactPokemon[] boxPokemon)
        {
            this.PlayerID = playerID;
            this.PlayerName = playerName;
            this.RivalName = rivalName;
            this.StartingPokemon = startingPokemon;

            this.CurrentLocation = currentLocation;
            this.LastHealLocation = lastHeal;

            this.Flags = flags;

            this.Money = money;

            this.Items = items;

            this.Badges = badges;
            this.DefeatedTrainers = defeatedTrainers;

            this.SeenPokemon = seenPokemon;
            this.CaughtPokemon = caughtPokemon;

            this.PartyPokemon = partyPokemon;
            this.BoxPokemon = boxPokemon;
        }
    }    
}
