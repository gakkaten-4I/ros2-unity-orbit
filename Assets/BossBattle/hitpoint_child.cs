using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class hitpoint_child : MonoBehaviour
{
    hitpoint move1;
    GameObject _Boss;
    // Start is called before the first frame update
    void Start () {
        _Boss = GameObject.Find("Boss");
        move1 = _Boss.GetComponent<hitpoint>();
    }

    void OnTriggerEnter2D(Collider2D collision){

        move1.hp -= 3;
        Debug.Log("hit");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
