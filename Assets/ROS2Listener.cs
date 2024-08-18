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

    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector3(0, 0, 0);
        ros2Unity = GameObject.Find("ROS2Main").GetComponent<ROS2UnityComponent>();
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
    void callback(geometry_msgs.msg.Vector3 msg)
    {

        targetPos.x = (float)msg.X;
        targetPos.y = (float)msg.Y;
        targetPos.z = (float)msg.Z;
        Debug.Log("Unity listener heard: [" + msg.X + "]");
    }
}
