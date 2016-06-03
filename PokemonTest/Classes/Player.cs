using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// This class describes a player - his Pokemon, his money, his items, etc.
    /// </summary>
    class Player
    {
        #region Fields & Properties

        #region Player Attributes

        /// <summary>
        /// The player's unique ID number. Except it currently isn't really unique.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// The player's name.
        /// </summary>
        public string Name { get; set; }       
        
        private int money;

        /// <summary>
        /// The amount of money the player currently has on hand.
        /// </summary>
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                if (value < 0 || money + value < 0)
                    money = 0;
                else
                    money = value; //Money cannot ever be less than 0.
            } 
        }

        #endregion

        #region Collections

        /// <summary>
        /// The Pokemon that the player currently has with him and can thus battle.
        /// </summary>
        public List<Pokemon> Party = new List<Pokemon>();

        /// <summary>
        /// The Pokemon that the player currently has in his PC storage and can later retrieve.
        /// </summary>
        public List<Pokemon> Box = new List<Pokemon>();

        /// <summary>
        /// The various Pokemon that the player may have encountered in his journey, but has not necessarily captured.
        /// </summary>
        public List<int> SeenPokemon = new List<int>();

        /// <summary>
        /// The various Pokemon that the player has encountered and captured in his journey.
        /// </summary>
        public List<int> CaughtPokemon = new List<int>();

        /// <summary>
        /// The player's current inventory of items.
        /// </summary>
        public List<ItemInstance> Bag = new List<ItemInstance>();

        /// <summary>
        /// The badges that the player has collected by defeating the various gym leaders.
        /// </summary>
        public List<string> Badges = new List<string>();

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default player constructor. Generates a player with the default name of "Red" with money set to 500.
        /// </summary>
        public Player()
        {
            PlayerID = Program.random.Next(1, 10001);

            Name = Settings.DefaultPlayerName;
            Money = 500;
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Displays the player's information to the player.
        /// </summary>
        public string PrintInfo()
        {
            return "Name: " + Name +
                   "\nBadges: " + Badges.Count +
                   "\nMoney: $" + Money +
                   "\nPokemon seen: " + SeenPokemon.Count +
                   "\nPokemon caught: " + CaughtPokemon.Count + "\n";
        }

        /// <summary>
        /// Displays the status for each Pokemon in the player's party, separated by an empty line for each Pokemon.
        /// </summary>
        public string PrintPartyStatus()
        {
            string status = "";

            for (int i = 0; i < Party.Count; i++)
            {
                status += Party.ElementAt(i).PrintInfo() + "\n";
            }

            return status;
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
            if (Party.Count <= 6)
            {
                if (displayMessage)
                    UI.WriteLine(pokemon.Name + " was added to the party!\n");

                AddToCaught(pokemon.species.PokedexNumber);

                Party.Add(pokemon);
            }

            //Else, if the player's box is not full, it is sent to the box instead.
            else if (Box.Count <= 30)
            {
                if (displayMessage)
                    UI.WriteLine("Your party is full, so " + pokemon.Name + " was sent to the box.\n");

                AddToCaught(pokemon.species.PokedexNumber);

                Box.Add(pokemon);
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
            if (!SeenPokemon.Exists(p => p == number))
                SeenPokemon.Add(number);
        }

        /// <summary>
        /// Adds a Pokemon to the player's list of caught Pokemon, if it has not already been caught. Also adds it to the seen list.
        /// </summary>
        /// <param name="number">The Pokedex number of the Pokemon to add to the list.</param>
        public void AddToCaught(int number)
        {
            if (!SeenPokemon.Exists(p => p == number))
                SeenPokemon.Add(number);

            if (!CaughtPokemon.Exists(p => p == number))
                CaughtPokemon.Add(number);
        }

        #endregion

        #region Selecting & Switching Pokemon

        public string PartyPokemonList()
        {
            string list = "";

            //First, all of the Pokemon in the player's party are listed.
            for (int i = 0; i < Party.Count; i++)
            {
                list += (i + 1) + " - Level " + Party.ElementAt(i).Level + " " + Party.ElementAt(i).Name + ", "
                            + Party.ElementAt(i).CurrentHP + "/" + Party.ElementAt(i).MaxHP + " HP.";
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
                if (validInput && index > 0 && index <= Party.Count)
                {
                    pokemon = Party.ElementAt(index - 1);
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
            if (Party.Count > 1)
            {
                UI.WriteLine("Please select a Pokemon to be switched with another. \n(Valid input: 1-" + Party.Count + " or press Enter to return)\n");

                //First, all of the Pokemon in the player's party are listed.
                UI.WriteLine(PartyPokemonList());

                //Next, input is taken from the player. 
                string input = UI.ReceiveInput();

                int selection1;
                bool validInput = int.TryParse(input, out selection1);

                //If the input is a number corresponding to a Pokemon in the player's party, the operation carries on.
                if (validInput && selection1 > 0 && selection1 < (Party.Count + 1))
                {
                    //The selected Pokemon is temporarily removed from the party.
                    pokemon1 = Party.ElementAt(selection1 - 1);
                    Party.RemoveAt(selection1 - 1);

                    //Then, the player's remaining Pokemon are listed.
                    UI.WriteLine("Now please select the Pokemon it will be switched with. (Valid input: 1-" + Party.Count + ")\n");

                    //Input is taken from the player again. 
                    UI.WriteLine(PartyPokemonList());

                    string input2 = UI.ReceiveInput();

                    int selection2;
                    bool validInput2 = Int32.TryParse(input2, out selection2);

                    //If the input is a number corresponding to one of the remaining Pokemon in the player's party, the operation carries on.
                    if (validInput2 && selection2 > 0 && selection2 < (Party.Count + 1))
                    {
                        //The second Pokemon is also temporarily removed from the player's party.

                        pokemon2 = Party.ElementAt(selection2 - 1);
                        Party.RemoveAt(selection2 - 1);

                        //Then, 

                        Party.Insert((selection2 - 1), pokemon1);
                        Party.Insert((selection1 - 1), pokemon2);

                        UI.WriteLine("Pokemon succesfully switched.\n");
                    }

                    else
                    {
                        UI.InvalidInput();
                        Party.Insert((selection1 - 1), pokemon1);
                    }

                }

                else
                {
                    if (input != "")
                        UI.InvalidInput();
                }
            }

            else if (Party.Count == 1)
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
            if (Bag.Count > 0)
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
            if (Bag.Count > 10)
            {
                UI.WriteLine("What kind of item would you like to view?");
                UI.WriteLine("(Valid input: pokeball, potion, heal, misc)");

                string input = UI.ReceiveInput();

                ItemType type;

                Enum.TryParse(input, out type);

                if (Bag.Exists(item => item.BaseItem.Type == type))
                {
                    //This line is genius and I should implement this like everywhere. I love you, Linq.
                    //It basically searches for every item of type "type", creates a list out of all matches, and then runs PrintInfo() for each match.
                    Bag.Where(item => item.BaseItem.Type == type).ToList().ForEach(item => UI.WriteLine(item.PrintInfo()));

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

                foreach (ItemInstance item in Bag)
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

            ItemInstance item = SelectItem("use");

            if (item != null)
            {
                item.BaseItem.Use();
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
            if (Bag.Count > 0)
            {
                ItemInstance item = SelectItem("use");

                if (item != null)
                {
                    if (item.BaseItem.UseCombat())
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
        public ItemInstance SelectItem(string method)
        {
            //Frankly this became a bit too big and difficult to follow, so I'm not too happy with it.

            //A temporary list used to display a sublist of a specific type of items, should the player's bag hold too more than 10 items.
            List<ItemInstance> tempList = new List<ItemInstance>(); 

            //If there are more than 10 unique kinds of items in the player's bag, he is asked to select a particular type of item to display.
            if (Bag.Count > 10)
            {
                UI.WriteLine("What kind of item would you like to " + method + "? ");
                UI.WriteLine("(Valid input: pokeball, potion, status heal, misc)");

                string input = UI.ReceiveInput();

                ItemType type;

                Enum.TryParse(input, out type);

                //Input is taken from the player as to what kind of items to display, and if the input is correct, the tempList list is populated by items of that particular type.
                if (Bag.Exists(item => item.BaseItem.Type == type))
                {
                    Bag.Where(item => item.BaseItem.Type == type).ToList().ForEach(item => { tempList.Add(item); });
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
                tempList = Bag;

            //Next, the game displays the contents of tempList next to their corresponding index number, and the player is asked for input again.

            int counter = 1;

            foreach (ItemInstance i in tempList)
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

        #region Adding & Removing        

        /// <summary>
        /// Attemps to add a quantity of an item to the player's bag, regardless of how it is added.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="quantity">The amount to add.</param>
        /// <param name="method">The method of acquisition of the item.</param>
        public void Add(Item item, int quantity, AddType method)
        {
            //First the game searches whether there's an item in the player's inventory with the same name as this item. If so, that many of the item are added.
            if (Bag.Exists(i => i.Equals(item)))
                Bag.Find(i => i.BaseItem.ItemID == item.ItemID).Count += quantity;

            //If an item with this item's name doesn't exist in the player's bag, this item gets added instead, with a starting count of "quantity".
            else
            {
                Bag.Add(new ItemInstance(item, quantity));
            }

            //A message is then printed depending on method of acquisition.
            if (method == AddType.Obtain)
                UI.WriteLine("You obtained " + AddRemoveQuantityFormat(item.Name, quantity) + "!\n");
        }

        /// <summary>
        /// Attemps to remove a quantity of an item from the player's bag, regardless of how it is removed.
        /// </summary>
        /// <param name="quantity">The amount to remove.</param>
        /// <param name="method">The method of acquisition of the item, i.e. "sold" or "used".</param>
        public void Remove(Item item, int quantity, RemoveType method)
        {
            //First the game checls wether the item exists in the player's inventory. If so, a "quantity" amount of it is removed.
            if (Bag.Exists(i => i.Equals(item)))
            {
                Bag.Find(i => i.BaseItem.ItemID == item.ItemID).Count -= quantity;

                //If there are less than 1 of the item, it gets removed from the player's bag.
                if (Bag.Find(i => i.BaseItem.ItemID == item.ItemID).Count < 1)
                    Bag.Remove(Bag.Find(i => i.BaseItem.ItemID == item.ItemID));
            }

            //This is just for the freak scenario where the game tries to remove an item from the player's bag that doesn't exist.
            //This should never happen ideally, but I've added this error message to facilitate for the case I ever make a silly mistake like that.
            else
            {
                UI.Error("The game tried to remove an item that does not exist in the player's bag.",
                         "The game tried to remove " + item.Name + " from the bag, but there were none.", 2);
            }

            if (method == RemoveType.Use)
                UI.WriteLine("You used " + AddRemoveQuantityFormat(item.Name, quantity) + "!");
        }

        /// <summary>
        /// Quickly formats the name of the item depending on the quantity to be added/removed.
        /// </summary>
        /// <param name="quantity">The amount to be added or removed.</param>
        /// <returns>The item's name formatted by quantity. Example: "a Potion" or "5 Potions"</returns>
        protected string AddRemoveQuantityFormat(string item, int quantity)
        {
            if (quantity == 1)
                return "a " + item;

            else
                return quantity + " " + item + "s";
        }

        #endregion

        #endregion
    }
}
