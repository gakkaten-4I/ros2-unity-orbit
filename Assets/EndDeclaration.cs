using UnityEngine;
using TMPro;

public class EndDeclaration : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro1;   // 左側に表示するテキスト
    public TextMeshProUGUI textMeshPro2;   // 右側に表示するテキスト
    public float duration = 1f;            // フェードインにかかる時間
    public float startOffsetY = 200f;     // フェードイン開始位置のYオフセット

    private Vector3 startPosition1;
    private Vector3 endPosition1;
    private Vector3 startPosition2;
    private Vector3 endPosition2;
    private CanvasGroup canvasGroup1;
    private CanvasGroup canvasGroup2;
    private float timer = 0f;

    void Start()
    {
        // 左側のテキスト設定
        RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
        startPosition1 = rectTransform1.anchoredPosition + new Vector2(0, -startOffsetY);
        endPosition1 = new Vector2(-350, rectTransform1.anchoredPosition.y);
        rectTransform1.anchoredPosition = startPosition1;

        // 回転を下からz軸270度に設定
        rectTransform1.localRotation = Quaternion.Euler(0, 0, 270);

        // CanvasGroupの取得と初期設定
        canvasGroup1 = textMeshPro1.GetComponent<CanvasGroup>();
        if (canvasGroup1 == null)
            canvasGroup1 = textMeshPro1.gameObject.AddComponent<CanvasGroup>();
        canvasGroup1.alpha = 0f;

        // 右側のテキスト設定
        RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
        startPosition2 = rectTransform2.anchoredPosition + new Vector2(0, startOffsetY);
        endPosition2 = new Vector2(350, rectTransform2.anchoredPosition.y);
        rectTransform2.anchoredPosition = startPosition2;

        // 回転を上からz軸90度に設定
        rectTransform2.localRotation = Quaternion.Euler(0, 0, 90);

        // CanvasGroupの取得と初期設定
        canvasGroup2 = textMeshPro2.GetComponent<CanvasGroup>();
        if (canvasGroup2 == null)
            canvasGroup2 = textMeshPro2.gameObject.AddComponent<CanvasGroup>();
        canvasGroup2.alpha = 0f;
    }

    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // 左側のテキストの位置と透明度を更新
            RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
            rectTransform1.anchoredPosition = Vector3.Lerp(startPosition1, endPosition1, progress);
            canvasGroup1.alpha = Mathf.Lerp(0, 1, progress);

            // 右側のテキストの位置と透明度を更新
            RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
            rectTransform2.anchoredPosition = Vector3.Lerp(startPosition2, endPosition2, progress);
            canvasGroup2.alpha = Mathf.Lerp(0, 1, progress);
        }
    }
}
