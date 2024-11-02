using ROS2;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ROS2Listener : MonoBehaviour
{
    private static ROS2UnityComponent ros2Unity;
    private static ROS2Node ros2Node;
    public static int BlueStart = 370; // 青チームのゴールの開始位置
    public static int BlueEnd = 840; // 青チームのゴールの終了位置
    public static int RedStart = 370; // 赤チームのゴールの開始位置
    public static int RedEnd = 840; // 赤チームのゴールの終了位置

    // Ros2Unity Data Type
    private ISubscription<geometry_msgs.msg.Vector3> PackCoord;
    private ISubscription<std_msgs.msg.Float64> BlueDtSensor;
    private ISubscription<std_msgs.msg.Float64> RedDtSensor;

    private Vector3 targetPos;
    private MainGameManager gsmScript;

    public GameObject debugCanvas;
    DebugTextController dc;

    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gsmObject = GameObject.Find("GameSceneManager");
        if (gsmObject != null)
        {
            gsmScript = gsmObject.GetComponent<MainGameManager>();
        }
        GameObject ObjectB = GameObject.Find("DeltaY_Debug");
        if (ObjectB != null)
            dc = ObjectB.GetComponent<DebugTextController>();
        targetPos = new Vector3(0, 0, 0);
        if(ros2Unity == null)
        {
            ros2Unity = transform.GetComponent<ROS2UnityComponent>();
        }
        if (ros2Node == null && ros2Unity.Ok())
        {
            string ros2NodeName = "ROS2UnityListenerNode" + DateTime.Now.Millisecond.ToString();
            UnityEngine.Debug.Log(ros2NodeName);
            ros2Node = ros2Unity.CreateNode(ros2NodeName);
            
        }

        if (MenuManager.isFieldOne)
        {
            // カメラのPythonプログラムからの座標を受け取る
            PackCoord = ros2Node.CreateSubscription<geometry_msgs.msg.Vector3>(
              "f1/chatter", callback);
            if(gsmScript != null)
            {
                // ゴール判定用の距離センサー(青チーム側)
                BlueDtSensor = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
                  "f1/distance_0", blue_distance_callback);
                // ゴール判定用の距離センサー(赤チーム側)
                RedDtSensor = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
                                 "f1/distance_1", red_distance_callback);
            }      
        }
        else
        {
            PackCoord = ros2Node.CreateSubscription<geometry_msgs.msg.Vector3>(
              "f2/chatter", callback);
            if(gsmScript != null)
            {
                // ゴール判定用の距離センサー(青チーム側)
                BlueDtSensor = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
                  "f2/distance_0", blue_distance_callback);
                // ゴール判定用の距離センサー(赤チーム側)
                RedDtSensor = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
                                 "f2/distance_1", red_distance_callback);
            }            
        }

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
        transform.position = targetPos;
    }


    double raw_y, tanTheta, deltaY;
    double raw_x, tanPhi, deltaX;
    float inGameFieldWidth = 16.0f; // フィールドのゲーム内幅(m)
    float inGameFieldHeight = 9.0f; // フィールドのゲーム内高さ(m)
    float screenWidth = 960.0f; // カメラ補正変換後の画面幅(pixel)
    float screenHeight = 540.0f; // カメラ補正変換後の画面高さ(pixel)
    float fieldHeight = 1.25f;　// フィールドの実高さ(m)
    float halfFieldWidth = 1.1f; // フィールドの半分の幅(m)
    float cameraHeight = 1.35f; // カメラのフィールド面からの高さ(m)
    float ledHeight = 0.04f; // パックの底からLEDの高さ(m)
    float cameraToScreenBottom = 0.45f; // カメラ直下からスクリーン下端までの距離(m)
    void callback(geometry_msgs.msg.Vector3 msg)
    {
        //x座標の補正
        raw_x= msg.X;
        tanPhi = cameraHeight / ((2 * raw_x - screenWidth) / screenWidth * halfFieldWidth);
        deltaX = ledHeight / tanPhi;
        // targetPos.x = (float)((raw_x-screenWidth*deltaX/halfFieldWidth)/(screenWidth/inGameFieldWidth));
        targetPos.x = (float)(raw_x  / (screenWidth/inGameFieldWidth));



        //Y座標の補正
        raw_y = msg.Y;
        tanTheta=cameraHeight/((cameraToScreenBottom+((screenHeight)-(raw_y*-1))/screenHeight)*fieldHeight);
        deltaY=ledHeight/tanTheta;
        // targetPos.y = (float)((raw_y - screenHeight * deltaY / fieldHeight) / (screenHeight/inGameFieldHeight));
        targetPos.y = (float)(raw_y  / (screenHeight/inGameFieldHeight));

        if (dc!=null)
            dc.text = deltaX.ToString();
        Debug.Log(msg);

    }

    //private double goalDistance = 1200.0f; //仮
    //private double goalStart = 370.0f; 
    //private double goalEnd = 840.0f; 
    private void blue_distance_callback(std_msgs.msg.Float64 msg)
    {
        if (msg.Data > BlueStart && msg.Data < BlueEnd)
        {
            gsmScript.IsBlueDetected = true;
        }
        else
        {
            gsmScript.IsBlueDetected = false;
        }
    }

    private void red_distance_callback(std_msgs.msg.Float64 msg)
    {
        if(msg.Data > RedStart && msg.Data < RedEnd)
        {
            gsmScript.IsRedDetected = true;
        }
        else
        {
            gsmScript.IsRedDetected = false;
        }
    }

    private IEnumerator Detect(bool IsBlue)
    {
        if (IsBlue)
        {
            gsmScript.IsBlueDetected = true;
            yield return new WaitForSecondsRealtime(0.5f);
            gsmScript.IsBlueDetected = false;
        }
        else
        {
            gsmScript.IsRedDetected = true;
            yield return new WaitForSecondsRealtime(0.5f);
            gsmScript.IsRedDetected = false;
        }
    }
}
