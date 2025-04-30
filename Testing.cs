using Xunit;

namespace DungeonExplorer
{
    public class PlayerTests
    {
        [Fact]

        public void TakeDamage_ShouldReduceHealth()
        {
            var player = new Player("testPlayer", 20);

            player.TakeDamage(19);

            Assert.Equal(1, player.health);

        }

        [Fact]

        public void DealDamage_ShouldReduceTargetHealth()
        {
            var player = new Player("testPlayer", 20);
            var weapon = new Weapon("testWeapon", 10);
            var target = new Monster("testMonster", 20, 10, "ah");

            player.DealDamage(target, weapon, "testWeapon", false);
            Assert.Equal(10, target.health);
        }
    }


    public class InventoryTests
    {
        [Fact]

        public void RemoveItem_ShouldReduceInventory()
        {
            var inventory = new Inventory();
            var item = new Item("testItem", 10);

            inventory.RemoveItem(item.name);

            Assert.DoesNotContain(item.name, inventory.inventory);
        }

        [Fact]

        public void AddItem_ShouldIncreaseInventory()
        {
            var inventory = new Inventory();
            var item = new Item("testItem", 10);

            inventory.AddItem(item.name);

            Assert.Contains(item.name, inventory.inventory);
        }

    } 
}
