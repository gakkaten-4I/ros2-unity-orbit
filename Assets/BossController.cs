using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIObjectDetection : MonoBehaviour
{
    public GraphicRaycaster raycaster;   // CanvasにアタッチされるRaycaster
    public EventSystem eventSystem;      // シーンに追加するEventSystem
    public Text debugText;               // デバッグ表示用

    void Start()
    {
        if (raycaster == null)
        {
            raycaster = GetComponent<GraphicRaycaster>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // マウスの左クリックを検出
        {
            DetectObjectAtMousePosition();
        }
    }

    void DetectObjectAtMousePosition()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);  // ポインタのデータを作成
        pointerData.position = Input.mousePosition;                        // 現在のマウス位置を取得

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);                           // Raycastを実行

        foreach (RaycastResult result in results)
        {
            debugText.text = "オブジェクトにヒット: " + result.gameObject.name;  // ヒットしたオブジェクト名を表示
            Debug.Log("オブジェクトにヒット: " + result.gameObject.name);
        }
    }
}