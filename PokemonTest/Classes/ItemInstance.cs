using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represents an instance of an item that a player will have in his inventory.
/// </summary>
namespace PokemonTextEdition.Classes
{
    class ItemInstance
    {
        #region Fields & Properties

        public Item BaseItem { get; set; }

        protected int count;

        /// <summary>
        /// Represents how many instances of the item the player holds.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value < 0)
                {
                    count = 0;

                    UI.Error("The game tried to set the count of an item to less than 0.",
                             "The game tried to set the count of " + BaseItem.Name + " to " + value + ", which is less than 0.", 2);
                }

                else if (value > 99)
                {
                    count = 99;
                }

                else
                    count = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructor for blank items. 
        ///  Creates an item of type PokeBall with a count of 1.
        /// </summary>
        public ItemInstance()
        {
            BaseItem = ItemList.AllItems.ElementAt(0);

            Count = 1;
        }

        protected ItemInstance(int iCount)
        {
            Count = iCount;
        }

        public ItemInstance(Item iItem, int iCount)
            : this(iCount)
        {
            BaseItem = iItem;           
        }


        public ItemInstance(string iName, int iCount)
            : this(iCount)
        {
            BaseItem = ItemList.AllItems.Find(i => i.Name == iName);
        }

        public ItemInstance(int iItemID, int iCount)
            : this(iCount)
        {
            BaseItem = ItemList.AllItems.Find(i => i.ItemID == iItemID);
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Displays the item's information among with the quantity presently in the bag.
        /// </summary>
        /// <returns>A formatted string listing the item's information. Example: "2x Potion - Heals a Pokemon"</returns>
        public string PrintInfo()
        {
            string countMessage = BaseItem.Name;

            if (Count > 1)
                countMessage += "s";

            return Count + "x " + countMessage + " - " + BaseItem.Description;
        }

        #endregion       
    }
}
