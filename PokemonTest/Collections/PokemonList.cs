using PokemonTextEdition.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition
{
    /// <summary>
    /// //A list of all the Pokemon species currently in the game.
    /// </summary>
    class PokemonList
    {          
        #region All Pokemon

        public static List<PokemonSpecies> allPokemon = new List<PokemonSpecies>() 
        {             
                new PokemonSpecies(0, "MissingNo", "Normal", "", "Debug", 1, 20, 20, 20, 20, 20, 20), //DEBUG

                new PokemonSpecies(1, "Bulbasaur", "Grass", "Poison", "Seed", 1, 45, 49, 49, 65, 65, 45, "Ivysaur", 16),
                new PokemonSpecies(2, "Ivysaur", "Grass", "Poison", "Seed", 1, 60, 62, 63, 80, 80, 60, "Venusaur", 32),
                new PokemonSpecies(3, "Venusaur", "Grass", "Poison", "Seed", 1, 80, 82, 83, 100, 100, 80),

                new PokemonSpecies(4, "Charmander", "Fire", "", "Lizard", 1, 39, 52, 43, 60, 50, 65, "Charmeleon", 16),
                new PokemonSpecies(5, "Charmeleon", "Fire", "", "Flame", 1, 58, 64, 58, 80, 65, 80, "Charizard", 36),
                new PokemonSpecies(6, "Charizard", "Fire", "Flying", "Flame", 1, 78, 84, 78, 109, 85, 100),

                new PokemonSpecies(7, "Squirtle", "Water", "", "Tiny Turtle", 1, 44, 48, 65, 50, 64, 43, "Wartortle", 16),
                new PokemonSpecies(8, "Wartortle", "Water", "", "Turtle", 1, 59, 63, 80, 65, 80, 58, "Blastoise", 36),
                new PokemonSpecies(9, "Blastoise", "Water", "", "Shellfish", 1, 79, 83, 100, 85, 105, 78),

                new PokemonSpecies(10, "Caterpie", "Bug", "", "Worm", 1, 45, 30, 35, 20, 20, 45, "Metapod", 7),
                new PokemonSpecies(11, "Metapod", "Bug", "", "Cocoon", 1, 50, 20, 55, 25, 25, 30, "Butterfree", 10),
                new PokemonSpecies(12, "Butterfree", "Bug", "Flying", "Butterfly", 1, 60, 45, 50, 90, 80, 70),

                new PokemonSpecies(13, "Weedle", "Bug", "Poison", "Hairy Bug", 1, 40, 35, 30, 20, 20, 50, "Kakuna", 7),
                new PokemonSpecies(14, "Kakuna", "Bug", "Poison", "Cocoon", 1, 45, 25, 50, 25, 25, 35, "Beedrill", 10),
                new PokemonSpecies(15, "Beedrill", "Bug", "Poison", "Poison Bee", 1, 65, 90, 40, 45, 80, 75),

                new PokemonSpecies(16, "Pidgey", "Normal", "Flying", "Tiny Bird", 1, 40, 45, 40, 35, 35, 56, "Pidgeotto", 18),
                new PokemonSpecies(17, "Pidgeotto", "Normal", "Flying", "Bird", 1, 63, 60, 55, 50, 50, 71, "Pidgeot", 36),
                new PokemonSpecies(18, "Pidgeot", "Normal", "Flying", "Bird", 1, 83, 80, 75, 70, 70, 101),

                new PokemonSpecies(19, "Rattata", "Normal", "", "Mouse", 1, 30, 56, 35, 25, 35, 72, "Raticate", 20),
                new PokemonSpecies(20, "Raticate", "Normal", "", "Mouse", 1, 55, 81, 60, 50, 70, 97),

                new PokemonSpecies(21, "Spearow", "Normal", "Flying", "Tiny Bird", 1, 40, 60, 30, 31, 31, 70, "Fearow", 20),
                new PokemonSpecies(22, "Fearow", "Normal", "Flying", "Beak", 1, 65, 90, 65, 61, 61, 100),

                new PokemonSpecies(23, "Ekans", "Poison", "", "Snake", 1, 35, 60, 44, 40, 54, 55, "Arbok", 22),
                new PokemonSpecies(24, "Arbok", "Poison", "", "Cobra", 1, 60, 85, 69, 65, 79, 80),

                new PokemonSpecies(25, "Pikachu", "Electric", "", "Mouse", 1, 35, 55, 40, 50, 50, 90, "Raichu", "Thunderstone", 0),
                new PokemonSpecies(26, "Raichu", "Electric", "", "Mouse", 1, 60, 90, 55, 90, 80, 110),

                new PokemonSpecies(27, "Sandshrew", "Ground", "", "Mouse", 1, 50, 75, 85, 20, 30, 40, "Sandslash", 22),
                new PokemonSpecies(28, "Sandslash", "Ground", "", "Mouse", 1, 75, 100, 110, 45, 55, 65),

                new PokemonSpecies(29, "Nidoran♀", "Poison", "", "Poison Pin", 1, 55, 47, 52, 40, 40, 41, "Nidorina", 16),
                new PokemonSpecies(30, "Nidorina", "Poison", "", "Poison Pin", 1, 70, 62, 67, 55, 55, 56, "Nidoqueen", "Moon Stone", 0),
                new PokemonSpecies(31, "Nidoqueen", "Poison", "Ground", "Drill", 1, 90, 92, 87, 75, 85, 76),

                new PokemonSpecies(32, "Nidoran♂", "Poison", "", "Poison Pin", 1, 46, 57, 40, 40, 40, 50, "Nidorino", 16),
                new PokemonSpecies(33, "Nidorino", "Poison", "", "Poison Pin", 1, 61, 72, 57, 55, 55, 65, "Nidoking", "Moon Stone", 0),
                new PokemonSpecies(34, "Nidoking", "Poison", "Ground", "Drill", 1, 81, 102, 77, 85, 75, 85),

                new PokemonSpecies(35, "Clefairy", "Fairy", "", "Fairy", 1, 70, 45, 48, 60, 65, 35, "Clefable", "Moon Stone", 0),
                new PokemonSpecies(36, "Clefable", "Fairy", "", "Fairy", 1, 95, 70, 73, 95, 90, 60),

                new PokemonSpecies(37, "Vulpix", "Fire", "", "Fox", 1, 38, 41, 40, 50, 65, 65, "Ninetales", "Fire Stone", 0),
                new PokemonSpecies(38, "Ninetales", "Fire", "", "Fox", 1, 73, 76, 75, 81, 100, 100),

                new PokemonSpecies(39, "Jigglypuff", "Normal", "Fairy", "Balloon", 1, 115, 45, 20, 45, 25, 20, "Wigglytuff", "Moon Stone", 0),
                new PokemonSpecies(40, "Wigglytuff", "Normal", "Fairy", "Balloon", 1, 140, 70, 45, 85, 50, 45),

                new PokemonSpecies(41, "Zubat", "Poison", "Flying", "Bat", 1, 40, 45, 35, 30, 40, 55, "Golbat", 22),
                new PokemonSpecies(42, "Golbat", "Poison", "Flying", "Bat", 1, 75, 80, 70, 65, 75, 90),

                new PokemonSpecies(43, "Oddish", "Grass", "Poison", "Weed", 1, 45, 50, 55, 75, 65, 30, "Gloom", 21),
                new PokemonSpecies(44, "Gloom", "Grass", "Poison", "Weed", 1, 60, 65, 70, 85, 75, 40, "Vileplume", "Leaf Stone", 0),
                new PokemonSpecies(45, "Vileplume", "Grass", "Poison", "Flower", 1, 75, 80, 85, 110, 90, 50),

                new PokemonSpecies(46, "Paras", "Bug", "Grass", "Mushroom", 1, 35, 70, 55, 45, 55, 25, "Parasect", 24),
                new PokemonSpecies(47, "Parasect", "Bug", "Grass", "Mushroom", 1, 60, 95, 80, 60, 80, 30),

                new PokemonSpecies(48, "Venonat", "Bug", "Poison", "Insect", 1, 60, 55, 50, 40, 55, 45, "Venomoth", 31),
                new PokemonSpecies(49, "Venomoth", "Bug", "Poison", "Poison Moth", 1, 70, 65, 60, 90, 75, 90),

                new PokemonSpecies(50, "Diglett", "Ground", "", "Mole", 1, 10, 55, 25, 35, 45, 95, "Dugtrio", 26),
                new PokemonSpecies(51, "Dugtrio", "Ground", "", "Mole", 1, 35, 80, 50, 50, 70, 120),

                new PokemonSpecies(52, "Meowth", "Normal", "", "Scratch Cat", 1, 40, 45, 35, 40, 40, 90, "Persian", 28),
                new PokemonSpecies(53, "Persian", "Normal", "", "Classy Cat", 1, 65, 70, 60, 65, 65, 115),

                new PokemonSpecies(54, "Psyduck", "Water", "", "Duck", 1, 50, 52, 48, 65, 50, 55, "Golduck", 33),
                new PokemonSpecies(55, "Golduck", "Water", "", "Duck", 1, 80, 82, 78, 95, 80, 85),

                new PokemonSpecies(56, "Mankey", "Fighting", "", "Pig Monkey", 1, 40, 80, 35, 35, 45, 70, "Primeape", 28),
                new PokemonSpecies(57, "Primeape", "Fighting", "", "Pig Monkey", 1, 65, 105, 60, 60, 70, 95),

                new PokemonSpecies(58, "Growlithe", "Fire", "", "Puppy", 1, 55, 70, 45, 70, 50, 60, "Arcanine", "Fire Stone", 0),
                new PokemonSpecies(59, "Arcanine", "Fire", "", "Legendary", 1, 90, 110, 80, 100, 80, 95),

                new PokemonSpecies(60, "Poliwag", "Water", "", "Tadpole", 1, 40, 50, 40, 40, 40, 90, "Poliwhirl", 25),
                new PokemonSpecies(61, "Poliwhirl", "Water", "", "Tadpole", 1, 65, 65, 65, 50, 50, 90, "Poliwrath", "Water Stone", 0),
                new PokemonSpecies(62, "Poliwrath", "Water", "Fighting", "Tadpole", 1, 90, 95, 95, 70, 90, 70),

                new PokemonSpecies(63, "Abra", "Psychic", "", "Psi", 1, 25, 20, 15, 105, 55, 90, "Kadabra", 16),
                new PokemonSpecies(64, "Kadabra", "Psychic", "", "Psi", 1, 40, 35, 30, 120, 70, 105, "Alakazam", 36),
                new PokemonSpecies(65, "Alakazam", "Psychic", "", "Psi", 1, 55, 50, 45, 135, 95, 120),

                new PokemonSpecies(66, "Machop", "Fighting", "", "Superpower", 1, 70, 80, 50, 35, 35, 35, "Machoke", 28),
                new PokemonSpecies(67, "Machoke", "Fighting", "", "Superpower", 1, 80, 100, 70, 50, 60, 45, "Machamp", 36),
                new PokemonSpecies(68, "Machamp", "Fighting", "", "Superpower", 1, 90, 130, 80, 65, 85, 55),

                new PokemonSpecies(69, "Bellsprout", "Grass", "Poison", "Flower", 1, 50, 75, 35, 70, 30, 40, "Weepinbell", 21),
                new PokemonSpecies(70, "Weepinbell", "Grass", "Poison", "Flycatcher", 1, 65, 90, 50, 85, 45, 55, "Victreebel", "Leaf Stone", 0),
                new PokemonSpecies(71, "Victreebel", "Grass", "Poison", "Flycatcher", 1, 80, 105, 65, 100, 70, 70),

                new PokemonSpecies(72, "Tentacool", "Water", "Poison", "Jellyfish", 1, 40, 40, 35, 50, 100, 70, "Tentacruel", 30),
                new PokemonSpecies(73, "Tentacruel", "Water", "Poison", "Jellyfish", 1, 80, 70, 65, 80, 120, 100),

                new PokemonSpecies(74, "Geodude", "Rock", "Ground", "Rock", 1, 40, 80, 100, 30, 30, 20, "Graveler", 25),
                new PokemonSpecies(75, "Graveler", "Rock", "Ground", "Rock", 1, 55, 95, 115, 45, 45, 35, "Golem", 36),
                new PokemonSpecies(76, "Golem", "Rock", "Ground", "Megaton", 1, 80, 120, 130, 55, 65, 45),

                new PokemonSpecies(77, "Ponyta", "Fire", "", "Fire Horse", 1, 50, 85, 55, 65, 65, 90, "Rapidash", 40),
                new PokemonSpecies(78, "Rapidash", "Fire", "", "Fire Horse", 1, 65, 100, 70, 80, 80, 105),

                new PokemonSpecies(79, "Slowpoke", "Water", "Psychic", "Dopey", 1, 90, 65, 65, 40, 40, 15, "Slowbro", 37),
                new PokemonSpecies(80, "Slowbro", "Water", "Psychic", "Hermit Crab", 1, 95, 75, 110, 100, 80, 30),

                new PokemonSpecies(81, "Magnemite", "Electric", "Steel", "Magnet", 1, 25, 35, 70, 95, 55, 45, "Magneton", 30),
                new PokemonSpecies(82, "Magneton", "Electric", "Steel", "Magnet", 1, 50, 60, 95, 120, 70, 70),

                new PokemonSpecies(83, "Farfetch'd", "Normal", "Flying", "Wild Duck", 1, 52, 65, 55, 58, 62, 60),

                new PokemonSpecies(84, "Doduo", "Normal", "Flying", "Twin Bird", 1, 35, 85, 45, 35, 35, 75, "Dodrio", 31),
                new PokemonSpecies(85, "Dodrio", "Normal", "Flying", "Triple Bird", 1, 60, 110, 70, 60, 60, 100),

                new PokemonSpecies(86, "Seel", "Water", "", "Sea Lion", 1, 65, 45, 55, 45, 70, 45, "Dewgong", 34),
                new PokemonSpecies(87, "Dewgong", "Water", "Ice", "Sea Lion", 1, 90, 70, 80, 70, 95, 70),

                new PokemonSpecies(88, "Grimer", "Poison", "", "Sludge", 1, 80, 80, 50, 40, 50, 25, "Muk", 38),
                new PokemonSpecies(89, "Muk", "Poison", "", "Sludge", 1, 105, 105, 75, 65, 100, 50),

                new PokemonSpecies(90, "Shellder", "Water", "", "Bivalve", 1, 30, 65, 100, 45, 25, 40, "Cloyster", "Water Stone", 0),
                new PokemonSpecies(91, "Cloyster", "Water", "Ice", "Bivalve", 1, 50, 95, 180, 85, 45, 70),

                new PokemonSpecies(92, "Gastly", "Ghost", "Poison", "Gas", 1, 30, 35, 30, 100, 35, 80, "Haunter", 25),
                new PokemonSpecies(93, "Haunter", "Ghost", "Poison", "Gas", 1, 45, 50, 45, 115, 55, 95, "Gengar", 36),
                new PokemonSpecies(94, "Gengar", "Ghost", "Poison", "Shadow", 1, 60, 65, 60, 130, 75, 110),

                new PokemonSpecies(95, "Onix", "Rock", "Ground", "Rock Snake", 1, 35, 45, 160, 30, 45, 70),

                new PokemonSpecies(96, "Drowzee", "Psychic", "", "Hypnosis", 1, 60, 48, 45, 43, 90, 42, "Hypno", 26),
                new PokemonSpecies(97, "Hypno", "Psychic", "", "Hypnosis", 1, 85, 73, 70, 73, 115, 67),

                new PokemonSpecies(98, "Krabby", "Water", "", "River Crab", 1, 30, 105, 90, 25, 25, 50, "Kingler", 28),
                new PokemonSpecies(99, "Kingler", "Water", "", "Pincer", 1, 55, 130, 115, 50, 50, 75),

                new PokemonSpecies(100, "Voltorb", "Electric", "", "Ball", 1, 40, 30, 50, 55, 55, 100, "Electrode", 30),
                new PokemonSpecies(101, "Electrode", "Electric", "", "Ball", 1, 60, 50, 70, 80, 80, 140),

                new PokemonSpecies(102, "Exeggcute", "Grass", "Psychic", "Egg", 1, 60, 40, 80, 60, 45, 40, "Exeggutor", "Leaf Stone", 0),
                new PokemonSpecies(103, "Exeggutor", "Grass", "Psychic", "Coconut", 1, 95, 95, 85, 125, 65, 55),

                new PokemonSpecies(104, "Cubone", "Ground", "", "Lonely", 1, 50, 50, 95, 40, 50, 35, "Marowak", 28),
                new PokemonSpecies(105, "Marowak", "Ground", "", "Bone Keeper", 1, 60, 80, 110, 50, 80, 45),

                new PokemonSpecies(106, "Hitmonlee", "Fighting", "", "Kicking", 1, 50, 120, 53, 35, 110, 87),

                new PokemonSpecies(107, "Hitmonchan", "Fighting", "", "Punching", 1, 50, 105, 79, 35, 110, 76),

                new PokemonSpecies(108, "Lickitung", "Normal", "", "Licking", 1, 90, 55, 75, 60, 75, 30),

                new PokemonSpecies(109, "Koffing", "Poison", "", "Poison Gas", 1, 40, 65, 95, 60, 45, 35, "Weezing", 35),
                new PokemonSpecies(110, "Weezing", "Poison", "", "Poison Gas", 1, 65, 90, 120, 85, 70, 60),

                new PokemonSpecies(111, "Rhyhorn", "Ground", "Rock", "Spikes", 1, 80, 85, 95, 30, 30, 25, "Rhydon", 42),
                new PokemonSpecies(112, "Rhydon", "Ground", "Rock", "Drill", 1, 105, 130, 120, 45, 45, 40),

                new PokemonSpecies(113, "Chansey", "Normal", "", "Egg", 1, 250, 5, 5, 35, 105, 50),

                new PokemonSpecies(114, "Tangela", "Grass", "", "Vine", 1, 65, 55, 115, 100, 40, 60), 

                new PokemonSpecies(115, "Kangaskhan", "Normal", "", "Parent", 1, 105, 95, 80, 40, 80, 90),

                new PokemonSpecies(116, "Horsea", "Water", "", "Dragon", 1, 30, 40, 70, 70, 25, 60, "Seadra", 32),
                new PokemonSpecies(117, "Seadra", "Water", "", "Dragon", 1, 55, 65, 95, 95, 45, 85),

                new PokemonSpecies(118, "Goldeen", "Water", "", "Goldfish", 1, 45, 67, 60, 35, 50, 63, "Seaking", 33),
                new PokemonSpecies(119, "Seaking", "Water", "", "Goldfish", 1, 80, 92, 65, 65, 80, 68),

                new PokemonSpecies(120, "Staryu", "Water", "", "Star Shape", 1, 30, 45, 55, 70, 55, 85, "Starmie", "Water Stone", 0),
                new PokemonSpecies(121, "Starmie", "Water", "Psychic", "Mysterious", 1, 60, 75, 85, 100, 85, 115), 

                new PokemonSpecies(122, "Mr. Mime", "Psychic", "Fairy", "Barrier", 1, 40, 45, 65, 100, 120, 90), 

                new PokemonSpecies(123, "Scyther", "Bug", "Flying", "Mantis", 1, 70, 110, 80, 55, 80, 105), 

                new PokemonSpecies(124, "Jynx", "Ice", "Psychic", "Human Shape", 1, 65, 50, 35, 115, 95, 95),

                new PokemonSpecies(125, "Electabuzz", "Electric", "", "Electric", 1, 65, 83, 57, 95, 85, 105),

                new PokemonSpecies(126, "Magmar", "Fire", "", "Spitfire", 1, 65, 95, 57, 100, 85, 93),

                new PokemonSpecies(127, "Pinsir", "Bug", "", "Stag Beetle", 1, 65, 125, 100, 55, 70, 85),

                new PokemonSpecies(128, "Tauros", "Normal", "", "Wild Bull", 1, 75, 100, 95, 40, 70, 110),

                new PokemonSpecies(129, "Magikarp", "Water", "", "Fish", 1, 20, 10, 55, 15, 20, 80),

                new PokemonSpecies(130, "Gyarados", "Water", "Flying", "Atrocious", 1, 95, 125, 79, 60, 100, 81),

                new PokemonSpecies(131, "Lapras", "Water", "Ice", "Transport", 1, 130, 85, 80, 85, 95, 60), 

                new PokemonSpecies(132, "Ditto", "Normal", "", "Transform", 1, 48, 48, 48, 48, 48, 48),

                new PokemonSpecies(133, "Eevee", "Normal", "", "Evolution", 1, 55, 55, 50, 45, 65, 55),
                new PokemonSpecies(134, "Vaporeon", "Water", "", "Bubble Jet", 1, 130, 65, 60, 110, 95, 65),
                new PokemonSpecies(135, "Jolteon", "Electric", "", "Lightning", 1, 65, 65, 60, 110, 95, 130),
                new PokemonSpecies(136, "Flareon", "Fire", "", "Flame", 1, 65, 130, 60, 95, 110, 65),

                new PokemonSpecies(137, "Porygon", "Normal", "", "Virtual", 1, 65, 60, 70, 85, 75, 40),

                new PokemonSpecies(138, "Omanyte", "Rock", "Water", "Spiral", 1, 35, 40, 100, 90, 55, 35, "Omastar", 40),
                new PokemonSpecies(139, "Omastar", "Rock", "Water", "Spiral", 1, 70, 60, 125, 115, 70, 55),

                new PokemonSpecies(140, "Kabuto", "Rock", "Water", "Shellfish", 1, 30, 80, 90, 55, 45, 55, "Kabutops", 40),                
                new PokemonSpecies(141, "Kabutops", "Rock", "Water", "Shellfish", 1, 60, 115, 105, 65, 70, 80),

                new PokemonSpecies(142, "Aerodactyl", "Rock", "Flying", "Fossil", 1, 80, 105, 65, 60, 75, 130),

                new PokemonSpecies(143, "Snorlax", "Normal", "", "Sleeping", 1, 160, 110, 65, 65, 110, 30),

                new PokemonSpecies(144, "Articuno", "Ice", "Flying", "Freeze",1, 90, 85, 100, 95, 125, 85),
                
                new PokemonSpecies(145, "Zapdos", "Electric", "Flying", "Electric", 1, 90, 90, 85, 125, 90, 100),

                new PokemonSpecies(146, "Moltres", "Fire", "Flying", "Flame", 1, 90, 100, 90, 125, 85, 90), 

                new PokemonSpecies(147, "Dratini", "Dragon", "", "Dragon", 1, 41, 64, 45, 50, 50, 50, "Dragonair", 30),
                new PokemonSpecies(148, "Dragonair", "Dragon", "", "Dragon", 1, 61, 84, 65, 70, 70, 70, "Dragonite", 55),
                new PokemonSpecies(149, "Dragonite", "Dragon", "Flying", "Dragon", 1, 91, 134, 95, 100, 100, 80),

                new PokemonSpecies(150, "Mewtwo", "Psychic", "", "Genetic", 1, 106, 110, 90, 154, 90, 130),

                new PokemonSpecies(151, "Mew", "Psychic", "", "New Species", 1, 100, 100, 100, 100, 100, 100)

            };

        #endregion

        /// <summary>
        /// Lists all Pokemon in the "allPokemon" list.
        /// </summary>
        public static void ListAllPokemon()
        {
            foreach (PokemonSpecies p in allPokemon)
            {
                string evolutionMessage = "";

                if (p.Evolves)
                    evolutionMessage = ", evolves into " + p.EvolvesInto;

                Console.WriteLine(p.Name + evolutionMessage);
            }

            Console.WriteLine("");
        }

        public static void DoSomething()
        {
           
        }

        public static void DisplayBSTs()
        {
            //Displays every Pokemon, sorted by its Base Stat Total - for error checking purposes.

            Dictionary<string, int> totals = new Dictionary<string, int>();

            foreach (PokemonSpecies p in allPokemon)
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

            foreach (PokemonSpecies p in allPokemon)
            {
                if (p.Evolves)
                    Console.WriteLine("{0} evolves into {1}.", p.Name, p.EvolvesInto);
            }
        }

    }
}
