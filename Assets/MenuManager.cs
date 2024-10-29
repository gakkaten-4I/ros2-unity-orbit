using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // キーが押されたらシーンを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // シーンの切り替え
            SceneManager.LoadScene("MainScene");
        }
    }
}
