using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core
{
    [CreateAssetMenu(fileName = "New Receptor Key Input Database", menuName = "MML/Receptor Key Input Database", order = 3)]
    public class ReceptorKeyInputDatabase : ScriptableObject
    {
        [SerializeField] 
        private List<KeyCodePair> keyInputs;

        public KeyCodePair GetRandomKeyInput()
        {
            return RandomHelper<KeyCodePair>.GetRandomFromList(keyInputs);
        }
    }
}