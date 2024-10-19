using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCanManager : MonoBehaviour
{
    private GameObject EnergyCanImage;
    private GameObject[] RedItems, BlueItems;
    // Start is called before the first frame update
    void Start()
    {
        EnergyCanImage = GameObject.Find("EnergyCan");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ç∑ÇËî≤ÇØÇΩÅI");
        EnergyCanImage.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
