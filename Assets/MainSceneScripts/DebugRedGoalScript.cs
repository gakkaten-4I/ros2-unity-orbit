using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRedGoalScript : MonoBehaviour
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
            Debug.Log("Red Goal");
            StartCoroutine(Detect());
        }
    }
    private IEnumerator Detect()
    {
        gsmScript.IsRedDetected = true;
        yield return new WaitForSecondsRealtime(0.5f);
        gsmScript.IsRedDetected = false;
    }
}
