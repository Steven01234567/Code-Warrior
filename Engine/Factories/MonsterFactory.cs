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
                    Monster mip = new Monster("Mip", "Mip.png", 10, 10, 1, 2, 1, 5, 10);

                    AddLootItem(mip, 9001, 100, 1);

                    return mip;

                case 102:
                    Monster mips = new Monster("Horde of Mips", "Mips.png", 100, 100, 1, 2, 10, 10, 5);

                    AddLootItem(mips, 9001, 100, 5);

                    return mips;

                case 201:
                    Monster nodes = new Monster("Nodes", "Nodes.png", 8, 8, 4, 4, 4, 16, 8);

                    AddLootItem(nodes, 9002, 75, 1);
                    AddLootItem(nodes, 9003, 25, 1);

                    return nodes;

                case 202:
                    Monster binaryTree = new Monster("Binary Tree", "BinaryTree.png", 128, 128, 32, 32, 1, 64, 64);

                    AddLootItem(binaryTree, 9002, 100, 63);
                    AddLootItem(binaryTree, 9003, 50, 32);
                    AddLootItem(binaryTree, 9004, 25, 1);

                    return binaryTree;

                case 301:
                    Monster python = new Monster("Python", "Python.png", 200, 200, 20, 20, 2, 200, 200);

                    return python;

                case 302:
                    Monster pythonDark = new Monster("Python in Darkness", "PythonDark.png", 200, 200, 99, 99, 2, 200, 200);

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
