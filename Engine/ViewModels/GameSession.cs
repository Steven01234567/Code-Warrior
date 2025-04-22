using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties

        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation 
        { 
            get { return _currentLocation; } 
            set 
            { 
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToSouth));

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();

                CurrentTrader = CurrentLocation.TraderHere;
            }
        }
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                _currentMonster = value;
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null)
                {
                    RaiseMessage($"\nYou see a {CurrentMonster.Name} here!");
                }
            }
        }

        public Trader CurrentTrader 
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }
        public Weapon CurrentWeapon { get; set; }
        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        public bool HasLocationToWest =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;
        #endregion
        public GameSession()
        {
            CurrentPlayer = new Player()
            {
                Name = "Oscar Owen Peterson"
            };

            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        // Stat Increases
        public void GiveHP()
        {
            if (CurrentPlayer.HasSkillPoints)
            {
                if (!CurrentPlayer.IsHPMaxed)
                {
                    CurrentPlayer.MaximumHitPoints += 20;
                    CurrentPlayer.HitPoints += 20;
                    CurrentPlayer.SkillPoints--;
                }
            }
        }
        public void GiveStrength()
        {
            if (CurrentPlayer.HasSkillPoints)
            {
                if (!CurrentPlayer.IsStrengthMaxed)
                {
                    CurrentPlayer.Strength += 1;
                    CurrentPlayer.SkillPoints--;
                }
            }
        }
        public void GiveAccuracy()
        {
            if (CurrentPlayer.HasSkillPoints)
            {
                if (!CurrentPlayer.IsAccuracyMaxed)
                {
                    CurrentPlayer.Accuracy += 1;
                    CurrentPlayer.SkillPoints--;
                }
            }
        }
        public void GivePrecision()
        {
            if (CurrentPlayer.HasSkillPoints)
            {
                if (!CurrentPlayer.IsHPMaxed)
                {
                    CurrentPlayer.Precision += 1;
                    CurrentPlayer.SkillPoints--;
                }
            }
        }

        // Movement Fuctions
        public void MoveNorth()
        {
            if (!HasLocationToNorth) return;
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
        }

        public void MoveWest()
        {
            if (!HasLocationToWest) return;
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
        }

        public void MoveEast()
        {
            if (!HasLocationToEast) return;
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
        }

        public void MoveSouth()
        {
            if (!HasLocationToSouth) return;
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
        }

        private void CompleteQuestsAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);
                
                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        foreach(ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            CurrentPlayer.RemoveItemFromInventory(ItemFactory.CreateGameItem(itemQuantity.ItemID, itemQuantity.Quantity));
                        }

                        RaiseMessage($"\nYou completed the '{quest.Name}' quest");

                        CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;
                        RaiseMessage($"You gained {quest.RewardExperiencePoints} experience points");

                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You earned {quest.RewardGold} gold");

                        foreach(ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem RewardItem = (ItemFactory.CreateGameItem(itemQuantity.ItemID, itemQuantity.Quantity));

                            CurrentPlayer.AddItemToInventory(RewardItem);
                            RaiseMessage($"You received {RewardItem.Quantity} {RewardItem.Name}");
                        }

                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage($"\n'{quest.Name}' quest started!");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with:");
                    foreach(ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"   {quest.RewardGold} gold");
                    foreach(ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
            if (CurrentMonster != null)
            {
                if (CurrentMonster.Name == "Python" && !CurrentPlayer.HasPytorch)
                {
                    CurrentMonster = MonsterFactory.GetMonster(302);
                }
            }
        }

        // Combat Logic
        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage + CurrentPlayer.Accuracy, CurrentWeapon.MaximumDamage);

            if (damageToMonster <= 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}");
            }
            else
            {
                damageToMonster += CurrentPlayer.Strength;

                if (CurrentWeapon.Effect == "Cleave")
                {
                    damageToMonster *= CurrentMonster.Amount;
                    if (CurrentMonster.Amount > 1)
                    {
                        RaiseMessage("Your attack hit all enemies!");
                    }
                }

                if (CurrentMonster.IsBleeding)
                {
                    damageToMonster *= 3;
                    damageToMonster /= 2;
                    CurrentMonster.IsBleeding = false;
                    RaiseMessage("You attacked when your enemy was vulnerable!");
                }
                if (CurrentWeapon.Effect == "Pierce")
                {
                    CurrentMonster.IsBleeding = true;
                    RaiseMessage("Your attack exposed the enemy!");
                }

                if (CurrentPlayer.Precision >= RandomNumberGenerator.NumberBetween(1, 100))
                {
                    damageToMonster *= 2;
                    RaiseMessage("You landed a critical hit!");
                }

                CurrentMonster.HitPoints -= damageToMonster;
                RaiseMessage($"You did {damageToMonster} damage to the {CurrentMonster.Name}");
            }

            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage($"\nYou defeated the {CurrentMonster.Name}!");

                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You gained {CurrentMonster.RewardExperiencePoints} experience points.");
                while (CurrentPlayer.ExperiencePoints >= CurrentPlayer.LevelUpExperiencePoints &&
                    !CurrentPlayer.IsLevelMaxed)
                {
                    CurrentPlayer.Level++;
                    CurrentPlayer.LevelUpExperiencePoints += CurrentPlayer.Level * CurrentPlayer.LevelUpExperiencePointsBase;
                    CurrentPlayer.SkillPoints += 1;
                    RaiseMessage($"You leveled up! +1 Skill point");
                }

                CurrentPlayer.Gold += CurrentMonster.RewardGold;
                RaiseMessage($"You earned {CurrentMonster.RewardGold} gold.");

                foreach(ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    GameItem item = ItemFactory.CreateGameItem(itemQuantity.ItemID, itemQuantity.Quantity);
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You received {itemQuantity.Quantity} {item.Name}.");
                }

                GetMonsterAtLocation();
            }
            else
            {
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} tried to attack and missed!");
                }
                else
                {
                    damageToPlayer *= (int)Math.Ceiling((decimal)CurrentMonster.Amount * (decimal)CurrentMonster.HitPoints / (decimal)CurrentMonster.MaximumHitPoints);
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
                }

                if (CurrentPlayer.HitPoints <= 0)
                {
                    RaiseMessage($"\nThe {CurrentMonster.Name} killed you.");

                    CurrentLocation = CurrentWorld.LocationAt(0, 0);
                    CurrentPlayer.HitPoints = CurrentPlayer.MaximumHitPoints;
                }
            }
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}