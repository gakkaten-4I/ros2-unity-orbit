using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitpoint : MonoBehaviour
{
    public int hp;
    //BossDefeatCounter  dmg;
    public GameObject boss;
    private BossBattleGameManager gameManager;
    

    // Inspectorで設定するパーティクルPrefab
    public GameObject deathParticle;

    void Start()
    {
        //dmg = boss.GetComponent<BossDefeatCounter>();  
        GameObject _gcm = GameObject.Find("GameSceneManager");
        gameManager = _gcm.GetComponent<BossBattleGameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }

    // ダメージを与える関数
    public void Damage(){
        hp -= 1;
        Debug.Log("Hit! HP: " + hp);
        if (BallManager.turn)
        {
            // Blueの与えたダメージを1増やす
            gameManager.OnBlueDamage();
        }
        else
        {
            // Redの与えたダメージを1増やす
            gameManager.OnRedDamage();
        }
        //dmg.BossDefeated(); //TODO: 削除

        if (hp <= 0)
        {
            Debug.Log("You Died!!");

            // パーティクルを生成して再生
            Instantiate(deathParticle, transform.position, Quaternion.identity);

            // オブジェクトを削除
            Destroy(gameObject);

            gameManager.OnBossDestroyed();
        }
    }

    void Update()
    {
         
        
    }
}
