using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource; 

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter2D は他のコライダーと接触したときに呼ばれる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトのタグが "wall" の場合
        if (collision.CompareTag("Wall"))
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(sound1);

        }
    }
}
