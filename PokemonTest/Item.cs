using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    [Serializable]
    public class Item
    {
        public string Name { get; set; } //The item's name.
        public string Type { get; set; } //The item's type - i.e., pokeball, potion, heal, etc.
        public string Description { get; set; } //A description of the item's purpose.

        public bool CanUseMultiple { get; set; } //This determines whether multiple of this item can be used at once.

        public int Value { get; set; } //The item's value when purchasing at a store.
        public int Count { get; set; } //How many instances of the item the player holds.

        public Item()
        {
            Name = "Sample Item";
            Type = "";
        }

        /// <summary>
        /// A constructor used for creating generic items. The type of item needs to be specified here.
        /// </summary>
        /// <param name="iName">The item's name.</param>
        /// <param name="iType">The item's type of use - i.e., pokeball, potion, cure, etc.</param>
        /// <param name="iDescription">A description of the item's purpose.</param>
        /// <param name="iMultiple">This determines whether multiple of this item can be used at once.</param>
        /// <param name="iValue">The item's value when purchasing at a store.</param>
        public Item(string iName, string iType, string iDescription, bool iMultiple, int iValue)
        {
            Name = iName;
            Type = iType;
            Description = iDescription;
            CanUseMultiple = iMultiple;
            Value = iValue;
        }

        /// <summary>
        /// A constructor for creating specific types of items, which will specify their type by themselves.
        /// </summary>
        /// <param name="iName">The item's name.</param>
        /// <param name="iDescription">A description of the item's purpose.</param>
        /// <param name="iMultiple">This determines whether multiple of this item can be used at once.</param>
        /// <param name="iValue">The item's value when purchasing at a store.</param>
        public Item(string iName, string iDescription, bool iMultiple, int iValue)
        {
            Name = iName;
            Description = iDescription;
            CanUseMultiple = iMultiple;
            Value = iValue;
        }

        public void Add(int quantity, string method)
        {
            //Code for adding an item to the player's bag, regardless of how it is to be obtained.

            //First the game searches whether there's an item in the player's inventory with the same name as this item. If so, a "quantity" amount of it is added.
            if (Overworld.player.items.Contains(Overworld.player.items.Find(i => i.Name == Name)))
                Overworld.player.items.Find(i => i.Name == Name).Count += quantity;
            
            //If an item with this item's name doesn't exist in the player's bag, this item gets added instead, with a starting count of "quantity".
            else
            {
                Overworld.player.items.Add(this);
                this.Count += quantity;
            }

            //A message is then printed depending on method of acquisition.
            if (method == "obtain")
                Console.WriteLine("You obtained {0}!", AddRemoveQuantityFormat(quantity));
        }

        public void Remove(int quantity, string method)
        {
            //Code for removing an item from the player's bag, regardless of how it is removed.

            //First the game searches whether there's an item in the player's inventory with the same name as this item. If so, a "quantity" amount of it is removed.
            if (Overworld.player.items.Contains(Overworld.player.items.Find(i => i.Name == Name)))
            {
                Overworld.player.items.Find(i => i.Name == Name).Count -= quantity;

                //If there are less than 1 of the item, it gets removed from the player's bag.
                if (Overworld.player.items.Find(i => i.Name == Name).Count < 1)
                    Overworld.player.items.Remove(Overworld.player.items.Find(i => i.Name == Name));
            }
            
            //This is just for the freak scenario where the game tries to remove an item from the player's bag that doesn't exist.
            //This should never happen ideally, but I've added this error message to facilitate for the case I ever make a silly mistake like that.
            else
            {
                Program.Log("Something went horribly wrong. The game tried to remove " + Name + " from the bag, but there were none.", 2);
                Console.WriteLine("Please contact the author, because something went horribly wrong. Also send him the log.txt file.");
            }

            /*A message is then printed depending on the way the item was removed.
            if (method == "use")
                Console.WriteLine("You used {0}!", AddRemoveQuantityFormat(quantity));
             */
        }

        public string AddRemoveQuantityFormat(int quantity)
        {
            //This method quickly formats the name of the item depending on the quantity to be added/removed.

            string countMessage;

            if (quantity == 1)
                return countMessage = "a " + Name;

            else
                return countMessage = quantity + " " + Name + "s";
        }

        public virtual bool Use()
        {
            Program.Log("The player tried to use a " + Name + " outside of combat.", 0);

            Console.WriteLine("This item cannot be used outside of combat.\n");

            return false;
        }

        public virtual bool UseCombat()
        {
            Program.Log("The player tried to use a " + Name + " during combat.", 0);

            Console.WriteLine("This item cannot be used during combat.\n");

            return false;
        }

        public string Print()
        {
            string countMessage = Name;

            if (Count > 1)
                countMessage = Name + "s";

            return Count + "x " + countMessage + " - " + Description;            
        }


    }
}
