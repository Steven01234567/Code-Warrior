using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();

            // Home
            newWorld.AddLocation(0, 0, "Home", "This is your home", 
                "PlayerHouse.png");

            // NPCs
            newWorld.AddLocation(-1, -1, "Risky, the 5th, Ashembly, the Alchemist", "Risky: \"Try my potions...for a fair price hohoho...\"", 
                "Risky.png");

            newWorld.AddLocation(0, -1, "Segmentius Faultius, the Merchant", "Segmentius: \"Self-proclaimed most honest merchant, no one else offers better deals than me!\"", 
                "Segmentius.png");

            newWorld.AddLocation(1, -1, "Cecil Caeser Gaston, the Blacksmith", "Cecil: \"I'm your local blacksmith, the best in town!\"", 
                "Cecil.png");

            newWorld.AddLocation(-1, 0, "Armstrong Ashembly, the Farmer", "Armstrong: \"Help me clear those Mips! They're ravaging my farm!\"",
                "Armstrong.png");
            newWorld.LocationAt(-1, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(1, 0, "Gitian Hubris, the Village Chief", "Gitian: \"The village is out of forks, bring me some branches so I can make more\"",
                "Gitian.png");
            newWorld.LocationAt(1, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(2));

            // Monster-related Locations
            newWorld.AddLocation(-2, 0, "West Path", "You encounter a Mip on your way to the field",
                "WestPath.png");
            newWorld.LocationAt(-2, 0).AddMonster(101, 100);

            newWorld.AddLocation(-2, 1, "Infested Field", "You encounter a horde of Mips",
                "Field.png");
            newWorld.LocationAt(-2, 1).AddMonster(101, 100);

            newWorld.AddLocation(2, 0, "East Path", "You encounter a bunch of Nodes",
                "EastPath.png");
            newWorld.LocationAt(2, 0).AddMonster(201, 100);

            newWorld.AddLocation(2, 1, "The Binary Tree", "You encounter the Binary Tree",
                "Tree.png");
            newWorld.LocationAt(2, 1).AddMonster(202, 100);

            newWorld.AddLocation(0, 1, "Entrance to the Python Lair", "It's too dark and dangerous to enter unequiped. Obtain a Pytorch before proceeding.",
                "Entrance.png");

            newWorld.AddLocation(0, 2, "The Python Lair", "The Python, twin-headed ruler of the Python Lair",
                "__NOT_MADE_YET__");
            newWorld.LocationAt(0, 2).AddMonster(301, 100);

            return newWorld;
        }
    }
}
