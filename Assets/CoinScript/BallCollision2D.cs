using UnityEngine;
using TMPro;
using System.Collections;

public class BallDestroyOnCollision2D : MonoBehaviour
{
    // 破壊する対象のレイヤー（Inspectorから設定できるようにする）
    public LayerMask destroyableLayer;
    public int CoinCount = 0;
    public int CoinCount2 = 0;
    public TextMeshProUGUI CoinText;
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
            Instantiate(CollectEffectBlue, transform.position, transform.rotation);

            CoinCount++;
            Destroy(other.gameObject);
            UpdateCoinCountText();
            contableFlag = 0;
            StartCoroutine(WaitAndExecuteFunction(0.1f));


        }
    }

    IEnumerator WaitAndExecuteFunction(float waitTime)
    {
        // 指定した時間だけ待機する

        yield return new WaitForSeconds(waitTime);
        contableFlag = 1;
        // 待機後に実行する関数
        
    }


    void UpdateCoinCountText()
    {
        // テキストメッシュプロに破壊数を表示
        CoinText.text = "Coin: " + CoinCount.ToString();
    }
}
