using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSound : MonoBehaviour
{
    public AudioClip sound1;

    public AudioClip sound2;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound2);
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
