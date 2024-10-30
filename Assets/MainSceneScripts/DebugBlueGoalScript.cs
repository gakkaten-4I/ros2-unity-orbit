using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBlueGoalScript : MonoBehaviour
{
    private MainGameManager gsmScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gsmObject = GameObject.Find("GameSceneManager");
        gsmScript = gsmObject.GetComponent<MainGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Blue Goal");
            gsmScript.IsBlueDetected = true;
        }
    }
}
