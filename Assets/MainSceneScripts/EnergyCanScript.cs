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
        Debug.Log("すり抜けた！");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
