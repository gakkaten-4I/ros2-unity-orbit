using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GunScript : MonoBehaviour
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
            //TODO: ボールの軌跡を太くする
            //StartCoroutine(gsmScript.Charge());
            _ = gsmScript.Charge(destroyCancellationToken);
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
