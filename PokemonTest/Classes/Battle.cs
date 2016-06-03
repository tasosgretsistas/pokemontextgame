using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;
using PokemonTextEdition.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonTextEdition.Classes
{
    /// <summary>
    /// The type of encounter of a battle, i.e. Wild or Trainer.
    /// </summary>
    enum BattleType
    {
        None,
        Wild,
        Trainer,
        Gym,
        Special,
        Multiplayer
    }

    /// <summary>
    /// The various types of actions that a player or the AI can choose between during a battle, ie.. "Fight" or "Run".
    /// </summary>
    enum BattleAction
    {
        None,
        Fight,
        Status,
        Switch,
        Item,
        Catch,
        Run
    }

    /// <summary>
    /// This class represents the various battles that can take place between the player and enemy trainers, enemy Pokemon or other players (NYI).
    /// Each type of battle has its own constructor and as such it is important that objects of this class are instantiated by the appropriate constructor.
    /// </summary>
    class Battle
    {
        #region Fields

        #region Battle Properties

        //These are the crucial attributes of the Battle class.    

        /// <summary>
        /// Determines the type of the encounter, i.e. EncounterType.Wild or EncounterType.Trainer.
        /// </summary>
        BattleType EncounterType { get; set; }

        /// <summary>
        /// Determines whether the battle is still ongoing or not.
        /// </summary>
        bool BattleOver { get; set; }

        /// <summary>
        /// Determines if the player won or lost the battle.
        /// This is marked public as the outcome of a battle is accessed by the trainer class to determine what happens after the battle.
        /// </summary>
        public bool PlayerVictory { get; set; }

        /// <summary>
        /// An index number that represents the index number of the AI's Pokemon it its party. 
        /// Used when the AI needs to switch Pokemon, at which case it gets incremented by 1.
        /// </summary>
        int AICurrentPokemonIndex { get; set; }

        /// <summary>
        /// Multiplier used to calculate how much damage a move that deals extra damage when used consecutively will deal.
        /// This should start at 1 (regular damage) and should be set to 2 (double damage) in such event.
        /// </summary>
        float SameMoveDamageBonus { get; set; }

        /// <summary>
        /// Counts how many times the player has attempted to escape from combat, and then multiplies the chance that a player's escape
        /// attempt will be succesful by that amount. For that reason, this should start at 1.
        /// </summary>
        int EscapeAttempts { get; set; }

        #endregion

        #region Pointers & States

        //These are only reference pointers to objects, which make it easier to handle combat.

        /// <summary>
        /// Points to the player object.
        /// This is marked public so that the trainer class knows which player the trainer did battle with.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Points to the enemy trainer object.
        /// </summary>
        Trainer EnemyTrainer { get; set; }

        /// <summary>
        /// The action that the player selected for his turn.
        /// </summary>
        BattleAction PlayerAction { get; set; }

        /// <summary>
        /// The action that the enemy selected for his turn.
        /// </summary>
        BattleAction EnemyAction { get; set; }

        /// <summary>
        /// Points to the player's currently active Pokemon.
        /// </summary>
        Pokemon PlayerPokemon { get; set; }

        /// <summary>
        /// Points to the enemy's currently active Pokemon.
        /// </summary>
        Pokemon EnemyPokemon { get; set; }

        /// <summary>
        /// Points to the player's selected move.
        /// </summary>
        Move PlayerMove { get; set; }

        /// <summary>
        /// Points to the enemy's selected move.
        /// </summary>
        Move EnemyMove { get; set; }

        /// <summary>
        /// Points to whichever move the player used last. 
        /// This is used for example for moves that do additional damage when used consecutively, or for checking that Protect isn't used consecutively.
        /// </summary>
        Move PlayerPreviousMove { get; set; }

        /// <summary>
        /// Points to whichever move the enemy used last. 
        /// This is used for example for moves that do additional damage when used consecutively, or for checking that Protect isn't used consecutively.
        /// </summary>
        Move EnemyPreviousMove { get; set; }

        /// <summary>
        /// Points to whichever Pokemon is attacking.
        /// </summary>
        Pokemon AttackingPokemon { get; set; }

        /// <summary>
        /// Points to whichever Pokemon is defending.
        /// </summary>
        Pokemon DefendingPokemon { get; set; }

        /// <summary>
        /// Points to which move is being used by the currently attacking Pokemon.
        /// </summary>
        Move CurrentMove { get; set; }

        /// <summary>
        /// A list of the player's Pokemon that participated in a battle against a particular Pokemon, used for awarding experience to all participating Pokemon.
        /// </summary>
        List<Pokemon> Participants { get; set; }

        #endregion

        #region Message Formatting

        //These fields are simply used to format the various messages that will be displayed to the player and 
        //as such have no functionality by themselves.

        //The formatted name of the enemy in the battle, used for logging purposes.
        //Examples: "Pokemon Trainer Blue" or "Enemy Pidgey".        
        //string EnemyName { get; set; }  

        /// <summary>
        /// A string formatted by which Pokemon is attacking, used for adjusting displayed messages.
        /// For example, this would be "Bulbasaur" if the player is attacking, or "The enemy Charmander" if the enemy is attacking.
        /// </summary>
        string AttackingPokemonName { get; set; }

        /// <summary>
        /// A string formatted by which Pokemon is defending, used for adjusting displayed messages.
        /// For example, this would be "Bulbasaur" if the player is defending, or "The enemy Charmander" if the enemy is defending.
        /// </summary>
        string DefendingPokemonName { get; set; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// This isn't a constructor inasmuch as that it should not be used to instantiate a Battle object. For that reason, it is set to protected accessiblity.
        /// <para>Rather, this contains a set of operations that all types of battle have to begin with, and as such all other constructors chain to this one.</para>
        /// </summary>
        protected Battle()
        {
            Player = Game.Player;

            PlayerAction = BattleAction.None;
            EnemyAction = BattleAction.None;

            BattleOver = false;

            AICurrentPokemonIndex = 0;
            SameMoveDamageBonus = 1;
            EscapeAttempts = 1;

            Participants = new List<Pokemon>();
        }

        /// <summary>
        /// Constructor for battles between the player and enemy trainers, which have a team of Pokemon.
        /// </summary>
        /// <param name="trainer">The enemy trainer's object.</param>
        public Battle(Trainer trainer)
            : this()
        {
            Program.Log("Battle against a trainer starts.", 1);

            EncounterType = BattleType.Trainer;
            EnemyTrainer = trainer;

            UI.WriteLine(trainer.DisplayName + " wants to battle!");

            AIChangePokemon(trainer.Party.ElementAt(AICurrentPokemonIndex));

            PokemonSelector();

            StartTurn();
        }

        /// <summary>
        /// Constructor for battles between the player and wild Pokemon, which fight by themselves.
        /// </summary>
        /// <param name="pokemon">The enemy Pokemon's object.</param>
        public Battle(Pokemon pokemon)
            : this()
        {
            Program.Log("Battle against a wild Pokemon starts.", 1);

            EncounterType = BattleType.Wild;

            UI.WriteLine("A wild level " + pokemon.Level + " " + pokemon.Name + " appeared!\n");

            AIChangePokemon(pokemon);

            PokemonSelector();

            StartTurn();
        }

        /// <summary>
        /// Constructor for battles between the player and special wild Pokemon, such as Mewtwo, which are not ordinary wild Pokemon fights.
        /// Special BattleType enum choices are reserved for such battles.
        /// </summary>
        /// <param name="pokemon">The enemy Pokemon's object.</param>
        /// <param name="battleType">The BattleType enum of the battle - effectively, its unique encounter type.</param>
        public Battle(Pokemon pokemon, BattleType battleType)
            : this(pokemon)
        {
            EncounterType = battleType;
        }

        #endregion

        #region Battle Engine

        /// <summary>
        /// Handles the events that need to take place at the start of the turn and subsequently passes control to the TurnOrder method, if the battle is ongoing.
        /// </summary>
        void StartTurn()
        {
            //The entirety of the combat sequence is wrapped in a while loop since combat is ongoing until the value of BattleOver is false.
            while (!BattleOver)
            {
                //If the player has not selected an action, he is taken to the Actions menu, where he may select an action.
                if (PlayerAction == BattleAction.None)
                    Actions();

                //Otherwise, if the player has selected, the order of actions in the turn will be determined next.
                else
                    TurnOrder();
            }
        }

        /// <summary>
        /// Determines the order of events that will take place in the turn and then performs them.
        /// </summary>
        void TurnOrder()
        {
            //First, the AI selects an action.
            AIAction();

            //Then, if the player chose to fight, the order of combat is established.
            if (PlayerAction == BattleAction.Fight)
                CombatOrder();

            //If the player chose not to fight, the AI will attack by itself.
            else
                AIAttack();

            //Finally, the end of turn effects take place.   
            EndTurn();

            Program.Log("The turn has ended. Returning to Actions.", 0);
        }

        /// <summary>
        /// Determines who will attack first between the player and the enemy, and then has them attack.
        /// </summary>
        void CombatOrder()
        {
            Program.Log("The game goes into Speed Priority calculation.", 0);

            //The game determines which is faster between the player's Pokemon and the enemy Pokemon.
            Pokemon attacker = SpeedCheck();

            //If the player's Pokemon was faster, it attacks first.
            if (attacker == PlayerPokemon)
            {
                Program.Log("The player's Pokemon is faster, so it attacks first.", 0);

                PlayerAttack();
                AIAttack();
            }

            else if (attacker == EnemyPokemon)
            {
                Program.Log("The enemy Pokemon is faster, so it attacks first.", 0);

                AIAttack();
                PlayerAttack();
            }
        }

        /// <summary>
        /// Determines which is faster between the player's Pokemon and the enemy Pokemon, including their move's priority level. 
        /// Has a failsafe in case of a speed tie.
        /// </summary>
        /// <returns>Whichever Pokemon is faster between the player's and the enemy's.</returns>
        Pokemon SpeedCheck()
        {
            if (PlayerMove.Priority > EnemyMove.Priority || 
                (PlayerPokemon.EffectiveSpeed > EnemyPokemon.EffectiveSpeed && PlayerMove.Priority == EnemyMove.Priority))
            {
                return PlayerPokemon;
            }

            else if (EnemyMove.Priority > PlayerMove.Priority || 
                (EnemyPokemon.EffectiveSpeed > PlayerPokemon.EffectiveSpeed && EnemyMove.Priority == PlayerMove.Priority))
            {
                return EnemyPokemon;
            }

            else
            {
                Program.Log("There was a speed tie between the two Pokemon, which will be resolved using a random number generator.", 1);

                if (Program.random.Next(0, 2) == 0)
                    return PlayerPokemon;

                else
                    return EnemyPokemon;
            }
        }

        /// <summary>
        /// Resolves any effects that take place at the end of the turn, and resets the turn-related parameters to their default state.
        /// </summary>
        void EndTurn()
        {
            Program.Log("End of turn resolution takes place.", 0);

            //The action of both the player and the enemy return to none.
            PlayerAction = BattleAction.None;
            EnemyAction = BattleAction.None;

            PlayerMove = null;
            EnemyMove = null;

            AttackingPokemon = null;
            DefendingPokemon = null;
            CurrentMove = null;

            //The moves used by both parties become the previously used move.
            PlayerPreviousMove = PlayerMove;
            EnemyPreviousMove = EnemyMove;

            //Both Pokemon lose the Protect status, as it only lasts 1 turn.
            PlayerPokemon.Protect = false;
            EnemyPokemon.Protect = false;

            ResidualDamage(EnemyPokemon);
            ResidualDamage(PlayerPokemon);

            LeechSeed(EnemyPokemon, PlayerPokemon);
            LeechSeed(PlayerPokemon, EnemyPokemon);
        }

        #endregion

        #region Player Actions       

        /// <summary>
        /// Presents the player with the choices that he has while battling.
        /// </summary>
        void Actions()
        {
            Program.Log("The player is taken to the Actions menu.", 0);

            UI.WriteLine("What will you do?\n(Available commands: (f)ight, (s)tatus, (i)item, s(w)itch, (c)atch, (r)un)");

            //First, input is received from the player.
            string action = UI.ReceiveInput();

            //Then, the action corresponding to the player's input takes place.
            switch (action.ToLower())
            {
                case "fight":
                case "f":
                    Fight();
                    break;

                case "status":
                case "s":
                    Status();
                    break;

                case "item":
                case "i":
                    Item();
                    break;

                case "switch":
                case "w":
                    Switch();
                    break;

                case "catch":
                case "c":
                    Catch();
                    break;

                case "run":
                case "r":
                    Run();
                    break;

                default:
                    Program.Log("The player input an invalid command at the Actions screen.", 0);

                    UI.InvalidInput();
                    break;
            }
        }

        /// <summary>
        /// Handles the events that need to take place when the player chooses to fight. Here, the player selects a move.
        /// </summary>
        void Fight()
        {
            Program.Log("The player chooses to fight.", 0);

            //First, the game checks if the player's Pokemon is locked into a move. If it is not, the player selects the move he wants to use.
            if (!PlayerPokemon.MoveLock)
            {
                UI.WriteLine("Please select a move. (Valid input: 1-" + PlayerPokemon.knownMoves.Count + ", or press Enter to return.)\n");

                //The player is asked to select a move.
                Move move = PlayerPokemon.SelectMove(false);

                //If the player made a valid selection...
                if (move != null)
                {
                    Program.Log("The player selected the move " + move.Name + ".", 0);

                    //That move becomes the Pokemon's selected move.
                    PlayerMove = move;

                    //Finally, the player's action is set to Fight.
                    PlayerAction = BattleAction.Fight;
                }

                //Otherwise, an error message is displayed, and the player is taken back to the StartTurn menu.
                else
                    Program.Log("The player chose an invalid move. Returning to Actions.", 0);
            }

            //If the player's Pokemon is locked into a move, it will automatically try to use that move.
            else
            {
                //First, the game checks whether the Pokemon knows the move it previously used, as a failsafe.
                if (PlayerPokemon.knownMoves.Exists(m => m.MoveID == PlayerPreviousMove.MoveID))
                {
                    Program.Log("The player's Pokemon is movelocked and automatically uses the move " + PlayerPreviousMove.Name + ".", 0);

                    //Then, that move becomes the Pokemon's selected move.
                    PlayerMove = PlayerPokemon.knownMoves.Find(m => m.MoveID == PlayerPreviousMove.MoveID);

                    //Finally, the player's action is set to Fight.
                    PlayerAction = BattleAction.Fight;
                }
            }
        }

        /// <summary>
        /// Displays a very simple status screen that shows the current status of the player's Pokemon as well as the current enemy Pokemon.
        /// </summary>
        void Status()
        {
            Program.Log("The player views the Status screen.", 0);

            UI.WriteLine("Your Pokemon:\n" +
                        PlayerPokemon.PrintInfo() + "\n");

            if (EncounterType == BattleType.Trainer)
                UI.Write("Your opponent's Pokemon: ");

            else if (EncounterType == BattleType.Wild)
                UI.Write("The wild Pokemon: ");

            UI.WriteLine(EnemyPokemon.PrintInfoBrief() + "\n");
        }

        /// <summary>
        /// Handles the events that need to take place when the player chooses to use an item during the battle,
        /// and then invokes the player's UseItemsCombat() method.
        /// </summary>
        void Item()
        {
            //If the player succesfully uses an item, his action is set to Item. 
            if (Player.UseItemsCombat())
                PlayerAction = BattleAction.Item;
        }

        /// <summary>
        /// Handles the events that need to take place when the player chooses to switch his active Pokemon.
        /// </summary>
        void Switch()
        {
            Program.Log("The player chooses to switch Pokemon.", 0);

            UI.WriteLine("Send out which Pokemon?\n(Valid input: 1-" + Player.Party.Count + ", or press Enter to return.)\n");

            //The player is asked to select a Pokemon from his party.
            Pokemon pokemon = Player.SelectPokemon(false);

            //If the player made a valid selection...
            if (pokemon != null)
            {
                //If the Pokemon the user selected is healthy and is not already the active Pokemon...
                if (pokemon != PlayerPokemon && !pokemon.Fainted)
                {
                    Program.Log("The player switches " + PlayerPokemon.Name + " out for " + pokemon.Name + ". The AI will now attack.", 1);

                    //The game resets the temporary effects on the currently active Pokemon that do not persist through switching.
                    PlayerPokemon.ResetTemporaryEffects(false);

                    UI.WriteLine("Good job, " + PlayerPokemon.Name + "! You deserve some rest." +
                                 PlayerPokemon.Name + " was withdrawn back into its Pokeball.");

                    //Then, the player that the Pokemon selected is sent out.
                    PlayerChangePokemon(pokemon);

                    //Finally, the player's action is set to Switch.
                    PlayerAction = BattleAction.Switch;
                }

                else if (pokemon.Fainted)
                {
                    UI.WriteLine("That Pokemon has fainted! It cannot battle!\n");

                    Program.Log("The player chose to switch to a fainted Pokemon. Returning to Actions.", 0);
                }

                else if (pokemon == PlayerPokemon)
                {
                    UI.WriteLine("That Pokemon is already the active Pokemon!\n");

                    Program.Log("The player chose to switch to the Pokemon that's already active. Returning to Actions.", 0);
                }
            }
        }

        #endregion

        #region Special Actions

        /// <summary>
        /// This code handles escaping from battle.
        /// </summary>
        void Run()
        {
            Program.Log("The player chose to run away.", 0);

            //First, the game examines whether it is currently in a wild fight. If so, the operation continues.
            if (EncounterType == BattleType.Wild)
            {
                PlayerAction = BattleAction.Run;

                PlayerPokemon.MoveLock = false;

                //This equation determines how probable it will be for the player the escape. It takes into account the player's 
                //Pokemon's speed, the enemy Pokemon's speed, and the amount of times that the player has attempted to escape.
                //A higher number translates into a higher probability of escaping.
                int escapeFactor = (((PlayerPokemon.Speed * 32) / (EnemyPokemon.Speed / 4)) + 30) * EscapeAttempts;

                //Then, a random number between 0 and 255 is rolled.
                int chanceToEscape = Program.random.Next(0, 256);

                //If the player's escape factor is higher than 255 or than the chanceToEscape number, the player succesfully escapes.
                if (escapeFactor > 255 || escapeFactor > chanceToEscape)
                {
                    UI.WriteLine("Escaped succesfully!\n");

                    Program.Log("The player successfully escaped with a chance of " + (escapeFactor / chanceToEscape * 100) +
                                "%. (Factor = " + escapeFactor + ", Roll = " + chanceToEscape + ")", 1);

                    BattleFinished();
                }

                //If the player is unsuccesful at escaping, the number of attempted escapess incremented by 1.
                else
                {
                    EscapeAttempts++;

                    UI.WriteLine("Couldn't run away!\n");

                    Program.Log("The player was unsuccesful at escaping, so the AI will now attack.", 1);
                }
            }

            //If not, an error is shown and the player is taken back to the StartTurn screen.
            else
            {
                UI.WriteLine("You can't run away from a trainer fight!\n");

                Program.Log("The player tried to escape from a trainer battle. Returning to Actions.", 0);
            }
        }

        /// <summary>
        /// This code handles catching wild Pokemon.
        /// </summary>
        void Catch()
        {
            Program.Log("The player chose to attempt to catch the Pokemon.", 0);

            //First, the program examines whether the player is currently fighting a wild Pokemon.
            if (EncounterType == BattleType.Wild)
            {
                //Then, it checks to see if the player has any remaining Poke Balls. If he does, one of them is used up.
                if (Player.Bag.Contains(Player.Bag.Find(i => i.BaseItem.Type == ItemType.Pokeball)))
                {
                    ItemInstance pokeball = Player.Bag.Find(i => i.BaseItem.Type == ItemType.Pokeball);

                    PlayerAction = BattleAction.Catch;

                    PlayerPokemon.MoveLock = false;

                    Player.Remove(pokeball.BaseItem, 1, RemoveType.Throw);
                    
                    UI.WriteLine("Threw a Poke Ball (" + pokeball.Count + " left) at the wild " + EnemyPokemon.Name + "! 1, 2, 3...");

                    //The enemy Pokemon's life percentage.
                    float life = (float)EnemyPokemon.PercentLife();

                    //Each specific PokeBall's catch rate multiplier. (NYI)
                    float ballBonus = 1; 

                    //First, the chance to catch the Pokemon is calculated, using its current HP %, its individual catch rate, and the bonus multiplier from the PokeBall used.
                    //The result is a number ranging from 13 to 79, times ballBonus and divided by pokemon.CatchRate. This number expresses the % chance to catch the Pokemon.
                    float catchRate = ((100 - life) + ((life - 60) / 3f)) * ballBonus / EnemyPokemon.species.CatchRate;

                    //Then, a random number between 0 and 100 is rolled.
                    int chance = Program.random.Next(1, 101);

                    //If the randomly generated number is smaller than the calculated catch rate, the Pokemon is caught.
                    if (catchRate > chance)
                    {
                        Program.Log("The player successfully caught " + EnemyPokemon.Name + " with a chance of " + catchRate  + "%. (Roll = " + chance + ")", 1);

                        UI.WriteLine("Gotcha! The wild " + EnemyPokemon.Name + " was caught!\n");

                        Player.AddPokemon(EnemyPokemon, true);

                        BattleFinished();
                    }

                    else
                    {
                        UI.WriteLine("Oh no! The wild " + EnemyPokemon.Name + " broke free!\n");

                        Program.Log("The player was unsuccesful at capturing the wild Pokemon, so the AI will now attack.", 1);
                    }
                }

                else
                {
                    UI.WriteLine("You don't have any PokeBalls in your bag!\n");

                    Program.Log("The player had no PokeBalls. Returning to Actions.", 0);
                }
            }

            //If the player is not fighting a wild Pokemon, an error is shown and the player is taken back to the menu.
            else
            {
                UI.WriteLine("You can't throw a PokeBall at a trainer's Pokemon!");

                Program.Log("The player tried to use a PokeBall during a trainer battle. Returning to Actions.", 0);
            }
        }

        #endregion

        #region Combat        

        /// <summary>
        /// Handles the events that need to take place when a Pokemon would attack another, including determing if the Pokemon will succesfully
        /// attack after being Paralyzed or Asleep, determing if the defending Pokemon would be immune to the attack, and determing if the
        /// attacking Pokemon's attack will be a hit or a miss.
        /// </summary>
        void CombatStart()
        {
            //If the player won't be prevented from attacking due to paralysis or sleep...
            if (StatusChecks())
            {
                //It attempts to use a move.
                UI.WriteLine(AttackingPokemonName + " used " + CurrentMove.Name + "!");

                //If the defending Pokemon has used Protect, combat ends here.
                if (DefendingPokemon.Protect)
                    UI.WriteLine(DefendingPokemonName + " was protected from the attack!");

                else
                {
                    //The game then checks the Type chart to see how effective the attack will be on the defending Pokemon.
                    float typeMod = TypeChart.Check(CurrentMove.Type, DefendingPokemon.species.Type1, DefendingPokemon.species.Type2);

                    //If the defending Pokemon would be immune to the attack (the Type chart would return 0), combat ends here.
                    if (typeMod == 0)
                        UI.WriteLine(DefendingPokemonName + " was immune to the attack!");

                    //Else, if the Pokemon's attack does not miss, the attack goes through.
                    else if (HitCheck())
                    {
                        Program.Log(AttackingPokemonName + " attacks " + DefendingPokemon.Name +
                            " (" + DefendingPokemon.CurrentHP + "/" + DefendingPokemon.MaxHP + " HP) with " + CurrentMove.Name + ".", 1);

                        //If the attacking Pokemon move's attribute is Status, it means that it won't do damage, so the game goes into effect resolution directly.
                        if (CurrentMove.Attribute == MoveAttribute.Status)
                        {
                            Program.Log("The attack does not deal damage, so effect resolution will take place.", 0);

                            Effect();
                        }

                        //Otherwise, the move does damage, so the game goes into damage calculations.
                        else
                        {
                            Program.Log("The attack deals damage, so damage calculation will take place.", 0);

                            DamageCalculation(typeMod);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the damage that would be dealt by a damaging attack.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="typeModifier"></param>
        void DamageCalculation(float typeModifier)
        {
            //The total damage inflicted to the defending Pokemon after all calculations are done. 
            float damage = 0;

            //This is used to accurately calculate how much damage the defending Pokemon took. 
            int previousHP = DefendingPokemon.CurrentHP;

            //An all-purpose multiplier for the final damage calculation.
            float damageMultiplier = 1;

            //The relational modifier between the attack's type and the defending Pokemon's type(s).  
            float typeMod = typeModifier;

            //The number of times an attack will strike, for multi-hit attacks.
            int numberOfHits = 1, hits = 1;

            //This string is used for logging, in order to determine whether moves do the correct amount of damage.
            string damageText = "";

            //The next part prints a message depending the result of the TypeChart calculation.
            // x2 or x4 would be a super effective attack, while x1/2 or x1/4 would be a not effective attack.
            if (typeMod == 2 || typeMod == 4)
                UI.WriteLine("It's super effective!");

            else if (typeMod == 0.5f || typeMod == 0.25f)
                UI.WriteLine("It's not very effective!");

            if (typeMod != 1f)
                damageText += " * " + typeMod + " Type";

            //Multi-hit attack calculation.
            if (CurrentMove.Effect == MoveEffect.MultipleHits)
            {
                //If the attack is a 2-hit attack, it will hit twice.
                if (CurrentMove.EffectCoefficient == 2)
                    numberOfHits = 2;

                //Otherwise the game will go into the process of calculating how many times it will hit, using RNG.
                else
                {
                    // 10% 5, 10% 4, 40% 3, 40% 2

                    int hitsRNG = Program.random.Next(1, 101);

                    if (CurrentMove.EffectCoefficient == 5 && hitsRNG <= 40)
                        numberOfHits = 5;

                    else if (CurrentMove.EffectCoefficient == 5 && hitsRNG > 80)
                        numberOfHits = 4;

                    else if (CurrentMove.EffectCoefficient == 5 && hitsRNG > 40)
                        numberOfHits = 3;

                    else
                        numberOfHits = 2;
                }

                hits = numberOfHits;

                Program.Log(CurrentMove.Name + " hit " + hits + " times.", 1);
            }

            if (CurrentMove.Effect == MoveEffect.SetDamage)
                damage = CurrentMove.EffectCoefficient; //If the selected move does a set amount of damage, the game skips damage calculation.

            else
            {
                do //This is a do-while loop to facilitate for the multiple hits scenario.
                {
                    //Same-Type Attack Bonus check. If the Pokemon's attack is the same type as the Pokemon itself, it receives a 50% damage boost.
                    if (AttackingPokemon.TypeCheck(CurrentMove.Type))
                    {
                        damageText += " * 1.5 STAB";

                        damageMultiplier *= 1.5f;
                    }

                    //Next the game calculates if the attack will be a critical hit.
                    int critical = Program.random.Next(1, 101);

                    //If the randomly rolled number is smaller than the probability of a critical hit, the attack is a critical hit.
                    if (critical <= Settings.CriticalHitChance ||
                        CurrentMove.Effect == MoveEffect.IncreasedCritChance && critical <= (Settings.CriticalHitChance * CurrentMove.EffectCoefficient))
                    {
                        Program.Log("The attack was a critical hit.", 0);

                        UI.WriteLine("A critical hit!");

                        damageText += " * 1.2 Crit";

                        damageMultiplier *= Settings.CriticalHitDamageMultiplier;
                    }

                    //This code simply makes wild Pokemon do 20% less damage. But one day, they'll turn against us!
                    if (AttackingPokemon == EnemyPokemon && EncounterType == BattleType.Wild)
                    {
                        damageText += " * 0.8 Wild";

                        damageMultiplier *= 0.8f;
                    }

                    //If the attacking Pokemon is burned, the status modifier for physical attacks is set to 0.5.
                    if (AttackingPokemon.Status == StatusCondition.Burn && CurrentMove.Attribute == MoveAttribute.Physical)
                    {
                        damageText += " * 0.5 Burn";

                        damageMultiplier *= 0.5f;
                    }

                    //The amount of damage dealt by the attack gets calculated here.
                    //Attack & defense or special attack & special defense are selected according to the move's attribute.            
                    if (CurrentMove.Attribute == MoveAttribute.Physical)
                    {
                        damage += (2 * AttackingPokemon.Level + 10) / 250f * AttackingPokemon.Attack / DefendingPokemon.Defense * CurrentMove.Damage * SameMoveDamageBonus + 2;
                    }
                    else if (CurrentMove.Attribute == MoveAttribute.Special)
                    {
                        damage += (2 * AttackingPokemon.Level + 10) / 250f * AttackingPokemon.SpecialAttack / DefendingPokemon.SpecialDefense * CurrentMove.Damage * SameMoveDamageBonus + 2;
                    }

                    //The minimum damage an attack can do is 1 so if the damage would be less than 1, damage becomes 1.
                    if (damage < 1)
                        damage = 1;

                    damageText = Math.Round((double)(damage), 2) + " Base" + damageText;

                    //The overall multiplier is then calculated and damage gets multiplied by that amount.
                    damageMultiplier *= typeMod;
                    damage *= damageMultiplier;

                    numberOfHits--;
                }
                while (numberOfHits >= 1);
            }

            if (CurrentMove.Effect == MoveEffect.MultipleHits)
                UI.WriteLine("The attack hit " + hits + " times!");

            Program.Log(DefendingPokemonName + " takes " + DamageMessageFormatter((int)damage, DefendingPokemon.CurrentHP) + "(" + damageText + ")", 1);

            //After the damage is calculated, it gets subtracted from the defending Pokemon's HP after being rounded down to the nearest integer.
            Damage(DefendingPokemon, (int)damage, true);

            //Finally, if the move has a secondary effect and the defending Pokemon did not faint, it gets resolved.
            if (CurrentMove.SecondaryEffect && !DefendingPokemon.Fainted)
            {
                

                Effect();
            }

            //Recoil damage calculation.
            if (CurrentMove.Effect == MoveEffect.Recoil && !BattleOver)
            {
                int recoil = (int)Math.Floor(damage * CurrentMove.EffectCoefficient);

                Program.Log(AttackingPokemonName + " suffers " + recoil + " damage in recoil.", 1);
                UI.WriteLine("" + AttackingPokemonName + " is damaged by recoil!");

                Damage(AttackingPokemon, recoil, false);
            }
        }

        int CalculateDamage(Move move, Pokemon defendingPokemon)
        {
            int damage = 0;

            return damage;
        }

        /// <summary>
        /// //This method deals with damage being dealt to a Pokemon.
        /// </summary>
        /// <param name="pokemon">The Pokemon object that the damage is being dealt to.</param>
        /// <param name="damage">The amount of damage that would be dealt.</param>
        /// <param name="displayMessage">Determines whether a message will be displayed that </param>
        void Damage(Pokemon pokemon, int damage, bool displayMessage)
        {
            //If the Pokemon will not faint from the incurred damage...
            if (pokemon.CurrentHP > damage)
            {
                //The damage is detracted from its HP.
                pokemon.CurrentHP -= damage;

                //If the displayMessage flag is up, and the battle is not over, its life
                if (displayMessage)
                {
                    Program.Log(pokemon.Name + " did not faint, so the game prints its remaining HP.", 0);

                    if (pokemon == EnemyPokemon)
                        UI.WriteLine("Enemy " + EnemyPokemon.Name + "'s remaining HP: " + EnemyPokemon.PercentLife() + "%\n");

                    if (pokemon == PlayerPokemon)
                        UI.WriteLine(PlayerPokemon.Name + "'s remaining HP: " + PlayerPokemon.CurrentHP + "\n");
                }
            }

            //Else, if it did faint...
            else
            {
                //Its life gets set to 0.
                pokemon.CurrentHP = 0;

                //Then, depending on which Pokemon fainted, the game prints the corresponding message.

                if (EnemyPokemon.Fainted)
                {
                    Program.Log("The AI's Pokemon fainted.", 1);

                    UI.WriteLine("The enemy " + EnemyPokemon.Name + " fainted!\n");

                    AIPokemonFainted();
                }

                else if (PlayerPokemon.Fainted)
                {
                    Program.Log("The player's Pokemon fainted.", 1);

                    UI.WriteLine(PlayerPokemon.Name + " fainted!\n");

                    PlayerPokemonFainted();
                }
            }
        }

        #endregion

        #region Checks

        /// <summary>
        /// Determines if a Pokemon's attack will hit or miss by checking the move's accuracy and its PerfectAccuracy flag.
        /// </summary>
        /// <returns>Returns true if the Pokemon will hit, or false if it will miss.</returns>
        bool HitCheck()
        {
            //If the attack's accuracy is lower than a randomly generated number or the move has perfect accuracy, the attack hits.
            if (CurrentMove.PerfectAccuracy || Program.random.Next(1, 101) <= CurrentMove.Accuracy)
            {
                return true;
            }

            //Otherwise, the attack misses and the Pokemon is no longer move-locked, if it was.
            else
            {
                UI.WriteLine("The attack missed!\n");

                Program.Log("The attack missed.", 1);

                AttackingPokemon.MoveLock = false;

                return false;
            }
        }

        /// <summary>
        /// Determines if a Pokemon is paralyzed and whether it will succesfully attack if it is.
        /// </summary>
        /// <returns>Returns true if the Pokemon is not paralyzed or if it will succesfully attack.</returns>
        bool ParalysisCheck()
        {
            if (AttackingPokemon.Status == StatusCondition.Paralysis)
            {
                //If a randomly generated number is lower than the chance to attack, the Pokemon attacks succesfully.
                if (Program.random.Next(1, 101) <= Settings.ParalysisAttackChance)
                    return true;

                //Otherwise, the Pokemon is prevented from attacking and is no longer move-locked, if it was.
                else
                {
                    AttackingPokemon.MoveLock = false;

                    UI.WriteLine("" + AttackingPokemonName + " is paralyzed! It can't move!\n");

                    Program.Log(AttackingPokemonName + " was paralyzed and didn't attack.", 1);

                    return false;
                }
            }

            else
                return true;

        }

        /// <summary>
        /// Determines if a Pokemon is asleep and whether it will wake up in order to attack if it is. It also adjusts the sleep counter.
        /// </summary>
        /// <returns>Returns true if the Pokemon is not asleep or if the Pokemon will wake up and attack.</returns>
        bool SleepCheck()
        {
            if (AttackingPokemon.Status == StatusCondition.Sleep)
            {
                //If the Pokemon was asleep and its sleep counter has hit 0, it wakes up.
                if (AttackingPokemon.SleepCounter == 0)
                {
                    AttackingPokemon.Status = StatusCondition.None;

                    UI.WriteLine("" + AttackingPokemonName + " woke up!\n");

                    Program.Log(AttackingPokemonName + " woke up.", 1);

                    return true;
                }

                else
                {
                    AttackingPokemon.SleepCounter--;

                    AttackingPokemon.MoveLock = false;

                    UI.WriteLine("" + AttackingPokemonName + " is fast asleep.");

                    Program.Log(AttackingPokemonName + " didn't attack due to sleep. Remaining sleep turns: " + AttackingPokemon.SleepCounter, 1);

                    return false;
                }
            }

            else
                return true;
        }

        /// <summary>
        /// Determines if a Pokemon is suffering from any status condition that could prevent it from attacking, and whether it will sucesfully attack if it does.
        /// </summary>
        /// <returns></returns>
        bool StatusChecks()
        {
            return ParalysisCheck() && SleepCheck();
        }

        #endregion

        #region Experience, Victory, Defeat 

        /// <summary>
        /// Experience calculation and award code. Right now there's a flat 300 experience threshold per level, soon to change.   
        /// </summary>
        /// <param name="pokemon">The Pokemon that will receive the experience.</param>
        void Experience(Pokemon pokemon)
        {
            //The overall experience multiplier, which incorporates every possible experience increase or decrease.
            float multiplier = 1;

            //This is a band-aid multiplier that simply makes trainer battles give more experience.
            if (EncounterType == BattleType.Trainer)
                multiplier = 1.3f;

            /* The amount of experience actually awarded for defeating another Pokemon.
             * This is actually an expression of percentage for the current level.
             * I.E., if an enemy Pokemon would yield 1.5 expYield experience, it would award 
             * the current Pokemon 450 exp using the 300 exp / level formula, so 1.5 level.
             * In a system with incremental experience thresholds per level, it would still be
             * 450 exp, which would amount to less than 1.5 level this time around.
            */

            float expYield;

            if (pokemon.Level <= EnemyPokemon.Level)
                expYield = ((EnemyPokemon.Level - pokemon.Level) * 0.33f) + 0.45f;

            else
                expYield = ((EnemyPokemon.Level - pokemon.Level) * 0.05f) + 0.45f;

            //Program.Log("Exp yield was " + expYield.ToString(), 1);
            //The above log code helped me test exp yield, keeping it here in case it comes in handy.

            //If expYield would amount to less than 10% of a level (0.1), it instead becomes 0.1.
            if (expYield < 0.1f)
                expYield = 0.1f;

            int expGain = (int)(expYield * 300 * multiplier / 1.3f);

            //pokemon.Experience += expGain;
            pokemon.Experience += expGain;

            Program.Log(pokemon.Name + " received " + expGain + " experience.", 1);
        }

        /// <summary>
        /// The "You win!" code.
        /// </summary>
        void Victory()
        {
            BattleFinished();

            Program.Log("The player has won, so BattleOver is now true.", 1);

            UI.WriteLine("Congratulations! You won!\n");

            //If the player was battling a trainer, he is awarded money. Then, the trainer's defeat speech plays.
            if (EncounterType == BattleType.Trainer)
            {
                Program.Log("The user was battling a trainer so he received money. The trainer's defeat speech plays.", 0);

                Player.Money += EnemyTrainer.Money;
                EnemyTrainer.Defeat(Game.Player);
            }

            else if (EncounterType == BattleType.Wild)
                Program.Log("The player was battling a wild Pokemon, and will now return to the overworld.", 0);
        }

        /// <summary>
        /// The "You lose!" code.
        /// </summary>
        void Defeat()
        {
            BattleFinished();

            Program.Log("The player has lost, so battleOver is now true.", 1);

            UI.WriteLine("Oh no! All of your Pokemon have fainted!");

            int moneyLoss = (int)(Math.Floor(Player.Money * 0.05));

            Program.Log("The player lost $" + moneyLoss + " for blacking out. ($" + (Player.Money - moneyLoss) + " left)", 1);

            Player.Money -= moneyLoss;

            UI.WriteLine("You have lost $" + moneyLoss + " for blacking out.");

            //If the player was battling a trainer, the trainer's victory speech plays.
            if (EncounterType == BattleType.Trainer)
                EnemyTrainer.Victory(Game.Player);

            else if (EncounterType == BattleType.Wild)
                Game.BlackOut();
        }

        #endregion               

        #region Player Methods

        /// <summary>
        /// Determines if the player has healthy Pokemon in his party and as such can battle.
        /// </summary>
        /// <returns>True if the player's party contains a non-fainted Pokemon, or false if it does not.</returns>
        bool PlayerCanBattle()
        {
            if (Player.Party.Exists(p => !p.Fainted))
                return true;

            else
                return false;
        }

        /// <summary>
        /// Selects the Pokemon that the player will start the battle as active by looping through the player's party and finding the first Pokemon that is not fainted.
        /// </summary>
        void PokemonSelector()
        {
            //First, the game checks whether there are any healthy Pokemon in the player's party.
            if (PlayerCanBattle())
            {
                //If so, it loops over the entire party until it finds the first Pokemon that's not fainted.
                for (int i = 0; i < Player.Party.Count; i++)
                {
                    if (!Player.Party.ElementAt(i).Fainted)
                    {
                        //That Pokemon then gets sent out and the operation terminates.
                        PlayerChangePokemon(Player.Party.ElementAt(i));

                        break;
                    }
                }
            }

            else
                UI.Error("The game started a battle while the player has no non-fainted Pokemon.",
                         "A fight was started while the player has no healthy Pokemon. Location: " + Overworld.CurrentLocation.Tag, 2);
        }

        /// <summary>
        /// Handles the events that need to take place when the player would change his active Pokemon, 
        /// i.e. by sending out a Pokemon at the start of the battle or switching Pokemon during the battle.
        /// </summary>
        /// <param name="pokemon">The Pokemon that is being sent out.</param>
        void PlayerChangePokemon(Pokemon pokemon)
        {
            //The Pokemon becomes the active Pokemon.
            PlayerPokemon = pokemon;

            //It is also added to the Participants list for the current Pokemon so that it will receive experience.
            AddToParticipants(PlayerPokemon);

            UI.WriteLine(PlayerPokemon.Name + " was sent out!\n");
        }

        /// <summary>
        /// Handles the events that need to take place in order for the player's Pokemon to attack, such as setting the AttackingPokemon
        /// equal to the Player's pokemon, setting the CurrentMove to the player's selected move, etc.
        /// </summary>
        void PlayerAttack()
        {
            //First, the game checks to see if the player's action is set to Fight. The reason for this is so that in the event that the
            //player's Pokemon is knocked out and he consequently has to switch, the PlayerFaint method would set PlayerAction to None
            //so that the player would not attack with the new Pokemon that was just sent out.

            //Another approach to this is to have a check on the method that invokes this method saying:
            //if (playerPokemon == defendingPokemon) -- as the player's Pokemon would have defended last for it to still be the active Pokemon
            //However, this isn't a great approach and should be avoided if possible.

            if (PlayerAction == BattleAction.Fight)
            {
                Program.Log("The player attacks.", 0);

                //The battle's parameters are set accordingly.

                AttackingPokemon = PlayerPokemon;
                DefendingPokemon = EnemyPokemon;
                CurrentMove = PlayerMove;
                AttackingPokemonName = PlayerPokemon.Name;
                DefendingPokemonName = "The enemy " + EnemyPokemon.Name;

                //If the selected move does double damage after being used consecutively and was used last turn, SameMoveDamageBonus becomes 2.
                if (PlayerMove.Effect == MoveEffect.ConsecutiveDamage && PlayerMove == PlayerPreviousMove)
                    SameMoveDamageBonus = 2;
                else
                    SameMoveDamageBonus = 1;

                //Finally, combat starts with the player attacking. 
                CombatStart();
            }
        }

        /// <summary>
        /// Code that triggers when the player's Pokemon faints. 
        /// </summary>
        void PlayerPokemonFainted()
        {
            PlayerAction = BattleAction.None;

            //First, the game checks if the player can still battle.
            if (PlayerCanBattle())
            {
                Program.Log("The player has remaining healthy Pokemon.", 0);

                UI.WriteLine("Send out which Pokemon?(Valid input: 1-" + Player.Party.Count + ")");

                Pokemon pokemon = Player.SelectPokemon(true);

                //If the Pokemon the user selected is alive, it becomes the active Pokemon.
                if (!pokemon.Fainted)
                {
                    Program.Log("The player switches " + PlayerPokemon.Name + " out for " + pokemon.Name + ".", 1);

                    Participants.Remove(PlayerPokemon);

                    PlayerChangePokemon(pokemon);
                }

                else
                {
                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    UI.WriteLine("That Pokemon has fainted!");

                    PlayerPokemonFainted();
                }

            }

            //Otherwise, if the player has no remaining healthy Pokemon, he is taken to the defeat screen.
            else
            {
                Program.Log("The player has no remaining healthy Pokemon.", 1);

                Defeat();
            }
        }

        #endregion

        #region AI Methods

        /// <summary>
        /// Determines which action the AI will do. At present, AI will only ever select a move and proceed to attack.
        /// TODO: Add more complex logic.
        /// </summary>
        void AIAction()
        {
            AIMoveSelection();
        }

        /// <summary>
        /// This code simulates the AI selecting a move by using a random number generator.
        /// TODO: Add more complex logic.
        /// </summary>
        void AIMoveSelection()
        {
            //If the AI's Pokemon is not locked into a move, it picks a random move.
            if (!EnemyPokemon.MoveLock)
                EnemyMove = EnemyPokemon.knownMoves.ElementAt(Program.random.Next(0, EnemyPokemon.knownMoves.Count));

            //If it is locked into a move, it simply uses that move again.
            else
                EnemyMove = EnemyPreviousMove;

            //If the move that the AI selected isn't null for some reason, its action is set to Fight.
            if (EnemyMove != null)
                EnemyAction = BattleAction.Fight;

            Program.Log("The AI selected the move " + EnemyMove.Name + ".", 0);
        }

        /// <summary>
        /// Handles the events that need to take place in order for the AI's Pokemon to attack, such as setting the AttackingPokemon
        /// equal to the AI's pokemon, setting the CurrentMove to the AI's selected move, etc.
        /// </summary>
        void AIAttack()
        {
            //First, the game checks to see if the AI's action is set to Fight. The reason for this is so that in the event that the
            //AI's Pokemon is knocked out and consequently has to be switched out, the AIFaint method would set EnemyAction to None
            //so that the AI would not attack with the new Pokemon that was just sent out.

            //Another approach to this is to have a check on the method that invokes this method saying:
            //if (enemyPokemon == defendingPokemon) -- as the AI's Pokemon would have defended last for it to still be the active Pokemon
            //However, this isn't a great approach and should be avoided if possible.

            if (EnemyAction == BattleAction.Fight)
            {
                //The battle's parameters are set accordingly.

                AttackingPokemon = EnemyPokemon;
                DefendingPokemon = PlayerPokemon;
                CurrentMove = EnemyMove;
                AttackingPokemonName = "The enemy " + EnemyPokemon.Name;
                DefendingPokemonName = PlayerPokemon.Name;

                //If the selected move does double damage after being used consecutively and was used last turn, SameMoveDamageBonus becomes 2.
                if (EnemyMove.Effect == MoveEffect.ConsecutiveDamage && EnemyMove == EnemyPreviousMove)
                    SameMoveDamageBonus = 2;
                else
                    SameMoveDamageBonus = 1;

                //Finally, combat starts with the AI attacking.
                CombatStart();
            }
        }

        /// <summary>
        /// Code that triggers when the AI's Pokemon faints.
        /// </summary>
        void AIPokemonFainted()
        {
            EnemyAction = BattleAction.None;

            //First, experience is awarded to all of the player's Pokemon that participated in the battle.
            foreach (Pokemon p in Participants)
            {
                if (!p.Fainted && p.Level <= 100)
                    Experience(p);
            }

            //If the AI has any remaining healthy Pokemon, it sends out its next Pokemon, based on the aiCurrentPokemonIndex index number.     
            if (EncounterType == BattleType.Trainer && EnemyTrainer.Party.Exists(pokemon => !pokemon.Fainted))
            {
                AICurrentPokemonIndex++;
                AIChangePokemon(EnemyTrainer.Party.ElementAt(AICurrentPokemonIndex));
            }

            //Otherwise, if the AI has no more Pokemon, or if the battle was with a wild Pokemon, the player wins.
            else
                Victory();
        }

        /// <summary>
        /// Handles the events that need to take place when the AI would change its active Pokemon, 
        /// i.e. by sending out a Pokemon at the start of the battle or switching Pokemon during the battle.
        /// </summary>
        /// <param name="pokemon">The Pokemon which will be the new active Pokemon.</param>
        void AIChangePokemon(Pokemon pokemon)
        {
            EnemyPokemon = pokemon;

            if (EncounterType == BattleType.Trainer)
            {
                Program.Log("The AI sends out " + EnemyPokemon.Name + ".", 1);

                UI.WriteLine(EnemyTrainer.DisplayName + " sent out a level " + EnemyPokemon.Level + " " + EnemyPokemon.species.Name + "!\n");
            }

            Participants.Clear();

            Player.AddToSeen(EnemyPokemon.species.PokedexNumber);
        }

        #endregion        

        #region Other Methods

        /// <summary>
        /// //This method formats the message for damage dealt to a Pokemon depending on whether it was overkill or not. Used primarily for logging purposes.
        /// </summary>
        /// <param name="damage">The damage that would be dealt to the Pokemon.</param>
        /// <param name="currentHP">The Pokemon's current HP.</param>
        /// <returns></returns>
        string DamageMessageFormatter(int damage, int currentHP)
        {
            if (damage <= currentHP)
                return damage + " damage.";

            else
                return currentHP + " damage.(" + (damage - currentHP) + " overkill)";
        }

        /// <summary>
        /// This method adds a Pokemon to the "Participants" list, for the purpose of gaining experience.
        /// </summary>
        /// <param name="pokemon">The Pokemon to add to the list.</param>
        void AddToParticipants(Pokemon pokemon)
        {
            if (!Participants.Contains(pokemon))
                Participants.Add(pokemon);
        }

        /// <summary>
        /// Handles the events that need to take place when the battle is over, such as setting BattleOver to false and
        /// This code restores the temporary variables of the battle and the various Pokemon to their default state.
        /// </summary>
        void BattleFinished()
        {
            foreach (Pokemon p in Player.Party)
                p.ResetTemporaryEffects(true);

            if (EncounterType == BattleType.Trainer)
                foreach (Pokemon p in EnemyTrainer.Party)
                    p.ResetTemporaryEffects(true);

            else
                EnemyPokemon.ResetTemporaryEffects(true);

            BattleOver = true;
        }

        /// <summary>
        /// Clears up the field off all hazards, when a move such as Rapid Spin is used.
        /// TODO: Add to this when I add more hazards.
        /// </summary>
        /// <param name="pokemon"></param>
        void ClearHazards(Pokemon pokemon)
        {
            if (pokemon.LeechSeed)
                pokemon.LeechSeed = false;

            UI.WriteLine(AttackingPokemonName + " cleared all hazards off the field using " + CurrentMove.Name + "!");
        }

        /// <summary>
        /// Residual damage due to status effects.
        /// </summary>
        /// <param name="pokemon"></param>
        void ResidualDamage(Pokemon pokemon)
        {
            if (pokemon.Status == StatusCondition.Burn || pokemon.Status == StatusCondition.Poison)
            {
                UI.WriteLine("" + PokemonNameFormatter(pokemon, true) + " lost some life due to its " + pokemon.Status + "!");

                //First, the damage that would be dealt is calculated.
                int residualDamage = (int)Math.Round(((pokemon.MaxHP / 8.0)), 0, MidpointRounding.AwayFromZero);

                Program.Log(pokemon.Name + " is " + pokemon.Status.ToString().ToLower() + "ed, so it takes " + DamageMessageFormatter(residualDamage, pokemon.CurrentHP), 1);

                //Then, that much damage gets dealt to the Pokemon.
                Damage(pokemon, residualDamage, false);
            }
        }

        /// <summary>
        /// Checks if a Pokemon is seeded, and if so, deals damage to it and heals the Leech Seed user.
        /// </summary>
        /// <param name="seededPokemon">The Pokemon that would be the target of Leech Seed.</param>
        /// <param name="seedUser">The Pokemon that would be the user of Leech Seed.</param>
        void LeechSeed(Pokemon seededPokemon, Pokemon seedUser)
        {
            if (seededPokemon.LeechSeed)
            {
                UI.WriteLine("" + PokemonNameFormatter(seededPokemon, true) + "'s life was drained by Leech Seed!");

                //First, the damage that would be dealt (and subsequently, life healed) is calculated.
                int seedDamage = (int)Math.Round((seededPokemon.MaxHP / 8.0), 0, MidpointRounding.AwayFromZero);
                int seedHealing = seedDamage;

                //If the damage would exceed the enemy Pokemon's remaining HP, the player's Pokemon only gets healed by the actual damage dealt.
                if (seedDamage > seededPokemon.CurrentHP)
                    seedHealing = seededPokemon.CurrentHP;

                //If the amount healed would exceed the player's Pokemon's maximum life, it becomes the difference between max life and current life.
                if (seedUser.CurrentHP + seedHealing > seedUser.MaxHP)
                    seedHealing = seedUser.MaxHP - seedUser.CurrentHP;

                //Then, that much damage is healed from the player's Pokemon.
                seedUser.CurrentHP += seedHealing;

                Program.Log(seededPokemon.Name + " is affected by Leech Seed, so it takes " + DamageMessageFormatter(seedDamage, seededPokemon.CurrentHP), 1);
                Program.Log(seedUser.Name + " heals " + seedHealing + " points of damage due to Leech Seed.", 1);

                //Finally, the enemy Pokemon takes that much damage.
                Damage(seededPokemon, seedDamage, false);
            }
        }

        string PokemonNameFormatter(Pokemon pokemon, bool capitalT)
        {
            if (pokemon == PlayerPokemon)
                return pokemon.Name;

            else
            {
                string t;

                if (capitalT)
                    t = "T";

                else
                    t = "t";

                return t + "he enemy " + pokemon.Name;
            }
        }

        #endregion

        #region Effect Resolution

        /// <summary>
        /// This method handles the resolution of the effects of moves, even if they're secondary effects.
        /// It takes a move's effect ID into consideration, and uses a single 1-100 random number generator to calculate probability.
        /// </summary>
        void Effect()
        {
            Program.Log("Effect resolution takes place.", 0);

            int chance = Program.random.Next(1, 101);

            if (CurrentMove.SecondaryEffect)
                Program.Log(CurrentMove.Name + " has a secondary effect, which will now be resolved. (Coefficient: " + CurrentMove.EffectCoefficient + ", Roll: " + chance + ")", 1);

            switch (CurrentMove.Effect)
            {
                //The comments for Burn also apply to all other status conditions.

                case MoveEffect.Burn:

                    //If the current move's attribute is Status (and thus the game does not need to check for a chance) OR
                    //the number the game rolled is equal to or lower than the probability the move will Burn...
                    if (CurrentMove.Attribute == MoveAttribute.Status || chance <= CurrentMove.EffectCoefficient)
                    {
                        //If the defending Pokemon is not currently suffering from a status condition and it is not a Fire-type Pokemon...
                        if (DefendingPokemon.Status == StatusCondition.None && !DefendingPokemon.TypeCheck(Type.Fire))
                        {
                            //It gets burnt.
                            DefendingPokemon.Burn();

                            Program.Log(DefendingPokemonName + " gets burnt by " + CurrentMove.Name
                                + ". Chance = " + CurrentMove.EffectCoefficient + "%, Roll = " + chance, 1);
                        }

                        //Otherwise, if the move was a status move, a message signifying that the move failed is displayed.
                        else if (CurrentMove.Attribute == MoveAttribute.Status)
                            UI.WriteLine("The move failed!");
                    }

                    break;

                case MoveEffect.Poison:

                    if (CurrentMove.Attribute == MoveAttribute.Status || chance <= CurrentMove.EffectCoefficient)
                    {
                        if (DefendingPokemon.Status == StatusCondition.None && !DefendingPokemon.TypeCheck(Type.Poison))
                        {
                            DefendingPokemon.Poison();

                            Program.Log(DefendingPokemonName + " gets poisoned by " + CurrentMove.Name
                                + ". Chance = " + CurrentMove.EffectCoefficient + "%, Roll = " + chance, 1);
                        }

                        else if (CurrentMove.Attribute == MoveAttribute.Status)
                            UI.WriteLine("The move failed!");
                    }

                    break;

                case MoveEffect.Paralysis:

                    if (CurrentMove.Attribute == MoveAttribute.Status || chance <= CurrentMove.EffectCoefficient)
                    {
                        if (DefendingPokemon.Status == StatusCondition.None && !DefendingPokemon.TypeCheck(Type.Electric))
                        {
                            DefendingPokemon.Paralyze();

                            Program.Log(DefendingPokemonName + " gets paralyzed by " + CurrentMove.Name + ". Chance = "
                                + CurrentMove.EffectCoefficient + "%, Roll = " + chance, 1);
                        }
                    }

                    break;

                case MoveEffect.Sleep:

                    if (CurrentMove.Attribute == MoveAttribute.Status || chance <= CurrentMove.EffectCoefficient)
                    {
                        if (DefendingPokemon.Status == StatusCondition.None)
                        {
                            int sleepRNG = Program.random.Next(1, 101);

                            //The amount of turns that the Pokemon will sleep for.
                            int turns;

                            //40% probability that the Pokemon will sleep for 1 turn.
                            if (sleepRNG <= 40)
                                turns = 1;

                            //40% probability that the Pokemon will sleep for 2 turns.
                            else if (sleepRNG <= 80)
                                turns = 2;

                            //20% probability that the Pokemon will sleep for 3 turns.
                            else
                                turns = 3;

                            DefendingPokemon.Sleep(turns);

                            Program.Log(DefendingPokemonName + " gets slept by " + CurrentMove.Name + " for " + turns + " turns.", 1);
                        }

                        else if (CurrentMove.Attribute == MoveAttribute.Status)
                            UI.WriteLine("The move failed!\n");
                    }

                    break;

                case MoveEffect.LeechSeed:

                    if (!DefendingPokemon.LeechSeed && !DefendingPokemon.TypeCheck(Type.Grass))
                    {
                        DefendingPokemon.LeechSeed = true;

                        UI.WriteLine(DefendingPokemonName + " got seeded!\n");

                        Program.Log(DefendingPokemonName + " gets afflicted by Leech Seed.", 1);
                    }

                    else
                        UI.WriteLine("The move failed!");

                    break;

                case MoveEffect.MoveLock:

                    AttackingPokemon.MoveLock = true;

                    Program.Log(AttackingPokemon.Name + " used " + CurrentMove + ", which move-locked it.", 0);

                    break;

                case MoveEffect.ConsecutiveDamage:

                    AttackingPokemon.MoveLock = true;

                    Program.Log(AttackingPokemon.Name + " used " + CurrentMove + ", which move-locked it.", 0);

                    break;

                case MoveEffect.ClearHazards:

                    ClearHazards(AttackingPokemon);

                    break;

                case MoveEffect.Protect:

                    if ((AttackingPokemon == PlayerPokemon && PlayerPreviousMove.Effect != MoveEffect.Protect) ||
                        (AttackingPokemon == EnemyPokemon && EnemyPreviousMove.Effect != MoveEffect.Protect))
                    {
                        AttackingPokemon.Protect = true;

                        UI.WriteLine(AttackingPokemonName + " protected itself!");

                        Program.Log(AttackingPokemonName + " is protected for this turn.", 1);
                    }

                    else
                        UI.WriteLine("The move failed!");

                    break;

                case MoveEffect.Disable:

                    break;

                case MoveEffect.Confusion:

                    break;

                case MoveEffect.ItemSteal:

                    break;

                default: //Failsafe.
                    {
                        UI.Error("The game tried to resolve an attack with an invalid effect type.",
                                 "The game tried to resolve the effect of " + CurrentMove.Name + "(Effect: " + CurrentMove.Effect + "), which does not exist.", 2);

                        break;
                    }
            }

            Program.Log("All effects have been resolved.", 0);
        }

        #endregion
    }
}
