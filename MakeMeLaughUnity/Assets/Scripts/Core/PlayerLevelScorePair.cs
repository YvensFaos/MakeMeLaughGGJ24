using System;
using Utils;

namespace Core
{
    [Serializable]
    public class PlayerLevelScorePair : Pair<PlayerLevelSO, int>, IComparable<PlayerLevelScorePair>
    {
        public PlayerLevelScorePair(PlayerLevelSO one, int two) : base(one, two)
        { }

        public int CompareTo(PlayerLevelScorePair other)
        {
            return Two.CompareTo(other.Two);
        }
    }
}