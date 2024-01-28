using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

public class DataPackageSpawner : MonoBehaviour
{
    [SerializeField]
    private List<DataPackageChancePair> data;
    [SerializeField]
    private int count;
    [SerializeField] 
    private float interval;
    [SerializeField]
    private bool onStart;
    [Space(20)]
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float velocity;
    [SerializeField] 
    private bool randomizeDirectionOnStart;
    [SerializeField] 
    private bool destroyAfterSpawning = true;

    private int internalCount;
    private List<Tuple<DataPackageController, float>> dataTupleList;

    private void Awake()
    {
        ConvertPairsToTuple();
    }

    private void Start()
    {
        ResetSpawning();
        if (randomizeDirectionOnStart)
        {
            direction = RandomPointUtils.GenerateRandomDirection2Din3D();
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

    private void ConvertPairsToTuple()
    {
        dataTupleList = new List<Tuple<DataPackageController, float>>();
        data.ForEach(data =>
        {
            dataTupleList.Add(new Tuple<DataPackageController, float>(data.One, data.Two));
        });
    }

    private IEnumerator SpawnCoroutine()
    {
        while (internalCount > 0)
        {
            --internalCount;
            var spawnDataController = RandomChancePair<DataPackageController>.GetRandomFromChanceList(dataTupleList, null);
            var newlySpawn = Instantiate(spawnDataController, transform.position, Quaternion.identity);
            newlySpawn.transform.SetParent(transform.parent);
            newlySpawn.Initialize(direction, velocity);
            yield return new WaitForSeconds(interval);
        }

        if (destroyAfterSpawning)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + direction * 3.0f);
    }
}