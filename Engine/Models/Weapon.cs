using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int HitRate { get; set; }
        public int CritRate { get; set; }

        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage, int hitRate, int critRate, string effect = "", int quantity = 1) 
            : base(itemTypeID, name, price, effect, quantity)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            HitRate = hitRate;
            CritRate = critRate;
        }

        public new Weapon Clone()
        {
            return new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage, HitRate, CritRate, Effect, Quantity);
        }
    }
}
