using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
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
                _ = gsmScript.EnableBlueShield(destroyCancellationToken);
                GameObject biaObject = GameObject.Find("BlueItemArea");
                BlueItemAreaScript biaScript = biaObject.GetComponent<BlueItemAreaScript>();
                biaScript.AddItem(Items.Shield);
            }
            else
            {
                _= gsmScript.EnableRedShield(destroyCancellationToken);
                GameObject riaObject = GameObject.Find("RedItemArea");
                BlueItemAreaScript riaScript = riaObject.GetComponent<BlueItemAreaScript>();
                riaScript.AddItem(Items.Shield);
            }
            gameObject.SetActive(false);
            _ = DelayedDestruction(destroyCancellationToken, 15); // アイテムの持続時間は10秒なので、それより長い時間で削除
        }
        
    }

    public async ValueTask DelayedDestruction(CancellationToken token, float waitTime)
    {
        await Task.Delay(TimeSpan.FromSeconds(waitTime), token);
        Destroy(gameObject);
    }
}
