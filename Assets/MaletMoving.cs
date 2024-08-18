using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaletMoving : MonoBehaviour
{

    public GameObject Malet;
    Vector3 MaletPosition;

    // Start is called before the first frame update
    void Start()
    {
        MaletPosition=new Vector3(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        MaletMove();
    }

    void MaletMove(){
        if(Input.GetKey(KeyCode.Tab)){
            MaletPosition.x=(Input.mousePosition.x)/50;
            MaletPosition.y=(Input.mousePosition.y)/50;
            MaletPosition.z=0f;
            Malet.transform.position=MaletPosition;
            Debug.Log(MaletPosition);
        }
    }
}
