
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitpoint : MonoBehaviour
{
   

    public int hp;
    // Start is called before the first frame update
    void start () {
        
        hp = 45;
    }
    void OnTriggerEnter2D(Collider2D collision){

        hp -= 3;
        Debug.Log("hit");
        
        if (hp <= 0) {
            Debug.Log("You Died!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}