using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossConcatenation : MonoBehaviour
{
    
    public int initialPosition = 0;
    

    public Sprite boss1Sprite;

    public GameObject bossPrefab;          // ボスのプレハブを参照するためのフィールド
    
    void Start(){
        if (bossPrefab == null)
        {
            Debug.LogError("bossPrefab is not assigned.");
        }
        for ( int i = 0; i < 5; i++){
            Vector3 bossPosition = new Vector3(initialPosition + (i * 100), 0, i*0.1f);
            GameObject newBoss = Instantiate(bossPrefab, bossPosition, Quaternion.identity); 
           // newObject.tag = "boss";
        }
        //         GameObject parentObject = new GameObject("BossImage");
        // for (int i = 0; i < 5; i++) // ボスを5つ並べるためのループ
        // {
        //       Vector3 bossPosition = new Vector3(initialPosition + (i * 100), 0, i*0.1f); // X座標を100ずつずらす
        //       GameObject newObject = new GameObject("Boss" + i); // 新しいGameObjectを作成
        //       newObject.transform.position = bossPosition;       // 位置を設定
        //       SpriteRenderer renderer = newObject.AddComponent<SpriteRenderer>(); // SpriteRendererを追加
        //       renderer.sprite = boss1Sprite; // スプライトを設定
        //       newObject.transform.localScale = new Vector3(1, 1, 1); // スケールが適切か確認    
        //  //     // Debugログで確認
        //   Debug.Log("Created object at position: " + newObject.transform.position);


    }
    void Update(){
        
    }
}