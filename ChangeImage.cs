
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeImage : MonoBehaviour
{
    private Animator _animator;
    

    
    hitpoint hp;
    GameObject Boss;
    void Start()
    {
        _animator = GetComponent<Animator>();
        

        Boss = GameObject.Find("Boss");
        hp = Boss.GetComponent<hitpoint>();
    }

    void Update()
    {
        int _hp = hp.hp;
        _animator.SetInteger("Hitpoint", _hp);

    }
}


