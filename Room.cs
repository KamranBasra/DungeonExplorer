using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    
    public class Room
    {
        public string description; // Declaring the description variable

        bool condition = false; // Declaring a terminating condtion so the process will loop until a value is returned


        public Room(string description)
        {
            this.description = description; // Setting the description
        }

        public string GetDescription() // Get Method to return the description of the room
        {
            return description;
        }



    }
}