using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Properties;
using PokemonTextEdition.Items;

namespace PokemonTextEdition
{
    [Serializable]
    public class Player
    {
        #region Properties & Constructors

        //A class that describes the player condition (Pokemon, money, badges, etc) as well as the player's progress in the game.

        //The player's name, as well as the player's rival.

        public string Name { get; set; }

        public string RivalName { get; set; }

        //The player's starting Pokemon. Determines the rival's Pokemon during various stages of the game.
        public string StartingPokemon { get; set; }

        //The player's current money on hand.
        private int money;

        public int Money
        {
            get { return money; }
            set { if (value < 0 || money + value < 0) money = 0; else money = value; } //Money cannot ever be less than 0.
        }

        //The player's current location. Used for saving/loading only.
        public string Location { get; set; }

        //The last city in which the player healed. Used when the player runs out of Pokemon.
        public string LastHealLocation { get; set; }

        //These two lists describe the Pokemon that the player currently owns.
        public List<Pokemon> party = new List<Pokemon>();
        public List<Pokemon> box = new List<Pokemon>();

        //These two lists describe the Pokemon that the player has encountered and captured.
        public List<string> seenPokemon = new List<string>();
        public List<string> caughtPokemon = new List<string>();

        //The player's list of items.
        public List<Item> items = new List<Item>();

        //A list of badges the player has collected from defeating the various gym leaders.
        public List<string> badgeList = new List<string>();

        //A list of all the trainers the player has defeated.
        public List<string> defeatedTrainers = new List<string>();

        public Player()
        {
            Name = "Red";
            RivalName = "Blue";
            StartingPokemon = "";
            LastHealLocation = "pallet";
            Money = 500;
        }

        #endregion

        #region General Methods

        public void PlayerInfo()
        {
            //This method displays the status for each Pokemon in the player's party, separated by an empty line for every Pokemon.
            Console.WriteLine("Your information: ");
            Console.WriteLine("");

            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Badges: " + badgeList.Count);
            Console.WriteLine("Money: $" + Money);
            Console.WriteLine("Pokemon seen: " + seenPokemon.Count);
            Console.WriteLine("Pokemon caught: " + caughtPokemon.Count);
            Console.WriteLine("");
        }

        public void PartyStatus()
        {
            //This method displays the status for each Pokemon in the player's party, separated by an empty line for every Pokemon.
            Console.WriteLine("Your party's status: \n");

            for (int i = 0; i < party.Count; i++)
            {
                party.ElementAt(i).PrintStatus();

                Console.WriteLine("");
            }
        }

        public void PartyHeal()
        {
            foreach (Pokemon p in Overworld.player.party)
            {
                p.CurrentHP = p.MaxHP;
                p.Status = "";
            }
        }

        public void BlackOut()
        {
            //Code that triggers when the player is defeated.

            Program.Log("The player has been defeated, and is now returning to the last Pokemon Center he visited - " + LastHealLocation, 1);

            Console.WriteLine("You will now be taken to the last city you rested at.");

            Program.AnyKey();

            PartyHeal();

            Overworld.LoadLocation(LastHealLocation);
        }

        #endregion

        #region Adding Pokemon

        /// <summary>
        /// Code for adding a Pokemon to the player's party, or box if his party is full. If both party and box are full, the Pokemon will be released.
        /// </summary>
        /// <param name="p">The new Pokemon.</param>
        /// <param name="method">Method of acquisition. Examples: catch, gift, trade</param>
        public void AddPokemon(Pokemon p, bool displayMessage)
        {
            //This method handles adding a new Pokemon to the player's lists of Pokemon, depending on how many Pokemon each list already holds.

            //If the player's party is not full, the new Pokemon is added to the party. 
            if (party.Count <= 6)
            {
                if (displayMessage)
                    Console.WriteLine("{0} was added to the party!", p.Name);

                AddToCaught(p.Name);

                party.Add(p);
            }

            //Else, if the player's box is not full, it is sent to the box instead.
            else if (box.Count <= 30)
            {
                if (displayMessage)
                    Console.WriteLine("Your party is full, so {0} was sent to the box.", p.Name);

                AddToCaught(p.Name);

                box.Add(p);
            }

            //If both the player's party and box are full, it is instead simply destroyed.
            else
                Console.WriteLine("Both your party and box are full, so {0}{1} had to be released!", p.Name);
        }

        public void AddToSeen(string name)
        {
            if (!seenPokemon.Exists(n => n == name))
                seenPokemon.Add(name);
        }

        public void AddToCaught(string name)
        {
            if (!seenPokemon.Exists(n => n == name))
                seenPokemon.Add(name);

            if (!caughtPokemon.Exists(n => n == name))
                seenPokemon.Add(name);
        }

        #endregion

        #region Selecting & Switching Pokemon

        public Pokemon SelectPokemon(bool mandatorySelection)
        {
            //This code is used when the player is asked to select a Pokemon in his party.

            //First, all of the Pokemon in the player's party are listed.
            for (int i = 0; i < party.Count; i++)
            {
                Console.WriteLine("{0} - Level {1} {2}, {3}/{4} HP.", i + 1, party.ElementAt(i).Level, party.ElementAt(i).Name,
                                                                        party.ElementAt(i).CurrentHP, party.ElementAt(i).MaxHP);
            }

            string input = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            //First, input is taken from the player. If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
            if (validInput && index > 0 && index <= party.Count)
            {
                return party.ElementAt(index - 1);
            }

            //If the player hit enter and the selection wasn't mandatory, he is returned back to whatever was happening.
            else if (input == String.Empty && !mandatorySelection)
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

        public void SwitchAround()
        {
            //This method is used to switch around the order of Pokemon in the player's party.

            //Temporary Pokemon objects to handle the switching operation.
            Pokemon pokemon1 = new Pokemon();
            Pokemon pokemon2 = new Pokemon();

            if (party.Count > 1)
            {
                //First, all of the player's Pokemon are listed.

                Console.WriteLine("Please select a Pokemon to be switched with another. \n(Valid input: 1-{0} or press Enter to return)\n", party.Count);

                for (int i = 0; i < party.Count; i++)
                {
                    Console.WriteLine("{0} - {1}, {2}/{3} HP.", i + 1, party.ElementAt(i).Name, party.ElementAt(i).CurrentHP, party.ElementAt(i).MaxHP);
                }

                string input = Console.ReadLine();
                int selection1;
                bool validInput = Int32.TryParse(input, out selection1);

                if (input != "")
                    Console.WriteLine("");

                //Next, input is taken from the player. If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
                if (validInput && selection1 > 0 && selection1 < (party.Count + 1))
                {
                    //The selected Pokemon is temporarily removed from the party and the player's remaining Pokemon are listed.

                    pokemon1 = party.ElementAt(selection1 - 1);
                    party.RemoveAt(selection1 - 1);

                    Console.WriteLine("Now please select the Pokemon it will be switched with. (Valid input: 1-{0})\n", party.Count);

                    for (int i = 0; i < party.Count; i++)
                    {
                        Console.WriteLine("{0} - {1}, {2}/{3} HP.", i + 1, party.ElementAt(i).Name, party.ElementAt(i).CurrentHP, party.ElementAt(i).MaxHP);
                    }

                    string input2 = Console.ReadLine();
                    int selection2;
                    bool validInput2 = Int32.TryParse(input2, out selection2);

                    if (input2 != "")
                        Console.WriteLine("");

                    //Input is taken from the player again. If the input is a number corresponding to one of the remaining Pokemon in the player's party, the operation carries on.
                    if (validInput2 && selection2 > 0 && selection2 < (party.Count + 1))
                    {
                        //The second Pokemon is also temporarily removed from the player's party.

                        pokemon2 = party.ElementAt(selection2 - 1);
                        party.RemoveAt(selection2 - 1);

                        //Then, 

                        party.Insert((selection2 - 1), pokemon1);
                        party.Insert((selection1 - 1), pokemon2);

                        Console.WriteLine("Pokemon succesfully switched.\n");
                    }

                    else
                    {
                        Console.WriteLine("Invalid input.\n");
                        party.Insert((selection1 - 1), pokemon1);
                    }

                }

                else
                {
                    if (input != "")
                        Console.WriteLine("Invalid input.\n");
                }
            }

            else if (party.Count == 1)
            {
                Console.WriteLine("\nThere is only one Pokemon in your party!\n");
            }
        }

        #endregion

        #region Items

        public void ItemsMain()
        {
            //This is the main method for accessing the player's bag. The player is asked if he wants to 
            //simply view his bag's contents or use an item, and gets redirected to the relevant menu.
            //If there are no items in the player's bag, a relevant message is displayed instead.

            if (items.Count > 0)
            {
                Console.WriteLine("What would you like to do with the items in your bag?");
                Console.WriteLine("(V)iew items, (u)se items, or Enter to return.");

                string input = Console.ReadLine();

                if (input != "")
                    Console.WriteLine("");

                switch (input)
                {
                    case "View":
                    case "view":
                    case "V":
                    case "v":

                        DisplayItems();

                        break;

                    case "Use":
                    case "use":
                    case "U":
                    case "u":

                        UseItems();

                        break;

                    case "":

                        break;

                    default:

                        Console.WriteLine("Invalid input!\n");
                        break;
                }
            }

            else
                Console.WriteLine("There are no items in your bag!\n");
        }

        public void DisplayItems()
        {
            //This method simply displays the contents of your bag, and is to be called when an item is not going to be used.

            //If there are more than 10 unique kinds of items in the player's bag, he is asked to select a particular type to display.
            if (items.Count > 10)
            {
                Console.WriteLine("What kind of item would you like to view?");
                Console.WriteLine("(Valid input: pokeball, potion, heal, misc)");

                string input = Console.ReadLine();

                if (input != "")
                    Console.WriteLine("");

                if (items.Exists(item => item.Type == input))
                {
                    items.Where(item => item.Type == input).ToList().ForEach(item => Console.WriteLine(item.Print()));

                    Console.WriteLine("");

                    ItemsMain();
                }

                else
                {
                    if (input != "")
                        Console.WriteLine("There were no items of type \"{0}\" in your bag.\n", input);

                    ItemsMain();
                }
            }

            else
            {
                Console.WriteLine("Items in your bag: ");

                foreach (Item i in items)
                {
                    Console.WriteLine(i.Print());
                }

                Console.WriteLine("");

                ItemsMain();
            }
        }

        public void UseItems()
        {
            //This method simply uses an item. An item is selected using the SelectItem method, and it is subsequently used.

            Item item = SelectItem("use");

            if (item.Name != "Sample Item")
            {
                item.Use();
                Console.WriteLine("");
            }

            ItemsMain();
        }

        public bool UseItemsCombat()
        {
            Item item = SelectItem("use");

            if (item.Name != "Sample Item")
            {
                if (item.UseCombat())
                    return true;

                else
                    return false;
            }

            else
                return false;
        }

        /// <summary>
        /// This method is used for selecting an item off the player's bag. 
        /// </summary>
        /// <param name="method">This designates how the item is going to be utilized - i.e. use, sell, discard. It affects the displayed message.</param>
        /// <returns></returns>
        public Item SelectItem(string method)
        {
            //This method is used for selecting an item off the player's bag. 
            //Frankly this became a bit too big and difficult to follow, so I'm not too happy with it.

            List<Item> tempList = new List<Item>(); //A temporary list used to display a sublist of a specific type of items, should the player's bag hold too more than 10 items.

            //If there are more than 10 unique kinds of items in the player's bag, he is asked to select a particular type of item to display.
            if (items.Count > 10)
            {
                Console.WriteLine("What kind of item would you like to {0}?", method);
                Console.WriteLine("(Valid input: pokeball, potion, status heal, misc)");

                string input = Console.ReadLine();

                if (input != "")
                    Console.WriteLine("");

                //Input is taken from the player as to what kind of items to display, and if the input is correct, the tempList list is populated by items of that particular type.
                if (items.Exists(item => item.Type == input))
                {
                    items.Where(item => item.Type == input).ToList().ForEach(item => { tempList.Add(item); });
                }

                else if (input != "")
                {
                    Console.WriteLine("There were no items of type \"{0}\" in your bag.\n", input);

                    return new Item();
                }

                else
                    return new Item();
            }

            //Else if there are less than 10 items in the player's bag, there is no need for a sublist so tempList becomes the player's bag. 
            else
                tempList = items;

            //Next, the game displays the contents of tempList next to their corresponding index number, and the player is asked for input again.

            int counter = 1;

            foreach (Item i in tempList)
            {
                if (i.Count > 0)
                {
                    Console.WriteLine("{0} - {1}", counter, i.Print());
                    counter++;
                }
            }

            Console.WriteLine("\nPlease select an item to {0}. (Valid input: 1-{1}, or Enter to return.)", method, tempList.Count);

            string input2 = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input2, out index);

            if (input2 != "")
                Console.WriteLine("");

            //If the input is a number corresponding to an item in tempList, that item gets selected.
            if (validInput && index > 0 && index <= tempList.Count)
                return tempList.ElementAt(index - 1);

            //If the input was smaller than 1, bigger than the tempList items count or not a number, an error message is shown.
            else
            {
                if (input2 != "")
                    Console.WriteLine("Invalid input.\n");

                return new Item();
            }
        }

        #endregion

    }
}
