using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    
    public class Room
    {
        private string description; // Declaring the description variable
        public List<string> randomRoom = new List<string>{" is flooded", " is decayed with broken glass on the floor", " is sandy and dry"}; // Declaring the randomRoom list filled with 3 descriptions

        bool condition = false; // Declaring a terminating condtion so the process will loop until a value is returned

        public string RandRoom(List<string> visitedRoom)
        {
            while (condition == false)
            {
                Random rnd = new Random(); // Generating a random number to act as an index for the randomRoom list, so a random description is used
                int index = rnd.Next(randomRoom.Count);
                string description = randomRoom[index];

                if (visitedRoom.Count == 6) // Checking if every room has been visited already
                {
                    description = ""; // Setting the description to blank so it can be interpreted in Game.cs
                    return description;

                }

                else if (visitedRoom.Contains(description)) // Checking if the selected room has already been visited
                {
                    condition = false;
                }

                else // If the selected room has not been visited then the decription of that room is returned
                {
                    condition = true;
                    visitedRoom.Add(description);
                    return description;

                }

            }

                return description;
        }

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