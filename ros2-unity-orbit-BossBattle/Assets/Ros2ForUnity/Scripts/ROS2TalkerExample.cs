using ROS2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ROS2
{

/// <summary>
/// An example class provided for testing of basic ROS2 communication
/// </summary>
public class ROS2TalkerExample : MonoBehaviour
{
    // Start is called before the first frame update
    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    //private IPublisher<std_msgs.msg.String> chatter_pub;
    private ISubscription<geometry_msgs.msg.Vector3> chatter_pub;

    void Start()
    {
            TryGetComponent(out ros2Unity);
        }

    void Update()
    {
        if (ros2Unity.Ok())
        {
            if (ros2Node == null)
            {
                ros2Node = ros2Unity.CreateNode("ROS2UnityTalkerNode");
                chatter_pub = (ISubscription<geometry_msgs.msg.Vector3>)ros2Node.CreatePublisher<geometry_msgs.msg.Vector3>("chatter");
            }

            geometry_msgs.msg.Vector3 msg = new geometry_msgs.msg.Vector3();
            msg.X = this.transform.position.x;
            msg.Y = this.transform.position.y;
            //chatter_pub.Publish(msg);
        }
    }
}

}  // namespace ROS2
