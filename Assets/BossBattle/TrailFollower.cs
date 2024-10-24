using UnityEngine;

public class TrailFollower : MonoBehaviour
{
    public GameObject target; // 追従対象のオブジェクト
    private TrailRecorder trailRecorder;

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
            transform.position = trailRecorder.GetDelayedPosition();
        }
    }
}
