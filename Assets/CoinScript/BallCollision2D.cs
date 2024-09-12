using UnityEngine;
using TMPro;

public class BallDestroyOnCollision2D : MonoBehaviour
{
    // 破壊する対象のレイヤー（Inspectorから設定できるようにする）
    public LayerMask destroyableLayer;
    public int CoinCount = 0;
    public int CoinCount2 = 0;
    public TextMeshProUGUI CoinText;
    public int PlayerNum = 0;

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

    // トリガーとしての衝突時に呼ばれるメソッド（2D）
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したオブジェクトのレイヤーが指定のLayerMaskに含まれているか確認
        if (((1 << other.gameObject.layer) & destroyableLayer) != 0)
        {
            // 指定のレイヤーであれば破壊する
            
            Debug.Log(CoinCount);
            Destroy(other.gameObject);
            CoinCount++;
            UpdateCoinCountText();
        }
    }

    void UpdateCoinCountText()
    {
        // テキストメッシュプロに破壊数を表示
        CoinText.text = "Coin: " + CoinCount.ToString();
    }
}