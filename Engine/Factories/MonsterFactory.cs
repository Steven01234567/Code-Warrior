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
                    Monster mip = new Monster("Mip", "Mip.png", 10, 10, 0, 3, 2, 5, 1);

                    AddLootItem(mip, 9001, 100, 1);

                    return mip;

                case 102:
                    Monster mips = new Monster("Horde of Mips", "Mips.png", 100, 100, 0, 3, 100, 25, 5);

                    AddLootItem(mips, 9001, 95, 5);

                    return mips;

                case 201:
                    Monster nodes = new Monster("Nodes", "Nodes.png", 64, 64, 4, 4, 128, 64, 4);

                    AddLootItem(nodes, 9002, 75, 1);
                    AddLootItem(nodes, 9003, 25, 1);

                    return nodes;

                case 202:
                    Monster binaryTree = new Monster("Binary Tree", "BinaryTree.png", 128, 128, 16, 16, 1024, 256, 1);

                    AddLootItem(binaryTree, 9002, 100, 63);
                    AddLootItem(binaryTree, 9003, 50, 32);
                    AddLootItem(binaryTree, 9004, 25, 1);

                    return binaryTree;

                case 301:
                    Monster python = new Monster("BOSS: The Python", "Python.png", 400, 400, 20, 20, 400, 400, 2);

                    return python;

                case 302:
                    Monster pythonDark = new Monster("BOSS: The Python in Darkness", "PythonDark.png", 400, 400, 99, 99, 400, 400, 2);

                    return pythonDark;

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
