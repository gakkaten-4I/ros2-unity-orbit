using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTextController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI debugText;
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = text;
    }
}
