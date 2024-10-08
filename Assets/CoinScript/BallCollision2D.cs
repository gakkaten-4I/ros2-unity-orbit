using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallDestroyOnCollision2D : MonoBehaviour
{
    // 破壊する対象のレイヤー（Inspectorから設定できるようにする）
    public LayerMask destroyableLayer;
    public int CoinCountRed = 0;
    public int CoinCountBlue = 0;
    public TextMeshProUGUI CoinTextBlue;
    public TextMeshProUGUI CoinTextRed;
    public int PlayerNum = 0;
    private int frameCount = 0;
    private int contableFlag = 1;

    public GameObject CollectEffectRed;　//赤エフェクト
    public GameObject CollectEffectBlue; //青エフェクト

    /*
        // 物理的な衝突時に呼ばれるメソッド（2D）
        void OnCollisionEnter2D(Collision2D collision)
        {
            // 衝突したオブジェクトのレイヤーが指定のLayerMaskに含まれているか確認
            if (((1 << collision.gameObject.layer) & destroyableLayer) != 0)
            {
                // 指定のレイヤーであれば破壊する
                Destroy(collision.gameObject);
            }
        }
        */

    void Update(){

    }

    // トリガーとしての衝突時に呼ばれるメソッド（2D）
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したオブジェクトのレイヤーが指定のLayerMaskに含まれているか確認
        if (((1 << other.gameObject.layer) & destroyableLayer) != 0 && contableFlag == 1)
        {
            // 指定のレイヤーであれば破壊する

            //エフェクト発生(青)
            if(playerNum = 0){
                Instantiate(CollectEffectBlue, transform.position, transform.rotation);

                Destroy(other.gameObject);
                UpdateCoinCountTextBlue();
                contableFlag = 0;
                StartCoroutine(WaitAndExecuteFunction(0.1f));
            }

            //エフェクト発生(赤)
            if(playerNum = 1){
                Instantiate(CollectEffectRed, transform.position, transform.rotation);

                Destroy(other.gameObject);
                UpdateCoinCountTextRed();
                contableFlag = 0;
                StartCoroutine(WaitAndExecuteFunction(0.1f));
            }
            


        }
    }

    IEnumerator WaitAndExecuteFunction(float waitTime)
    {
        // 指定した時間だけ待機する

        yield return new WaitForSeconds(waitTime);
        contableFlag = 1;
        // 待機後に実行する関数
        
    }

    //ブルーサイドとレッドサイドの点数の表示    
    void UpdateCoinCountTextBlue()
    {
        CoinCountBlue =+ 1;
        // テキストメッシュプロに破壊数を表示
        CoinTextBlue.text = "Coin: " + CoinCountRed.ToString();
    }

    void UpdateCoinCountTextRed()
    {
        CoinCountRed += 1;
        CoinTextRed.text = "Coin: " + CoinCountRed.Tostirng();
    }
}
