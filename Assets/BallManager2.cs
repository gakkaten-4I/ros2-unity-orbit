using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        Vector3 target = new Vector3(10, -5, 0);
        float step = 2.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(current, target, step);

    }
}
