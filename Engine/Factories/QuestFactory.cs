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
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 10));
            rewardItems.Add(new ItemQuantity(8001, 1));
            _quests.Add(new Quest(1, "Get Those Mips!", "Help Armstrong clear his field of Mips and bring back proof", itemsToComplete, 25, 10, rewardItems));

            itemsToComplete.Add(new ItemQuantity(9002, 10));
            rewardItems.Add(new ItemQuantity(8002, 1));
            _quests.Add(new Quest(2, "We Need More Forks!", "Bring Gitian some branches to make forks", itemsToComplete, 25, 10, rewardItems));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
