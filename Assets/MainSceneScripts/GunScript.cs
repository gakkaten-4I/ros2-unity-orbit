using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(gsmScript.Charge());
            Destroy(gameObject);
        }

    }
}
