using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CreatePrefab : MonoBehaviour
{
    // プレハブ格納用
    public GameObject CubePrefab;
 
    // Start is called before the first frame update
    void Start()
    {
         
    }
 
    // Update is called once per frame
    void Update()
    {
        // 一定時間ごとにプレハブを生成
        if(Time.frameCount % 60 == 0)
        {
            // 生成位置
            Vector3 pos = new Vector3(0.0f, 10.0f, 0.0f);
            // プレハブを指定位置に生成
            Instantiate(CubePrefab, pos, Quaternion.identity);
        }    
    }
}