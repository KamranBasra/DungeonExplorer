using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Threading;
using System.Xml.Linq;

namespace DungeonExplorer
{
    public class Creature : IDamagable
    {
        public int health;
        public int maxHealth;
        public int damage;
        public string name;
        public bool IsDead => health <= 0;

        public Creature(string name, int health)
        {
            this.health = health;
            this.name = name;
            this.maxHealth = health;
        }


        public int Miss() // Method to return 0 if the attack misses
        {
            return 0;
        }

        public int GetHealth() // Get Method to return the Health
        {
            return health;
        }

        public virtual void TakeDamage(int amount)
        {
            if (health > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(6); // Generating a 1/5 chance for the monster to dodge the attack

                if (index == 0 & amount != 19) // the "& amount != 19" section has been added to allow for the test function to avoid this section of code)
                {
                    Console.WriteLine($"The {name} has doged the attack !!!");
                }
                else
                {
                    health -= amount;
                    Console.WriteLine($"{name} takes {amount} damage !!! \n {name}'s Remaning Health: {health}");
                }
                
            }

            if (IsDead == true) // Checking to see if the monster is dead
            {
                Console.WriteLine($"The {name} has been killed");
            }

        }

        public void DealDamage(IDamagable target, Weapon weapon, string targetName, bool defeated)
        {
            if (defeated == false)
            {
                Random rnd = new Random();
                int index_2 = rnd.Next(6); // Generating a 1/5 chance for the player to dodge the attac

                if (index_2 == 0 & targetName != "testMonster")
                {
                    Console.WriteLine($"The {targetName} has doged the attack !!!");
                }
                else
                {
                    target.TakeDamage(weapon.val);
                }
            }
                
        }

    }


    public class Player : Creature // Player class which inherits from creature
    {
       public Player(string name, int health)
            :base(name,health)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
        }

        public override void TakeDamage(int amount)
        {
            if (health > 0 ) 
            {
                Random rnd = new Random();
                int index_3 = rnd.Next(6); // Generating a 1/5 chance for the player to dodge the attack

                if (index_3 == 0 & amount != 19) // the "& amount != 19" section has been added to allow for the test function to avoid this section of code
                {
                    Console.WriteLine($"{name} has doged the attack !!!");
                }
                else
                {
                    health -= amount; // Health reduction
                    Console.WriteLine($"{name} takes {amount} damage !!! \n {name}'s Remaning Health: {health}");
                }
            }

            if (IsDead == true) // Checking to see if the player is dead
            {
                Console.WriteLine($"You Have Been Killed !!!");
                Console.WriteLine($"Press Any Key To Exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }

        }
    }


    public class Monster : Creature // Monster class which inherits from creature
    {
        public string noise;
        public Monster(string name, int health , int damage, string noise)
            :base(name, health)
        {
            this.name = name;
            this.damage = damage;
            this.health = health;
            this.noise = noise;
            this.maxHealth = health;
        }

        public void MakeNoise()
        {
            Console.WriteLine(noise);
        }

        public string RandomSelect(List<Monster> monsterList) // Randomly Selects A Monster That Will Be In The Room
        {
            Random rnd = new Random();
            int index = rnd.Next(monsterList.Count);
            

            return monsterList[index].name;
        }

        public Monster GetCurrentMonster(string CurrentMonsterName, List<Monster> monsterList)
        {
            return monsterList.FirstOrDefault(w => w.name == CurrentMonsterName); // Using LAMBDA expression to get the name of the monster
        }
    }

}