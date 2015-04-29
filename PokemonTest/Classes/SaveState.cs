using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition.Classes
{
    [Serializable]
    class SaveState
    {
        string GameVersion { get; set; }
        DateTime SaveDate { get; set; }

        string PlayerName { get; set; }
        string RivalName { get; set; }
        string StartingPokemon { get; set; }

        int Money { get; set; }

        string CurrentLocation { get; set; }
        string LastHealLocation { get; set; }

        List<string> SeenPokemon { get; set; }
        List<string> CaughtPokemon { get; set; }

        CompactPokemon[] PartyPokemon;
        CompactPokemon[] BoxPokemon;

        public SaveState(string version, DateTime date, string playerName, string rivalName, string startingPokemon, int money, string currentLocation, string lastHeal, List<string> seenPokemon, List<string> caughtPokemon, List<Pokemon> partyPokemon, List<Pokemon> boxPokemon)
        {
            GameVersion = version;
            SaveDate = date;
            PlayerName = playerName;
            RivalName = rivalName;
            StartingPokemon = startingPokemon;
            Money = money;
            CurrentLocation = currentLocation;
            LastHealLocation = lastHeal;

            SeenPokemon = seenPokemon;

            CaughtPokemon = caughtPokemon;

            PartyPokemon = new CompactPokemon[partyPokemon.Count];

            for (int i = 0; i < partyPokemon.Count; i++)
            {
                PartyPokemon[i] = ConvertToCompact(partyPokemon.ElementAt(i));
            }

            BoxPokemon = new CompactPokemon[boxPokemon.Count];

            for (int i = 0; i < boxPokemon.Count; i++)
            {
                BoxPokemon[i] = ConvertToCompact(boxPokemon.ElementAt(i));
            }
        }

        CompactPokemon ConvertToCompact(Pokemon pokemon)
        {
            CompactPokemon compact = new CompactPokemon(pokemon.species.PokedexNumber, pokemon.Name, pokemon.Level, pokemon.Experience, pokemon.IndividualValues, pokemon.CurrentHP, pokemon.Status, ConvertMoves(pokemon.knownMoves));

            return compact;
        }

        string[] ConvertMoves(List<Moves> moves)
        {
            string[] Moves = new string[moves.Count];

            for (int i = 0; i < moves.Count; i++)
            {
                Moves[i] = moves.ElementAt(i).Name;
            }

            return Moves;
        }

        List<int> ConvertPokemonListToInt(List<Pokemon> pokemonList)
        {
            List<int> list = new List<int>();

            foreach (Pokemon pokemon in pokemonList)
            {
                list.Add(pokemon.species.PokedexNumber);
            }

            return list;
        }

    }

    [Serializable]
    class CompactPokemon
    {
        int PokedexNumber;

        string Nickname;

        int Level;
        int Experience;

        int[] IndividualValues;

        int CurrentHP;
        string Status;

        string[] Moves;

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
}
