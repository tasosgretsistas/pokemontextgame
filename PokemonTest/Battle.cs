using PokemonTextEdition.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextEdition
{
    class Battle
    {
        //A random number generator that all methods that require randomness will use.
        Random rng = new Random();

        //An object that reresents the current enemy trainer.
        Trainer enemyTrainer = new Trainer();

        //A list of Pokemon that participated in a battle against a particular Pokemon, for the purpose of experience calculation.
        List<Pokemon> participants = new List<Pokemon>();

        string encounterType; //Determines the type of the encounter.
        int aiCurrentPokemonIndex = 0; //This is used as a counter for the AI to switch Pokemon.

        //These empty objects are used to identify the player's selected Pokemon & move.
        Pokemon playerPokemon = new Pokemon();
        Moves playerMove = new Moves();

        //These, on the other hand, are used for the AI's Pokemon.
        Pokemon enemyPokemon = new Pokemon();
        Moves enemyMove = new Moves();

        //These variables are used to check whether a Pokemon is using the same move as before.
        //This is useful for moves that deal increased damage while being used consecutively.
        double sameMoveDamageBonus = 1;
        Moves previousPlayerMove = new Moves();
        Moves previousEnemyMove = new Moves();

        //Code to adjust combat calculations based on who's attacking and who's defending.
        Pokemon attackingPokemon = new Pokemon();
        Pokemon defendingPokemon = new Pokemon();
        Moves currentMove = new Moves();

        //These two strings make it easier to tell who's attacking and who's defending, as well as adjust messages accordingly.
        string attackerName = "";
        string defenderName = "";

        //A bool that tells the program that the battle is now over. Not the most elegant solution, but it works!
        bool fightOver = false;

        //An integer that counts how many times the player has tried to escape.
        int escapeAttempts = 1;


        /// <summary>
        /// This method is used to start a battle with a trainer or a special Pokemon. The Wild method is used for battles with wild Pokemon.
        /// </summary>
        /// <param name="t">The Trainer object for the enemy trainer.</param>
        /// <param name="type">A special tag to determine several in-battle factors based on the type of battle - i.e. special Pokemon, showcase battle, etc. Default is "trainer".</param>
        public void Start(Trainer t, string type)
        {
            //The code for starting a battle with a trainer. It requires two parameters - an enemy trainer object, 
            //as well as a type of encounter tag that is to be used for special battles.

            Program.Log("Battle against a trainer starts.", 1);

            PokemonSelection();

            enemyPokemon = t.party.ElementAt(0);
            enemyTrainer = t;
            encounterType = type;

            Console.WriteLine("\n{0} wants to battle!\n{0} sent out a level {1} {2}!\n", t.Name, enemyPokemon.level, enemyPokemon.name);

            Actions();
        }

        public void Wild(Pokemon e)
        {
            //The code for starting a battle with a wild Pokemon. It is virtually identical to the trainer battle method,
            //except it requires a Pokemon as an input, rather than a trainer.

            Program.Log("Battle against a wild Pokemon starts.", 1);

            PokemonSelection();

            enemyPokemon = e;
            encounterType = "wild";

            Console.WriteLine("\nA wild level {0} {1} appeared!\n", e.level, e.name);

            Actions();
        }

        void PokemonSelection()
        {
            //This method selects a starting Pokemon for the trainer by looping over 
            //all Pokemon in the player's party until it finds one that's not fainted.

            for (int i = 0; i < Overworld.player.party.Count; i++)
            {
                if (Overworld.player.party.ElementAt(i).currentHP != 0)
                {
                    playerPokemon = Overworld.player.party.ElementAt(i);

                    AddToParticipants(playerPokemon);

                    break;
                }
            }
        }

        void AddToParticipants(Pokemon p)
        {
            if (!participants.Contains(p))
                participants.Add(p);
        }

        void Actions()
        {
            //This is kind of a "main menu" screen for battles, from which the player picks an action.

            Program.Log("The player is taken to the Actions menu.", 1);

            Console.WriteLine("What will you do?\n(Available commands: (f)ight, (s)tatus, s(w)itch, (c)atch, (r)un)");

            string action = Console.ReadLine();

            if (action != "")
                Console.WriteLine("");

            //This switch handles player input. 
            switch (action)
            {
                case "Fight":
                case "fight":
                case "f":
                    Fight();
                    break;

                case "Status":
                case "status":
                case "s":
                    Status();
                    break;

                case "Switch":
                case "switch":
                case "w":
                    Switch();
                    break;

                case "Catch":
                case "catch":
                case "c":
                    Catch();
                    break;

                case "Run":
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

        void Status()
        {
            //A very simple status screen that displays the current status of the player's and the AI's Pokemon.

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

        void Switch()
        {
            //A screen for switching the active Pokemon.

            Program.Log("The player chooses to switch Pokemon.", 1);

            Console.WriteLine("Send out which Pokemon?\n(Valid input: 1-{0}, or press Enter to return.)\n", Overworld.player.party.Count);

            Pokemon pokemon = Overworld.player.SelectPokemon(false);

            //If the Pokemon the user selected is healthy and is not already the active Pokemon, it becomes the active Pokemon.
            //The AI also gets to attack once if the player succesfully changes Pokemon.
            if (pokemon.name != "Blank" && pokemon != playerPokemon && pokemon.currentHP > 0)
            {
                Program.Log("The player switches " + playerPokemon.name + " out for " + pokemon.name + ". The AI will now attack.", 1);

                CancelTemporaryEffects(playerPokemon);

                playerPokemon = pokemon;

                AddToParticipants(playerPokemon);

                Console.WriteLine("\n{0} was sent out!", playerPokemon.name);

                //The AI will attack once following the player's switch.
                AIAttack();
            }

            else if (pokemon.name != "Blank" && pokemon.currentHP <= 0)
            {
                Program.Log("The player chose to switch to a fainted Pokemon. Returning to Actions.", 0);
                Console.WriteLine("\nThat Pokemon has fainted!\n");

                Actions();
            }

            else if (pokemon.name != "Blank" && pokemon == playerPokemon)
            {
                Program.Log("The player chose to switch to the Pokemon that's already active. Returning to Actions.", 0);
                Console.WriteLine("\nThat Pokemon is already the active Pokemon!\n");

                Actions();
            }

            else
                Actions();           
        }

        void Fight()
        {
            //The first part of the fighting code. Here, the player selects a move.

            Program.Log("The player chooses to fight.", 1);

            //If the player's Pokemon is not move-locked, the player selects a move.
            if (!playerPokemon.moveLocked)
            {
                Console.WriteLine("Please select a move. (Valid input: 1-{0}, or press Enter to return.)\n", playerPokemon.knownMoves.Count);

                Moves tempMove = playerPokemon.SelectMove(false);

                //The player is asked to select a move. If his selecetion is correct, the operation goes on.
                if (tempMove.Name != "Blank")
                {
                    playerMove = tempMove;

                    Program.Log("The player selected the move " + playerMove.Name + ".", 0);

                    //The AI then selects a move.
                    AI();

                    //The program then goes into Speed priority calculation.
                    SpeedPriority();
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
                    AI();

                    //The program then goes into Speed priority calculation.
                    SpeedPriority();
                }
            }
        }

        void AI()
        {
            //The AI's attack code. It uses a random number generator to simulate the AI selecting attacks.
            //TODO: Add more complex logic.           

            //While the enemy's Pokemon is not move-locked, it picks a random move.
            if (!enemyPokemon.moveLocked)
                enemyMove = enemyPokemon.knownMoves.ElementAt(rng.Next(0, enemyPokemon.knownMoves.Count));

            //If the enemy Pokemon is movelocked, it just uses the same move again.
            else
                enemyMove = previousEnemyMove;

            Program.Log("The AI selected the move " + enemyMove.Name + ".", 0);
        }

        void SpeedPriority()
        {
            //Speed priority checking code. This method essentially determines how the turn will play out.

            Program.Log("The game goes into Speed Priority calculation.", 1);

            //Speed adjustments in case of paralysis.
            int playerSpeed = playerPokemon.speed;
            int enemySpeed = enemyPokemon.speed;

            if (playerPokemon.status == "paralysis")
                playerSpeed = (int)Math.Round((double)playerSpeed * 0.25);

            if (enemyPokemon.status == "paralysis")
                enemySpeed = (int)Math.Round((double)enemySpeed * 0.25);

            //If the player's Pokemon is faster or its move had higher priority, it goes first. 
            if (playerSpeed >= enemySpeed && playerMove.Priority >= enemyMove.Priority)
            {
                Program.Log("The player's Pokemon is faster, so it attacks first.", 0);

                PreBattle("player");

                //If the fight is not over and the enemy Pokemon has not fainted, it attacks.
                if (!fightOver && enemyPokemon == defendingPokemon)
                    PreBattle("AI");
            }

            //Otherwise, the AI's Pokemon goes first.
            else if (playerSpeed < enemySpeed || playerMove.Priority < enemyMove.Priority)
            {
                Program.Log("The AI's Pokemon is faster, so it attacks first.", 0);

                PreBattle("AI");

                //If the fight is not over and the player's Pokemon has not fainted, it attacks.
                if (!fightOver && playerPokemon == defendingPokemon)
                    PreBattle("player");
            }

            //Then, the end of turn effects are resolved, and if the fight is not over, the player is taken back to the Actions menu.   
            if (!fightOver)
            {
                EndTurn();

                if (!fightOver)
                {
                    Program.Log("The turn has ended. Returning to Actions.", 1);

                    Console.WriteLine("");

                    Actions();
                }
            }
        }

        void PreBattle(string attacker)
        {
            //Quick pre-battle calculations and helpful message adjustments.

            Program.Log("The " + attacker + " attacks.", 1);

            if (attacker == "player")
            {
                attackingPokemon = playerPokemon;
                defendingPokemon = enemyPokemon;
                currentMove = playerMove;
                attackerName = playerPokemon.name;
                defenderName = "The enemy " + enemyPokemon.name;

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
                attackerName = "The enemy " + enemyPokemon.name;
                defenderName = playerPokemon.name;

                //If the selected move does double damage after being used consecutively and was used last turn, sameMoveDamageBonus becomes 2.
                if (enemyMove == previousEnemyMove && enemyMove.EffectID == 4)
                    sameMoveDamageBonus = 2;
                else
                    sameMoveDamageBonus = 1;

                previousEnemyMove = enemyMove;
            }

            if (attackingPokemon.status == "paralysis" && !Paralyzed() || attackingPokemon.status == "sleep" && !Asleep() || attackingPokemon.status == "" || attackingPokemon.status == "poison")
            {
                Console.WriteLine("\n{0} used {1}!", attackerName, currentMove.Name);

                if (defendingPokemon.protect)
                    Console.WriteLine("{0} was protected from the attack!", defenderName);

                else
                {
                    double typeMod = TypeChart.Check(currentMove, defendingPokemon.type, defendingPokemon.type2);

                    if (typeMod == 0.0)
                    {
                        Console.WriteLine("{0} was immune to the attack!", defenderName);
                    }

                    else
                    {
                        //If a player uses a move that locks you into a move, this sets the moveLocked flag to true.
                        if (currentMove.EffectID == 4)
                        {
                            Program.Log(attackingPokemon.name + " used " + currentMove + ", which move-locked it.", 0);
                            attackingPokemon.moveLocked = true;
                        }

                        if (currentMove.Attribute == "Status")
                        {
                            Program.Log("The attack selected by the " + attacker + " does not deal damage, so Effect() will take place.", 0);

                            if (Hit())
                                Effect(attacker);
                        }

                        else
                        {
                            Program.Log("The attack selected by the " + attacker + " deals damage, so Damage() will take place.", 0);

                            if (Hit())
                                Damage(attacker, typeMod);
                        }
                    }
                }
            }
        }

        bool Hit()
        {
            //This method calculates whether a move will hit or not.

            //If the attack's accuracy is lower than a randomly generated number or the move has perfect accuracy, the attack hits.
            if (rng.Next(1, 101) < currentMove.Accuracy || currentMove.PerfectAccuracy)
            {
                return true;
            }

            //Otherwise, the attack misses and the Pokemon is no longer move-locked, if it was.
            else
            {
                Program.Log("The attack missed.", 0);
                Console.WriteLine("The attack missed!");

                CancelMoveLock(attackingPokemon);

                return false;
            }
        }

        bool Paralyzed()
        {
            //This method calculates whether a paralyzed Pokemon will attack or not.

            //If a randomly generated number is higher than 25, the Pokemon attacks.
            if (rng.Next(1, 101) > 25)
            {
                return false;
            }

            //Otherwise, the attack misses and the Pokemon is no longer move-locked, if it was.
            else
            {
                Program.Log(attackerName + " was paralyzed and didn't attack.", 0);
                Console.WriteLine("{0} is paralyzed! It can't move!", attackerName);

                CancelMoveLock(attackingPokemon);

                return true;
            }
        }

        bool Asleep()
        {
            //This method handles sleep and the sleep counter.

            //If the Pokemon was asleep and its sleep counter has hit 0, it wakes up.
            if (attackingPokemon.sleepCounter == 0)
            {
                Program.Log(attackerName + " woke up.", 0);
                Console.WriteLine("{0} woke up!", attackerName);

                attackingPokemon.status = "";

                return false;
            }

            else
            {
                Program.Log(attackerName + " didn't attack due to sleep. Remaining sleep turns: " + attackingPokemon.sleepCounter, 0);
                Console.WriteLine("{0} is fast asleep.", attackerName);

                attackingPokemon.sleepCounter--;

                return true;
            }
        }

        void Damage(string attacker, double mod)
        {
            //This code that handles damage calculations.

            double damage = 0; //The total damage inflicted to the defending Pokemon after all calculations are done. 
            int previousHP = defendingPokemon.currentHP; //This is used to accurately calculate how much damage the defending Pokemon took. 

            double statusModifier = 1; //A modifier depending on the attacking Pokemon's status.
            double multiplier = 1; //An all-purpose multiplier for the final damage calculation.

            //This represents the relational modifier between the attack's type and the defending Pokemon's type(s).
            //The TypeChart.Check method is invoked to produce a number that will get plugged directly into the damage formula.
            double typeMod = mod;

            int numberOfHits = 1, hits = 1; //The number of times an attack will strike, for multi-hit attacks.

            Program.Log(attackerName + " attacks " + defenderName + " (" + defendingPokemon.currentHP + "/" + defendingPokemon.maxHP + " HP) with "
                        + currentMove.Name + " (typeMod: " + typeMod.ToString() + ")", 1);

            //Multi-hit attack calculation.
            if (currentMove.EffectID == 13)
            {
                if (currentMove.EffectN == 2)
                    numberOfHits = 2; //If the attack is a 2-hit attack, it will hit twice.

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
                damage = currentMove.EffectN; //If the selected move does a set amount of damage, the game skips damage calculation and goes damage gets dealt directly.

            else
            {
                do //This is a do-while loop to facilitate for the multiple hits scenario.
                {
                    //The next part prints a message explaining the result of the TypeChart calculation to the player.
                    if (typeMod == 2.0 || typeMod == 4.0)
                    {
                        Console.WriteLine("It's super effective!");
                    }

                    else if (typeMod == 0.5 || typeMod == 0.25)
                    {
                        Console.WriteLine("It's not very effective!");
                    }

                    //Same-Type Attack Bonus check. If the Pokemon's attack is the same type as the Pokemon itself, it receives a 50% damage boost.
                    if (attackingPokemon.type == currentMove.Type || attackingPokemon.type2 == currentMove.Type)
                    {
                        Program.Log("The attack has STAB.", 0);

                        multiplier *= 1.5;
                    }

                    int critical = rng.Next(1, 101);

                    //Critical hit calculator. I've set it to 10% chance and 20% extra damage for a crit as I dislike RNG.
                    if (critical < 11 || currentMove.EffectID == 9 && critical < 21)
                    {
                        Program.Log("The attack was a critical hit.", 0);

                        Console.WriteLine("A critical hit!");
                        multiplier *= 1.2;
                    }

                    //This code simply makes wild Pokemon do 20% less damage. But one day, they'll turn against us!
                    if (attacker == "AI" && encounterType == "wild")
                    {
                        Program.Log("Encounter type is 'wild', so the AI's attack deals 20% less damage.", 0);

                        multiplier *= 0.8;
                    }

                    //The overall multiplier is then calculated.
                    multiplier *= typeMod;

                    //If the attacking Pokemon is burned, the status modifier for physical attacks is set to 0.5.
                    if (attackingPokemon.status == "burn" && currentMove.Attribute == "Physical")
                    {
                        Program.Log(attackingPokemon.name + " was burned, so statusMod = 0.5.", 0);
                        statusModifier = 0.5;
                    }

                    //And this is where the damage is actually calculated. The multiplier only supports STAB and type related multipliers for now.
                    //Attack & defense or special attack & special defense are selected according to the move's attribute.            
                    if (currentMove.Attribute == "Physical")
                    {
                        damage += ((2.0 * (double)attackingPokemon.level + 10.0) / 250.0 * ((double)attackingPokemon.attack * statusModifier) /
                                            (double)defendingPokemon.defense * ((double)currentMove.Damage * sameMoveDamageBonus) + 2.0) * multiplier;
                    }
                    else if (currentMove.Attribute == "Special")
                    {
                        damage += ((2.0 * (double)attackingPokemon.level + 10.0) / 250.0 * (double)attackingPokemon.specialAttack /
                                            (double)defendingPokemon.specialDefense * ((double)currentMove.Damage * sameMoveDamageBonus) + 2.0) * multiplier;
                    }

                    //The minimum damage an attack can do is 1 so if the damage would be less than 1, damage becomes 1.
                    if (damage < 1)
                        damage = 1;

                    numberOfHits--;
                }
                while (numberOfHits >= 1);
            }

            if (currentMove.EffectID == 13)
                Console.WriteLine("The attack hit {0} times!", hits);

            //After the damage is calculated, it gets subtracted from the defending Pokemon's HP after being rounded down to the nearest integer.
            defendingPokemon.currentHP -= (int)(Math.Floor(damage));

            Program.Log(defenderName + " took " + (previousHP - defendingPokemon.currentHP).ToString() + " damage.", 1);

            //Finally, if the move has a secondary effect, it gets resolved.
            if (currentMove.SecondaryEffect)
            {
                Program.Log("The move has a secondary effect, so it will now be resolved.", 0);

                Effect(attacker);
            }

            //Recoil damage calculation.
            if (currentMove.EffectID == 8)
            {
                int recoil = (int)Math.Floor(damage * currentMove.EffectN);

                Program.Log(attackerName + " suffers " + recoil.ToString() + " damage in recoil.", 0);
                Console.WriteLine("\n{0} is damaged by recoil!", attackerName);

                attackingPokemon.currentHP -= recoil;
            }

            FaintCheck();

            //If the fight is not over due to the attack, the HP of the defending Pokemon is printed and the battle goes on.

            if (!fightOver && attacker == "player" && enemyPokemon == defendingPokemon)
            {
                Program.Log("No Pokemon has fainted, so the game prints " + enemyPokemon.name + "'s remaining HP.", 1);
                Console.WriteLine("Enemy {0}'s remaining HP: {1}%", enemyPokemon.name, enemyPokemon.PercentLife());
            }

            if (!fightOver && attacker == "AI" && playerPokemon == defendingPokemon)
            {
                Program.Log("No Pokemon has fainted, so the game prints " + playerPokemon.name + "'s remaining HP.", 1);
                Console.WriteLine("{0}'s remaining HP: {1}", playerPokemon.name, playerPokemon.currentHP);

            }
        }

        void Effect(string attacker)
        {
            //This method handles the various effects of moves, even if they're secondary effects.
            //It takes a move's effect ID into consideration, and uses a single 1-100 random number generator to calculate probability.

            int chance = rng.Next(1, 101);

            Program.Log("Effect resolution takes place. Seed = " + chance.ToString(), 1);
            Program.Log("If the Seed number is smaller than the move's Chance, the effect will go through. ", 1);

            switch (currentMove.EffectID)
            {
                case 1: //Effect ID 1: Moves that burn with a certain % chance.

                    if (chance < currentMove.EffectN)
                    {
                        if (defendingPokemon.status == "" && defendingPokemon.type != "Fire")
                        {
                            Program.Log(defenderName + " gets burnt by " + currentMove.Name + ". Chance = " + currentMove.EffectN, 0);

                            Console.WriteLine("{0} got burnt by the attack!", defenderName);
                            defendingPokemon.status = "burn";
                        }
                    }

                    break;

                case 2: //Effect ID 2: Moves that paralyze with a certain % chance.

                    if (chance < currentMove.EffectN)
                    {
                        if (defendingPokemon.status == "" && defendingPokemon.type != "Electric")
                        {
                            Program.Log(defenderName + " gets paralyzed by " + currentMove.Name + ". Chance = " + currentMove.EffectN, 0);

                            Console.WriteLine("{0} got paralyzed by the attack!", defenderName);
                            defendingPokemon.status = "paralysis";
                        }
                    }

                    break;

                case 3: //Effect ID 3: Moves that poison with a certain % chance.

                    if (chance < currentMove.EffectN)
                    {
                        if (defendingPokemon.status == "" && defendingPokemon.type != "Poison")
                        {
                            Program.Log(defenderName + " gets poisoned by " + currentMove.Name + ". Chance = " + currentMove.EffectN, 0);

                            Console.WriteLine("{0} got poisoned by the attack!", defenderName);
                            defendingPokemon.status = "poison";
                        }
                    }

                    break;

                case 5: //Effect ID 5: Leech Seed.

                    if (defendingPokemon.type != "Grass" && !defendingPokemon.leechSeed)
                    {
                        Program.Log(defenderName + " gets afflicted by Leech Seed.", 0);

                        Console.WriteLine("{0} got seeded!", defenderName);

                        defendingPokemon.leechSeed = true;
                    }

                    else
                    {
                        Console.WriteLine("The move failed!");
                    }

                    break;

                case 6: //Effect ID 6: Poison.

                    if (defendingPokemon.status == "" && defendingPokemon.type != "Poison")
                    {
                        Program.Log(defenderName + " gets poisoned by " + currentMove.Name + ".", 0);

                        Console.WriteLine("{0} got poisoned by the attack!", defenderName);

                        defendingPokemon.status = "poison";
                    }

                    else
                    {
                        Console.WriteLine("The move failed!");
                    }

                    break;

                case 7: //Effect ID 7: Sleep.

                    if (defendingPokemon.status == "")
                    {
                        int sleepRNG = rng.Next(1, 101);

                        if (sleepRNG < 31)
                            defendingPokemon.sleepCounter = 1;

                        else if (sleepRNG < 61)
                            defendingPokemon.sleepCounter = 3;

                        else
                            defendingPokemon.sleepCounter = 2;

                        Program.Log(defenderName + " gets slept by " + currentMove.Name + ". Duration: " + defendingPokemon.sleepCounter.ToString() + " turns.", 0);

                        Console.WriteLine("{0} fell asleep!", defenderName);

                        defendingPokemon.status = "sleep";
                    }

                    else
                    {
                        Console.WriteLine("The move failed!");
                    }

                    break;

                case 11: //Effect ID 11: Rapid Spin.

                    RapidSpin(attackingPokemon);

                    break;

                case 12: //Effect ID 12: Protect.

                    if (attacker == "player" && previousPlayerMove.EffectID != 12 || attacker == "AI" && previousEnemyMove.EffectID != 12)
                    {
                        Program.Log(attackerName + " is protected for this turn.", 0);

                        Console.WriteLine("{0} protected itself!", attackerName);

                        attackingPokemon.protect = true;
                    }

                    else
                    {
                        Console.WriteLine("The move failed!");
                    }

                    break;
            }

            Program.Log("All effects have been resolved.", 0);
        }

        void EndTurn()
        {
            //The effects that take place at the end of the turn are resolved in this method.

            Program.Log("End of turn resolution takes place.", 1);

            playerPokemon.protect = false;
            enemyPokemon.protect = false;

            if (playerPokemon.status == "burn" || playerPokemon.status == "poison")
            {
                Program.Log(playerPokemon.name + " is " + playerPokemon.status + "ed, so it loses HP.", 0);

                Console.WriteLine("\n{0} lost some life to its {1}!", playerPokemon.name, playerPokemon.status);
                playerPokemon.currentHP -= (int)(Math.Floor((double)(playerPokemon.maxHP / 8.0)));

                FaintCheck();
            }

            if (playerPokemon.leechSeed)
            {
                Program.Log(playerPokemon.name + " is affected by Leech Seed, so it loses HP.", 0);

                Console.WriteLine("\n{0}'s life was drained by Leech Seed!", playerPokemon.name);

                int seedDamage = (int)(Math.Floor((double)(playerPokemon.maxHP / 8.0)));

                playerPokemon.currentHP -= seedDamage;
                enemyPokemon.currentHP += seedDamage;

                FaintCheck();
            }

            if (enemyPokemon.status == "burn" || enemyPokemon.status == "poison")
            {
                Program.Log(enemyPokemon.name + " is " + enemyPokemon.status + "ed, so it loses HP.", 0);

                Console.WriteLine("\nThe enemy {0} lost some life to its {1}!", enemyPokemon.name, enemyPokemon.status);
                enemyPokemon.currentHP -= (int)(Math.Floor((double)(enemyPokemon.maxHP / 8.0)));

                FaintCheck();
            }

            if (enemyPokemon.leechSeed)
            {
                Program.Log(enemyPokemon.name + " is affected by Leech Seed, so it loses HP.", 0);

                Console.WriteLine("\n{0}'s life was drained by Leech Seed!", enemyPokemon.name);

                int seedDamage = (int)(Math.Floor((double)(enemyPokemon.maxHP / 8.0)));

                playerPokemon.currentHP += seedDamage;
                enemyPokemon.currentHP -= seedDamage;
            }
        }

        void FaintCheck()
        {
            //This method checks whether any Pokemon have fainted. If so, it goes to the respective Faint() screen.
            //It returns a string so that the program knows whose Pokemon fainted - the player's, or the AI's.

            Program.Log("The game checks whether a Pokemon has fainted.", 1);

            if (playerPokemon.currentHP <= 0)
            {
                Program.Log("The player's Pokemon fainted.", 1);

                Console.WriteLine("{0} fainted!", playerPokemon.name);

                playerPokemon.Faint();

                Faint();
            }

            else if (enemyPokemon.currentHP <= 0)
            {
                Program.Log("The AI's Pokemon fainted.", 1);

                Console.WriteLine("The enemy {0} fainted!", enemyPokemon.name);

                enemyPokemon.Faint();

                AIFaint();
            }
        }

        void Faint()
        {
            //Code that triggers when the player's Pokemon faints.        

            //First, the game checks if there are any Pokemon in the player's party that are still healthy.
            if (Overworld.player.party.Exists(pokemon => pokemon.currentHP != 0))
            {
                Program.Log("The player has remaining healthy Pokemon.", 0);

                Console.WriteLine("\nSend out which Pokemon?\n(Valid input: 1-{0})\n", Overworld.player.party.Count);

                Pokemon pokemon = Overworld.player.SelectPokemon(true);


                //If the Pokemon the user selected is alive, it becomes the active Pokemon.
                if (pokemon.name != "Blank" && pokemon.currentHP > 0)
                {
                    Program.Log("The player switches " + playerPokemon.name + " out for " + pokemon.name + ". Returning to Actions.", 1);

                    participants.Remove(playerPokemon);

                    playerPokemon = pokemon;

                    AddToParticipants(playerPokemon);

                    Console.WriteLine("\n{0} was sent out!", playerPokemon.name);
                }

                else
                {
                    Program.Log("The player selected a Pokemon that has fainted.", 0);
                    Console.WriteLine("That Pokemon has fainted!");

                    Faint();
                }
            }

            //Otherwise, if the player has no remaining healthy Pokemon, he is taken to the defeat screen.
            else
            {
                Program.Log("The player has no remaining healthy Pokemon.", 0);

                Defeat();
            }
        }

        void AIFaint()
        {
            //Code that triggers when the AI's Pokemon faints.

            //First, experience is awarded to all of the player's Pokemon that participated in the battle.
            foreach (Pokemon p in participants)
            {
                if (p.currentHP > 0 && p.level < 101)
                {
                    Experience(p);
                }
            }

            //If the AI has any remaining healthy Pokemon, it sends out its next Pokemon, based on the aiCurrentPokemonIndex index number.     
            if (encounterType == "trainer" && enemyTrainer.party.Exists(pokemon => pokemon.currentHP != 0))
            {
                aiCurrentPokemonIndex++;
                enemyPokemon = enemyTrainer.party.ElementAt(aiCurrentPokemonIndex);

                Console.WriteLine("\n{0} sent out a level {1} {2}!\n", enemyTrainer.Name, enemyPokemon.level, enemyPokemon.name);
                Program.Log("The AI has more Pokemon, so it sends out " + enemyPokemon.name + ". Returning to Actions.", 1);

                participants.Clear();
                AddToParticipants(playerPokemon);

                Actions();
            }

            //Otherwise, if the AI has no more Pokemon, or if the battle was with a wild Pokemon, the player wins.
            else
            {
                Victory();
            }
        }

        void Victory()
        {
            //The "You win!" code. 

            Console.WriteLine("\nCongratulations! You won!");

            fightOver = true;
            Program.Log("The AI has no more Pokemon, so the player wins. fightOver is now true.", 1);

            //If the player was battling a trainer, he is awarded money. Then, the trainer's defeat speech plays.
            if (encounterType == "trainer")
            {
                Program.Log("The user was battling a trainer so he received money. The trainer's defeat speech plays.", 0);

                Overworld.player.Money += enemyTrainer.Money;
                enemyTrainer.Defeat();
            }

            else if (encounterType == "wild")
            {
                Program.Log("The player was battling a wild Pokemon, and will now return to the overworld.", 0);
            }
        }

        void Defeat()
        {
            //The "You lose!" code - pretty much identical to the victory code, should probably make it a bit nicer.

            Console.WriteLine("\nOh no! All of your Pokemon have fainted!");

            fightOver = true;
            Program.Log("The player has lost, so fightOver is now true.", 1);
            Program.Log("The player will lose a small amount of money and then be taken back to the last Pokemon Center he visited.", 0);

            int moneyLoss = (int)(Math.Floor((double)Overworld.player.Money * 0.05));

            Console.WriteLine("You have lost ${0} for blacking out.\n", moneyLoss);
            Overworld.player.Money -= moneyLoss;

            //If the player was battling a trainer, the trainer's victory speech plays.
            if (encounterType == "trainer")
            {
                Program.Log("The player was battling a trainer. The trainer's victory speech plays.", 0);

                enemyTrainer.Victory();
            }

            else if (encounterType == "wild")
            {
                Console.WriteLine("You will now be taken to the last city you rested at.");

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();

                Overworld.player.PartyHeal();

                Overworld.LoadLocation(Overworld.player.LastHeal);
            }
        }

        void Experience(Pokemon p)
        {
            //Experience calculation and award code. Right now there's a flat 300 experience threshold per level, soon to change.   

            double multiplier = 1; //This is a band-aid multiplier that simply makes trainer battles give more experience.

            if (encounterType == "trainer")
                multiplier = 1.30;

            /* The amount of experience actually awarded for defeating another Pokemon.
             * This is actually an expression of percentage for the current level.
             * I.E., if an enemy Pokemon would yield 1.5 expYield experience, it would award 
             * the current Pokemon 450 exp using the 300 exp / level formula, so 1.5 level.
             * In a system with incremental experience thresholds per level, it would still be
             * 450 exp, which would amount to less than 1.5 level this time around.
            */
            double expYield;

            if (p.level <= enemyPokemon.level)
                expYield = (((double)enemyPokemon.level - (double)p.level) * 0.33) + 0.45;

            else
                expYield = (((double)enemyPokemon.level - (double)p.level) * 0.05) + 0.45;

            //Program.Log("Exp yield was " + expYield.ToString(), 1);
            //The above log code helped me test exp yield, keeping it here in case it comes in handy.

            //If expYield would amount to less than 10% of a level (0.1), it instead becomes 0.1.
            if (expYield < 0.1)
                expYield = 0.1;

            p.experience += Convert.ToInt32(expYield * 300.0 * multiplier / 1.3);

            Program.Log(p.name + " received " + Convert.ToInt32(expYield * 300.0 * multiplier / 1.4) + " experience.", 1);

            //This is a while loop to facilitate for the case that a Pokemon gains more than 1 level at a time.
            while (p.experience >= 300)
            {
                Program.Log(p.name + " levelled up.", 0);

                p.LevelUp();
            }
        }

        void Run()
        {
            //Code for escaping from battle.

            Program.Log("The player chose to run away.", 1);

            //First, the game examines whether it is currently in a wild fight. If so, the operation continues.
            if (encounterType == "wild")
            {
                CancelMoveLock(playerPokemon);

                int escapeFactor = (((playerPokemon.speed * 32) / (enemyPokemon.speed / 4)) + 30) * escapeAttempts; ;
                int chanceToEscape = rng.Next(0, 256);

                //To explain all this - the player's escape factor is calculated using the above equation, that takes into account
                //the player's Pokemon's speed, the wild Pokemon's speed, and the number of times the player has attempted to escape.
                if (escapeFactor > 255 || escapeFactor > chanceToEscape)
                {
                    //If the player's escape factor is higher than 255 or than a randomly generated chanceToEscape number, the player succesfully escapes.
                    Program.Log("The player successfully escaped. (Seed = " + chanceToEscape.ToString() + ", Rate = " + escapeFactor.ToString() + ")" +
                                    " The player should now return to the overworld.", 1);

                    Console.WriteLine("Escaped succesfully!");

                    CancelTemporaryEffects(playerPokemon);

                    fightOver = true;

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

        void Catch()
        {
            //Code for catching Pokemon wild Pokemon.

            //First, the program examines whether it is currently in a wild fight. If not, an error is shown and the player is taken back to the menu.
            Program.Log("The player chose to attempt to catch the Pokemon.", 1);

            if (encounterType == "wild")
            {
                //Then, it checks to see if the player has any remaining Poke Balls. If he does, one of them is used up.
                if (ItemsList.pokeball.Count > 0)
                {
                    CancelMoveLock(playerPokemon);

                    ItemsList.pokeball.Count--;

                    Console.WriteLine("Threw a Poke Ball ({1} left) at the wild {0}! 1, 2, 3...", enemyPokemon.name, ItemsList.pokeball.Count);

                    int life = enemyPokemon.PercentLife(); //The enemy Pokemon's life percentage.
                    int ballBonus = 1; //Each specific PokeBall's catch rate multiplier. (NYI)

                    //First, the chance to catch the Pokemon is calculated, using its current HP %, its individual catch rate, and the bonus multiplier from the PokeBall used.
                    //The result is a number ranging from 13 to 79, times ballBonus and divided by pokemon.CatchRate. This number expresses the % chance to catch the Pokemon.
                    double catchRate = ((100 - life) + ((life - 60) / 3)) * ballBonus / enemyPokemon.catchRate;
                    double chance = rng.Next(1, 101);

                    //If the randomly generated number is smaller than the calculated catch rate, the Pokemon is caught.
                    if (catchRate > chance)
                    {
                        Program.Log("The player successfully caught the Pokemon. (Seed = " + chance.ToString() + ", Rate = " + catchRate.ToString() + ")" +
                                    " fightOver = true, and the player should now return to the overworld.", 1);

                        Console.WriteLine("Gotcha! The wild {0} was caught!", enemyPokemon.name);

                        Overworld.player.AddPokemon(enemyPokemon, "catch");

                        CancelTemporaryEffects(playerPokemon);

                        fightOver = true;

                        return;
                    }

                    //If the player fails to catch the enemy Pokemon, it gets to attack once, and then the player is sent to the Actions() menu again.
                    else
                    {
                        Program.Log("The player was unsuccesful at capturing, so the AI will now attack.", 1);

                        Console.WriteLine("Oh no! The wild {0} broke free!", enemyPokemon.name);

                        AIAttack();
                    }
                }

                else
                {
                    Program.Log("The player derped - no more PokeBalls. Returning to Actions.", 0);
                    Console.WriteLine("You don't have any remaining PokeBalls to use!\n");

                    Actions();
                }
            }

            else
            {
                Program.Log("The player derped - trainer fight. Returning to Actions.", 0);
                Console.WriteLine("You can't throw a PokeBall at a trainer's Pokemon!\n");

                Actions();
            }
        }

        void AIAttack()
        {
            //This method gets called up when the AI alone should attack.

            AI(); //The AI selects an attack,
            PreBattle("AI"); // then uses it.

            if (!fightOver)
            {
                EndTurn();

                if (!fightOver)
                {
                    Program.Log("The AI attacked. Returning to Actions.", 0);

                    Console.WriteLine("");

                    Actions();
                }
            }
        }

        void CancelMoveLock(Pokemon pokemon)
        {
            //This cancels a Pokemon's move-lock.

            if (pokemon.moveLocked)
                pokemon.moveLocked = false;
        }

        void CancelTemporaryEffects(Pokemon pokemon)
        {
            //This method cancels all temporary effects affecting a Pokemon.

            if (pokemon.moveLocked)
                pokemon.moveLocked = false;

            if (pokemon.leechSeed)
                pokemon.leechSeed = false;

            if (pokemon.protect)
                pokemon.protect = false;
        }

        void RapidSpin(Pokemon pokemon)
        {
            //This method gets called up when Rapid Spin is used, which clears up the field off all hazards.
            //TODO: Add to this when I add more hazards.
            if (pokemon.leechSeed)
                pokemon.leechSeed = false;

            Console.WriteLine("{0} cleared all effects off the field using Rapid Spin!", attackerName);
        }
    }
}
