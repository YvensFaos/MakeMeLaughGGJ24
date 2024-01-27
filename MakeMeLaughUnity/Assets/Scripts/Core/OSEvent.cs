using NaughtyAttributes;
using UnityEngine;
using Utils;

namespace Core
{
    [CreateAssetMenu(fileName = "New OS Event", menuName = "MML/OS Event", order = 2)]
    public class OSEvent : ScriptableObject
    {
        [SerializeField]
        private int chance;
        
        [Header("Data Spawner")]
        [SerializeField]
        private bool generateDataSpawner;
        [SerializeField, ShowIf("generateDataSpawner")]
        private DataPackageSpawner dataSpawner;

        [Header("Data")]
        [SerializeField] 
        private bool generateRandomData;
        [SerializeField, ShowIf("generateRandomData")]
        private DataPackageController data;
        
        //TODO add virus and issues
        //TODO add pop up
        //TODO add screen effect

        public void Execute(BoxCollider2D spawnArea, Transform spawnParent)
        {
            if (generateDataSpawner)
            {
                var position = RandomPointUtils.GetRandomPointWithBox2D(spawnArea);
                var newDataSpawner = Instantiate(dataSpawner, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
                newDataSpawner.transform.SetParent(spawnParent);
                MainFrame.GetSingleton().Console().AddConsoleLine("Data Stream Open.", "#");
            }

            if (generateRandomData)
            {
                var position = RandomPointUtils.GetRandomPointWithBox2D(spawnArea);
                var newData = Instantiate(data, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
                newData.transform.SetParent(spawnParent);
                var randomDirection = RandomPointUtils.GenerateRandomDirection2D();
                var randomDirectionVec3 = new Vector3(randomDirection.x, randomDirection.y, 0.0f);
                newData.Initialize(randomDirectionVec3, Random.Range(0.5f, 2.0f), 1);
                MainFrame.GetSingleton().Console().AddConsoleLine("Data Leaked.", "#");
            }
        }
        
        public int GetChance() => chance;
    }
}