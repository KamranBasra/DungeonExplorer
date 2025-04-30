using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player1
    {
        public string Name { get; private set; } // Declaring name variable
        public int Health { get; private set; } // Declaring health variable
        private List<string> inventory = new List<string>();

        public Player1(string name, int health) 
        {
            Name = name; // Setting the player name
            Health = health; // Setting the health
        }
        public void PickUpItem(string item) // Method to add item to inventory
        {
            inventory.Add(item);
        }
        public string InventoryContents() // Get Method to return the inventory contents
        {
            return string.Join(", ", inventory);
        }

        public int GetHealth() // Get Method to retun the Health
        {
            return Health;
        }
    }
}