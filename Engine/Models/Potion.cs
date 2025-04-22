using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Potion : GameItem
    {
        public Potion(int itemTypeID, string name, int price, string effect, int quantity = 1) : base(itemTypeID, name, price, effect, quantity)
        {

        }
        public new Potion Clone()
        {
            return new Potion(ItemTypeID, Name, Price, Effect, Quantity);
        }
    }

    
}
