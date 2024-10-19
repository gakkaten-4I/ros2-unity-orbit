 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //エナジー缶のゲームオブジェクト
    private GameObject EnergyCan;
    private GameObject[]  RedItems, BlueItems;
    
    // Start is called before the first frame update
    void Start()
    {
        RedItems = new GameObject[4];
        Application.targetFrameRate = 60;//フレームレートを60に固定
        EnergyCan = GameObject.Find("EnergyCan");
        EnergyCan.SetActive(false);
        RedItems[0]= GameObject.Find("RedItem1");
        Debug.Log("success");
    }
    private IEnumerator Energy()
    {
        // 210秒間待つ
        yield return new WaitForSecondsRealtime(2);
        EnergyAppeare();
    }
    void EnergyAppeare()
    {
        EnergyCan.SetActive(true);
    }
    void EnergyAppeareRed()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // コルーチンの起動
        StartCoroutine(Energy());
    }
}
