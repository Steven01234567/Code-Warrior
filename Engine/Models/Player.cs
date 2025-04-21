using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : BaseNotificationClass
    {
        private string _name;
        private string _characterClass;
        private int _hitPoints;
        private int _experiencePoints;
        private int _level;
        private int _gold;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int ExperiencePoints 
        {
            get { return _experiencePoints; } 
            set 
            { 
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            } 
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }
        public ObservableCollection<GameItem> Inventory { get; set; }
        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public bool HasPytorch => Inventory.Any(i => i.ItemTypeID == 200);

        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }

        public void AddItemToInventory(GameItem item)
        {
            foreach(GameItem inventoryItem in Inventory)
            {
                if (item.ItemTypeID == inventoryItem.ItemTypeID)
                {
                    inventoryItem.Quantity += item.Quantity;
                    OnPropertyChanged(nameof(Weapons));
                    OnPropertyChanged(nameof(HasPytorch));
                    return;
                }
            }

            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
            OnPropertyChanged(nameof(HasPytorch));
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
                    OnPropertyChanged(nameof(Weapons));
                    OnPropertyChanged(nameof(HasPytorch));
                    break;
                }
            }
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach(ItemQuantity itemQuantity in items)
            {
                if (!Inventory.Any(i => i.ItemTypeID == itemQuantity.ItemID && i.Quantity >= itemQuantity.Quantity))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
