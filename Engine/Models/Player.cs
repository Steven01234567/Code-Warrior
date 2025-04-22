using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : BaseNotificationClass
    {
        private string _name;
        private int _levelCap = 100;
        private int _level = 1;
        private int _experiencePoints = 0;
        private int _levelUpExperiencePoints = 50;
        private int _gold = 15;

        private readonly int _hitPointsCap = 500;
        private int _maximumHitPoints = 20;
        private int _hitPoints;
        private readonly int _strengthCap = 25;
        private int _strength = 0;
        private readonly int _accuracyCap = 25;
        private int _accuracy = 0;
        private readonly int _precisionCap = 25;
        private int _precision = 0;
        private int _skillPoints = 200;

        // Name
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        // Levels and XP
        public int LevelCap => _levelCap;
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(LevelUpExperiencePoints));
                OnPropertyChanged(nameof(IsLevelMaxed));
            }
        }
        public bool IsLevelMaxed => Level >= LevelCap;
        public int ExperiencePoints 
        {
            get { return _experiencePoints; } 
            set 
            { 
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
                OnPropertyChanged(nameof(ExperiencePointsDisplay));
            } 
        }
        public int LevelUpExperiencePoints
        {
            get { return _levelUpExperiencePoints; }
            set
            {
                _levelUpExperiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePointsDisplay));
            }
        }
        public string ExperiencePointsDisplay => $"{ExperiencePoints}/{LevelUpExperiencePoints}";

        // Gold
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        ///
        /// Stats
        ///

        // HP
        public int HitPointsCap => _hitPointsCap;
        public int MaximumHitPoints 
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;

                OnPropertyChanged(nameof(MaximumHitPoints));
                OnPropertyChanged(nameof(IsHPMaxed));
                OnPropertyChanged(nameof(HitPointsDisplay));
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
                OnPropertyChanged(nameof(HitPointsDisplay));
                OnPropertyChanged(nameof(HasSkillPointsForHP));
            }
        }
        public bool IsHPMaxed => MaximumHitPoints >= HitPointsCap;
        public string HitPointsDisplay => $"{HitPoints}/{MaximumHitPoints}";

        // Strength
        public int StrengthCap => _strengthCap;
        public int Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                OnPropertyChanged(nameof(Strength));
                OnPropertyChanged(nameof(IsStrengthMaxed));
                OnPropertyChanged(nameof(HasSkillPointsForStrength));
            }
        }
        public bool IsStrengthMaxed => Strength >= StrengthCap;

        // Accuracy
        public int AccuracyCap => _accuracyCap;
        public int Accuracy
        {
            get { return _accuracy; }
            set
            {
                _accuracy = value;
                OnPropertyChanged(nameof(Accuracy));
                OnPropertyChanged(nameof(IsAccuracyMaxed));
                OnPropertyChanged(nameof(HasSkillPointsForAccuracy));
            }
        }
        public bool IsAccuracyMaxed => Accuracy >= AccuracyCap;

        // Precision
        public int PrecisionCap => _precisionCap;
        public int Precision
        {
            get { return _precision; }
            set
            {
                _precision = value;
                OnPropertyChanged(nameof(Precision));
                OnPropertyChanged(nameof(IsPrecisionMaxed));
                OnPropertyChanged(nameof(HasSkillPointsForPrecision));
            }
        }
        public bool IsPrecisionMaxed => Precision >= PrecisionCap;
        public int SkillPoints 
        { 
            get { return _skillPoints; }
            set
            {
                _skillPoints = value;
                OnPropertyChanged(nameof(SkillPoints));
                OnPropertyChanged(nameof(HasSkillPoints));
                OnPropertyChanged(nameof(HasSkillPointsForHP));
                OnPropertyChanged(nameof(HasSkillPointsForStrength));
                OnPropertyChanged(nameof(HasSkillPointsForAccuracy));
                OnPropertyChanged(nameof(HasSkillPointsForPrecision));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        //public List<GameItem> Potions => Inventory.Where(i => i is Potion).ToList();
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public bool HasSkillPoints => SkillPoints > 0;
        public bool HasSkillPointsForHP => HasSkillPoints && !IsHPMaxed;
        public bool HasSkillPointsForStrength => HasSkillPoints && !IsStrengthMaxed;
        public bool HasSkillPointsForAccuracy => HasSkillPoints && !IsAccuracyMaxed;
        public bool HasSkillPointsForPrecision => HasSkillPoints && !IsPrecisionMaxed;
        public bool HasPytorch { get; private set; }

        public Player()
        {
            HitPoints = MaximumHitPoints;

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
            if (item.ItemTypeID == 2002)
            {
                HasPytorch = true;
            }
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
                        if (item.ItemTypeID == 2002)
                        {
                            HasPytorch = false;
                        }
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
