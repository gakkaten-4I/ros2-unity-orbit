using ROS2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ROS2Listener : MonoBehaviour
{
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    //private ISubscription<std_msgs.msg.String> chatter_sub;
    private ISubscription<geometry_msgs.msg.Vector3> chatter_sub;
    private ISubscription<std_msgs.msg.Float64> distance_sub;
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
            chatter_sub = ros2Node.CreateSubscription<geometry_msgs.msg.Vector3>(
              "chatter", callback);
            distance_sub = ros2Node.CreateSubscription<std_msgs.msg.Float64>(
              "distance__dev_ttyUSB0", distance_callback);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
        transform.position = targetPos;
    }
    double raw_y, tanTheta, deltaY;
    double raw_x, tanPhi, deltaX;
    float screenWidth = 960.0f;
    float screenHeight = 540.0f;
    float fieldHeight = 1.25f;
    float halfFieldWidth = 1.1f;
    float cameraHeight = 1.35f;
    float ledHeight = 0.04f;
    float cameraToScreenBottom = 0.45f;
    void callback(geometry_msgs.msg.Vector3 msg)
    {
        //x���W�̂���␳
        raw_x= msg.X;
        tanPhi = cameraHeight / ((2 * raw_x - screenWidth) / screenWidth * halfFieldWidth);
        deltaX = ledHeight / tanPhi;
        // targetPos.x = (float)((raw_x-screenWidth*deltaX/halfFieldWidth)/60.0f);
        targetPos.x = (float)(raw_x  / 60.0f);



        //Y���W�̂���␳
        raw_y = msg.Y;
        tanTheta=cameraHeight/((cameraToScreenBottom+((screenHeight)-(raw_y*-1))/screenHeight)*fieldHeight);
        deltaY=ledHeight/tanTheta;
        // targetPos.y = (float)((raw_y - screenHeight * deltaY / fieldHeight) / 60.0f);
        targetPos.y = (float)(raw_y  / 60.0f);

        if (dc!=null)
            dc.text = deltaX.ToString();

    }

    private double goalDistance = 1200.0f; //仮
    private double goalStart = 300.0f; //仮
    private double goalEnd = 900.0f; //仮
    void distance_callback(std_msgs.msg.Float64 msg)
    {
        if (msg.Data < goalDistance)
            if (msg.Data > goalStart && msg.Data < goalEnd)
                mainGameManager.Goal();
    }
}
