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

    private int internalCount;
    private List<Tuple<DataPackageController, float>> dataTupleList;

    private void Awake()
    {
        ConvertPairsToTuple();
    }

    private void Start()
    {
        ResetSpawning();
        if (onStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public void ResetSpawning()
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
            newlySpawn.transform.SetParent(transform);
            newlySpawn.Initialize(direction, velocity);
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + direction * 3.0f);
    }
}