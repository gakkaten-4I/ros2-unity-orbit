using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildScript : MonoBehaviour
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
            if (BallManager.turn)// trueの時は青ボール
            {
                StartCoroutine(gsmScript.EnableBlueShield());
            }
            else
            {
                StartCoroutine(gsmScript.EnableRedShield());
            }
            Destroy(gameObject);
        }
        
    }
}
