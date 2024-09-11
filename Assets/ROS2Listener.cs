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
    private Vector3 targetPos;

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
    float screenWidth = 885.0f;
    float screenHeight = 668.0f;
    float fieldHeight = 1.3f;
    float halfFieldWidth = 0.84f;
    float cameraHeight = 1.12f;
    float ledHeight = 0.04f;
    float cameraToScreenBottom = 0.44f;
    void callback(geometry_msgs.msg.Vector3 msg)
    {
        //x���W�̂���␳
        raw_x= msg.X;
        tanPhi = cameraHeight / ((2 * raw_x - screenWidth) / screenWidth * halfFieldWidth);
        deltaX = ledHeight / tanPhi;
        targetPos.x = (float)((raw_x-screenWidth*deltaX/halfFieldWidth)/66.8f);


        //Y���W�̂���␳
        raw_y = msg.Y;
        tanTheta=cameraHeight/((cameraToScreenBottom+((screenHeight)-(raw_y*-1))/screenHeight)*fieldHeight);
        deltaY=ledHeight/tanTheta;
        targetPos.y = (float)((raw_y - screenHeight * deltaY / fieldHeight) / 66.8f);

        if(dc!=null)
            dc.text = deltaX.ToString();

    }
}
