using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition
{
    class Mart
    {
        //The mart's "stock" - a list of items it can sell.
        List<Item> stock = new List<Item>();

        //The item that is currently being purchased/sold.
        Item item;   

        /// <summary>
        /// The "start" method of the mart.
        /// </summary>
        /// <param name="martStock">The mart's stock. This should be provided by each specific location.</param>
        public void Welcome(List<Item> martStock)
        {
            //First, a generic greeting for all stores is displayed.
            Console.WriteLine("Welcome to the Pokemon Mart! We've got goods of all kinds! Take a look!\n");

            //Then, the mart's stock is loaded.
            if (martStock != null && martStock.Count > 0)
                stock = martStock;

            //This is a failsafe in the event that a mart is loaded with no stock for whatever reason.
            else
                stock = new List<Item> { ItemList.potion }; 

            //Finally, the player is taken to the options screen, where he may decide what to do.
            Options();
        }

        /// <summary>
        /// This method is effectively the store's main menu. Here, the player decides whether he wants to buy, sell or exit.
        /// </summary>
        void Options()
        {
            //The item object is nulled so as to reset which item is currently being selected in the Buy and Sell methods.
            item = null;

            Console.WriteLine("What would you like to do at the mart?\n(Available actions: buy, sell, exit)");

            //Input is requested of the player.
            string action = Console.ReadLine();

            if (action != "")
                Console.WriteLine("");

            //Then, the respective method is triggered.
            switch (action)
            {
                case "Buy":
                case "buy":

                    Buy();

                    break;

                case "Sell":
                case "sell":

                    Sell();

                    break;

                case "Exit":
                case "exit":
                    break;

                default:  

                    Console.WriteLine("Invalid input!\n");

                    Options();

                    break;
            }
        }

        /// <summary>
        /// This method handles the player purchasing items at the mart.
        /// </summary>
        void Buy()
        {
            Console.WriteLine("Of course! What are you after? We have the following items in stock:\n");

            //The player is asked to select an item.
            item = DisplayStock();

            //If the player selected a valid item, its properties are displayed, and the operation carries on.
            if (item != null)
            {
                int itemCount = 0;

                if (Overworld.player.items.Contains(Overworld.player.items.Find(i => i.ItemID == item.ItemID)))
                    itemCount = Overworld.player.items.Find(i => i.ItemID == item.ItemID).Count; //This searches for the selected item in the player's bag and returns how many of the item the player has.

                Console.WriteLine("{0}s, huh? They're ${1} each. How many would you like to buy?\n(You have {2} {0}s on you. Money: ${3}.\nEnter amount to buy or press enter to return.)",
                                  item.Name, item.Value, itemCount, Overworld.player.Money);

                //Next, the player is asked to provide input as to how many of the selected item to purchase.
                string decision = Console.ReadLine();
                int amount;
                bool validInput = Int32.TryParse(decision, out amount);

                if (decision != "")
                    Console.WriteLine("");

                //If the player's input was valid and the amount that he selected was at least 1...
                if (validInput && amount > 0)
                {
                    //... If he has enough money, that many of that item are added to the player's items list.
                    if (Overworld.player.Money >= (item.Value * amount))
                    {
                        string formattedName = item.Name;

                        if (amount > 1)
                            formattedName += "s";

                        item.Add(amount, "buy");

                        Overworld.player.Money -= (item.Value * amount);

                        Console.WriteLine("There you go! {0} {1}, ${2} all in all!\nThank you, come again!\n", amount, formattedName, (item.Value * amount));
                    }

                    //Otherwise, the player is informed that he doesn't have enough money.
                    else
                    {
                        Console.WriteLine("It appears you don't have enough money!\n");
                    }

                    //Finally, the player is returned to the Options menu.
                    Options();
                }

                //If the player just hit Enter, he is returned to the Options menu without an error.
                else if (decision == "")                
                    Options();                

                //Otherwise, an error message is displayed and the player is returned to the Options menu.
                else
                {
                    Console.WriteLine("Invalid input.\n");

                    Options();
                }
            }

            else
                Options();
        }

        /// <summary>
        /// This method handles the player selling items at the mart.
        /// </summary>
        void Sell()
        {
            //First, the game checks to see whether the player has at least one item in his bag.
            if (Overworld.player.items.Count > 0)
            {
                Console.WriteLine("Selling? No problem! What would you like to sell?\n");

                //If so, he is asked to select an item from his bag.
                item = Overworld.player.SelectItem("sell");

                //If the player selected a valid item, its properties are displayed, and the operation carries on.
                if (item != null)
                {
                    int saleValue = (int)(item.Value / 2); //The item's price for selling - half the purchasing price.
                    int itemCount = item.Count;

                    Console.WriteLine("{0}s, right? I can give you ${1} a piece. How many are you selling?\n(You have {2} {0}s on you. Money: ${3}.\nEnter amount to sell or press enter to return.)",
                                      item.Name, saleValue, itemCount, Overworld.player.Money);

                    string decision = Console.ReadLine();
                    int amount;
                    bool validInput = Int32.TryParse(decision, out amount);

                    if (decision != "")
                        Console.WriteLine("");

                    //The player then gives input again as to how many of an item he wants to sll. If the player's input was correct, the operation continues.
                    if (validInput && amount > 0)
                    {
                        string formattedName = item.Name;

                        if (amount > 1)
                            formattedName += "s";

                        //If the player at least as many of the selected item as the given amount, that many of that item are removed from the player's bag.
                        if (item.Count >= amount)
                        {
                            item.Remove(amount, "sell");

                            Overworld.player.Money += (saleValue * amount);

                            Console.WriteLine("Alright, here you go, ${0} for {1} {2}!\nThank you, come again!\n", (item.Value * amount), amount, formattedName);
                        }

                        //Otherwise, the player is informed that he doesn't that many of that item.
                        else
                        {
                            Console.WriteLine("You don't have that many {0} on you.\n", formattedName);
                        }

                        Options();
                    }

                    else if (decision == "")
                    {
                        Options();
                    }

                    else
                    {
                        Console.WriteLine("Invalid input.\n");

                        Options();
                    }
                }

                else
                    Options();
            }

            else
            {
                Console.WriteLine("You don't have any items to sell!\n");

                Options();
            }
        }

        /// <summary>
        /// Asks the player to select an item from the mart's stock.
        /// </summary>
        /// <returns>Returns an item object if the player's input was valid, or null if it was not.</returns>
        Item DisplayStock()
        {
            //First, every item that the mart has in stock is
            for (int i = 0; i < stock.Count; i++)
                Console.WriteLine("{0} - {1}", (i + 1), stock.ElementAt(i).Name);

            Console.WriteLine("\nPlease select an item to purchase. (Valid input: 1-{0})", stock.Count);

            //Then, input is taken from the player.

            string input = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            if (input != "")
                Console.WriteLine("");

            //If the input is a number corresponding to an item in the mart's stock, that item gets selected.
            if (validInput && index > 0 && index <= stock.Count )
            {
                return stock.ElementAt(index - 1);
            }

            //If the player hit enter, he is returned back to the mart main screen.
            else if (input == "")
            {
                return null;
            }

            //If the input was smaller than 1, bigger than the mart's stock count or not a number, an error message is shown.
            else
            {
                Console.WriteLine("Invalid input.\n");

                return null;
            }
        }  
    }
}
