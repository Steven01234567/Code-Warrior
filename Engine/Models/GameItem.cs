using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem : BaseNotificationClass
    {
        private int _quantity;

        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Price { get; set; }
        public int Quantity 
        { 
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public GameItem(int itemTypeID, string name, int price,string effect = "",  int quantity = 1)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Effect = effect;
            Price = price;
            Quantity = quantity;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Price, Effect, Quantity);
        }
    }
}
