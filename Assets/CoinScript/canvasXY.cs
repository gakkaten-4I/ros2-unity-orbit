using UnityEngine;

public class GetCanvasPosition : MonoBehaviour
{
    void Start()
    {
        // CanvasオブジェクトのRectTransformを取得
        RectTransform canvasRectTransform = GetComponent<RectTransform>();

        // ローカルUI座標（Anchored Position）の取得
        Vector2 anchoredPosition = canvasRectTransform.anchoredPosition;

        // ローカルUI座標のX, Y成分を個別に取得
        float posX_anchored = anchoredPosition.x;
        float posY_anchored = anchoredPosition.y;

        Debug.Log("Anchored Position: ");
        Debug.Log("PosX: " + posX_anchored);
        Debug.Log("PosY: " + posY_anchored);
    }
}
