
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maletmouse : MonoBehaviour
{
    // カメラの参照を追加
    public Camera mainCamera;

    void Start()
    {
        // カメラが指定されていない場合、メインカメラを取得
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // マウスのスクリーン座標を取得
        Vector3 mousePosition = Input.mousePosition;

        // マウスのスクリーン座標をワールド座標に変換
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // オブジェクトの位置をマウスのワールド座標に設定
        transform.position = worldPosition;
    }
}
