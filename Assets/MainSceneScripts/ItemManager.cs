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
        StartCoroutine(DelayMethod(30, SpawnEnergy)); //30秒後にSpawnEnergyを実行
        StartCoroutine(RepeatSpawn());
    }

    // EnergyCanを生成
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
            yield return new WaitForSeconds(15);
        }
    }

    // 遅延実行するためのメソッドを共有する
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
