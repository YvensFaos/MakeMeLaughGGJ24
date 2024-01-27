using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core
{
    [CreateAssetMenu(fileName = "New Receptor Key Input Database", menuName = "MML/Receptor Key Input Database", order = 3)]
    public class ReceptorKeyInputDatabase : ScriptableObject
    {
        [SerializeField] 
        private List<KeyCode> keyInputs;

        public KeyCode GetRandomKeyInput()
        {
            return RandomHelper<KeyCode>.GetRandomFromList(keyInputs);
        }
    }
}