using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// This class describes a player - his Pokemon, his money, his items, as well as his progress in the game.
    /// </summary>
    class Player
    {
        #region Properties

        public int PlayerID { get; set; }

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
        public List<int> seenPokemon = new List<int>();
        public List<int> caughtPokemon = new List<int>();

        //The player's list of items.
        public List<Item> items = new List<Item>();

        //A list of badges the player has collected from defeating the various gym leaders.
        public List<string> badgeList = new List<string>();

        //A list of all the trainers the player has defeated.
        public List<int> defeatedTrainers = new List<int>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default player constructor. Generates a player with the default name of "Red", while assigning the rival the default name "Blue".
        /// </summary>
        public Player()
        {
            PlayerID = new Random().Next(1, 10001);

            Name = "Red";
            RivalName = "Blue";
            StartingPokemon = "";
            LastHealLocation = "pallet";
            Money = 500;
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Displays the player's information to the player.
        /// </summary>
        public void PlayerInfo()
        {
            UI.WriteLine("Your information: \n");

            UI.WriteLine("Name: " + Name);
            UI.WriteLine("Badges: " + badgeList.Count);
            UI.WriteLine("Money: $" + Money);
            UI.WriteLine("Pokemon seen: " + seenPokemon.Count);
            UI.WriteLine("Pokemon caught: " + caughtPokemon.Count);
            UI.WriteLine("");
        }

        /// <summary>
        /// Displays the status for each Pokemon in the player's party, separated by an empty line for each Pokemon.
        /// </summary>
        public void PartyStatus()
        {
            UI.WriteLine("Your party's status: \n");

            for (int i = 0; i < party.Count; i++)
            {
                UI.WriteLine(party.ElementAt(i).PrintInfo());

                UI.WriteLine("");
            }
        }

        /// <summary>
        /// Restores every Pokemon in the player's party to their maximum health and cures them of any status ailments.
        /// </summary>
        /// <param name="displayMessage">Determines whether a message will be displayed to the player.</param>
        public void PartyHeal(bool displayMessage)
        {
            if (displayMessage)
                UI.WriteLine("Your Pokemon were restored to full health.\n");

            foreach (Pokemon p in party)
            {
                p.HealFull(false);
                p.CureStatus(false);
                p.ResetTemporaryEffects(true);
            }
        }

        /// <summary>
        /// This code handles the event of the player "blacking out" -- running out of available Pokemon during a fight.
        /// Heals of all the player's Pokemon then invokes the Overworld to load the location where the player last healed at.
        /// </summary>
        public void BlackOut()
        {
            Program.Log("The player has been defeated, and is now returning to the last Pokemon Center he visited - " + LastHealLocation, 1);

            UI.WriteLine("You will now be taken to the last city you rested at.");

            UI.AnyKey();

            PartyHeal(false);

            Overworld.LoadLocationString(LastHealLocation);
        }

        #endregion

        #region Adding Pokemon

        /// <summary>
        /// Add a Pokemon to the player's party, or box if his party is full. If both party and box are full, the Pokemon is not added to either.
        /// </summary>
        /// <param name="pokemon">The Pokemon to add.</param>
        /// <param name="displayMessage">Determines whether a message should be displayed to the player.</param>
        public void AddPokemon(Pokemon pokemon, bool displayMessage)
        {
            //If the player's party is not full, the new Pokemon is added to the party. 
            if (party.Count <= 6)
            {
                if (displayMessage)
                    UI.WriteLine(pokemon.Name + " was added to the party!\n");

                AddToCaught(pokemon.species.PokedexNumber);

                party.Add(pokemon);
            }

            //Else, if the player's box is not full, it is sent to the box instead.
            else if (box.Count <= 30)
            {
                if (displayMessage)
                    UI.WriteLine("Your party is full, so " + pokemon.Name + " was sent to the box.\n");

                AddToCaught(pokemon.species.PokedexNumber);

                box.Add(pokemon);
            }

            //If both the player's party and box are full, it is instead simply destroyed.
            else
                UI.WriteLine("Both your party and box are full, so " + pokemon.Name + " had to be released!\n");
        }

        /// <summary>
        /// Adds a Pokemon to the player's list of seen Pokemon, if it has not already been seen.
        /// </summary>
        /// <param name="number">The Pokedex number of the Pokemon to add to the list.</param>
        public void AddToSeen(int number)
        {
            if (!seenPokemon.Exists(p => p == number))
                seenPokemon.Add(number);
        }

        /// <summary>
        /// Adds a Pokemon to the player's list of caught Pokemon, if it has not already been caught. Also adds it to the seen list.
        /// </summary>
        /// <param name="number">The Pokedex number of the Pokemon to add to the list.</param>
        public void AddToCaught(int number)
        {
            if (!seenPokemon.Exists(p => p == number))
                seenPokemon.Add(number);

            if (!caughtPokemon.Exists(p => p == number))
                caughtPokemon.Add(number);
        }

        #endregion

        #region Selecting & Switching Pokemon

        public string PartyPokemonList()
        {
            string list = "";

            //First, all of the Pokemon in the player's party are listed.
            for (int i = 0; i < party.Count; i++)
            {
                list += (i + 1) + " - Level " + party.ElementAt(i).Level + " " + party.ElementAt(i).Name + ", "
                            + party.ElementAt(i).CurrentHP + "/" + party.ElementAt(i).MaxHP + " HP.";
            }

            return list;

        }

        /// <summary>
        /// Asks the player to select a Pokemon from his party.
        /// </summary>
        /// <param name="mandatorySelection">Determines whether it is mandatory for the player to select a Pokemon or not.</param>
        /// <returns>Returns the Pokemon object that the player selected if his selection was valid, or null if it was invalid.</returns>
        public Pokemon SelectPokemon(bool mandatorySelection)
        {
            Pokemon pokemon;

            do
            {
                //First, all of the Pokemon in the player's party are listed.
                UI.WriteLine(PartyPokemonList());

                //Then, input is taken from the player.
                string input = UI.ReceiveInput();

                int index;
                bool validInput = int.TryParse(input, out index);

                //If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
                if (validInput && index > 0 && index <= party.Count)
                {
                    pokemon = party.ElementAt(index - 1);
                }

                //If the player hit enter and the selection wasn't mandatory, he is returned back to whatever was happening.
                else if (input == String.Empty && !mandatorySelection)
                {
                    Program.Log("The player chose to cancel the operation.", 0);

                    pokemon = null;
                }

                //If the input was smaller than 1, bigger than the player's party size or not a number, an error message is shown.
                else
                {
                    UI.InvalidInput();

                    Program.Log("The player gave invalid input. Returning to what was previously happening.", 0);                    

                    pokemon = null;
                }
            }

            while (mandatorySelection && pokemon == null);

            return pokemon;
        }

        /// <summary>
        /// Switches around the order of the Pokemon in the player's party.
        /// </summary>
        public void SwitchAround()
        {
            //Temporary Pokemon objects to handle the switching operation.
            Pokemon pokemon1;
            Pokemon pokemon2;

            //The player has to have more than 1 Pokemon in his party in order to be able to switch their order.
            if (party.Count > 1)
            {
                UI.WriteLine("Please select a Pokemon to be switched with another. \n(Valid input: 1-" + party.Count + " or press Enter to return)\n");

                //First, all of the Pokemon in the player's party are listed.
                UI.WriteLine(PartyPokemonList());

                //Next, input is taken from the player. 
                string input = UI.ReceiveInput();

                int selection1;
                bool validInput = int.TryParse(input, out selection1);

                //If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
                if (validInput && selection1 > 0 && selection1 < (party.Count + 1))
                {
                    //The selected Pokemon is temporarily removed from the party.
                    pokemon1 = party.ElementAt(selection1 - 1);
                    party.RemoveAt(selection1 - 1);

                    //Then, the player's remaining Pokemon are listed.
                    UI.WriteLine("Now please select the Pokemon it will be switched with. (Valid input: 1-" + party.Count + ")\n");

                    //Input is taken from the player again. 
                    UI.WriteLine(PartyPokemonList());

                    string input2 = UI.ReceiveInput();

                    int selection2;
                    bool validInput2 = Int32.TryParse(input2, out selection2);

                    //If the input is a number corresponding to one of the remaining Pokemon in the player's party, the operation carries on.
                    if (validInput2 && selection2 > 0 && selection2 < (party.Count + 1))
                    {
                        //The second Pokemon is also temporarily removed from the player's party.

                        pokemon2 = party.ElementAt(selection2 - 1);
                        party.RemoveAt(selection2 - 1);

                        //Then, 

                        party.Insert((selection2 - 1), pokemon1);
                        party.Insert((selection1 - 1), pokemon2);

                        UI.WriteLine("Pokemon succesfully switched.\n");
                    }

                    else
                    {
                        UI.InvalidInput();
                        party.Insert((selection1 - 1), pokemon1);
                    }

                }

                else
                {
                    if (input != "")
                        UI.InvalidInput();
                }
            }

            else if (party.Count == 1)
            {
                UI.WriteLine("There is only one Pokemon in your party!\n");
            }
        }

        #endregion

        #region Items

        /// <summary>
        /// This is the main method for accessing the player's bag. The player is asked if he wants to simply view his bag's contents or use an item,
        /// and gets redirected to the relevant menu. If there are no items in the player's bag, a relevant message is displayed instead.
        /// </summary>
        public void ItemsMain()
        {
            if (items.Count > 0)
            {
                UI.WriteLine("What would you like to do with the items in your bag?");
                UI.WriteLine("(V)iew items, (u)se items, or Enter to return.");

                string input = UI.ReceiveInput();

                switch (input.ToLower())
                {
                    case "view":
                    case "v":

                        DisplayItems();

                        break;

                    case "use":
                    case "u":

                        UseItems();

                        break;

                    case "":

                        break;

                    default:

                        UI.InvalidInput();
                        break;
                }
            }

            else
                UI.WriteLine("There are no items in your bag!\n");
        }

        /// <summary>
        /// Displays the contents of the player's bag, and is to be called when an item is not going to be used.
        /// </summary>
        public void DisplayItems()
        {
            //If there are more than 10 unique kinds of items in the player's bag, he is asked to select a particular type to display.
            if (items.Count > 10)
            {
                UI.WriteLine("What kind of item would you like to view?");
                UI.WriteLine("(Valid input: pokeball, potion, heal, misc)");

                string input = UI.ReceiveInput();

                ItemType type;

                Enum.TryParse(input, out type);

                if (items.Exists(item => item.Type == type))
                {
                    //This line is genius and I should implement this like everywhere. I love you, Linq.
                    //It basically searches for every item of type "type", creates a list out of all matches, and then runs PrintInfo() for each match.
                    items.Where(item => item.Type == type).ToList().ForEach(item => UI.WriteLine(item.PrintInfo()));

                    UI.WriteLine("");

                    ItemsMain();
                }

                else
                {
                    if (input != "")
                        UI.WriteLine("There were no items of type \"" + input + "\" in your bag.\n");

                    ItemsMain();
                }
            }

            else
            {
                UI.WriteLine("Items in your bag: ");

                foreach (Item item in items)
                {
                    UI.WriteLine(item.PrintInfo());
                }

                UI.WriteLine("");

                ItemsMain();
            }
        }

        /// <summary>
        /// Asks the user to select an item, and subsequently uses it.
        /// </summary>
        public void UseItems()
        {

            Item item = SelectItem("use");

            if (item != null)
            {
                item.Use();
                UI.WriteLine("");
            }

            ItemsMain();
        }

        /// <summary>
        /// Asks the user to select an item, and subsequently uses it. Additionally returns boolean to output the result of the operation.
        /// Same as the UseItems() method, except this method is to be called during combat.         
        /// </summary>
        /// <returns>Returns true if an item was used, or false otherwise.</returns>
        public bool UseItemsCombat()
        {
            if (items.Count > 0)
            {
                Item item = SelectItem("use");

                if (item != null)
                {
                    if (item.UseCombat())
                        return true;

                    else
                        return false;
                }

                else
                    return false;
            }

            else
            {
                UI.WriteLine("There are no items in your bag!\n");

                return false;
            }
        }

        /// <summary>
        /// Asks the player to select an item from their bag. 
        /// </summary>
        /// <param name="method">This designates how the item is going to be utilized - i.e. use, sell, discard. It affects the displayed message.</param>
        /// <returns></returns>
        public Item SelectItem(string method)
        {
            //Frankly this became a bit too big and difficult to follow, so I'm not too happy with it.

            List<Item> tempList = new List<Item>(); //A temporary list used to display a sublist of a specific type of items, should the player's bag hold too more than 10 items.

            //If there are more than 10 unique kinds of items in the player's bag, he is asked to select a particular type of item to display.
            if (items.Count > 10)
            {
                UI.WriteLine("What kind of item would you like to " + method + "? ");
                UI.WriteLine("(Valid input: pokeball, potion, status heal, misc)");

                string input = UI.ReceiveInput();

                ItemType type;

                Enum.TryParse(input, out type);

                //Input is taken from the player as to what kind of items to display, and if the input is correct, the tempList list is populated by items of that particular type.
                if (items.Exists(item => item.Type == type))
                {
                    items.Where(item => item.Type == type).ToList().ForEach(item => { tempList.Add(item); });
                }

                else if (input != "")
                {
                    UI.WriteLine("There were no items of type \"" + input + "\" in your bag.\n");

                    return null;
                }

                else
                    return null;
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
                    UI.WriteLine(counter + " - " + i.PrintInfo());
                    counter++;
                }
            }

            UI.WriteLine("\nPlease select an item to " + method + ". (Valid input: 1-" + tempList.Count + ", or Enter to return.)");

            string input2 = UI.ReceiveInput();
            int index;
            bool validInput = Int32.TryParse(input2, out index);

            //If the input is a number corresponding to an item in tempList, that item gets selected.
            if (validInput && index > 0 && index <= tempList.Count)
                return tempList.ElementAt(index - 1);

            //If the input was smaller than 1, bigger than the tempList items count or not a number, an error message is shown.
            else
            {
                if (input2 != "")
                    UI.InvalidInput();

                return null;
            }
        }

        #endregion
    }
}
