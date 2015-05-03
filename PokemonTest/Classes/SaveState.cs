using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// This class deals with saving the game. It essentially compresses all of the player's information as well
    /// as his Pokemon to a bare minimum in order to create compact save files that are completely future-proof.
    /// </summary>
    [Serializable]
    struct SaveState
    {
        #region Fields & Properties

        public string GameVersion;
        public DateTime SaveDate;

        public string PlayerName;
        public string RivalName;
        public string StartingPokemon;

        public string CurrentLocation;
        public string LastHealLocation;

        public int Money;

        public CompactItem[] Items;

        public string[] Badges;
        public string[] DefeatedTrainers;
        public string[] SeenPokemon;
        public string[] CaughtPokemon;

        public CompactPokemon[] PartyPokemon;
        public CompactPokemon[] BoxPokemon;

        #endregion

        #region Constructors

        /// <summary>
        /// This is the constructor of the SaveState class for the Windows Console platform, which receives input in the form of strings, int, DateTime and Lists of objects.
        /// </summary>
        /// <param name="version">The version of the game.</param>
        /// <param name="date">The date of the save state's creation.</param>
        /// <param name="playerName">The player's name.</param>
        /// <param name="rivalName">The player rival's name.</param>
        /// <param name="startingPokemon">The name of the player's starting Pokemon.</param>
        /// <param name="money">The player's money on hand.</param>
        /// <param name="currentLocation">The player's current location.</param>
        /// <param name="lastHeal">The player's last healed location.</param>
        /// <param name="seenPokemon">The list of Pokemon the player has encountered.</param>
        /// <param name="caughtPokemon">The list of Pokemon the player has caught.</param>
        /// <param name="partyPokemon">The list of Pokemon the player has in his party.</param>
        /// <param name="boxPokemon">The list of Pokemon the player has in storage.</param>
        public SaveState(string version, DateTime date, string playerName, string rivalName, string startingPokemon, string currentLocation, string lastHeal, int money,
        List<Item> items, List<string> badges, List<string> defeatedTrainers, List<string> seenPokemon, List<string> caughtPokemon, List<Pokemon> partyPokemon, List<Pokemon> boxPokemon) :this()
        {
            this.GameVersion = version;
            this.SaveDate = date;

            this.PlayerName = playerName;
            this.RivalName = rivalName;
            this.StartingPokemon = startingPokemon;

            this.CurrentLocation = currentLocation;
            this.LastHealLocation = lastHeal;

            this.Money = money;

            this.Badges = badges.ToArray();
            this.DefeatedTrainers = defeatedTrainers.ToArray();
            this.SeenPokemon = seenPokemon.ToArray();
            this.CaughtPokemon = caughtPokemon.ToArray();

            this.Items = new CompactItem[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                this.Items[i] = ItemToCompact(items.ElementAt(i));
            }

            this.PartyPokemon = new CompactPokemon[partyPokemon.Count];

            for (int i = 0; i < partyPokemon.Count; i++)
            {
                this.PartyPokemon[i] = PokemonToCompact(partyPokemon.ElementAt(i));
            }

            this.BoxPokemon = new CompactPokemon[boxPokemon.Count];

            for (int i = 0; i < boxPokemon.Count; i++)
            {
                this.BoxPokemon[i] = PokemonToCompact(boxPokemon.ElementAt(i));
            }
        }

        #endregion

        #region Helpful Methods

        CompactPokemon PokemonToCompact(Pokemon pokemon)
        {
            return new CompactPokemon(pokemon.species.PokedexNumber, pokemon.Nickname, pokemon.Level, pokemon.Experience, pokemon.IndividualValues, pokemon.CurrentHP, pokemon.Status, MovesListToString(pokemon.knownMoves));
        }

        CompactItem ItemToCompact(Item item)
        {
            return new CompactItem(item.Name, item.Count);
        }

        string[] MovesListToString(List<Move> moves)
        {
            string[] Moves = new string[moves.Count];

            for (int i = 0; i < moves.Count; i++)
            {
                Moves[i] = moves.ElementAt(i).Name;
            }

            return Moves;
        }        

        #endregion
    }

    [Serializable]
    struct CompactPokemon
    {
        //A compact version of the Pokemon class that stores only the absolute minimum to describe the class in int and string form.

        public int PokedexNumber;

        public string Nickname;

        public int Level;
        public int Experience;

        public int[] IndividualValues;

        public int CurrentHP;
        public string Status;

        public string[] Moves;

        public CompactPokemon(int pokedexNumber, string nickname, int level, int experience, int[] individualValues, int currentHP, string status, string[] moves)
        {
            PokedexNumber = pokedexNumber;
            Nickname = nickname;
            Level = level;
            Experience = experience;
            IndividualValues = individualValues;
            CurrentHP = currentHP;
            Status = status;
            Moves = moves;
        }
    }

    [Serializable]
    struct CompactItem
    {
        //A compact version of the Item class that stores only the absolute minimum to describe the class in int and string form.

        public string Name;
        public int Count;

        public CompactItem(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
