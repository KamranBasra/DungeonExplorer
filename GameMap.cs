using System;
using System.CodeDom.Compiler;
using DungeonExplorer;

public class GameMap
{
    public Room currentRoom { get; private set; }
    
    public string Move(string direction, Room currentRoom ,Room room1, Room room2, Room room3, Room room4, Room room5) // Controls the movement of the player so they can explore different rooms under certain paths
    {
        if (direction == "Left")
        {
            if (currentRoom.GetDescription() == room1.GetDescription()) // If there is a room in this direction then that rooms desccription is returned
            {
                return room3.GetDescription();
            }

            else if (currentRoom.GetDescription() == room2.GetDescription()) // If there is a room in this direction then that rooms desccription is returned
            {
                return room1.GetDescription();
            }

            else
            {
                Console.WriteLine("You Cannot Move In That Direction"); // If there is not a room in this direction then the current rooms description is returned
                return currentRoom.GetDescription();
            }
        }

        else if (direction == "Right")
        {
            if (currentRoom.GetDescription() == room1.GetDescription()) // If there is a room in this direction then that rooms desccription is returned
            {
                return room2.GetDescription();
            }

            else if (currentRoom.GetDescription() == room3.GetDescription()) // If there is a room in this direction then that rooms desccription is returned
            {
                return room1.GetDescription();
            }

            else
            { 
                Console.WriteLine("You Cannot Move In That Direction"); // If there is not a room in this direction then the current rooms description is returned
                return currentRoom.GetDescription();
            }
        }

        else if (direction == "Up")
        {
             if (currentRoom.GetDescription() == room1.GetDescription()) // If there is a room in this direction then that rooms desccription is returned
            {
                return room4.GetDescription();
             }

            else if (currentRoom.GetDescription() == room5.GetDescription()) // If there is a room in this direction then that rooms desccription is returned
            {
                return room1.GetDescription();
            }

            else
            {
                Console.WriteLine("You Cannot Move In That Direction"); // If there is not a room in this direction then the current rooms description is returned
                return currentRoom.GetDescription();
            }

        }

        else if (direction == "Down")
        {
            if (currentRoom.GetDescription() == room1.GetDescription())// If there is a room in this direction then that rooms desccription is returned 
            {
                return room5.GetDescription();
            }

            else if (currentRoom.GetDescription() == room4.GetDescription())// If there is a room in this direction then that rooms desccription is returned
            {
                return room1.GetDescription();
            }

            else
            {
                Console.WriteLine("You Cannot Move In That Direction"); // If there is not a room in this direction then the current rooms description is returned
                return currentRoom.GetDescription();
            }
        }

        else
        {
            Console.WriteLine("Invalid Input !!!"); // Error handling encase an erronous input is entered
            Console.WriteLine("You Are Still In The Same Room");
            return currentRoom.GetDescription();
        }



    }



}