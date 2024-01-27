using System;
using UnityEngine;
using Utils;

namespace Core
{
    [Serializable]
    public class PlayerLevelScorePair : Pair<PlayerLevelSO, int>, IComparable<PlayerLevelScorePair>
    {
        [SerializeField]
        private bool finalLevel;        
        
        public PlayerLevelScorePair(PlayerLevelSO one, int two) : base(one, two)
        { }

        public int CompareTo(PlayerLevelScorePair other)
        {
            return Two.CompareTo(other.Two);
        }

        public bool IsFinalLevel() => finalLevel;
    }
}