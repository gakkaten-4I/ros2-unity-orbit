using builtin_interfaces.msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject EnergyCanPrefab;
    [SerializeField] public GameObject BarrierPrefab;
    [SerializeField] GameObject[] Items;
    [SerializeField] Transform EnergyCanSpawnPoint;
    [SerializeField] Transform[] ItemSpawnPoints;
    [SerializeField] public Transform[] BarrierSpawnPoints;
    GameObject EmergencyImage;
    [SerializeField] GameObject BlueEmergencyImage;
    [SerializeField] GameObject RedEmergencyImage;

    SpriteRenderer color;
    float cla;
    float duration = 1.6f;  // n秒間
    float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayMethod(30, SpawnEnergy)); //30秒後にSpawnEnergyを実行
        StartCoroutine(RepeatSpawn());
        //EmergencyImage.SetActive(false);
        //color=EmergencyImage.GetComponent<SpriteRenderer>();
        
        BlueEmergencyImage.SetActive(false);
        RedEmergencyImage.SetActive(false);
    }

    // EnergyCanを生成
    private void SpawnEnergy()
    {
        // Instantiate energy can
        Instantiate(EnergyCanPrefab, EnergyCanSpawnPoint.position, Quaternion.identity);
        
    }
    //public IEnumerator Emergence()
    public void Emergence(bool turn)
    {
        if (turn)//trueの時は青が取得
        {
            EmergencyImage=BlueEmergencyImage;
            EmergencyImage.SetActive(true);
        }
        else
        {
            EmergencyImage=RedEmergencyImage;
            EmergencyImage.SetActive(true);
        }
        color = EmergencyImage.GetComponent<SpriteRenderer>();
        cla = color.color.a;
        StartCoroutine(DisplayEmergency());
    }
    IEnumerator DisplayEmergency()
    {
        if (elapsedTime > duration)
        {
            EmergencyImage.SetActive(false);
            yield break;
        }
        elapsedTime += UnityEngine.Time.deltaTime;
        cla += 0.1f;
        if (cla > 1)
        {
            cla = 0;
        }
        EmergencyImage.GetComponent<SpriteRenderer>().color = new Color(color.color.r, color.color.g, color.color.b, cla);
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(DisplayEmergency());
    }
    /*
    IEnumerator DisplayEmergency()
    {
        for (int i = 0; i < 4; i++)
        {
            EmergencyImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            EmergencyImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    */

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
