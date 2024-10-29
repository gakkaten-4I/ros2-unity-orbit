using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public float invincibleDuration = 0.2f; // 無敵時間（秒）
    private bool isInvincible = false; // 無敵状態かどうか
    private float invincibleTimer = 0.5f; // 無敵時間のタイマー

    void Update()
    {
        // 無敵状態の場合、タイマーを進行させる
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            // 無敵時間が終わったら無敵状態を解除
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
                Debug.Log("無敵状態終了");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){

        ActivateInvincibility();
    }

    // ダメージを受けるメソッド（例）
    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("無敵状態なのでダメージを受けません");
            return; // 無敵状態のときはダメージを受けない
        }

        // 通常のダメージ処理
        Debug.Log("ダメージを受けました: " + damage);
        // ここで実際にHPを減らすなどの処理を追加
    }

    // 無敵状態を開始するメソッド
    public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibleTimer = invincibleDuration;
        Debug.Log("無敵状態開始");
    }
}
