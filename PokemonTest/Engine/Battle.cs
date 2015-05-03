using PokemonTextEdition.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    /// <summary>
    /// This is an instance class that represents a battle between the player and either an enemy trainer or a wild Pokemon.
    /// It should be instantiated by invoking either the Start method for trainer battles, or the Wild method for wild Pokemon battles.
    /// </summary>
    class Battle
    {
        #region Variables

        //A random number generator that all methods that require randomness will use.
        Random rng = new Random();

        //An object that represents the current enemy trainer.
        Trainer enemyTrainer = new Trainer();

        //A list of Pokemon that participated in a battle against a particular Pokemon, for the purpose of experience calculation.
        List<Pokemon> participants = new List<Pokemon>();

        string encounterType; //Determines the type of the encounter.
        int aiCurrentPokemonIndex = 0; //This is used as a counter for the AI to switch Pokemon.

        //These objects identify the player's current Pokemon and the move he selected.
        Pokemon playerPokemon = new Pokemon();
        Move playerMove = new Move();

        //These, on the other hand, are used for the AI's Pokemon and its selected move.
        Pokemon enemyPokemon = new Pokemon();
        Move enemyMove = new Move();

        //These objects identify the last move used by each side, i.e. in order to identify if the same move was used twice.   
        Move previousPlayerMove = new Move();
        Move previousEnemyMove = new Move();

        float sameMoveDamageBonus = 1; //This is used for moves that deal increased damage while being used consecutively.

        //These objects make it easier to do damage calculations.
        Pokemon attackingPokemon = new Pokemon();
        Pokemon defendingPokemon = new Pokemon();
        Move currentMove = new Move();

        //These two strings make it easier to tell who's attacking and who's defending, as well as adjust messages accordingly.
        string attackingPokemonName = "";
        string defendingPokemonName = "";

        //A bool that tells the program that the battle is now over. Not the most elegant solution, but it works!
        bool battleOver = false;

        //An integer that counts how many times the player has tried to escape.
        int escapeAttempts = 1;

        #endregion

        #region Battle Start

        /// <summary>
        /// This method is used to start a battle with a trainer or a special Pokemon. The Wild method is used for battles with wild Pokemon.
        /// </summary>
        /// <param name="t">The Trainer object for the enemy trainer.</param>
        /// <param name="type">A special tag to determine several in-battle factors based on the type of battle - i.e. special Pokemon, showcase battle, etc. Default is "trainer".</param>
        public void Start(Trainer t, string type)
        {
            Program.Log("Battle against a trainer starts.", 1);

            PokemonSelection();

            enemyPokemon = t.party.ElementAt(0);
            enemyTrainer = t;
            encounterType = type;

            Console.WriteLine("{0} wants to battle!\n{0} sent out a level {1} {2}!\n", t.DisplayName, enemyPokemon.Level, enemyPokemon.Name);

            Overworld.player.AddToSeen(enemyPokemon.Name);

            Actions();
        }

        /// <summary>
        /// This method is used to start a battle with a wild Pokemon. It is virtually identical to the trainer battle method,
        /// except that it requires a Pokemon object as an input, rather than a trainer.
        /// </summary>
        /// <param name="e">The enemy Pokemon object.</param>
        public void Wild(Pokemon e)
        {
            Program.Log("Battle against a wild Pokemon starts.", 1);

            PokemonSelection();

            enemyPokemon = e;
            encounterType = "wild";

            Console.WriteLine("A wild level {0} {1} appeared!\n", e.Level, e.Name);

            Overworld.player.AddToSeen(e.Name);

            Actions();
        }

        void PokemonSelection()
        {
            //This method selects a starting Pokemon for the trainer. 
            //First, it checks whether there are non-fainted Pokemon in the player's party. 
            //If so, it loops over the entire party until it finds the first Pokemon that's not fainted.

            if (Overworld.player.party.Exists(p => !p.Fainted))
            {
                for (int i = 0; i < Overworld.player.party.Count; i++)
                {
                    if (!Overworld.player.party.ElementAt(i).Fainted)
                    {
                        playerPokemon = Overworld.player.party.ElementAt(i);

                        AddToParticipants(playerPokemon);

                        break;
                    }
                }
            }

            else
            {
                Console.WriteLine("ERROR: The game tried to start a fight while the player has no non-fainted Pokemon.");
                Console.WriteLine("Please inform the author about this issue - Battle.cs, PokemonSelection()");
            }
        }

        #endregion

        #region Basic Actions

        /// <summary>
        /// The main menu screen of the battle, from which the player picks an action.
        /// </summary>
        void Actions()
        {
            Program.Log("The player is taken to the Actions menu.", 0);

            Console.WriteLine("What will you do?\n(Available commands: (f)ight, (s)tatus, (i)item, s(w)itch, (c)atch, (r)un)");

            string action = Console.ReadLine();

            if (action != "")
                Console.WriteLine("");

            //This switch handles player input. 
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

                    Console.WriteLine("Invalid command!\n");

                    Actions();
                    break;
            }

            return;
        }

        /// <summary>
        /// A very simple status screen that displays the current status of the player's Pokemon as well as the current enemy Pokemon.
        /// </summary>
        void Status()
        {
            Program.Log("The player views the Status screen.", 0);

            Console.WriteLine("Your Pokemon: ");
            playerPokemon.PrintStatus();
            Console.WriteLine("");

            if (encounterType == "trainer")
                Console.Write("Your opponent's Pokemon: ");

            else if (encounterType == "wild")
                Console.Write("The wild Pokemon: ");

            enemyPokemon.BriefStatus();

            Console.WriteLine("");

            Actions();
        }

        /// <summary>
        /// The code that triggers when the player chooses to use an item.
        /// </summary>
        void Item()
        {
            //If the player succesfully uses an item, the AI attacks once. 
            if (Overworld.player.UseItemsCombat())
                AIAttack();

            //Otherwise, the player is taken back to the actions screen.
            else
                Actions();
        }

        /// <summary>
        /// This code handles switching the player's active Pokemon.
        /// </summary>
        void Switch()
        {
            Program.Log("The player chooses to switch Pokemon.", 0);

            Console.WriteLine("Send out which Pokemon?\n(Valid input: 1-{0}, or press Enter to return.)\n", Overworld.player.party.Count);

            Pokemon pokemon = Overworld.player.SelectPokemon(false);

            //If the Pokemon the user selected is healthy and is not already the active Pokemon, it becomes the active Pokemon.
            //The AI also gets to attack once if the player succesfully changes Pokemon.
            if (pokemon != null && pokemon != playerPokemon && !pokemon.Fainted)
            {
                Program.Log("The player switches " + playerPokemon.Name + " out for " + pokemon.Name + ". The AI will now attack.", 1);

                playerPokemon.RestoreTemporaryStatus(false);

                playerPokemon = pokemon;

                AddToParticipants(playerPokemon);

                Console.WriteLine("\n{0} was sent out!", playerPokemon.Name);

                //The AI will attack once following the player's switch.
                AIAttack();
            }

            else if (!pokemon.Fainted)
            {
                Program.Log("The player chose to switch to a fainted Pokemon. Returning to Actions.", 0);
                Console.WriteLine("\nThat Pokemon has fainted!\n");

                Actions();
            }

            else if (pokemon == playerPokemon)
            {
                Program.Log("The player chose to switch to the Pokemon that's already active. Returning to Actions.", 0);
                Console.WriteLine("\nThat Pokemon is already the active Pokemon!\n");

                Actions();
            }

            else
                Actions();
        }

        #endregion

        #region Combat

        /// <summary>
        /// This is the first part of code that handles Pokemon fighting. Here, the player selects a move.
        /// </summary>
        void Fight()
        {
            Program.Log("The player chooses to fight.", 1);

            //If the player's Pokemon is not move-locked, the player selects a move.
            if (!playerPokemon.moveLocked)
            {
                Console.WriteLine("Please select a move. (Valid input: 1-{0}, or press Enter to return.)\n", playerPokemon.knownMoves.Count);

                Move tempMove = playerPokemon.SelectMove(false);

                //The player is asked to select a move. If his selecetion is correct, the operation goes on.
                if (tempMove != null)
                {
                    playerMove = tempMove;

                    Program.Log("The player selected the move " + playerMove.Name + ".", 0);

                    //The AI then selects a move.
                    AIMoveSelection();

                    //The program then goes into Speed priority calculation.
                    TurnOrder();
                }

                else
                {
                    //Otherwise, an error message is displayed, and the player is taken back to the Actions menu.
                    Program.Log("The player chose an invalid move. Returning to Actions.", 0);

                    Actions();
                }
            }

            else
            {
                //Else if the player's Pokemon is movelocked and it still knows the move it used last turn, that move gets automatically selected.
                if (playerPokemon.knownMoves.Exists(m => m.Name == previousPlayerMove.Name))
                {
                    playerMove = playerPokemon.knownMoves.Find(m => m.Name == previousPlayerMove.Name);

                    Program.Log("The player's Pokemon is movelocked and automatically uses the move " + playerMove.Name + ".", 0);

                    //The AI then selects a move.
                    AIMoveSelection();

                    //The program then goes into Speed priority calculation.
                    TurnOrder();
                }
            }
        }

        /// <summary>
        /// This code simulates the AI selecting a move by using a random number generator.
        /// TODO: Add more complex logic.
        /// </summary>
        void AIMoveSelection()
        {
            //While the enemy's Pokemon is not move-locked, it picks a random move.
            if (!enemyPokemon.moveLocked)
                enemyMove = enemyPokemon.knownMoves.ElementAt(rng.Next(0, enemyPokemon.knownMoves.Count));

            //If the enemy Pokemon is movelocked, it just uses the same move again.
            else
                enemyMove = previousEnemyMove;

            Program.Log("The AI selected the move " + enemyMove.Name + ".", 0);
        }

        /// <summary>
        /// This method determines the order of the events of the turn by checking each Pokemon's speed priority.
        /// </summary>
        void TurnOrder()
        {
            Program.Log("The game goes into Speed Priority calculation.", 0);

            //Speed adjustments in case of paralysis.
            int playerSpeed = playerPokemon.Speed;
            int enemySpeed = enemyPokemon.Speed;

            if (playerPokemon.Status == "paralysis")
                playerSpeed = (int)(playerSpeed * 0.25f);

            if (enemyPokemon.Status == "paralysis")
                enemySpeed = (int)(enemySpeed * 0.25f);

            //If the player's Pokemon is faster or its move had higher priority, it goes first. 
            if (playerSpeed >= enemySpeed && playerMove.Priority >= enemyMove.Priority)
            {
                Program.Log("The player's Pokemon is faster, so it attacks first.", 0);

                PreCombat("player");

                //If the fight is not over and the enemy Pokemon has not fainted, it attacks.
                if (!battleOver && enemyPokemon == defendingPokemon)
                    PreCombat("AI");
            }

            //Otherwise, the AI's Pokemon goes first.
            else if (playerSpeed < enemySpeed || playerMove.Priority < enemyMove.Priority)
            {
                Program.Log("The AI's Pokemon is faster, so it attacks first.", 0);

                PreCombat("AI");

                //If the fight is not over and the player's Pokemon has not fainted, it attacks.
                if (!battleOver && playerPokemon == defendingPokemon)
                    PreCombat("player");
            }

            //Then, the end of turn effects are resolved, and if the fight is not over, the player is taken back to the Actions menu.   
            if (!battleOver)
            {
                EndOfTurn();

                if (!battleOver)
                {
                    Program.Log("The turn has ended. Returning to Actions.", 1);

                    Console.WriteLine("");

                    Actions();
                }
            }
        }

        /// <summary>
        /// This method does some quick pre-combat calculations and helpful message adjustments.
        /// </summary>
        /// <param name="attacker"></param>
        void PreCombat(string attacker)
        {
            Program.Log("The " + attacker + " attacks.", 0);

            if (attacker == "player")
            {
                attackingPokemon = playerPokemon;
                defendingPokemon = enemyPokemon;
                currentMove = playerMove;
                attackingPokemonName = playerPokemon.Name;
                defendingPokemonName = "The enemy " + enemyPokemon.Name;

                //If the selected move does double damage after being used consecutively and was used last turn, sameMoveDamageBonus becomes 2.
                if (playerMove == previousPlayerMove && playerMove.EffectID == 4)
                    sameMoveDamageBonus = 2;
                else
                    sameMoveDamageBonus = 1;

                previousPlayerMove = playerMove;
            }

            else if (attacker == "AI")
            {
                attackingPokemon = enemyPokemon;
                defendingPokemon = playerPokemon;
                currentMove = enemyMove;
                attackingPokemonName = "The enemy " + enemyPokemon.Name;
                defendingPokemonName = playerPokemon.Name;

                //If the selected move does double damage after being used consecutively and was used last turn, sameMoveDamageBonus becomes 2.
                if (enemyMove == previousEnemyMove && enemyMove.EffectID == 4)
                    sameMoveDamageBonus = 2;
                else
                    sameMoveDamageBonus = 1;

                previousEnemyMove = enemyMove;
            }

            if (ParalysisCheck() && SleepCheck())
            {
                Console.WriteLine("\n{0} used {1}!", attackingPokemonName, currentMove.Name);

                if (defendingPokemon.protect)
                    Console.WriteLine("{0} was protected from the attack!", defendingPokemonName);

                else
                {
                    float typeMod = TypeChart.Check(currentMove.Type, defendingPokemon.species.Type1, defendingPokemon.species.Type2);

                    if (typeMod == 0)
                        Console.WriteLine("{0} was immune to the attack!", defendingPokemonName);

                    else
                    {
                        //If a player uses a move that locks you into a move, this sets the moveLocked flag to true.
                        if (currentMove.EffectID == 4)
                        {
                            Program.Log(attackingPokemon.Name + " used " + currentMove + ", which move-locked it.", 0);
                            attackingPokemon.moveLocked = true;
                        }

                        if (currentMove.Attribute == "Status")
                        {
                            Program.Log("The attack selected by the " + attacker + " does not deal damage, so Effect() will take place.", 0);

                            if (HitCheck())
                                Effect(attacker);
                        }

                        else
                        {
                            Program.Log("The attack selected by the " + attacker + " deals damage, so Damage() will take place.", 0);

                            if (HitCheck())
                                DamageCalculation(attacker, typeMod);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the damage that would be dealt by a damaging attack.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="mod"></param>
        void DamageCalculation(string attacker, float mod)
        {


            float damage = 0; //The total damage inflicted to the defending Pokemon after all calculations are done. 
            int previousHP = defendingPokemon.CurrentHP; //This is used to accurately calculate how much damage the defending Pokemon took. 

            float multiplier = 1; //An all-purpose multiplier for the final damage calculation.
            float typeMod = mod;  //The relational modifier between the attack's type and the defending Pokemon's type(s).    

            int numberOfHits = 1, hits = 1; //The number of times an attack will strike, for multi-hit attacks.

            //This string is used for logging, in order to determine whether moves do the correct amount of damage.
            string damageText = "";

            Program.Log(attackingPokemonName + " attacks " + defendingPokemon.Name + " (" + defendingPokemon.CurrentHP + "/" + defendingPokemon.MaxHP + " HP) with "
                        + currentMove.Name + ".", 1);

            //The next part prints a message explaining the result of the TypeChart calculation to the player.
            if (typeMod == 2 || typeMod == 4)
                Console.WriteLine("It's super effective!");

            else if (typeMod == 0.5f || typeMod == 0.25f)
                Console.WriteLine("It's not very effective!");

            if (typeMod != 1f)
                damageText += " * " + typeMod + " Type";

            //Multi-hit attack calculation.
            if (currentMove.EffectID == 13)
            {
                //If the attack is a 2-hit attack, it will hit twice.

                if (currentMove.EffectN == 2)
                    numberOfHits = 2;

                else
                {
                    //Otherwise the game will go into the process of calculating how many times it will hit, using RNG.

                    int hitsRNG = rng.Next(1, 101);

                    if (currentMove.EffectN == 5 && hitsRNG > 90)
                        numberOfHits = 5;

                    else if (currentMove.EffectN == 5 && hitsRNG > 80)
                        numberOfHits = 4;

                    else if (currentMove.EffectN == 5 && hitsRNG > 40)
                        numberOfHits = 3;

                    else
                        numberOfHits = 2;
                }

                hits = numberOfHits;

                Program.Log(currentMove.Name + " hit " + hits + " times.", 1);
            }

            if (currentMove.EffectID == 10)
                damage = currentMove.EffectN; //If the selected move does a set amount of damage, the game skips damage calculation.

            else
            {
                do //This is a do-while loop to facilitate for the multiple hits scenario.
                {
                    //Same-Type Attack Bonus check. If the Pokemon's attack is the same type as the Pokemon itself, it receives a 50% damage boost.
                    if (attackingPokemon.TypeCheck(currentMove.Type))
                    {
                        damageText += " * 1.5 STAB";

                        multiplier *= 1.5f;
                    }

                    int critical = rng.Next(1, 101);

                    //Critical hit calculator. I've set it to 10% chance and 20% extra damage for a crit as I dislike RNG.
                    if (critical < 11 || currentMove.EffectID == 9 && critical < 21)
                    {
                        Program.Log("The attack was a critical hit.", 0);

                        Console.WriteLine("A critical hit!");

                        damageText += " * 1.2 Crit";

                        multiplier *= 1.2f;
                    }

                    //This code simply makes wild Pokemon do 20% less damage. But one day, they'll turn against us!
                    if (attacker == "AI" && encounterType == "wild")
                    {
                        damageText += " * 0.8 Wild";

                        multiplier *= 0.8f;
                    }

                    //If the attacking Pokemon is burned, the status modifier for physical attacks is set to 0.5.
                    if (attackingPokemon.Status == "burn" && currentMove.Attribute == "Physical")
                    {
                        damageText += " * 0.5 Burn";

                        multiplier *= 0.5f;
                    }

                    //The amount of damage dealt by the attack gets calculated here.
                    //Attack & defense or special attack & special defense are selected according to the move's attribute.            
                    if (currentMove.Attribute == "Physical")
                    {
                        damage += (2 * attackingPokemon.Level + 10) / 250f * attackingPokemon.Attack / defendingPokemon.Defense * currentMove.Damage * sameMoveDamageBonus + 2;
                    }
                    else if (currentMove.Attribute == "Special")
                    {
                        damage += (2 * attackingPokemon.Level + 10) / 250f * attackingPokemon.SpecialAttack / defendingPokemon.SpecialDefense * currentMove.Damage * sameMoveDamageBonus + 2;
                    }

                    //The minimum damage an attack can do is 1 so if the damage would be less than 1, damage becomes 1.
                    if (damage < 1)
                        damage = 1;

                    damageText = Math.Round((double)(damage), 2) + " Base" + damageText;

                    //The overall multiplier is then calculated and damage gets multiplied by that amount.
                    multiplier *= typeMod;
                    damage *= multiplier;

                    numberOfHits--;
                }
                while (numberOfHits >= 1);
            }

            if (currentMove.EffectID == 13)
                Console.WriteLine("The attack hit {0} times!", hits);

            Program.Log(defendingPokemonName + " takes " + DamageMessageFormatter((int)damage, defendingPokemon.CurrentHP) + "(" + damageText + ")", 1);

            //After the damage is calculated, it gets subtracted from the defending Pokemon's HP after being rounded down to the nearest integer.
            Damage(defendingPokemon, (int)damage, true);

            //Program.Log(defenderName + " took " + (previousHP - defendingPokemon.currentHP).ToString() + " damage.", 1);

            //Finally, if the move has a secondary effect, it gets resolved.
            if (currentMove.SecondaryEffect)
            {
                Program.Log("The move has a secondary effect, so it will now be resolved.", 0);

                Effect(attacker);
            }

            //Recoil damage calculation.
            if (currentMove.EffectID == 8 && !battleOver)
            {
                int recoil = (int)Math.Floor(damage * currentMove.EffectN);

                Program.Log(attacker + " suffers " + recoil + " damage in recoil.", 1);
                Console.WriteLine("\n{0} is damaged by recoil!", attacker);

                Damage(attackingPokemon, recoil, false);
            }
        }

        /// <summary>
        /// The effects that take place at the end of the turn are resolved in this method.
        /// </summary>
        void EndOfTurn()
        {
            Program.Log("End of turn resolution takes place.", 0);

            //Both Pokemon lose the Protect status, as it only lasts 1 turn.
            playerPokemon.protect = false;
            enemyPokemon.protect = false;

            if (enemyPokemon.Status == "burn" || enemyPokemon.Status == "poison")
            {
                //Residual damage due to status effects.

                Console.WriteLine("\nThe enemy {0} lost some life due to its {1}!", enemyPokemon.Name, enemyPokemon.Status);

                //First, the damage that would be dealt is calculated.
                int residualDamage = (int)Math.Round(((enemyPokemon.MaxHP / 8.0)), 0, MidpointRounding.AwayFromZero);

                Program.Log(enemyPokemon.Name + " is " + enemyPokemon.Status + "ed, so it takes " + DamageMessageFormatter(residualDamage, enemyPokemon.CurrentHP), 1);

                //Then, that much damage gets dealt to the Pokemon.
                Damage(enemyPokemon, residualDamage, false);
            }

            if (enemyPokemon.leechSeed)
            {
                //Damage and healing due to Leech Seed.

                Console.WriteLine("\n{0}'s life was drained by Leech Seed!", enemyPokemon.Name);

                //First, the damage that would be dealt (and subsequently, life healed) is calculated.
                int seedDamage = (int)Math.Round((enemyPokemon.MaxHP / 8.0), 0, MidpointRounding.AwayFromZero);
                int seedHealing = seedDamage;

                //If the damage would exceed the enemy Pokemon's remaining HP, the player's Pokemon only gets healed by the actual damage dealt.
                if (seedDamage > enemyPokemon.CurrentHP)
                    seedHealing = enemyPokemon.CurrentHP;

                //If the amount healed would exceed the player's Pokemon's maximum life, it becomes the difference between max life and current life.
                if (playerPokemon.CurrentHP + seedHealing > playerPokemon.MaxHP)
                    seedHealing = playerPokemon.MaxHP - playerPokemon.CurrentHP;

                //Then, that much damage is healed from the player's Pokemon.
                playerPokemon.CurrentHP += seedHealing;

                Program.Log(enemyPokemon.Name + " is affected by Leech Seed, so it takes " + DamageMessageFormatter(seedDamage, enemyPokemon.CurrentHP), 1);
                Program.Log(playerPokemon.Name + " heals " + seedHealing + " points of damage due to Leech Seed.", 1);

                //Finally, the enemy Pokemon takes that much damage.
                Damage(enemyPokemon, seedDamage, false);
            }

            //These are mirrors of the previous methods, but for the player's Pokemon instead of the AI's.

            if (playerPokemon.Status == "burn" || playerPokemon.Status == "poison")
            {
                Console.WriteLine("\n{0} lost some life due to its {1}!", playerPokemon.Name, playerPokemon.Status);

                int residualDamage = (int)Math.Round(((playerPokemon.MaxHP / 8.0)), 0, MidpointRounding.AwayFromZero);

                Program.Log(playerPokemon.Name + " is " + playerPokemon.Status + "ed, so it takes " + DamageMessageFormatter(residualDamage, playerPokemon.CurrentHP), 1);

                Damage(playerPokemon, residualDamage, false);
            }

            if (playerPokemon.leechSeed)
            {
                //Damage and healing due to Leech Seed.

                Console.WriteLine("\n{0}'s life was drained by Leech Seed!", playerPokemon.Name);

                int seedDamage = (int)Math.Round((playerPokemon.MaxHP / 8.0), 0, MidpointRounding.AwayFromZero);
                int seedHealing = seedDamage;

                if (seedDamage > playerPokemon.CurrentHP)
                    seedHealing = playerPokemon.CurrentHP;

                if (enemyPokemon.CurrentHP + seedHealing > enemyPokemon.MaxHP)
                    seedHealing = enemyPokemon.MaxHP - enemyPokemon.CurrentHP;

                enemyPokemon.CurrentHP += seedHealing;

                Program.Log(playerPokemon.Name + " is affected by Leech Seed, so it takes " + DamageMessageFormatter(seedDamage, playerPokemon.CurrentHP), 1);
                Program.Log(enemyPokemon.Name + " heals " + seedHealing + " points of damage due to Leech Seed.", 1);

                Damage(playerPokemon, seedDamage, false);
            }
        }

        #endregion

        #region Checks

        /// <summary>
        /// This method calculates whether a move will hit or not by invoking a random number generator.
        /// </summary>
        /// <returns>Returns true if the Pokemon will hit, or false if it will miss.</returns>
        bool HitCheck()
        {
            //If the attack's accuracy is lower than a randomly generated number or the move has perfect accuracy, the attack hits.
            if (rng.Next(1, 101) < currentMove.Accuracy || currentMove.PerfectAccuracy)
            {
                return true;
            }

            //Otherwise, the attack misses and the Pokemon is no longer move-locked, if it was.
            else
            {
                Program.Log("The attack missed.", 1);
                Console.WriteLine("The attack missed!");

                CancelMoveLock(attackingPokemon);

                return false;
            }
        }

        /// <summary>
        /// Determines if a Pokemon is paralyzed and whether it will succesfully attack if it is.
        /// </summary>
        /// <returns>Returns true if the Pokemon is not paralyzed or if it will succesfully attack.</returns>
        bool ParalysisCheck()
        {
            if (attackingPokemon.Status == "paralysis")
            {
                //If a randomly generated number is higher than 25, the Pokemon attacks succesfully.
                if (rng.Next(1, 101) > 25)
                    return true;

                //Otherwise, the Pokemon is prevented from attacking and is no longer move-locked, if it was.
                else
                {
                    Program.Log(attackingPokemonName + " was paralyzed and didn't attack.", 1);
                    Console.WriteLine("\n{0} is paralyzed! It can't move!", attackingPokemonName);

                    CancelMoveLock(attackingPokemon);

                    return false;
                }
            }

            else
                return true;

        }

        /// <summary>
        /// Determines if a Pokemon is asleep, and whether it will wake up in order to attack if it is. It also adjusts the sleep counter.
        /// </summary>
        /// <returns>Returns true if the Pokemon is not asleep or if the Pokemon will wake up and attack.</returns>
        bool SleepCheck()
        {
            if (attackingPokemon.Status == "sleep")
            {
                //If the Pokemon was asleep and its sleep counter has hit 0, it wakes up.
                if (attackingPokemon.sleepCounter == 0)
                {
                    Program.Log(attackingPokemonName + " woke up.", 1);
                    Console.WriteLine("\n{0} woke up!", attackingPokemonName);

                    attackingPokemon.Status = "";

                    return true;
                }

                else
                {
                    Program.Log(attackingPokemonName + " didn't attack due to sleep. Remaining sleep turns: " + attackingPokemon.sleepCounter, 1);
                    Console.WriteLine("\n{0} is fast asleep.", attackingPokemonName);

                    attackingPokemon.sleepCounter--;

                    return false;
                }
            }

            else
                return true;
        }

        #endregion

        #region Effect Resolution

        /// <summary>
        /// This method handles the resolution of the effects of moves, even if they're secondary effects.
        /// It takes a move's effect ID into consideration, and uses a single 1-100 random number generator to calculate probability.
        /// </summary>
        /// <param name="attacker"></param>
        void Effect(string attacker)
        {
            int chance = rng.Next(1, 101);

            if (currentMove.SecondaryEffect)
            {
                Program.Log("Effect resolution takes place. Seed = " + chance.ToString(), 1);
                Program.Log("If the Seed number is smaller than the move's Chance, the effect will go through. ", 1);
            }

            switch (currentMove.EffectID)
            {
                case 1: //% Based Burn

                    if (chance < currentMove.EffectN)
                    {
                        if (defendingPokemon.Status == "" && !defendingPokemon.TypeCheck("Fire"))
                        {
                            Program.Log(defendingPokemonName + " gets burnt by " + currentMove.Name + ". Chance = " + currentMove.EffectN, 1);

                            Console.WriteLine("{0} got burnt by the attack!", defendingPokemonName);
                            defendingPokemon.Status = "burn";
                        }
                    }

                    break;

                case 2: //% Based Paralysis

                    if (chance < currentMove.EffectN)
                    {
                        if (defendingPokemon.Status == "" && !defendingPokemon.TypeCheck("Electric"))
                        {
                            Program.Log(defendingPokemonName + " gets paralyzed by " + currentMove.Name + ". Chance = " + currentMove.EffectN, 1);

                            Console.WriteLine("{0} got paralyzed by the attack!", defendingPokemonName);
                            defendingPokemon.Status = "paralysis";
                        }
                    }

                    break;

                case 3: //% Based Poison

                    if (chance < currentMove.EffectN)
                    {
                        if (defendingPokemon.Status == "" && !defendingPokemon.TypeCheck("Poison"))
                        {
                            Program.Log(defendingPokemonName + " gets poisoned by " + currentMove.Name + ". Chance = " + currentMove.EffectN, 1);

                            Console.WriteLine("{0} got poisoned by the attack!", defendingPokemonName);
                            defendingPokemon.Status = "poison";
                        }
                    }

                    break;

                case 5: //Leech Seed

                    if (!defendingPokemon.leechSeed && !defendingPokemon.TypeCheck("Grass"))
                    {
                        Program.Log(defendingPokemonName + " gets afflicted by Leech Seed.", 1);

                        Console.WriteLine("{0} got seeded!", defendingPokemonName);

                        defendingPokemon.leechSeed = true;
                    }

                    else
                        Console.WriteLine("The move failed!");

                    break;

                case 6: //Poison

                    if (defendingPokemon.Status == "" && !defendingPokemon.TypeCheck("Poison"))
                    {
                        Program.Log(defendingPokemonName + " gets poisoned by " + currentMove.Name + ".", 1);

                        Console.WriteLine("{0} got poisoned by the attack!", defendingPokemonName);

                        defendingPokemon.Status = "poison";
                    }

                    else
                        Console.WriteLine("The move failed!");

                    break;

                case 7: //Sleep

                    if (defendingPokemon.Status == "")
                    {
                        int sleepRNG = rng.Next(1, 101);

                        if (sleepRNG < 31)
                            defendingPokemon.sleepCounter = 1;

                        else if (sleepRNG < 61)
                            defendingPokemon.sleepCounter = 3;

                        else
                            defendingPokemon.sleepCounter = 2;

                        Program.Log(defendingPokemonName + " gets slept by " + currentMove.Name + " for " + defendingPokemon.sleepCounter.ToString() + " turns.", 1);

                        Console.WriteLine("{0} fell asleep!", defendingPokemonName);

                        defendingPokemon.Status = "sleep";
                    }

                    else
                        Console.WriteLine("The move failed!");

                    break;

                case 11: //Rapid Spin

                    RapidSpin(attackingPokemon);

                    break;

                case 12: //Protect

                    if (attacker == "player" && previousPlayerMove.EffectID != 12 || attacker == "AI" && previousEnemyMove.EffectID != 12)
                    {
                        Program.Log(attackingPokemonName + " is protected for this turn.", 1);

                        Console.WriteLine("{0} protected itself!", attackingPokemonName);

                        attackingPokemon.protect = true;
                    }

                    else
                        Console.WriteLine("The move failed!");

                    break;

                case 14: //Disable

                    break;

                case 15: //% based confusion

                    break;

                case 17: //Item steal

                    break;

                case 20: //Confusion

                    break;

                default: //Foolproof.
                    {
                        Console.WriteLine("Uh oh, something went wrong. The game tried to resolve an effect whose ID isn't");
                        Console.WriteLine("handled by the the effect resolution program. Please contact the author with");
                        Console.WriteLine("your log.txt file so he can figure how that happened. :|");

                        Program.Log("The game tried to resolve the effect of " + currentMove.Name + " (Effect ID: " + currentMove.EffectID + "), which does not exist.", 2);

                        break;
                    }
            }

            Program.Log("All effects have been resolved.", 0);
        }

        #endregion

        #region Damage, Fainting & Experience

        /// <summary>
        /// //This method deals with damage being dealt to a Pokemon.
        /// </summary>
        /// <param name="pokemon">The Pokemon object that the damage is being dealt to.</param>
        /// <param name="damage">The amount of damage that would be dealt.</param>
        /// <param name="displayMessage">Determines whether a message will be displayed that </param>
        void Damage(Pokemon pokemon, int damage, bool displayMessage)
        {
            Program.Log("The game checks whether a Pokemon has fainted.", 0);

            //If the Pokemon will not faint from the incurred damage...
            if (pokemon.CurrentHP > damage)
            {
                //The damage is detracted from its HP.
                pokemon.CurrentHP -= damage;

                //If the displayMessage flag is up, and the battle is not over, its life
                if (displayMessage && !battleOver)
                {
                    Program.Log("No Pokemon has fainted, so the game prints " + pokemon.Name + "'s remaining HP.", 0);

                    if (pokemon == enemyPokemon)
                        Console.WriteLine("Enemy {0}'s remaining HP: {1}%", enemyPokemon.Name, enemyPokemon.PercentLife());

                    if (pokemon == playerPokemon)
                        Console.WriteLine("{0}'s remaining HP: {1}", playerPokemon.Name, playerPokemon.CurrentHP);
                }
            }

            //Else, if it did faint...
            else
            {
                //Its life gets set to 0.
                pokemon.CurrentHP = 0;

                //Then, depending on which Pokemon fainted, the game goes into the corresponding faint handling method.

                if (enemyPokemon.Fainted)
                {
                    Program.Log("The AI's Pokemon fainted.", 1);

                    Console.WriteLine("\nThe enemy {0} fainted!", enemyPokemon.Name);

                    AIPokemonFainted();
                }

                else if (playerPokemon.Fainted)
                {
                    Program.Log("The player's Pokemon fainted.", 1);

                    Console.WriteLine("\n{0} fainted!", playerPokemon.Name);

                    PlayerPokemonFainted();
                }
            }
        }

        /// <summary>
        /// Code that triggers when the player's Pokemon faints. 
        /// </summary>
        void PlayerPokemonFainted()
        {
            //First, the game checks if there are any Pokemon in the player's party that are still healthy.
            if (Overworld.player.party.Exists(pokemon => !pokemon.Fainted))
            {
                Program.Log("The player has remaining healthy Pokemon.", 0);

                Console.WriteLine("\nSend out which Pokemon?\n(Valid input: 1-{0})\n", Overworld.player.party.Count);

                Pokemon pokemon = Overworld.player.SelectPokemon(true);

                //If the Pokemon the user selected is alive, it becomes the active Pokemon.
                if (!pokemon.Fainted)
                {
                    Program.Log("The player switches " + playerPokemon.Name + " out for " + pokemon.Name + ".", 1);

                    participants.Remove(playerPokemon);

                    playerPokemon = pokemon;

                    AddToParticipants(playerPokemon);

                    Console.WriteLine("{0} was sent out!", playerPokemon.Name);
                }

                else
                {
                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    Console.WriteLine("That Pokemon has fainted!");

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

        /// <summary>
        /// Code that triggers when the AI's Pokemon faints.
        /// </summary>
        void AIPokemonFainted()
        {
            //First, experience is awarded to all of the player's Pokemon that participated in the battle.
            foreach (Pokemon p in participants)
            {
                if (!p.Fainted && p.Level < 101)
                    Experience(p);
            }

            //If the AI has any remaining healthy Pokemon, it sends out its next Pokemon, based on the aiCurrentPokemonIndex index number.     
            if (encounterType == "trainer" && enemyTrainer.party.Exists(pokemon => !pokemon.Fainted))
            {
                aiCurrentPokemonIndex++;
                enemyPokemon = enemyTrainer.party.ElementAt(aiCurrentPokemonIndex);

                Console.WriteLine("\n{0} sent out a level {1} {2}!\n", enemyTrainer.Name, enemyPokemon.Level, enemyPokemon.Name);
                Program.Log("The AI has more Pokemon, so it sends out " + enemyPokemon.Name + ".", 1);

                participants.Clear();
                AddToParticipants(playerPokemon);

                Overworld.player.AddToSeen(enemyPokemon.Name);

                Actions();
            }

            //Otherwise, if the AI has no more Pokemon, or if the battle was with a wild Pokemon, the player wins.
            else
                Victory();
        }

        /// <summary>
        /// Experience calculation and award code. Right now there's a flat 300 experience threshold per level, soon to change.   
        /// </summary>
        /// <param name="pokemon">The Pokemon that will receive the experience.</param>
        void Experience(Pokemon pokemon)
        {
            float multiplier = 1; //This is a band-aid multiplier that simply makes trainer battles give more experience.

            if (encounterType == "trainer")
                multiplier = 1.3f;

            /* The amount of experience actually awarded for defeating another Pokemon.
             * This is actually an expression of percentage for the current level.
             * I.E., if an enemy Pokemon would yield 1.5 expYield experience, it would award 
             * the current Pokemon 450 exp using the 300 exp / level formula, so 1.5 level.
             * In a system with incremental experience thresholds per level, it would still be
             * 450 exp, which would amount to less than 1.5 level this time around.
            */

            float expYield;

            if (pokemon.Level <= enemyPokemon.Level)
                expYield = ((enemyPokemon.Level - pokemon.Level) * 0.33f) + 0.45f;

            else
                expYield = ((enemyPokemon.Level - pokemon.Level) * 0.05f) + 0.45f;

            //Program.Log("Exp yield was " + expYield.ToString(), 1);
            //The above log code helped me test exp yield, keeping it here in case it comes in handy.

            //If expYield would amount to less than 10% of a level (0.1), it instead becomes 0.1.
            if (expYield < 0.1f)
                expYield = 0.1f;

            int expGain = (int)(expYield * 300 * multiplier / 1.3f);

            pokemon.Experience += expGain;

            Program.Log(pokemon.Name + " received " + expGain + " experience.", 1);

            //This is a while loop to facilitate for the case that a Pokemon gains more than 1 level at a time.
            while (pokemon.Experience >= 300)
                pokemon.LevelUp();
        }

        #endregion

        #region Victory & Defeat
        /// <summary>
        /// The "You win!" code.
        /// </summary>
        void Victory()
        {
            BattleOverAdjustments();

            Program.Log("The player has won, so battleOver is now true.", 1);

            Console.WriteLine("\nCongratulations! You won!\n");

            //If the player was battling a trainer, he is awarded money. Then, the trainer's defeat speech plays.
            if (encounterType == "trainer")
            {
                Program.Log("The user was battling a trainer so he received money. The trainer's defeat speech plays.", 0);

                Overworld.player.Money += enemyTrainer.Money;
                enemyTrainer.Defeat();
            }

            else if (encounterType == "wild")
                Program.Log("The player was battling a wild Pokemon, and will now return to the overworld.", 0);
        }

        /// <summary>
        /// The "You lose!" code.
        /// </summary>
        void Defeat()
        {
            BattleOverAdjustments();

            Program.Log("The player has lost, so battleOver is now true.", 1);

            Console.WriteLine("\nOh no! All of your Pokemon have fainted!");

            int moneyLoss = (int)(Math.Floor(Overworld.player.Money * 0.05));

            Program.Log("The player lost $" + moneyLoss + " for blacking out. ($" + (Overworld.player.Money - moneyLoss) + " left)", 1);

            Overworld.player.Money -= moneyLoss;

            Console.WriteLine("You have lost ${0} for blacking out.", moneyLoss);

            //If the player was battling a trainer, the trainer's victory speech plays.
            if (encounterType == "trainer")
                enemyTrainer.Victory();

            else if (encounterType == "wild")
                Overworld.player.BlackOut();
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
            if (encounterType == "wild")
            {
                CancelMoveLock(playerPokemon);

                int escapeFactor = (((playerPokemon.Speed * 32) / (enemyPokemon.Speed / 4)) + 30) * escapeAttempts; ;
                int chanceToEscape = rng.Next(0, 256);

                //To explain all this - the player's escape factor is calculated using the above equation, that takes into account
                //the player's Pokemon's speed, the wild Pokemon's speed, and the number of times the player has attempted to escape.
                if (escapeFactor > 255 || escapeFactor > chanceToEscape)
                {
                    //If the player's escape factor is higher than 255 or than a randomly generated chanceToEscape number, the player succesfully escapes.
                    Program.Log("The player successfully escaped. (Seed = " + chanceToEscape.ToString() + ", Rate = " + escapeFactor.ToString() + ")" +
                                    " The player should now return to the overworld.", 1);

                    Console.WriteLine("Escaped succesfully!\n");

                    BattleOverAdjustments();

                    return;
                }

                else
                {
                    //If not, the player is unsuccesful at escaping. The number of attempts increases, and 
                    //the AI gets to attack once before the player is sent back to the Actions menu.
                    Program.Log("The player was unsuccesful at escaping, so the AI will now attack.", 1);

                    Console.WriteLine("Couldn't run away!");

                    escapeAttempts++;
                    AIAttack();
                }
            }

            //If not, an error is shown and the player is taken back to the Actions screen.
            else
            {
                Program.Log("The player tried to escape from a trainer. Returning to Actions.", 0);

                Console.WriteLine("You can't run away from a trainer fight!\n");

                Actions();
            }
        }

        /// <summary>
        /// This code handles catching wild Pokemon.
        /// </summary>
        void Catch()
        {
            //First, the program examines whether it is currently in a wild fight. If not, an error is shown and the player is taken back to the menu.
            Program.Log("The player chose to attempt to catch the Pokemon.", 0);

            if (encounterType == "wild")
            {
                //Then, it checks to see if the player has any remaining Poke Balls. If he does, one of them is used up.
                if (Overworld.player.items.Contains(Overworld.player.items.Find(i => i.Type == "pokeball")))
                {
                    CancelMoveLock(playerPokemon);

                    Overworld.player.items.Find(i => i.Type == "pokeball").Remove(1, "throw");

                    Console.WriteLine("Threw a Poke Ball ({1} left) at the wild {0}! 1, 2, 3...\n", enemyPokemon.Name, ItemList.pokeball.Count);

                    float life = (float)enemyPokemon.PercentLife(); //The enemy Pokemon's life percentage.
                    float ballBonus = 1; //Each specific PokeBall's catch rate multiplier. (NYI)

                    //First, the chance to catch the Pokemon is calculated, using its current HP %, its individual catch rate, and the bonus multiplier from the PokeBall used.
                    //The result is a number ranging from 13 to 79, times ballBonus and divided by pokemon.CatchRate. This number expresses the % chance to catch the Pokemon.
                    float catchRate = ((100 - life) + ((life - 60) / 3)) * ballBonus / enemyPokemon.species.CatchRate;
                    int chance = rng.Next(1, 101);

                    //If the randomly generated number is smaller than the calculated catch rate, the Pokemon is caught.
                    if (catchRate > chance)
                    {
                        Program.Log("The player successfully caught the Pokemon. (Seed = " + chance.ToString() + ", Rate = " + catchRate.ToString() + ")" +
                                    " battleOver = true, and the player should now return to the overworld.", 1);

                        Console.WriteLine("Gotcha! The wild {0} was caught!", enemyPokemon.Name);

                        Overworld.player.AddPokemon(enemyPokemon, true);

                        Console.WriteLine("");

                        BattleOverAdjustments();

                        return;
                    }

                    //If the player fails to catch the enemy Pokemon, it gets to attack once, and then the player is sent to the Actions() menu again.
                    else
                    {
                        Program.Log("The player was unsuccesful at capturing, so the AI will now attack.", 1);

                        Console.WriteLine("Oh no! The wild {0} broke free!", enemyPokemon.Name);

                        AIAttack();
                    }
                }

                else
                {
                    Program.Log("The had no remaining PokeBalls. Returning to Actions.", 0);
                    Console.WriteLine("You don't have any remaining PokeBalls to use!\n");

                    Actions();
                }
            }

            else
            {
                Program.Log("The player tried to throw a PokeBall during a trainer fight. Returning to Actions.", 0);
                Console.WriteLine("You can't throw a PokeBall at a trainer's Pokemon!\n");

                Actions();
            }
        }

        #endregion

        #region Important Methods

        /// <summary>
        /// This method is invoked when the AI alone should attack.
        /// </summary>
        void AIAttack()
        {
            AIMoveSelection(); //The AI selects an attack,
            PreCombat("AI"); // then uses it.

            if (!battleOver)
            {
                EndOfTurn();

                if (!battleOver)
                {
                    Program.Log("The AI attacked. Returning to Actions.", 0);

                    Console.WriteLine("");

                    Actions();
                }
            }
        }

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
                return currentHP + " damage. (" + (damage - currentHP) + " overkill)";
        }

        #endregion

        #region Supplementary Methods

        /// <summary>
        /// This method adds a Pokemon to the "Participants" list, for the purpose of gaining experience.
        /// </summary>
        /// <param name="p">The Pokemon to add to the list.</param>
        void AddToParticipants(Pokemon p)
        {
            if (!participants.Contains(p))
                participants.Add(p);
        }

        /// <summary>
        /// This code restores the temporary variables of the battle and the various Pokemon to their default state.
        /// </summary>
        void BattleOverAdjustments()
        {
            foreach (Pokemon p in Overworld.player.party)
                p.RestoreTemporaryStatus(true);

            battleOver = true;
        }

        /// <summary>
        /// This cancels a Pokemon's move-lock.
        /// </summary>
        /// <param name="pokemon"></param>
        void CancelMoveLock(Pokemon pokemon)
        {


            if (pokemon.moveLocked)
                pokemon.moveLocked = false;
        }

        /// <summary>
        /// This method gets called up when Rapid Spin is used, which clears up the field off all hazards.
        /// TODO: Add to this when I add more hazards.
        /// </summary>
        /// <param name="pokemon"></param>
        void RapidSpin(Pokemon pokemon)
        {
            if (pokemon.leechSeed)
                pokemon.leechSeed = false;

            Console.WriteLine("{0} cleared all hazards off the field using Rapid Spin!", attackingPokemonName);
        }

        #endregion
    }
}
