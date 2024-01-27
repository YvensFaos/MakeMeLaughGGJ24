using System;
using System.Collections.Generic;
using Utils;

namespace Core
{
    [Serializable]
    public class PlayerLevelOSEventListPair : Pair<PlayerLevelSO, List<OSEvent>>
    {
        private List<Tuple<OSEvent, float>> eventChanceTupleList;
        
        public PlayerLevelOSEventListPair(PlayerLevelSO one, List<OSEvent> two) : base(one, two)
        { }

        public void GenerateTupleList()
        {
            eventChanceTupleList = new List<Tuple<OSEvent, float>>();
            Two.ForEach(osEvent => {
                eventChanceTupleList.Add(new Tuple<OSEvent, float>(osEvent, osEvent.GetChance()));
            });
        }

        public List<Tuple<OSEvent, float>> TupleList() => eventChanceTupleList;
    }
}