using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSound : MonoBehaviour
{
    public AudioClip sound1;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sound(){
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
    }
}
