using ROS2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ROS2Listener : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;

    // Ros2Unity Data Type
    private ISubscription<geometry_msgs.msg.Vector3> PackCoord;
    private ISubscription<std_msgs.msg.Float64> BlueDtSensor;
    private ISubscription<std_msgs.msg.Float64> RedDtSensor;

    private Vector3 targetPos;
    public MainGameManager mainGameManager;

    public GameObject debugCanvas;
    DebugTextController dc;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ObjectB = GameObject.Find("DeltaY_Debug");
        if (ObjectB != null)
            dc = ObjectB.GetComponent<DebugTextController>();
        targetPos = new Vector3(0, 0, 0);
        ros2Unity = transform.GetComponent<ROS2UnityComponent>();
        if (ros2Node == null && ros2Unity.Ok())
        {
            ros2Node = ros2Unity.CreateNode("ROS2UnityListenerNode");
            // カメラのPythonプログラムからの座標を受け取る
            PackCoord = ros2Node.CreateSubscription<geometry_msgs.msg.Vector3>(
              "chatter", callback);
            // ゴール判定用の距離センサー(青チーム側)
            BlueDtSensor = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
              "distance__dev_ttyUSB0", blue_distance_callback);
            // ゴール判定用の距離センサー(赤チーム側)
            RedDtSensor = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
                             "distance__dev_ttyUSB1", red_distance_callback);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
        // transform.position = targetPos;
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

    }

    private double goalDistance = 1200.0f; //仮
    private double goalStart = 300.0f; //仮
    private double goalEnd = 900.0f; //仮
    private void blue_distance_callback(std_msgs.msg.Float64 msg)
    {
        if (msg.Data < goalDistance)
            if (msg.Data > goalStart && msg.Data < goalEnd)
                mainGameManager.OnBlueGoalEnter();
    }

    private void red_distance_callback(std_msgs.msg.Float64 msg)
    {
        if (msg.Data < goalDistance)
            if (msg.Data > goalStart && msg.Data < goalEnd)
                mainGameManager.OnRedGoalEnter();
    }
}
