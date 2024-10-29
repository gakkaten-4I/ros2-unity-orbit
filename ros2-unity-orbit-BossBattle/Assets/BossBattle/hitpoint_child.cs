using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitpoint_child : MonoBehaviour
{
    hitpoint move1;  // Bossのスクリプト参照
    GameObject _Boss;  // Bossオブジェクト

    // Inspectorから設定するパーティクルPrefab
    public GameObject deathParticle;

    void Start()
    {
        // Bossオブジェクトとそのスクリプトの取得
        _Boss = GameObject.Find("Boss");
        move1 = _Boss.GetComponent<hitpoint>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //本体のダメージ関数を呼ぶ
        
        // HPを減少させる
        move1.Damage();
        Debug.Log("hit! Boss HP: " + move1.hp);

        // HPが0以下になったらエフェクトを表示してBossを削除
        if (move1.hp <= 30)
        {
            Debug.Log("Boss Defeated!");

            // パーティクルエフェクトを再生
            Instantiate(deathParticle, _Boss.transform.position, Quaternion.identity);

            // Bossオブジェクトを削除
            Destroy(this.gameObject);
        }
    }


}
