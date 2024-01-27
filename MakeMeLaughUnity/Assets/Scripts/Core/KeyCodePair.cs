using System;
using UnityEngine;
using Utils;

namespace Core
{
    [Serializable]
    public class KeyCodePair : Pair<KeyCode, KeyCode>
    {
        public KeyCodePair(KeyCode one, KeyCode two) : base(one, two)
        { }
    }
}