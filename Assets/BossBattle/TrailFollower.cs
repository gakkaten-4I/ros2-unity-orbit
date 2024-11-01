using UnityEngine;

public class TrailFollower : MonoBehaviour
{
    public GameObject target; // 追従対象のオブジェクト
    public Route route;
    private TrailRecorder trailRecorder;
    private Vector3 targetPos;

    void Start()
    {
        if (target != null)
        {
            trailRecorder = target.GetComponent<TrailRecorder>();
        }
    }

    void Update()
    {
        if (trailRecorder != null)
        {
            // 0.5秒遅れた位置に追従させる
            //transform.position = trailRecorder.GetDelayedPosition();
            targetPos = trailRecorder.GetDelayedPosition();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, route.speed*Time.deltaTime);
    }
}
