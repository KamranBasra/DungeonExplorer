using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
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
        private Weapon longsword;
        private Potion fullPotion;
        private Potion halfPotion;
        private GameMap map;
        private Room room1;
        private Room room2;
        private Room room3;
        private Room room4;
        private Room room5;



        public Game()
        {
            // Initialize the objects

            Console.WriteLine("What is your name? ");
            string name = Console.ReadLine();

            player = new Player(name, 20);
            currentRoom = new Room("The Room is dark and misty");
            map = new GameMap();
         
            dragon = new Monster("Dragon", 15, 7, "GGRRRAAAUUUGHHH!");
            zombie = new Monster("Zombie", 12, 5, "Uuurrgghhh");
            skeleton = new Monster("Skeleton", 10, 4, "clack clack clack");
            spider = new Monster("Spider", 7, 3, "skkssksskssk");

            room1 = new Room("The Room is dark and misty");
            room2 = new Room("The Room is flooded");
            room3 = new Room("The Room is decayed with broken glass on the floor");
            room4 = new Room("The Room is sandy and dry");
            room5 = new Room(" The Room is an abandoned blacksmiths quater");

            axe = new Weapon("axe", 5);
            bow = new Weapon("bow", 3);
            sword = new Weapon("sword", 6);
            knife = new Weapon("knife", 4);
            longsword = new Weapon("longsword", 7);
            starterKnife = new Weapon("rusty knife", 2);

            fullPotion = new Potion("Big Potion", 10);
            halfPotion = new Potion("Small Potion", 5);

            inventory = new Inventory();

        }
        public void Start()
        {
            // Defining game variables
            bool playing = true;
            bool opened = false;
            bool defeated = false;
            bool roomStarted = false;
            string currentMonsterName = "";
            string previousMonsterName;


            List<string> visitedRoom = new List<string>(); // Initializing a list of visited rooms
            List<string> foundWeapons = new List<string> (); // Initializing a list of found weapons
            List<string> foundPotions = new List<string> (); // Initializing a list of found potions

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
            foundWeapons.Add("rusty knife");

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


                if (defeated == false) // If the monster has not yet been defeated then it still appears in the title, and the option to move on is not present
                {
                    Console.WriteLine($"{currentRoom.GetDescription()}, ahead of you there is a {currentMonsterName}");
                    Console.WriteLine("\nWould You Like to:\n 1) Look Around \n 2) Open the Chest \n 3) Check Your Current Status \n 4) Open Your Inventory \n 5) Fight The Monster Ahead Of You \n 6) Exit\n");
                }

                else if (defeated == true)
                {
                    Console.WriteLine($"{currentRoom.GetDescription()}");
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
                                int index = rnd.Next(weaponList.Count); // Randomly selected whether a potion or a weapon is added
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

                                else if (index < potionList.Count)  // Randomly selected whether a potion or a weapon is added
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

                                    else if (index_2 == 1) // Then further randomizes whether a small or big potion is found
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
                        Console.Write(inventory.GetInventory()); // Displays inventory contents
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
                                        inventory.RemoveItem(deletedItem); // Removes an item from inventory
                                        condition = false;
                                    }

                                    else if (deletedItem == "menu")
                                    {
                                        condition = false;
                                    }

                                    else if (inventory.ContainsItem(deletedItem) == false)
                                    {
                                        Console.WriteLine("Your Inventory Does Not Contain This Item"); // Error handling if the item is not in the inventory
                                    }
                                }


                            }

                            else if (decision_2 == "2")
                            {
                                selectedItem = inventory.SelectItem(); // Selects an item
                            }

                            else if (decision_2 == "3")
                            {
                                Console.Write(inventory.ShowWeapons(weaponList)); // Filters to show only weapons in the inventory
                            }

                            else if (decision_2 == "4")
                            {
                                Console.Write(inventory.ShowPotions(potionList)); // Filters to show only weapons in the inventory
                            }

                            else if (decision_2 == "5")
                            {
                                continue;
                            }

                        }

                        else if (decision == "2") // Selects an item
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
                    Console.WriteLine("Which Direction Would You Like To Move In? Up/Down/Left/Right");
                    string direction = Console.ReadLine();

                    try
                    {
                        string previousDescription = currentRoom.GetDescription(); // Temporary hold for the old room description
                        string newDescription = map.Move(direction, currentRoom, room1, room2, room3, room4, room5);
                        if (newDescription == previousDescription)
                        {
                            continue;
                        }
                        else
                        {
                            opened = false;
                            roomStarted = false;
                            defeated = false;
                            currentRoom.description = newDescription;
                        }

                    }
                    catch (FormatException ex) // Error handling  if there are any random exception
                    {
                        Console.WriteLine(ex.Message);
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
                            Console.WriteLine($" Your Inventory Contains: {inventory.GetInventory()}"); // Displays the Contents of the inventory
                            Console.WriteLine("Would you like to:\n 1) Select A New Item \n 2) Continue ");
                            string decision_2 = Console.ReadLine();

                            if (decision_2 == "1")
                            {
                                selectedItem = inventory.SelectItem(); // Selects a new item to be used

                            }

                            if (decision_2 == "2") // Returns Back To Menu
                            {
                                continue;
                            }
                        }

                        else if (decision == "2") // Displays the currently selected item
                        {
                            Console.WriteLine($"The Item That Is Currently Selected is: {selectedItem}");
                        }

                        else if (decision == "3")
                        {
                            if (selectedItem == null) // Error handling incase the user has not got an item selected
                            {
                                Console.WriteLine("No Item Is Currently Selected, Please Select An Item !!!");
                            }


                            else if ((inventory.ShowPotions(potionList)).Contains(selectedItem)) // If a potion is equpped then the potion is used withouth engaging with the monster
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

                            else if ((inventory.ShowWeapons(weaponList)).Contains(selectedItem)) // If a weapon is equipped then the weapon is used against the monster
                            {
                                Monster currentMonsterObject = monsterList.FirstOrDefault(m => m.name == currentMonsterName);
                                player.DealDamage(currentMonsterObject, inventory.SelectWeapon(selectedItem, weaponList), currentMonsterName, defeated);
                                if (currentMonsterObject.health > 0 & defeated == false) // Making sure the monster is alive and has not been previously defeated
                                {
                                    player.TakeDamage(currentMonsterObject.damage);
                                }
                                if (currentMonsterObject.IsDead)
                                {
                                    defeated = true;
                                   // currentMonsterObject.health = currentMonsterObject.maxHealth;
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


    
