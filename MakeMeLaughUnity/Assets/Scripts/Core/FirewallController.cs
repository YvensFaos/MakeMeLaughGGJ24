using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core
{
    public class FirewallController : MonoBehaviour
    {
        [SerializeField] private List<AntivirusController> antiviruses;
        [SerializeField] private int count;
        [SerializeField] private float interval;
        [SerializeField] private bool onStart;

        [Space(20)] [SerializeField] private List<Vector3> directions;
        [SerializeField] private float velocity;
        [SerializeField] private bool randomizeDirectionOnStart;
        [SerializeField] private bool destroyAfterSpawning = true;

        private int internalCount;

        private void Start()
        {
            ResetSpawning();
            if (randomizeDirectionOnStart)
            {
                for (var i = 0; i < directions.Count; i++)
                {
                    directions[i] = RandomPointUtils.GenerateRandomDirection2Din3D();
                }
            }

            if (onStart)
            {
                StartSpawning();
            }
        }

        private void StartSpawning()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private void ResetSpawning()
        {
            internalCount = count;
        }

        private IEnumerator SpawnCoroutine()
        {
            while (internalCount > 0)
            {
                --internalCount;
                directions.ForEach(dir =>
                {
                    var spawnDataController =
                        RandomHelper<AntivirusController>.GetRandomFromList(antiviruses);
                    var newlySpawn = Instantiate(spawnDataController, transform.position, Quaternion.identity);
                    newlySpawn.transform.SetParent(transform.parent);
                    newlySpawn.Initialize(dir, velocity);
                });
                yield return new WaitForSeconds(interval);
            }

            if (destroyAfterSpawning)
            {
                Destroy(gameObject, 0.5f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            directions?.ForEach(dir => { Gizmos.DrawLine(transform.position, transform.position + dir * 1.2f); });
        }
    }
}