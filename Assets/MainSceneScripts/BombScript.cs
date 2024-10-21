using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball") // ボールに当たったら
        {
            GameObject gsmObject = GameObject.Find("GameSceneManager");
            MainGameManager gsmScript = gsmObject.GetComponent<MainGameManager>();
            //TODO: ボムを食らった時のビジュアルエフェクトを追加する

            if (BallManager.turn)// trueの時は青ボール
            {
                StartCoroutine(gsmScript.BombBlue());
            }else
            {
                StartCoroutine(gsmScript.BombRed());
            }
            Destroy(gameObject);
        }
        
    }
}
