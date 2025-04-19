using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch(monsterID)
            {
                case 101:
                    Monster mip = new Monster("Mip", "Mip.png", 5, 5, 5, 1);

                    AddLootItem(mip, 9001, 100, 1);

                    return mip;

                case 201:
                    Monster node = new Monster("Node", "Node.png", 8, 8, 8, 2);

                    AddLootItem(node, 9002, 75, 1);
                    AddLootItem(node, 9003, 25, 1);

                    return node;

                case 202:
                    Monster tree = new Monster("Tree", "Tree.png", 64, 64, 64, 8);

                    AddLootItem(tree, 9002, 100, 63);
                    AddLootItem(tree, 9003, 50, 32);
                    AddLootItem(tree, 9004, 25, 1);

                    return tree;

                case 301:
                    Monster python = new Monster("Python", "Python.png", 200, 200, 100, 50);

                    return python;

                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage, int quantity)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.Inventory.Add(new ItemQuantity(itemID, quantity));
            }
        }
    }
}
