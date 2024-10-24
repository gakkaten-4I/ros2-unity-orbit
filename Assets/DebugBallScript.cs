using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBallScript : MonoBehaviour
{
    private bool isMouseButtonDown = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
        }
        if (isMouseButtonDown)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objectPos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
        }

        if(BallManager.turn)//blue
        {
            spriteRenderer.color= new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else
        {
            spriteRenderer.color= new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
