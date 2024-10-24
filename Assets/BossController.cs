using UnityEngine;
using TMPro;

public class BossDamage : MonoBehaviour
{
    // 破壊する対象のレイヤー（Inspectorから設定できるようにする）
    public LayerMask destroyableLayer;
    public int player_a_hp = 0;
    public int player_b_hp = 0;
    public TextMeshProUGUI scoreText;
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
            
            Debug.Log("hit");
            Destroy(other.gameObject);
            player_a_hp++;
            UpdateplayerCountText();
        }
    }

    void UpdateplayerCountText()
    {
        // scoreText = GameObject.Find("player_a_score");
        // Text a_score = scoreText.GetComponent<Text>();
        // // テキストメッシュプロに破壊数を表示
        // a_score.text = "Coin: " + player_a_hp.ToString();
    }
}
