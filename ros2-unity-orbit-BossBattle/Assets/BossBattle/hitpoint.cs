using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitpoint : MonoBehaviour
{
    public int hp;
    

    // Inspectorで設定するパーティクルPrefab
    public GameObject deathParticle;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }
    // ダメージを与える関数
    public void Damage(){
        hp -= 1;
        Debug.Log("Hit! HP: " + hp);

        if (hp <= 0)
        {
            Debug.Log("You Died!!");

            // パーティクルを生成して再生
            Instantiate(deathParticle, transform.position, Quaternion.identity);

            // オブジェクトを削除
            Destroy(gameObject);

            SceneManager.LoadScene("MainScene");
        }
        
    }
}
