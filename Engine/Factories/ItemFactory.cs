using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static List<GameItem> _standardGameItems;

        static ItemFactory()
        {
            _standardGameItems = new List<GameItem>();

            _standardGameItems.Add(new Weapon(1001, "The C", 0, 5, 10));
            _standardGameItems.Add(new Weapon(1002, "The C++", 100, 5, 10));
            _standardGameItems.Add(new Weapon(1003, "The C#", 100, 10, 20));
            _standardGameItems.Add(new GameItem(9001, "Jar of Mips", 15));
            _standardGameItems.Add(new GameItem(9002, "Branch", 5));
            _standardGameItems.Add(new GameItem(9003, "Leaf", 25));
            _standardGameItems.Add(new GameItem(9004, "Root", 50));
            _standardGameItems.Add(new GameItem(8001, "Rust Metal", 30));
            _standardGameItems.Add(new GameItem(8002, "Polished RustMetal", 60));
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if (standardItem != null)
            {
                if (standardItem is Weapon)
                {
                    return (standardItem as Weapon).Clone();
                }

                return standardItem.Clone();
            }

            return null;
        }
    }
}
