using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Trader : BaseNotificationClass
    {
        public string Name { get; set; }

        public ObservableCollection<GameItem> Inventory { get; set; }

        public Trader(string name) 
        {
            Name = name;
            Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            foreach (GameItem inventoryItem in Inventory)
            {
                if (item.ItemTypeID == inventoryItem.ItemTypeID)
                {
                    inventoryItem.Quantity += item.Quantity;
                    return;
                }
            }

            Inventory.Add(item);
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            foreach (GameItem inventoryItem in Inventory)
            {
                if (item.ItemTypeID == inventoryItem.ItemTypeID)
                {
                    if (item.Quantity >= inventoryItem.Quantity)
                    {
                        Inventory.Remove(inventoryItem);
                    }
                    else if (item.Quantity < inventoryItem.Quantity)
                    {
                        inventoryItem.Quantity -= item.Quantity;
                    }
                    break;
                }
            }
        }
    }
}
