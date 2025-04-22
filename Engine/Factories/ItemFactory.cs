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
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

        static ItemFactory()
        {
            _standardGameItems.Add(new Weapon(1001, "The C", 10, 4, 8, 90, 15));
            _standardGameItems.Add(new Weapon(1002, "The C++", 50, 4, 16, 70, 5, "Cleave"));
            _standardGameItems.Add(new Weapon(1003, "The C#", 500, 8, 16, 95, 25));
            _standardGameItems.Add(new Weapon(1011, "The Java-lin", 300, 8, 16, 50, 20, "Pierce"));
            _standardGameItems.Add(new GameItem(2001, "Full Potion", 10));
            _standardGameItems.Add(new GameItem(2002, "Pytorch", 500));
            _standardGameItems.Add(new GameItem(8001, "Rustmetal", 100));
            _standardGameItems.Add(new GameItem(8002, "Polished Rustmetal", 200));
            _standardGameItems.Add(new GameItem(9001, "Jar of Mips", 15));
            _standardGameItems.Add(new GameItem(9002, "Branch", 5));
            _standardGameItems.Add(new GameItem(9003, "Leaf", 25));
            _standardGameItems.Add(new GameItem(9004, "Root", 50));
        }

        public static GameItem CreateGameItem(int itemTypeID, int quantity = 1)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if (standardItem != null)
            {
                standardItem.Quantity = quantity;

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
