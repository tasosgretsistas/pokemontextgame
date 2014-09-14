using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Properties;

namespace PokemonTextEdition
{
    [Serializable]
    public class Player
    {
        //A class that describes the player condition (Pokemon, money, badges, etc) as well as the player's progress in the game.

        //The player's name, as well as the player's rival.
        public string Name { get; set; }
        public string RivalName { get; set; }

        //The player's starting Pokemon. Determines the rival's Pokemon during various stages of the game.
        public string Starter { get; set; }

        //The player's basic parameters.
        public int Money { get; set; }
        public int Badges { get; set; }

        public List<string> badgeList = new List<string>();

        //The player's current location. Used for saving/loading only.
        public string Location { get; set; }

        //The last city in which the player healed. Used when the player runs out of Pokemon.
        public string LastHeal { get; set; }

        //The various lists of Pokemon that a player might have.
        public List<Pokemon> party = new List<Pokemon>();
        public List<Pokemon> box = new List<Pokemon>();
        public List<Pokemon> seenPokemon = new List<Pokemon>();
        public List<Pokemon> caughtPokemon = new List<Pokemon>();

        //The player's list of items.
        public List<Item> items = new List<Item>();

        //A list of all the trainers the player has defeated.
        public List<string> defeatedTrainers = new List<string>();

        public Player()
        {
            Name = "Ash";
            RivalName = "Gary";
            Starter = "";
            LastHeal = "pallet";
            Money = 500;
            Badges = 0;

        }

        public bool PartyFull()
        {
            //This method quickly determines whether the player's party is full.
            if (party.Count < 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool BoxFull()
        {
            //This method quickly determines whether the player's box is full.
            if (box.Count < 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Code for adding a Pokemon to the player's party, or box if his party is full.
        /// </summary>
        /// <param name="p">The new Pokemon.</param>
        /// <param name="method">Method of acquisition. Examples: catch, gift, trade</param>
        public void AddPokemon(Pokemon p, string method)
        {
            //This method handles adding a new Pokemon to the player's lists of Pokemon, depending on how many Pokemon each list already holds.

            if (!PartyFull())
            {
                //If the player's party is not full, the new Pokemon is added to the party.                

                Console.WriteLine("{0} was added to the party!", p.name);

                party.Add(p);                
            }

            else if (!BoxFull())
            {
                //Else, if the player's box is not full, it is sent to the box instead.

                string extraText = "";

                if (method == "catch")
                    extraText = "the caught ";

                Console.WriteLine("Your party is full, so {0}{1} was sent to the box.", extraText, p.name);

                box.Add(p);                
            }

            else
            {
                //If both the player's party and box are full, it is instead simply destroyed.

                string extraText = "";

                if (method == "catch")
                    extraText = "the caught ";

                Console.WriteLine("Both your party and box are full, so {0}{1} had to be released!", extraText, p.name);
            }

        }

        public void PartyStatus()
        {
            //This method displays the status for each Pokemon in the player's party.
            Console.WriteLine("\nYour party's status: ");

            foreach (Pokemon p in party)
            {
                p.PrintStatus();
            }
        }

        public void PartyHeal()
        {
            foreach (Pokemon p in Overworld.player.party)
            {
                p.currentHP = p.maxHP;
                p.status = "";
            }
        }

        public void Items()
        {
            //This method simply displays the contents of your bag, and is to be called when an item is not going to be used.

            Console.WriteLine("\nItems in your bag: ");

            foreach (Item i in items)
            {
                Console.WriteLine(i.Print());
            }
        }

        public void ListItems()
        {
            //This method displays the contents of your bag and their corresponding index number, and is to be called when an item needs to be selected for using.

            Console.WriteLine("\nItems in your bag:\n");

            for (int i = 0; i < items.Count; i++)
                Console.WriteLine("{0} - {1}", (i + 1), items.ElementAt(i).Print());
        }

        public void SwitchAround()
        {
            Pokemon temp1 = new Pokemon();
            Pokemon temp2 = new Pokemon();

            if (party.Count > 1)
            {

                Console.WriteLine("\nPlease select a Pokemon to be switched with another. (Valid input: 1-{0})\n", party.Count);

                for (int i = 0; i < party.Count; i++)
                {
                    Console.WriteLine("{0} - {1}, {2}/{3} HP.", i + 1, party.ElementAt(i).name, party.ElementAt(i).currentHP, party.ElementAt(i).maxHP);
                }

                int one;
                bool validInput = Int32.TryParse(Console.ReadLine(), out one);

                //Next, input is taken from the player. If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
                if (validInput && one > 0 && one < (party.Count + 1))
                {
                    temp1 = party.ElementAt(one - 1);
                    party.RemoveAt(one - 1);

                    Console.WriteLine("\nNow please select the Pokemon it will be switched with. (Valid input: 1-{0})\n", party.Count);

                    for (int i = 0; i < party.Count; i++)
                    {
                        Console.WriteLine("{0} - {1}, {2}/{3} HP.", i + 1, party.ElementAt(i).name, party.ElementAt(i).currentHP, party.ElementAt(i).maxHP);
                    }

                    int two;
                    bool validInput2 = Int32.TryParse(Console.ReadLine(), out two);

                    if (validInput2 && two > 0 && two < (party.Count + 1))
                    {
                        temp2 = party.ElementAt(two - 1);
                        party.RemoveAt(two - 1);

                        party.Insert((two - 1), temp1);
                        party.Insert((one - 1), temp2);

                        Console.WriteLine("\nPokemon succesfully switched.");
                    }

                    else
                    {
                        Console.WriteLine("Incorrect input.");
                        party.Insert((one - 1), temp1);
                    }

                }

                else
                {
                    Console.WriteLine("Incorrect input.");
                }
            }

            else if (party.Count == 1)
            {
                Console.WriteLine("You only have one Pokemon!");
            }

        }

        public Pokemon SelectPokemon(bool mandatory)
        {
            //This code is used when the player is asked to select a Pokemon in his party.

            //First, all of the Pokemon in the player's party are listed.
            for (int i = 0; i < party.Count; i++)
            {
                Console.WriteLine("{0} - Level {1} {2}, {3}/{4} HP.", i + 1, party.ElementAt(i).level, party.ElementAt(i).name,
                                                                        party.ElementAt(i).currentHP, party.ElementAt(i).maxHP);
            }

            string input = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            //First, input is taken from the player. If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
            if (validInput && index > 0 && index < (party.Count + 1))
            {
                return party.ElementAt(index - 1);
            }

            //If the player hit enter and the selection wasn't mandatory, he is returned back to whatever was happening.
            else if (input == "" && !mandatory)
            {
                Program.Log("The player chose to cancel the operation.", 0);

                return new Pokemon();
            }

            //If the input was smaller than 1, bigger than the player's party size or not a number, an error message is shown.
            else
            {
                Program.Log("The player gave invalid input. Returning to what was previously happening.", 0);
                Console.WriteLine("Invalid input.\n");

                return new Pokemon();
            }
        }
    }
}
