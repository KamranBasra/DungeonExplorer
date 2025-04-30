using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace DungeonExplorer
{

    public class Item
    {

       public string name;
       public int val;
       public string selectedItem;
    
       public Item(string name, int val)
        {
            this.name = name;
            this.val = val;
        }
    }

    public class Inventory // Inventory class
    {
        public List<string> inventory = new List<string>();
        public string selectedItem;
        public void AddItem(string item) // Method to add items to inventory
        {
            inventory.Add(item);
        }

        public void RemoveItem(string item) // Method to remove items from inventory
        {
            inventory.Remove(item);
        }

        public string GetInventory() // Method to get inventory contents
        {
            return string.Join(", ", inventory);
        }

        public bool ContainsItem(string item) // Method to check if the inventory contains an item
        {
            if (inventory.Contains(item)) return true;

            else return false;
        }

        public string ShowWeapons(List<Weapon> weaponList) // Method to list all of the weapons in the inventory
        {
            var matching = weaponList
            .Where(n => inventory.Contains(n.name)) // LAMBDA functions
            .Select(n => n.name);
            return string.Join(", ", matching);
        }

        public string ShowPotions(List<Potion> potionList) // Method to list all of the potions in the inventory
        {
            var matching = potionList
            .Where(n => inventory.Contains(n.name))
            .Select(n => n.name);
            return string.Join(", ", matching);
        }

        public bool IsFull(List<Potion> potionList, List<Weapon> weaponList) // Methid to check if the inventory is full
        {
            if (inventory.Count == (potionList.Count + weaponList.Count))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public Weapon SelectWeapon(string selectdItem, List<Weapon> weaponList) // Method to select a weapon object
        {
            return weaponList.FirstOrDefault(w => w.name == selectedItem);
        }

        public string SelectItem()
        {
            bool condition = true;
            while (condition == true)
            {
                Console.WriteLine($"Your Inventory Currently Contains {GetInventory()}");
                Console.WriteLine("Type The Name Of The Item You Would Like to Select, or Type 'menu' to return to the menu ");
                selectedItem = Console.ReadLine();

                if (inventory.Contains(selectedItem) == true) // Selects the item if it exists
                {
                    condition = false;
                    Console.WriteLine($"Your Current Selected Item Is Now {selectedItem}");
                    return selectedItem;
                }

                else if (selectedItem == "menu")
                {
                    condition = false;
                    return null;
                }

                else if (inventory.Contains(selectedItem) == false) // Error handling if the inventory does not contain the item the user is requesting
                {
                    Console.WriteLine("Your Inventory Does Not Contain This Item");
                }

            }
            return selectedItem;
        }
    }

    public class Weapon : Item // Weapon class
    {
        public Weapon(string name, int val)
            :base(name,val)
        {
            this.name = name;
            this.val = val;
        }

        public int GetDamage()
        {
            return val;


        }
    }

    public class Potion : Item // Potion class
    {
        public Potion(string name, int val)
            : base(name, val)
        {
            this.name = name;
            this.val = val;
        }

        public void UsePotion(Player player) // Method to use potion which will increase health
        {
            int currentHealth = player.health;
            if (player.health + val > 20)
            {
                player.health = 20;
            }
            else
            {
                player.health += val;
            }
               
            Console.WriteLine($"Health Has Increased By {player.health - currentHealth}"); // Tells the user how much the health has increased by
        }
    }
}