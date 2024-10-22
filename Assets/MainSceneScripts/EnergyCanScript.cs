using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCanManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball") // ボールに当たったら
        {
            GameObject gsmObject = GameObject.Find("GameSceneManager");
            MainGameManager gsmScript = gsmObject.GetComponent<MainGameManager>();
            _ = gsmScript.EnableBlueShield(destroyCancellationToken);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
