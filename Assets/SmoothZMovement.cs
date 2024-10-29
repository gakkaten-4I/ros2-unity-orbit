using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SmoothZMovement : MonoBehaviour
{
    // コルーチンでZ座標を滑らかに変化させる
    IEnumerator MoveZ()
    {
        float duration1 = 0.15f;  // 0.15秒でZ座標を1に  // 0.3秒でZ座標を0に戻す
        float startZ = 0f;        // 初期Z座標
        float endZ = -1f;          // 移動先のZ座標

        // Z座標を0から1へ変化させる (0.15秒間)
        float elapsedTime = 0f;
        while (elapsedTime < duration1)
        {
            elapsedTime += Time.deltaTime;
            float zPos = Mathf.Lerp(startZ, endZ, elapsedTime / duration1);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
            yield return null;
        }

        // Z座標を1から0へ戻す (さらに0.3秒間)
        elapsedTime = 0f;
        while (elapsedTime < duration1)
        {
            elapsedTime += Time.deltaTime;
            float zPos = Mathf.Lerp(endZ, startZ, elapsedTime / duration1);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
            yield return null;
        }

        // 最後にしっかりとZ座標を0に設定
        transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
    }

    // スタート時にコルーチンを呼び出す
    void start()
    {
        StartCoroutine(MoveZ());
    }
}