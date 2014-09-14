using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokemonTextEdition.Properties;

namespace PokemonTextEdition
{
    class Mart
    {
        //The mart's "stock" - a list of items it can sell.
        List<Item> stock = new List<Item>();

        //The item that is currently being purchased/sold.
        Item item = new Item();        

        public void Welcome(List<Item> l)
        {
            //A generic greeting for all stores.
            Console.WriteLine("\nWelcome to the Pokemon Mart! We've got goods of all kinds! Take a look!");

            stock = l;

            Options();
        }

        void Options()
        {
            //The store's main menu. Here, the player selects whether he wants to buy, sell, or exit.

            Console.WriteLine("\nWhat would you like to do at the mart?\n(Available actions: buy, sell, exit)");

            string action = Console.ReadLine();

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
            }
        }

        void Buy()
        {
            //This method is called up when the player chooses to buy.

            Console.WriteLine("");
            Console.WriteLine("Of course! What are you after? We have the following items in stock:\n");

            //The player is asked to select an item.
            item = DisplayStock();

            //If the player selected a valid item, its properties are displayed, and the operation carries on.
            if (item.Name != "Sample Item")
            {
                int itemCount = 0;

                if (Overworld.player.items.Contains(Overworld.player.items.Find(i => i.Name == item.Name)))
                    itemCount = Overworld.player.items.Find(i => i.Name == item.Name).Count;

                Console.WriteLine("\n{0}s, huh? They're ${1} each. How many would you like to buy?\n(You have {2} {0}s on you. Money: ${3}.\nEnter amount to buy or press enter to return.)",
                                  item.Name, item.Value, itemCount, Overworld.player.Money);

                string decision = Console.ReadLine();
                int amount;
                bool validInput = Int32.TryParse(decision, out amount);

                //The player then gives input again as to how many of an item he wants to buy.
                if (validInput && amount > 0)
                {

                    //If the player's input was valid and he has enough money, that many of that item are added to the player's items list.
                    if (Overworld.player.Money >= (item.Value * amount))
                    {
                        string formattedName = item.Name;

                        if (amount > 1)
                            formattedName += "s";

                        item.Add(amount, "buy");

                        Overworld.player.Money -= (item.Value * amount);

                        Console.WriteLine("\nThere you go! {0} {1}, ${2} all in all!\nThank you, come again!", amount, formattedName, (item.Value * amount));
                    }

                    //Otherwise, the player is informed that he doesn't have enough money.
                    else
                    {
                        Console.WriteLine("It appears you don't have enough money!");
                    }

                    Options();
                }

                else if (decision == "")
                {
                    Options();
                }

                else
                {
                    Console.WriteLine("Invalid input");

                    Options();
                }
            }

            else
                Options();
        }

        void Sell()
        {
            //This method is called up when the player chooses to sell.

            Console.WriteLine("");
            Console.WriteLine("Selling? No problem! What would you like to sell?");

            //The player is asked to select an item.
            item = ItemSelection();

            //If the player selected a valid item, its properties are displayed, and the operation carries on.
            if (item.Name != "Sample Item")
            {
                int saleValue = (int)(item.Value / 2); //The item's price for selling - half the purchasing price.
                int itemCount = 0;

                if (Overworld.player.items.Contains(Overworld.player.items.Find(i => i.Name == item.Name)))
                    itemCount = Overworld.player.items.Find(i => i.Name == item.Name).Count;

                Console.WriteLine("\n{0}s, right? I can give you ${1} a piece. How many are you selling?\n(You have {2} {0}s on you. Money: ${3}.\nEnter amount to sell or press enter to return.)",
                                  item.Name, saleValue, itemCount, Overworld.player.Money);

                string decision = Console.ReadLine();
                int amount;
                bool validInput = Int32.TryParse(decision, out amount);

                Console.WriteLine("");

                //The player then gives input again as to how many of an item he wants to buy. If the player's input was correct, the operation continues.
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

                        Console.WriteLine("Alright, here you go, ${0} for {1} {2}!\nThank you, come again!", (item.Value * amount), amount, formattedName);
                    }

                    //Otherwise, the player is informed that he doesn't have enough money.
                    else
                    {
                        Console.WriteLine("You don't have that many {0} on you.", formattedName);
                    }

                    Options();
                }

                else if (decision == "")
                {
                    Options();
                }

                else
                {
                    Console.WriteLine("Invalid input.");

                    Options();
                }
            }

            else
                Options();
        }

        Item DisplayStock()
        {
            //This method is used when the player needs to select an item to buy from the mart's stock.

            for (int i = 0; i < stock.Count; i++)
                Console.WriteLine("{0} - {1}", (i + 1), stock.ElementAt(i).Name);

            Console.WriteLine("\nPlease select an item to purchase. (Valid input: 1-{0})", stock.Count);

            string input = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            //First, input is taken from the player. If the input is a number corresponding to an item in the mart's stock, that item gets selected.
            if (validInput && index > 0 && index < stock.Count )
            {
                return stock.ElementAt(index - 1);
            }

            //If the player hit enter, he is returned back to the mart main screen.
            else if (input == "")
            {
                return new Item();
            }

            //If the input was smaller than 1, bigger than the mart's stock count or not a number, an error message is shown.
            else
            {
                Console.WriteLine("Invalid input.");

                return new Item();
            }
        }

        Item ItemSelection()
        {
            //This method is used when the player needs to select an item to sell from his bag.

            Overworld.player.ListItems();

            Console.WriteLine("\nPlease select an item to sell. (Valid input: 1-{0})", Overworld.player.items.Count);

            string input = Console.ReadLine();
            int index;
            bool validInput = Int32.TryParse(input, out index);

            //First, input is taken from the player. If the input is a number corresponding to an item in the player's bag, that item gets selected.
            if (validInput && index > 0 && index < Overworld.player.items.Count)
            {
                return Overworld.player.items.ElementAt(index - 1);
            }

            //If the player hit enter, he is returned back to the mart main screen.
            else if (input == "")
            {
                return new Item();
            }

            //If the input was smaller than 1, bigger than the player's items count or not a number, an error message is shown.
            else
            {
                Console.WriteLine("Invalid input.");

                return new Item();
            }
        }

        
    }
}
