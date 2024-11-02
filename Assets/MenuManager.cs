using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle IsFieldOne;
    public TMP_InputField BlueStart;
    public TMP_InputField BlueEnd;
    public TMP_InputField RedStart;
    public TMP_InputField RedEnd;

    public static bool isFieldOne = true;
    
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
            if(IsFieldOne.isOn)
            {
                isFieldOne = true;
            }
            else
            {
                isFieldOne = false;
            }
            ROS2Listener.BlueStart = int.Parse(BlueStart.text);
            ROS2Listener.BlueEnd = int.Parse(BlueEnd.text);
            ROS2Listener.RedStart = int.Parse(RedStart.text);
            ROS2Listener.RedEnd = int.Parse(RedEnd.text);
            // シーンの切り替え
            SceneManager.LoadScene("MainScene");
        }
    }
}
