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

            newWorld.AddLocation(0, 0, "Home", "This is your home", "/Engine;component/Images/Locations/PlayerHouse.png");
            newWorld.AddLocation(-1, 0, "Ashembly Risky the 5th, the Alchemist", "Try my potions...for a fair price hohoho...", "/Engine;component/Images/Locations/__NOT_MADE_YET__");
            newWorld.AddLocation(0, 1, "Segmentius Faultius, the Trader", "Self-proclaimed most trustworthy trader, no one else offers better deals than me!", "/Engine;component/Images/Locations/__NOT_MADE_YET__");
            newWorld.AddLocation(1, 0, "Cecil Ceaser Gaston, the Blacksmith", "Your local blacksmith, the best in town!", "/Engine;component/Images/Locations/__NOT_MADE_YET__");

            return newWorld;
        }
    }
}
