using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossConcatenation : MonoBehaviour
{
    
    public int initialPosition = 100;
    

    public Sprite boss1Sprite;

    public GameObject bossPrefab;          // ボスのプレハブを参照するためのフィールド
    
    void Start(){
                GameObject parentObject = new GameObject("BossImage");
        for (int i = 0; i < 5; i++) // ボスを5つ並べるためのループ
        {
             Vector3 bossPosition = new Vector3(initialPosition + (i * 100), 0, 0); // X座標を100ずつずらす
             GameObject newObject = new GameObject("Boss" + i); // 新しいGameObjectを作成
             newObject.transform.position = bossPosition;       // 位置を設定
             SpriteRenderer renderer = newObject.AddComponent<SpriteRenderer>(); // SpriteRendererを追加
             renderer.sprite = boss1Sprite; // スプライトを設定
             newObject.transform.localScale = new Vector3(1, 1, 1); // スケールが適切か確認    
        //     // Debugログで確認
        // Debug.Log("Created object at position: " + newObject.transform.position);


        // ボスの位置を設定
        //    Vector3 bossPosition = new Vector3(initialPosition + (i * 100), 0, 0); // X座標を100ずつずらす
            
            // プレハブを複製して位置を設定
        //    GameObject newBoss = Instantiate(bossPrefab, bossPosition, Quaternion.identity); 
        //    newBoss.transform.SetParent(parentObject.transform);
            // デバッグログで情報を確認
        //    Debug.Log("Created Boss " + i + " at position: ");
        }
    }
    void Update(){
        // public Vector3 bossposition = new Vector3(position,0,0);
        // GameObject newObject = Instantiate(boss1Sprite ,bossposition,0);
        // position = position + 100; 
    }
}