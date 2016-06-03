using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Classes
{
    class Mart
    {
        //The mart's "stock" - a list of items it can sell.
        List<Item> Stock = new List<Item>();

        public Mart(List<Item> stock)
        {
            //Then, the mart's stock is loaded.
            if (stock != null && stock.Count > 0)
                Stock = stock;

            //This is a failsafe in the event that a mart is loaded with no stock for whatever reason.
            else
                Stock = new List<Item> { ItemList.potion };
        }

        /// <summary>
        /// The "start" method of the mart.
        /// </summary>
        /// <param name="martStock">The mart's stock. This should be provided by each specific location.</param>
        public void Welcome()
        {
            //A generic greeting for all stores is displayed.
            UI.WriteLine("Welcome to the Pokemon Mart! We've got goods of all kinds! Take a look!\n");            

            //The player is taken to the options screen, where he may decide what to do.
            Options();
        }

        /// <summary>
        /// This method is effectively the store's main menu. Here, the player decides whether he wants to buy, sell or exit.
        /// </summary>
        void Options()
        {
            UI.WriteLine("What would you like to do at the mart?\n(Available actions: (b)uy, (s)ell, (e)xit)");

            //Input is requested of the player.
            string action = UI.ReceiveInput();

            //Then, the respective method is triggered.
            switch (action.ToLower())
            {
                case "buy":
                case "b":

                    Buy();

                    break;

                case "sell":
                case "s":

                    Sell();

                    break;
                    
                case "exit":
                case "e":
                    break;

                default:

                    UI.InvalidInput();

                    Options();

                    break;
            }
        }

        /// <summary>
        /// This method handles the player purchasing items at the mart.
        /// </summary>
        void Buy()
        {
            UI.WriteLine("Of course! What are you after? We have the following items in stock:\n");

            //The player is asked to select an item.
            Item itemToBuy = DisplayStock();

            //If the player selected a valid item, its properties are displayed, and the operation carries on.
            if (itemToBuy != null)
            {
                int itemCount = 0;

                //This part searches for the selected item in the player's bag and returns how many of the item the player has.
                if (Game.Player.Bag.Contains(Game.Player.Bag.Find(i => i.BaseItem.ItemID == itemToBuy.ItemID)))
                    itemCount = Game.Player.Bag.Find(i => i.BaseItem.ItemID == itemToBuy.ItemID).Count; 

                UI.WriteLine(itemToBuy.Name + "s, huh? They're $" + itemToBuy.Value + " each. How many would you like to buy?" +
                            "\n(You have " + itemCount + " " + itemToBuy.Name + "s on you. Money: $" + Game.Player.Money +
                            "\nEnter amount to buy or press enter to return.)");

                //Next, the player is asked to provide input as to how many of the selected item to purchase.
                string decision = UI.ReceiveInput();
                int amount;
                bool validInput = Int32.TryParse(decision, out amount);

                //If the player's input was valid and the amount that he selected was at least 1...
                if (validInput && amount > 0)
                {
                    //... If he has enough money, that many of that item are added to the player's items list.
                    if (Game.Player.Money >= (itemToBuy.Value * amount))
                    {
                        string formattedName = itemToBuy.Name;

                        if (amount > 1)
                            formattedName += "s";
                        // [FIX]
                        //Item.Add(amount, AddType.Buy);

                        Game.Player.Money -= (itemToBuy.Value * amount);

                        UI.WriteLine("There you go! " + amount + " " + formattedName + ", $" + (itemToBuy.Value * amount) + " all in all!\nThank you, come again!\n");
                    }

                    //Otherwise, the player is informed that he doesn't have enough money.
                    else
                    {
                        UI.WriteLine("It appears you don't have enough money!\n");
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
                    UI.InvalidInput();

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
            if (Game.Player.Bag.Count > 0)
            {
                UI.WriteLine("Selling? No problem! What would you like to sell?\n");

                //If so, he is asked to select an item from his bag.
                ItemInstance itemToSell = Game.Player.SelectItem("sell");

                //If the player selected a valid item, its properties are displayed, and the operation carries on.
                if (itemToSell != null)
                {
                    int saleValue = (int)(itemToSell.BaseItem.Value / 2); //The item's price for selling - half the purchasing price.
                    int itemCount = itemToSell.Count;

                    UI.WriteLine(itemToSell.BaseItem.Name + "s, right? I can give you $" + saleValue + " a piece. How many are you selling?" +
                                     "\n(You have " + itemCount + " " + itemToSell.BaseItem.Name + "s on you. Money: $" + Game.Player.Money + 
                                     "\nEnter amount to sell or press enter to return.)");

                    string decision = UI.ReceiveInput();
                    int amount;
                    bool validInput = Int32.TryParse(decision, out amount);

                    //The player then gives input again as to how many of an item he wants to sll. If the player's input was correct, the operation continues.
                    if (validInput && amount > 0)
                    {
                        string formattedName = itemToSell.BaseItem.Name;

                        if (amount > 1)
                            formattedName += "s";

                        //If the player at least as many of the selected item as the given amount, that many of that item are removed from the player's bag.
                        if (itemToSell.Count >= amount)
                        {
                            Game.Player.Remove(itemToSell.BaseItem, amount, RemoveType.Sell);

                            Game.Player.Money += (saleValue * amount);

                            UI.WriteLine("Alright, here you go, $" + (itemToSell.BaseItem.Value * amount) + " for " + amount + " " + formattedName + "!\nThank you, come again!\n");
                        }

                        //Otherwise, the player is informed that he doesn't that many of that item.
                        else
                        {
                            UI.WriteLine("You don't have that many " + formattedName + " on you.\n");
                        }

                        Options();
                    }

                    else if (decision == "")
                    {
                        Options();
                    }

                    else
                    {
                        UI.InvalidInput();

                        Options();
                    }
                }

                else
                    Options();
            }

            else
            {
                UI.WriteLine("You don't have any items to sell!\n");

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
            for (int i = 0; i < Stock.Count; i++)
                UI.WriteLine((i + 1) + " - " + Stock.ElementAt(i).Name);

            UI.WriteLine("\nPlease select an item to purchase. (Valid input: 1-" + Stock.Count + ")");

            //Then, input is taken from the player.

            string input = UI.ReceiveInput();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            //If the input is a number corresponding to an item in the mart's stock, that item gets selected.
            if (validInput && index > 0 && index <= Stock.Count )
            {
                return Stock.ElementAt(index - 1);
            }

            //If the player hit enter, he is returned back to the mart main screen.
            else if (input == "")
            {
                return null;
            }

            //If the input was smaller than 1, bigger than the mart's stock count or not a number, an error message is shown.
            else
            {
                UI.InvalidInput();

                return null;
            }
        }  
    }
}
