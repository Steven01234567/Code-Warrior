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
            _standardGameItems.Add(new Weapon(1002, "The C++", 50, 8, 16, 85, 5, "Cleave"));
            _standardGameItems.Add(new Weapon(1003, "The C#", 1500, 16, 32, 95, 25));
            _standardGameItems.Add(new Weapon(1011, "The Java-lin", 1200, 16, 32, 75, 20, "Pierce"));
            _standardGameItems.Add(new Potion(2001, "Health Potion", 10, "+50 HP"));
            _standardGameItems.Add(new GameItem(7001, "Pytorch", 2000));
            _standardGameItems.Add(new GameItem(8001, "Rustmetal", 80));
            _standardGameItems.Add(new GameItem(8002, "Polished Rustmetal", 200));
            _standardGameItems.Add(new GameItem(9001, "Jar of Mips", 5));
            _standardGameItems.Add(new GameItem(9002, "Branch", 10));
            _standardGameItems.Add(new GameItem(9003, "Leaf", 15));
            _standardGameItems.Add(new GameItem(9004, "Root", 25));
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

                if (standardItem is Potion)
                {
                    return (standardItem as Potion).Clone();
                }

                return standardItem.Clone();
            }

            return null;
        }
    }
}
