using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();
        
        static QuestFactory()
        {
            List<ItemQuantity> quest1_ItemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> quest2_ItemsToComplete = new List<ItemQuantity>();

            List<ItemQuantity> quest1_RewardItems = new List<ItemQuantity>();
            List<ItemQuantity> quest2_RewardItems = new List<ItemQuantity>();

            quest1_ItemsToComplete.Add(new ItemQuantity(9001, 10));
            quest1_RewardItems.Add(new ItemQuantity(8001, 1));
            _quests.Add(new Quest(1, "Get Those Mips!", "Help Armstrong clear his field of Mips and bring back proof", quest1_ItemsToComplete, 25, 10, quest1_RewardItems));

            quest2_ItemsToComplete.Add(new ItemQuantity(9002, 10));
            quest2_RewardItems.Add(new ItemQuantity(8002, 1));
            _quests.Add(new Quest(2, "We Need More Forks!", "Bring Gitian some branches to make forks", quest2_ItemsToComplete, 25, 10, quest2_RewardItems));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
