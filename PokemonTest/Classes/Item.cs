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

        #region Methods

        public string PrintInfo()
        {
            return this.Name + " - " + this.Description;
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
