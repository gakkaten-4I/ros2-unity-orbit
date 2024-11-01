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

    public AudioClip sound1;
    //public AudioClip sound2;

    AudioSource audioSource; 

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
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
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
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.8f;

            GameObject obj = GameObject.Find("GameObject");
    
    // GameObjectが見つかった場合、そのオブジェクトにアタッチされている DeadSound スクリプトを取得
                if (obj != null)
                {
                    DeadSound deadSoundScript = obj.GetComponent<DeadSound>();
                    
                    // DeadSound スクリプトが存在する場合、sound メソッドを呼び出す
                    if (deadSoundScript != null)
                    {
                        deadSoundScript.sound();
                    }

                }

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
