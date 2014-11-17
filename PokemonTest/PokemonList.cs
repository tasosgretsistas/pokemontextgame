using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class PokemonList
    {  
        //A list of all Pokemon. 
        //TODO: Optimize this.
        #region All Pokemon

        public static List<Pokemon> allPokemon = new List<Pokemon>() 
        { 
                new Pokemon("Bulbasaur", "Grass", "Poison", "Seed", 1, 1, 45, 49, 49, 65, 65, 45, "Ivysaur", 16),
                new Pokemon("Ivysaur", "Grass", "Poison", "Seed", 2, 1, 60, 62, 63, 80, 80, 60, "Venusaur", 32),
                new Pokemon("Venusaur", "Grass", "Poison", "Seed", 3, 1, 80, 82, 83, 100, 100, 80),

                new Pokemon("Charmander", "Fire", "", "Lizard", 4, 1, 39, 52, 43, 60, 50, 65, "Charmeleon", 16),
                new Pokemon("Charmeleon", "Fire", "", "Flame", 5, 1, 58, 64, 58, 80, 65, 80, "Charizard", 36),
                new Pokemon("Charizard", "Fire", "Flying", "Flame", 6, 1, 78, 84, 78, 109, 85, 100),

                new Pokemon("Squirtle", "Water", "", "Tiny Turtle", 7, 1, 44, 48, 65, 50, 64, 43, "Wartortle", 16),
                new Pokemon("Wartortle", "Water", "", "Turtle", 8, 1, 59, 63, 80, 65, 80, 58, "Blastoise", 36),
                new Pokemon("Blastoise", "Water", "", "Shellfish", 9, 1, 79, 83, 100, 85, 105, 78),

                new Pokemon("Caterpie", "Bug", "", "Worm", 10, 1, 45, 30, 35, 20, 20, 45, "Metapod", 7),
                new Pokemon("Metapod", "Bug", "", "Cocoon", 11, 1, 50, 20, 55, 25, 25, 30, "Butterfree", 10),
                new Pokemon("Butterfree", "Bug", "Flying", "Butterfly", 12, 1, 60, 45, 50, 90, 80, 70),

                new Pokemon("Weedle", "Bug", "Poison", "Hairy Bug", 13, 1, 40, 35, 30, 20, 20, 50, "Kakuna", 7),
                new Pokemon("Kakuna", "Bug", "Poison", "Cocoon", 14, 1, 45, 25, 50, 25, 25, 35, "Beedrill", 10),
                new Pokemon("Beedrill", "Bug", "Poison", "Poison Bee", 15, 1, 65, 90, 40, 45, 80, 75),

                new Pokemon("Pidgey", "Normal", "Flying", "Tiny Bird", 16, 1, 40, 45, 40, 35, 35, 56, "Pidgeotto", 18),
                new Pokemon("Pidgeotto", "Normal", "Flying", "Bird", 17, 1, 63, 60, 55, 50, 50, 71, "Pidgeot", 36),
                new Pokemon("Pidgeot", "Normal", "Flying", "Bird", 18, 1, 83, 80, 75, 70, 70, 101),

                new Pokemon("Rattata", "Normal", "", "Mouse", 19, 1, 30, 56, 35, 25, 35, 72, "Raticate", 20),
                new Pokemon("Raticate", "Normal", "", "Mouse", 20, 1, 55, 81, 60, 50, 70, 97),

                new Pokemon("Spearow", "Normal", "Flying", "Tiny Bird", 21, 1, 40, 60, 30, 31, 31, 70, "Fearow", 20),
                new Pokemon("Fearow", "Normal", "Flying", "Beak", 22, 1, 65, 90, 65, 61, 61, 100),

                new Pokemon("Ekans", "Poison", "", "Snake", 23, 1, 35, 60, 44, 40, 54, 55, "Arbok", 22),
                new Pokemon("Arbok", "Poison", "", "Cobra", 24, 1, 60, 85, 69, 65, 79, 80),

                new Pokemon("Pikachu", "Electric", "", "Mouse", 25, 1, 35, 55, 40, 50, 50, 90, "Raichu", "Thunderstone", 0),
                new Pokemon("Raichu", "Electric", "", "Mouse", 26, 1, 60, 90, 55, 90, 80, 110),

                new Pokemon("Sandshrew", "Ground", "", "Mouse", 27, 1, 50, 75, 85, 20, 30, 40, "Sandslash", 22),
                new Pokemon("Sandslash", "Ground", "", "Mouse", 28, 1, 75, 100, 110, 45, 55, 65),

                new Pokemon("Nidoran♀", "Poison", "", "Poison Pin", 29, 1, 55, 47, 52, 40, 40, 41, "Nidorina", 16),
                new Pokemon("Nidorina", "Poison", "", "Poison Pin", 30, 1, 70, 62, 67, 55, 55, 56, "Nidoqueen", "Moon Stone", 0),
                new Pokemon("Nidoqueen", "Poison", "Ground", "Drill", 31, 1, 90, 92, 87, 75, 85, 76),

                new Pokemon("Nidoran♂", "Poison", "", "Poison Pin", 32, 1, 46, 57, 40, 40, 40, 50, "Nidorino", 16),
                new Pokemon("Nidorino", "Poison", "", "Poison Pin", 33, 1, 61, 72, 57, 55, 55, 65, "Nidoking", "Moon Stone", 0),
                new Pokemon("Nidoking", "Poison", "Ground", "Drill", 34, 1, 81, 102, 77, 85, 75, 85),

                new Pokemon("Clefairy", "Fairy", "", "Fairy", 35, 1, 70, 45, 48, 60, 65, 35, "Clefable", "Moon Stone", 0),
                new Pokemon("Clefable", "Fairy", "", "Fairy", 36, 1, 95, 70, 73, 95, 90, 60),

                new Pokemon("Vulpix", "Fire", "", "Fox", 37, 1, 38, 41, 40, 50, 65, 65, "Ninetales", "Fire Stone", 0),
                new Pokemon("Ninetales", "Fire", "", "Fox", 38, 1, 73, 76, 75, 81, 100, 100),

                new Pokemon("Jigglypuff", "Normal", "Fairy", "Balloon", 39, 1, 115, 45, 20, 45, 25, 20, "Wigglytuff", "Moon Stone", 0),
                new Pokemon("Wigglytuff", "Normal", "Fairy", "Balloon", 40, 1, 140, 70, 45, 85, 50, 45),

                new Pokemon("Zubat", "Poison", "Flying", "Bat", 41, 1, 40, 45, 35, 30, 40, 55, "Golbat", 22),
                new Pokemon("Golbat", "Poison", "Flying", "Bat", 42, 1, 75, 80, 70, 65, 75, 90),

                new Pokemon("Oddish", "Grass", "Poison", "Weed", 43, 1, 45, 50, 55, 75, 65, 30, "Gloom", 21),
                new Pokemon("Gloom", "Grass", "Poison", "Weed", 44, 1, 60, 65, 70, 85, 75, 40, "Vileplume", "Leaf Stone", 0),
                new Pokemon("Vileplume", "Grass", "Poison", "Flower", 45, 1, 75, 80, 85, 110, 90, 50),

                new Pokemon("Paras", "Bug", "Grass", "Mushroom", 46, 1, 35, 70, 55, 45, 55, 25, "Parasect", 24),
                new Pokemon("Parasect", "Bug", "Grass", "Mushroom", 47, 1, 60, 95, 80, 60, 80, 30),

                new Pokemon("Venonat", "Bug", "Poison", "Insect", 48, 1, 60, 55, 50, 40, 55, 45, "Venomoth", 31),
                new Pokemon("Venomoth", "Bug", "Poison", "Poison Moth", 49, 1, 70, 65, 60, 90, 75, 90),

                new Pokemon("Diglett", "Ground", "", "Mole", 50, 1, 10, 55, 25, 35, 45, 95, "Dugtrio", 26),
                new Pokemon("Dugtrio", "Ground", "", "Mole", 51, 1, 35, 80, 50, 50, 70, 120),

                new Pokemon("Meowth", "Normal", "", "Scratch Cat", 52, 1, 40, 45, 35, 40, 40, 90, "Persian", 28),
                new Pokemon("Persian", "Normal", "", "Classy Cat", 53, 1, 65, 70, 60, 65, 65, 115),

                new Pokemon("Psyduck", "Water", "", "Duck", 54, 1, 50, 52, 48, 65, 50, 55, "Golduck", 33),
                new Pokemon("Golduck", "Water", "", "Duck", 55, 1, 80, 82, 78, 95, 80, 85),

                new Pokemon("Mankey", "Fighting", "", "Pig Monkey", 56, 1, 40, 80, 35, 35, 45, 70, "Primeape", 28),
                new Pokemon("Primeape", "Fighting", "", "Pig Monkey", 57, 1, 65, 105, 60, 60, 70, 95),

                new Pokemon("Growlithe", "Fire", "", "Puppy", 58, 1, 55, 70, 45, 70, 50, 60, "Arcanine", "Fire Stone", 0),
                new Pokemon("Arcanine", "Fire", "", "Legendary", 59, 1, 90, 110, 80, 100, 80, 95),

                new Pokemon("Poliwag", "Water", "", "Tadpole", 60, 1, 40, 50, 40, 40, 40, 90, "Poliwhirl", 25),
                new Pokemon("Poliwhirl", "Water", "", "Tadpole", 61, 1, 65, 65, 65, 50, 50, 90, "Poliwrath", "Water Stone", 0),
                new Pokemon("Poliwrath", "Water", "Fighting", "Tadpole", 62, 1, 90, 95, 95, 70, 90, 70),

                new Pokemon("Abra", "Psychic", "", "Psi", 63, 1, 25, 20, 15, 105, 55, 90, "Kadabra", 16),
                new Pokemon("Kadabra", "Psychic", "", "Psi", 64, 1, 40, 35, 30, 120, 70, 105, "Alakazam", 36),
                new Pokemon("Alakazam", "Psychic", "", "Psi", 65, 1, 55, 50, 45, 135, 95, 120),

                new Pokemon("Machop", "Fighting", "", "Superpower", 66, 1, 70, 80, 50, 35, 35, 35, "Machoke", 28),
                new Pokemon("Machoke", "Fighting", "", "Superpower", 67, 1, 80, 100, 70, 50, 60, 45, "Machamp", 36),
                new Pokemon("Machamp", "Fighting", "", "Superpower", 68, 1, 90, 130, 80, 65, 85, 55),

                new Pokemon("Bellsprout", "Grass", "Poison", "Flower", 69, 1, 50, 75, 35, 70, 30, 40, "Weepinbell", 21),
                new Pokemon("Weepinbell", "Grass", "Poison", "Flycatcher", 70, 1, 65, 90, 50, 85, 45, 55, "Victreebel", "Leaf Stone", 0),
                new Pokemon("Victreebel", "Grass", "Poison", "Flycatcher", 71, 1, 80, 105, 65, 100, 70, 70),

                new Pokemon("Tentacool", "Water", "Poison", "Jellyfish", 72, 1, 40, 40, 35, 50, 100, 70, "Tentacruel", 30),
                new Pokemon("Tentacruel", "Water", "Poison", "Jellyfish", 73, 1, 80, 70, 65, 80, 120, 100),

                new Pokemon("Geodude", "Rock", "Ground", "Rock", 74, 1, 40, 80, 100, 30, 30, 20, "Graveler", 25),
                new Pokemon("Graveler", "Rock", "Ground", "Rock", 75, 1, 55, 95, 115, 45, 45, 35, "Golem", 36),
                new Pokemon("Golem", "Rock", "Ground", "Megaton", 76, 1, 80, 120, 130, 55, 65, 45),

                new Pokemon("Ponyta", "Fire", "", "Fire Horse", 77, 1, 50, 85, 55, 65, 65, 90, "Rapidash", 40),
                new Pokemon("Rapidash", "Fire", "", "Fire Horse", 78, 1, 65, 100, 70, 80, 80, 105),

                new Pokemon("Slowpoke", "Water", "Psychic", "Dopey", 79, 1, 90, 65, 65, 40, 40, 15, "Slowbro", 37),
                new Pokemon("Slowbro", "Water", "Psychic", "Hermit Crab", 80, 1, 95, 75, 110, 100, 80, 30),

                new Pokemon("Magnemite", "Electric", "Steel", "Magnet", 81, 1, 25, 35, 70, 95, 55, 45, "Magneton", 30),
                new Pokemon("Magneton", "Electric", "Steel", "Magnet", 82, 1, 50, 60, 95, 120, 70, 70),

                new Pokemon("Farfetch'd", "Normal", "Flying", "Wild Duck", 83, 1, 52, 65, 55, 58, 62, 60),

                new Pokemon("Doduo", "Normal", "Flying", "Twin Bird", 84, 1, 35, 85, 45, 35, 35, 75, "Dodrio", 31),
                new Pokemon("Dodrio", "Normal", "Flying", "Triple Bird", 85, 1, 60, 110, 70, 60, 60, 100),

                new Pokemon("Seel", "Water", "", "Sea Lion", 86, 1, 65, 45, 55, 45, 70, 45, "Dewgong", 34),
                new Pokemon("Dewgong", "Water", "Ice", "Sea Lion", 87, 1, 90, 70, 80, 70, 95, 70),

                new Pokemon("Grimer", "Poison", "", "Sludge", 88, 1, 80, 80, 50, 40, 50, 25, "Muk", 38),
                new Pokemon("Muk", "Poison", "", "Sludge", 89, 1, 105, 105, 75, 65, 100, 50),

                new Pokemon("Shellder", "Water", "", "Bivalve", 90, 1, 30, 65, 100, 45, 25, 40, "Cloyster", "Water Stone", 0),
                new Pokemon("Cloyster", "Water", "Ice", "Bivalve", 91, 1, 50, 95, 180, 85, 45, 70),

                new Pokemon("Gastly", "Ghost", "Poison", "Gas", 92, 1, 30, 35, 30, 100, 35, 80, "Haunter", 25),
                new Pokemon("Haunter", "Ghost", "Poison", "Gas", 93, 1, 45, 50, 45, 115, 55, 95, "Gengar", 36),
                new Pokemon("Gengar", "Ghost", "Poison", "Shadow", 94, 1, 60, 65, 60, 130, 75, 110),

                new Pokemon("Onix", "Rock", "Ground", "Rock Snake", 95, 1, 35, 45, 160, 30, 45, 70),

                new Pokemon("Drowzee", "Psychic", "", "Hypnosis", 96, 1, 60, 48, 45, 43, 90, 42, "Hypno", 26),
                new Pokemon("Hypno", "Psychic", "", "Hypnosis", 97, 1, 85, 73, 70, 73, 115, 67),

                new Pokemon("Krabby", "Water", "", "River Crab", 98, 1, 30, 105, 90, 25, 25, 50, "Kingler", 28),
                new Pokemon("Kingler", "Water", "", "Pincer", 99, 1, 55, 130, 115, 50, 50, 75),

                new Pokemon("Voltorb", "Electric", "", "Ball", 100, 1, 40, 30, 50, 55, 55, 100, "Electrode", 30),
                new Pokemon("Electrode", "Electric", "", "Ball", 101, 1, 60, 50, 70, 80, 80, 140),

                new Pokemon("Exeggcute", "Grass", "Psychic", "Egg", 102, 1, 60, 40, 80, 60, 45, 40, "Exeggutor", "Leaf Stone", 0),
                new Pokemon("Exeggutor", "Grass", "Psychic", "Coconut", 103, 1, 95, 95, 85, 125, 65, 55),

                new Pokemon("Cubone", "Ground", "", "Lonely", 104, 1, 50, 50, 95, 40, 50, 35, "Marowak", 28),
                new Pokemon("Marowak", "Ground", "", "Bone Keeper", 105, 1, 60, 80, 110, 50, 80, 45),

                new Pokemon("Hitmonlee", "Fighting", "", "Kicking", 106, 1, 50, 120, 53, 35, 110, 87),

                new Pokemon("Hitmonchan", "Fighting", "", "Punching", 107, 1, 50, 105, 79, 35, 110, 76),

                new Pokemon("Lickitung", "Normal", "", "Licking", 108, 1, 90, 55, 75, 60, 75, 30),

                new Pokemon("Koffing", "Poison", "", "Poison Gas", 109, 1, 40, 65, 95, 60, 45, 35, "Weezing", 35),
                new Pokemon("Weezing", "Poison", "", "Poison Gas", 110, 1, 65, 90, 120, 85, 70, 60),

                new Pokemon("Rhyhorn", "Ground", "Rock", "Spikes", 111, 1, 80, 85, 95, 30, 30, 25, "Rhydon", 42),
                new Pokemon("Rhydon", "Ground", "Rock", "Drill", 112, 1, 105, 130, 120, 45, 45, 40),

                new Pokemon("Chansey", "Normal", "", "Egg", 113, 1, 250, 5, 5, 35, 105, 50),

                new Pokemon("Tangela", "Grass", "", "Vine", 114, 1, 65, 55, 115, 100, 40, 60), 

                new Pokemon("Kangaskhan", "Normal", "", "Parent", 115, 1, 105, 95, 80, 40, 80, 90),

                new Pokemon("Horsea", "Water", "", "Dragon", 116, 1, 30, 40, 70, 70, 25, 60, "Seadra", 32),
                new Pokemon("Seadra", "Water", "", "Dragon", 117, 1, 55, 65, 95, 95, 45, 85),

                new Pokemon("Goldeen", "Water", "", "Goldfish", 118, 1, 45, 67, 60, 35, 50, 63, "Seaking", 33),
                new Pokemon("Seaking", "Water", "", "Goldfish", 119, 1, 80, 92, 65, 65, 80, 68),

                new Pokemon("Staryu", "Water", "", "Star Shape", 120, 1, 30, 45, 55, 70, 55, 85, "Starmie", "Water Stone", 0),
                new Pokemon("Starmie", "Water", "Psychic", "Mysterious", 121, 1, 60, 75, 85, 100, 85, 115), 

                new Pokemon("Mr. Mime", "Psychic", "Fairy", "Barrier", 122, 1, 40, 45, 65, 100, 120, 90), 

                new Pokemon("Scyther", "Bug", "Flying", "Mantis", 123, 1, 70, 110, 80, 55, 80, 105), 

                new Pokemon("Jynx", "Ice", "Psychic", "Human Shape", 124, 1, 65, 50, 35, 115, 95, 95),

                new Pokemon("Electabuzz", "Electric", "", "Electric", 125, 1, 65, 83, 57, 95, 85, 105),

                new Pokemon("Magmar", "Fire", "", "Spitfire", 126, 1, 65, 95, 57, 100, 85, 93),

                new Pokemon("Pinsir", "Bug", "", "Stag Beetle", 127, 1, 65, 125, 100, 55, 70, 85),

                new Pokemon("Tauros", "Normal", "", "Wild Bull", 128, 1, 75, 100, 95, 40, 70, 110),

                new Pokemon("Magikarp", "Water", "", "Fish", 129, 1, 20, 10, 55, 15, 20, 80),

                new Pokemon("Gyarados", "Water", "Flying", "Atrocious", 130, 1, 95, 125, 79, 60, 100, 81),

                new Pokemon("Lapras", "Water", "Ice", "Transport", 131, 1, 130, 85, 80, 85, 95, 60), 

                new Pokemon("Ditto", "Normal", "", "Transform", 132, 1, 48, 48, 48, 48, 48, 48),

                new Pokemon("Eevee", "Normal", "", "Evolution", 133, 1, 55, 55, 50, 45, 65, 55),
                new Pokemon("Vaporeon", "Water", "", "Bubble Jet", 134, 1, 130, 65, 60, 110, 95, 65),
                new Pokemon("Jolteon", "Electric", "", "Lightning", 135, 1, 65, 65, 60, 110, 95, 130),
                new Pokemon("Flareon", "Fire", "", "Flame", 136, 1, 65, 130, 60, 95, 110, 65),

                new Pokemon("Porygon", "Normal", "", "Virtual", 137, 1, 65, 60, 70, 85, 75, 40),

                new Pokemon("Omanyte", "Rock", "Water", "Spiral", 138, 1, 35, 40, 100, 90, 55, 35, "Omastar", 40),
                new Pokemon("Omastar", "Rock", "Water", "Spiral", 139, 1, 70, 60, 125, 115, 70, 55),

                new Pokemon("Kabuto", "Rock", "Water", "Shellfish", 140, 1, 30, 80, 90, 55, 45, 55, "Kabutops", 40),                
                new Pokemon("Kabutops", "Rock", "Water", "Shellfish", 141, 1, 60, 115, 105, 65, 70, 80),

                new Pokemon("Aerodactyl", "Rock", "Flying", "Fossil", 142, 1, 80, 105, 65, 60, 75, 130),

                new Pokemon("Snorlax", "Normal", "", "Sleeping", 143, 1, 160, 110, 65, 65, 110, 30),

                new Pokemon("Articuno", "Ice", "Flying", "Freeze", 144, 1, 90, 85, 100, 95, 125, 85),
                
                new Pokemon("Zapdos", "Electric", "Flying", "Electric", 145, 1, 90, 90, 85, 125, 90, 100),

                new Pokemon("Moltres", "Fire", "Flying", "Flame", 146, 1, 90, 100, 90, 125, 85, 90), 

                new Pokemon("Dratini", "Dragon", "", "Dragon", 149, 1, 41, 64, 45, 50, 50, 50, "Dragonair", 30),
                new Pokemon("Dragonair", "Dragon", "", "Dragon", 148, 1, 61, 84, 65, 70, 70, 70, "Dragonite", 55),
                new Pokemon("Dragonite", "Dragon", "Flying", "Dragon", 149, 1, 91, 134, 95, 100, 100, 80),

                new Pokemon("Mewtwo", "Psychic", "", "Genetic", 150, 1, 106, 110, 90, 154, 90, 130),

                new Pokemon("Mew", "Psychic", "", "New Species", 151, 1, 100, 100, 100, 100, 100, 100)

            };

        #endregion

        public static void DisplayBSTs()
        {
            //Displays every Pokemon, sorted by its Base Stat Total - for error checking purposes.

            Dictionary<string, int> totals = new Dictionary<string, int>();

            foreach (Pokemon p in allPokemon)
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

        public static void DisplayEvolutions()
        {
            //Displays every Pokemon that evolves, as well as its evolution - for error checking purposes.

            foreach (Pokemon p in allPokemon)
            {
                if (p.Evolves)
                    Console.WriteLine("{0} evolves into {1}.", p.Name, p.EvolvesInto);
            }
        }

    }
}
