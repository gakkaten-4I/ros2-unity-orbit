using System.Collections.Generic;
using UnityEngine;

public class TrailRecorder : MonoBehaviour
{
    public float delayTime = 0.5f; // 0.5秒遅れ
    private Queue<Vector3> positionHistory = new Queue<Vector3>();
    private float recordInterval = 0.1f; // 位置を記録する間隔
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 一定時間ごとに位置を記録する
        if (timer >= recordInterval)
        {
            positionHistory.Enqueue(transform.position);
            timer = 0f;
        }

        // 指定の遅延時間を超える古いデータを削除
        while (positionHistory.Count > delayTime / recordInterval)
        {
            positionHistory.Dequeue();
        }
    }

    // 遅れた位置を取得するメソッド
    public Vector3 GetDelayedPosition()
    {
        if (positionHistory.Count > 0)
        {
            return positionHistory.Peek(); // キューの先頭にある位置を返す
        }

        return transform.position; // キューが空なら現在位置を返す
    }
}
