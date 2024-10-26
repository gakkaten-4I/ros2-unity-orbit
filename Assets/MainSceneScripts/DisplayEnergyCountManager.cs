using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayEnergyCountManager : MonoBehaviour
{
    //テキストオブジェクト
    [SerializeField] private TextMeshProUGUI EnergyCount;

    //MainGameManager mainGameManager;

    // Start is called before the first frame update
    void Start()
    {
        EnergyCount.text = ":" + 0 + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReflectCount(short count)
    {
        EnergyCount.text = ":" + count + "";
    }
}
