using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject EnergyCanPrefab;
    [SerializeField] GameObject[] Items;
    [SerializeField] Transform EnergyCanSpawnPoint;
    [SerializeField] Transform[] ItemSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayMethod(5, SpawnEnergy)); //2�b���SpawnEnergy�����s
        StartCoroutine(RepeatSpawn());
    }

    // EnergyCan�𐶐�
    private void SpawnEnergy()
    {
        // Instantiate energy can
        Instantiate(EnergyCanPrefab, EnergyCanSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator RepeatSpawn()
    {
        while (true)
        {
            // Do anything
            Vector3 spawnPoint = ItemSpawnPoints[UnityEngine.Random.Range(0, ItemSpawnPoints.Length)].position;
            GameObject item = Items[UnityEngine.Random.Range(0, Items.Length)];
            Instantiate(item, spawnPoint, Quaternion.identity);
            Debug.Log("Item Spawned");
            yield return new WaitForSeconds(2);
        }
    }

    // �x�����s���邽�߂̃��\�b�h�����L����
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
