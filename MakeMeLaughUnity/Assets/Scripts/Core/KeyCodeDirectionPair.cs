using System;
using UnityEngine;
using Utils;

namespace Core
{
    [Serializable]
    public class KeyCodeDirectionPair : Pair<KeyCode, Vector3>
    {
        public KeyCodeDirectionPair(KeyCode one, Vector3 two) : base(one, two)
        { }
    }
}