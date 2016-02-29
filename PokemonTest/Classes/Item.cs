using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// The item's type - i.e., pokeball, potion, heal, etc.
    /// </summary>
    enum ItemType
    {
        None,
        Pokeball,
        Potion,
        StatusHeal,
        Misc,
        Key,
        Debug
    }

    /// <summary>
    /// The way in which the item was obtained - i.e. buy, obtain, create, etc.
    /// </summary>
    enum AddType
    {
        None,
        Obtain,
        Buy
    }

    /// <summary>
    /// The way in which the item was removed - i.e. sell, destroy, lose, etc.
    /// </summary>
    enum RemoveType
    {
        None,
        Use,
        Sell,
        Throw, //For Pokeballs
    }

    /// <summary>
    /// This class represents the various items that can be found within the game. By default, the class represents generic miscelaneous items, 
    /// and should be inherited by specific types of items, i.e. Potions and Pokeballs, which need to have different functionality. 
    /// </summary>
    class Item
    {
        #region Fields

        /// <summary>
        /// The item's unique ID number.
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// The item's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The item's type - i.e., pokeball, potion, etc.
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// A description of the item's purpose.
        /// <para>Example: "A tool for catching wild Pokemon."</para>
        /// </summary>
        public string Description { get; set; }

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
                             "The game tried to set the count of " + Name + " to " + value + ", which is less than 0.", 2);
                }

                else if (value > 99)
                {
                    count = 99;
                }

                else
                    count = value;
            }
        }

        protected int value;

        /// <summary>
        /// The item's value when purchasing at a store. Valid range: 0 or higher.
        /// </summary>
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value >= 0)
                    this.value = value;

                else
                    this.value = 0;
            }
        }

        /// <summary>
        /// Determines whether multiple of this item can be used at once.
        /// </summary>
        //public bool CanUseMultiple { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Blank item constructor. Creates an item named "Undefined Item" and sets every other attribute set to 0 and empty string.
        /// </summary>
        public Item()
        {
            ItemID = 0;

            Name = "Undefined Item";
            Type = ItemType.None;
            Description = string.Empty;

            Count = 0;
            Value = 0;

            //CanUseMultiple = false;            
        }

        /// <summary>
        /// Constructor for creating specific types of items, which will specify their type by themselves.
        /// <para>This constructor should only be used by classes that plan to inherit from Item, or should be chained from 
        /// another constructor that accepts the item's type as a parameter. For that reason, it is marked protected.</para>
        /// </summary>
        /// <param name="iID">The item's ID.</param>
        /// <param name="iName">The item's name.</param>
        /// <param name="iDescription">A description of the item's purpose.</param>
        /// <param name="iValue">The item's value when buying from a store.</param>
        protected Item(int iID, string iName, string iDescription, int iValue)
        {
            ItemID = iID;

            Name = iName;
            Description = iDescription;

            Value = iValue;

            //CanUseMultiple = iMultiple;            
        }

        /// <summary>
        /// Constructor for creating generic items. The type of item needs to be specified here.
        /// </summary>
        /// <param name="iID">The item's ID.</param>
        /// <param name="iName">The item's name.</param>
        /// <param name="iType">The item's type of use - i.e., pokeball, potion, cure, etc.</param>
        /// <param name="iDescription">A description of the item's purpose.</param>
        /// <param name="iMultiple">This determines whether multiple of this item can be used at once.</param>
        /// <param name="iValue">The item's value when purchasing at a store.</param>
        public Item(int iID, string iName, ItemType iType, string iDescription, bool iMultiple, int iValue) 
            : this(iID, iName, iDescription, iValue)
        {
            Type = iType;
        }        

        #endregion

        #region General Methods

        /// <summary>
        /// Displays the item's information among with the quantity presently in the bag.
        /// </summary>
        /// <returns>A formatted string listing the item's information. Example: "2x Potion - Heals a Pokemon"</returns>
        public string PrintInfo()
        {
            string countMessage = Name;

            if (Count > 1)
                countMessage = Name + "s";

            return Count + "x " + countMessage + " - " + Description;
        }

        /// <summary>
        /// This method represents the item being used. It needs to be overriden by usable items, as by default it shows an error for non-usable items.
        /// </summary>
        /// <returns>The result of trying to use the item - always false in this case.</returns>
        public virtual bool Use()
        {
            UI.WriteLine("This item cannot be used outside of combat.");

            Program.Log("The player tried to use a " + Name + " outside of combat.", 0);            

            return false;
        }

        /// <summary>
        /// This method represents the item being used during combat. It needs to be overriden by usable items, as by default it shows an error for non-usable items.
        /// </summary>
        /// <returns>The result of trying to use the item during combat - always false in this case.</returns>
        public virtual bool UseCombat()
        {
            UI.WriteLine("This item cannot be used during combat.");

            Program.Log("The player tried to use a " + Name + " during combat.", 0);            

            return false;
        }

        #endregion

        #region Adding & Removing        

        /// <summary>
        /// Attemps to add a quantity of an item to the player's bag, regardless of how it is added.
        /// </summary>
        /// <param name="quantity">The amount to add.</param>
        /// <param name="method">The method of acquisition of the item.</param>
        public void Add(int quantity, AddType method)
        {
            //First the game searches whether there's an item in the player's inventory with the same name as this item. If so, that many of the item are added.
            if (Overworld.Player.items.Exists(i => i.Equals(this)))
                Overworld.Player.items.Find(i => i.ItemID == ItemID).Count += quantity;
            
            //If an item with this item's name doesn't exist in the player's bag, this item gets added instead, with a starting count of "quantity".
            else
            {
                Overworld.Player.items.Add(this);
                Count += quantity;
            }

            //A message is then printed depending on method of acquisition.
            if (method == AddType.Obtain)
                UI.WriteLine("You obtained " + AddRemoveQuantityFormat(quantity) + "!\n");
        }

        /// <summary>
        /// Attemps to remove a quantity of an item from the player's bag, regardless of how it is removed.
        /// </summary>
        /// <param name="quantity">The amount to remove.</param>
        /// <param name="method">The method of acquisition of the item, i.e. "sold" or "used".</param>
        public void Remove(int quantity, RemoveType method)
        {
            //First the game checls wether the item exists in the player's inventory. If so, a "quantity" amount of it is removed.
            if (Overworld.Player.items.Exists(i => i.Equals(this)))
            {
                Overworld.Player.items.Find(i => i.ItemID == ItemID).Count -= quantity;

                //If there are less than 1 of the item, it gets removed from the player's bag.
                if (Overworld.Player.items.Find(i => i.ItemID == ItemID).Count < 1)
                    Overworld.Player.items.Remove(Overworld.Player.items.Find(i => i.ItemID == ItemID));
            }
            
            //This is just for the freak scenario where the game tries to remove an item from the player's bag that doesn't exist.
            //This should never happen ideally, but I've added this error message to facilitate for the case I ever make a silly mistake like that.
            else
            {
                UI.Error("The game tried to remove an item that does not exist in the player's bag.",
                         "The game tried to remove " + Name + " from the bag, but there were none.", 2);
            }
            
            if (method == RemoveType.Use)
                UI.WriteLine("You used " + AddRemoveQuantityFormat(quantity) + "!");
        }

        /// <summary>
        /// Quickly formats the name of the item depending on the quantity to be added/removed.
        /// </summary>
        /// <param name="quantity">The amount to be added or removed.</param>
        /// <returns>The item's name formatted by quantity. Example: "a Potion" or "5 Potions"</returns>
        protected string AddRemoveQuantityFormat(int quantity)
        {
            if (quantity == 1)
                return "a " + Name;

            else
                return quantity + " " + Name + "s";
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Checks if this item is the same as another.
        /// </summary>
        /// <param name="obj">Returns true if the ID of both items is the same.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Item i = obj as Item;

            if ((object)i == null)
                return false;

            return (ItemID == i.ItemID);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
