using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics.Eventing.Reader;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private Monster dragon;
        private Monster zombie;
        private Monster skeleton;
        private Monster spider;
        private Inventory inventory;
        private Weapon axe;
        private Weapon bow;
        private Weapon sword;
        private Weapon knife;
        private Weapon starterKnife;
        private Potion fullPotion;
        private Potion halfPotion;


        public Game()
        {
            // Initialize the game with one room and one player

            Console.WriteLine("What is your name? ");
            string name = Console.ReadLine();

            player = new Player(name, 20);
            currentRoom = new Room("The Room is dark and misty");
         
            dragon = new Monster("Dragon", 15, 7, "GGRRRAAAUUUGHHH!");
            zombie = new Monster("Zombie", 12, 5, "Uuurrgghhh");
            skeleton = new Monster("Skeleton", 10, 4, "clack clack clack");
            spider = new Monster("Spider", 7, 3, "skkssksskssk");

            axe = new Weapon("axe", 5);
            bow = new Weapon("bow", 3);
            sword = new Weapon("sword", 7);
            knife = new Weapon("knife", 4);
            starterKnife = new Weapon("rusty knife", 2);

            fullPotion = new Potion("Big Potion", 10);
            halfPotion = new Potion("Small Potion", 5);

            inventory = new Inventory();

        }
        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = true;
            bool opened = false;
            bool defeated = false;
            bool roomStarted = false;
            string currentMonsterName = "";
            string previousMonsterName;


            List<string> visitedRoom = new List<string>(); // Initializing a list of visited rooms
            List<string> foundWeapons = new List<string> (); // Initializing a list of items
            List<string> foundPotions = new List<string> (); // Initializing a list of items

            List<Monster> monsterList = new List<Monster>();
            List<Weapon> weaponList = new List<Weapon>();
            List<Potion> potionList = new List<Potion>();

            monsterList.Add(dragon);
            monsterList.Add(spider);
            monsterList.Add(skeleton);
            monsterList.Add(zombie);

            weaponList.Add(axe);
            weaponList.Add(bow);
            weaponList.Add(sword);
            weaponList.Add(knife);
            weaponList.Add(starterKnife);
            inventory.AddItem("rusty knife");

            potionList.Add(fullPotion);
            potionList.Add(halfPotion);

            string selectedItem = null; // Initializing a variable for a selected item
            while (playing)
            {
               
                string ChosenItem;
                // Code your playing logic here                

                // Generating a random number to select a random index from the ItemList
                Random rnd = new Random();
                int monsterIndex = rnd.Next(monsterList.Count);
                previousMonsterName = currentMonsterName;

                if (roomStarted == false)
                {
                    monsterIndex = rnd.Next(monsterList.Count);

                    currentMonsterName = monsterList[monsterIndex].name;

                    roomStarted = true;
                }

                else
                {
                    currentMonsterName = previousMonsterName;
                }

                previousMonsterName = currentMonsterName;


                if (defeated == false)
                {
                    Console.WriteLine($"The room {currentRoom.GetDescription()}, ahead of you there is a {currentMonsterName}");
                    Console.WriteLine("\nWould You Like to:\n 1) Look Around \n 2) Open the Chest \n 3) Check Your Current Status \n 4) Open Your Inventory \n 5) Fight The Monster Ahead Of You \n 6) Exit\n");
                }

                else if (defeated == true)
                {
                    Console.WriteLine($"The room {currentRoom.GetDescription()}");
                    Console.WriteLine("\nWould You Like to:\n 1) Look Around \n 2) Open the Chest \n 3) Check Your Current Status \n 4) Open Your Inventory \n 5) Move Onto The Next Room \n 6) Exit\n"); // Options for the user to progress
                }

                string ans = Console.ReadLine();

                if (ans == "1") // Provides the room description
                {
                    Console.WriteLine("");
                    Console.Write(currentRoom.GetDescription());
                    Console.WriteLine("");
                }

                else if (ans == "2") // Will open the chest in the room
                {
                    bool moveOnCondition = false;
                    if (opened == false) // Checking to see if the chest for that room has already been opened
                    {
                        if (inventory.IsFull(potionList, weaponList) == true) // Checking to see if ItemList is empty
                        {
                            Console.WriteLine("The Chest Is Empty !!! ");
                        }


                        else // If the ItemList is not empty then item is added to inventory
                        {
                            while (moveOnCondition == false)
                            {
                                int index = rnd.Next(weaponList.Count);
                                if (index > potionList.Count)
                                {
                                    ChosenItem = weaponList[index].name;
                                    if (foundWeapons.Contains(ChosenItem))
                                    {
                                    }
                                    else
                                    {
                                        inventory.AddItem(ChosenItem);
                                        foundWeapons.Add(ChosenItem);
                                        Console.WriteLine($"\n {ChosenItem} has been added to your inventory.");
                                        moveOnCondition = true;
                                    }

                                }

                                else if (index < potionList.Count)
                                {
                                    int index_2 = rnd.Next(2);
                                    if (index_2 == 0)
                                    {
                                        ChosenItem = weaponList[index].name;
                                        if (foundWeapons.Contains(ChosenItem))
                                        {
                                        }
                                        else
                                        {
                                            inventory.AddItem(ChosenItem);
                                            foundWeapons.Add(ChosenItem);
                                            Console.WriteLine($"\n {ChosenItem} has been added to your inventory.");
                                            moveOnCondition = true;
                                        }
                                    }

                                    else if (index_2 == 1)
                                    {
                                        ChosenItem = potionList[index].name;
                                        if (foundPotions.Contains(ChosenItem))
                                        {
                                        }
                                        else
                                        {
                                            inventory.AddItem(ChosenItem);
                                            foundPotions.Add(ChosenItem);
                                            Console.WriteLine($"\n {ChosenItem} has been added to your inventory.");
                                            moveOnCondition = true;
                                        }
                                    }

                                }
                            }
                            opened = true;
                        }

                    }

                    else if (opened == true) // If the chest has been opened
                    {
                        Console.WriteLine($"\n This Rooms Chest Has Already Been Opened! ");
                    }
                }

                else if (ans == "3") // If input is "3" then the inventory and current health is displayed
                {
                    if (inventory.GetInventory() == "") // Checking to see if the inventory is empty
                    {
                        Console.WriteLine("Your inventory is currently empty !!! ");
                    }

                    else
                    {
                        Console.WriteLine("Your inventory currently contains:");
                        Console.Write(inventory.GetInventory());
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Your current health is: ");
                    Console.Write(player.GetHealth());
                    Console.WriteLine("");


                }

                else if (ans == "4")
                {
                    if (inventory.GetInventory() == "") // Checking to see if the inventory is empty
                    {
                        Console.WriteLine("Your inventory is currently empty !!! ");
                    }

                    else
                    {

                        Console.WriteLine("Your inventory Currently Contains of:");
                        Console.Write(inventory.GetInventory());
                        Console.WriteLine("");
                        Console.WriteLine("Would you like to:\n 1) Configure Your Inventory \n 2) Select An Item \n 3) Continue");
                        string decision = Console.ReadLine();

                        if (decision == "1")
                        {
                            Console.WriteLine("Would you like to:\n 1) Delete An Item \n 2) Select An Item \n 3) View All Weapons \n 4) View All Potions \n 5) Continue");
                            string decision_2 = Console.ReadLine();

                            if (decision_2 == "1")
                            {
                                bool condition = true;

                                while (condition == true)
                                {
                                    Console.WriteLine("Type The Name Of The Item You Would Like to Remove, or Type 'menu' to return to the menu ");
                                    string deletedItem = Console.ReadLine();

                                    if (inventory.ContainsItem(deletedItem) == true)
                                    {
                                        inventory.RemoveItem(deletedItem);
                                        condition = false;
                                    }

                                    else if (deletedItem == "menu")
                                    {
                                        condition = false;
                                    }

                                    else if (inventory.ContainsItem(deletedItem) == false)
                                    {
                                        Console.WriteLine("Your Inventory Does Not Contain This Item");
                                    }
                                }


                            }

                            else if (decision_2 == "2")
                            {
                                selectedItem = inventory.SelectItem();
                            }

                            else if (decision_2 == "3")
                            {
                                Console.Write(inventory.ShowWeapons(weaponList));
                            }

                            else if (decision_2 == "4")
                            {
                                Console.Write(inventory.ShowPotions(potionList));
                            }

                            else if (decision_2 == "5")
                            {
                                continue;
                            }

                        }

                        else if (decision == "2")
                        {
                            selectedItem = inventory.SelectItem();
                        }


                        else if (decision == "3")
                        {
                            continue;
                        }
                    }
                }

                else if ((ans == "5" & defeated == true)) // If the input is "5" then the player will advance to another room
                {
                    string newDescription = currentRoom.RandRoom(visitedRoom);

                    if (newDescription == "") // If the returned value is blank, then every room has been visited already
                    {
                        Console.WriteLine("All rooms have been visited ");
                    }
                    else // Otherwise the new room is visited
                    {
                        currentRoom = new Room(newDescription);
                        visitedRoom.Add(newDescription); // Adding the current description to the visitedRoom list to ensure the same room is not revisited
                        opened = false;
                        roomStarted = false;
                        defeated = false;
                    }
                }

                else if (ans == "5" & defeated == false)
                {
                    bool exitCondition = false;
                    bool condition = false;
                    while (condition == false & exitCondition == false)
                    {
                        Console.WriteLine("Would you like to:\n 1) View Your Inventory \n 2) View Currently Selected Item \n 3) Use Item \n 4) Return To Menu ");
                        string decision = Console.ReadLine();

                        if (decision == "1")
                        {
                            Console.WriteLine($" Your Inventory Contains: {inventory.GetInventory()}");
                            Console.WriteLine("Would you like to:\n 1) Select A New Item \n 2) Continue ");
                            string decision_2 = Console.ReadLine();

                            if (decision_2 == "1")
                            {
                               selectedItem = inventory.SelectItem();
                                                                  
                            }

                            if (decision_2 == "2")
                            {
                                continue;
                            }
                        }

                        else if (decision == "2")
                        {
                            Console.WriteLine($"The Item That Is Currently Selected is: {selectedItem}");
                        }

                        else if (decision == "3")
                        {
                            if (selectedItem == null)
                            {
                                Console.WriteLine("No Item Is Currently Selected, Please Select An Item !!!");
                            }


                            else if ( (inventory.ShowPotions(potionList)).Contains(selectedItem ))
                            {
                                if (selectedItem == "Big Potion")
                                {
                                    fullPotion.UsePotion(player);
                                }

                                else if (selectedItem == "Small Potion")
                                {
                                    halfPotion.UsePotion(player);
                                }
                            }

                            else if ((inventory.ShowWeapons(weaponList)).Contains(selectedItem))
                            {
                                Monster currentMonsterObject = monsterList.FirstOrDefault(m => m.name == currentMonsterName);
                                player.DealDamage(currentMonsterObject, inventory.SelectWeapon(selectedItem, weaponList), currentMonsterName, defeated);
                                if (currentMonsterObject.health > 0 & defeated == false)
                                {
                                    player.TakeDamage(currentMonsterObject.damage);
                                }
                                if (currentMonsterObject.IsDead)
                                {
                                    defeated = true;
                                    currentMonsterObject.health = currentMonsterObject.maxHealth;
                                }
                                if (defeated == true)
                                {
                                    Console.WriteLine("Please Return To Menu");
                                }
                            }

                        }

                        else if (decision == "4")
                        {
                            exitCondition = true;
                        }
                    }
                }


                else if (ans == "6") // Exiting condition
                {
                    Environment.Exit(0);
                }

                else // Error checking so if any value other than 1,2,3,4 or 5 is entered then a message is diplayed
                {
                    Console.WriteLine("Input Is Invalid !!!");
                }
            }      
        }
    }
}


    
