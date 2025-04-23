using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        private readonly string _cleaveDescriptionDisplay = "Deals extra damage to groups of enemies";
        private readonly string _pierceDescriptionDisplay = "Next attack does x1.5 damage";

        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public string DamageDisplay => $"{MinimumDamage}-{MaximumDamage}";
        public int HitRate { get; set; }
        public string HitRateDisplay => $"{HitRate}%";
        public int CritRate { get; set; }
        public string CritRateDisplay => $"{CritRate}%";

        public bool HasEffect => Effect != "";
        public string EffectNameDisplay
        {
            get
            {
                if (HasEffect)
                {
                    return $"{Effect}:";
                }
                return "";
            }
        }
        public string EffectDescriptionDisplay
        {
            get 
            {
                switch(Effect)
                {
                    case "Cleave":
                        return CleaveDescriptionDisplay;

                    case "Pierce":
                        return PierceDescriptionDisplay;

                    default:
                        return "";
                }
            }
        }
        public string CleaveDescriptionDisplay => _cleaveDescriptionDisplay;
        public string PierceDescriptionDisplay => _pierceDescriptionDisplay;

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
