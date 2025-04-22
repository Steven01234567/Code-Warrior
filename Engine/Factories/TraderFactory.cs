using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();

        static TraderFactory()
        {
            Trader cecil = new Trader("Cecil");
            cecil.AddItemToInventory(ItemFactory.CreateGameItem(1002, 1));
            cecil.AddItemToInventory(ItemFactory.CreateGameItem(1003, 1));
            cecil.AddItemToInventory(ItemFactory.CreateGameItem(1011, 1));

            Trader risky = new Trader("Risky");
            risky.AddItemToInventory(ItemFactory.CreateGameItem(2001, 1000));
            risky.AddItemToInventory(ItemFactory.CreateGameItem(7001, 1));

            Trader segmentius = new Trader("Segmentius");
            segmentius.AddItemToInventory(ItemFactory.CreateGameItem(9001, 100));
            segmentius.AddItemToInventory(ItemFactory.CreateGameItem(9002, 100));
            segmentius.AddItemToInventory(ItemFactory.CreateGameItem(9003, 100));
            segmentius.AddItemToInventory(ItemFactory.CreateGameItem(9004, 100));
            segmentius.AddItemToInventory(ItemFactory.CreateGameItem(8001, 100));
            segmentius.AddItemToInventory(ItemFactory.CreateGameItem(8002, 100));

            AddTraderToList(cecil);
            AddTraderToList(risky);
            AddTraderToList(segmentius);
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader name '{trader.Name}'");
            }

            _traders.Add(trader);
        }
    }
}
