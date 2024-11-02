using UnityEngine;
using TMPro;
using System.Collections;

public class EndDeclaration : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro1;   // 左側に表示するテキスト
    public TextMeshProUGUI textMeshPro2;   // 右側に表示するテキスト
    public float duration = 1f;            // フェードインとフェードアウトにかかる時間
    public float waitDuration = 1f;        // フェードイン後の待機時間
    public float startOffsetY = 200f;      // フェードイン開始位置のYオフセット

    private Vector3 startPosition1;
    private Vector3 endPosition1;
    private Vector3 startPosition2;
    private Vector3 endPosition2;
    private CanvasGroup canvasGroup1;
    private CanvasGroup canvasGroup2;

    void Start()
    {
        // 左側のテキスト設定
        RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
        startPosition1 = rectTransform1.anchoredPosition + new Vector2(0, -startOffsetY);
        endPosition1 = new Vector2(-175, rectTransform1.anchoredPosition.y);
        rectTransform1.anchoredPosition = startPosition1;
        rectTransform1.localRotation = Quaternion.Euler(0, 0, 270);

        // CanvasGroupの取得と初期設定
        canvasGroup1 = textMeshPro1.GetComponent<CanvasGroup>();
        if (canvasGroup1 == null)
            canvasGroup1 = textMeshPro1.gameObject.AddComponent<CanvasGroup>();
        canvasGroup1.alpha = 0f;

        // 右側のテキスト設定
        RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
        startPosition2 = rectTransform2.anchoredPosition + new Vector2(0, startOffsetY);
        endPosition2 = new Vector2(175, rectTransform2.anchoredPosition.y);
        rectTransform2.anchoredPosition = startPosition2;
        rectTransform2.localRotation = Quaternion.Euler(0, 0, 90);

        // CanvasGroupの取得と初期設定
        canvasGroup2 = textMeshPro2.GetComponent<CanvasGroup>();
        if (canvasGroup2 == null)
            canvasGroup2 = textMeshPro2.gameObject.AddComponent<CanvasGroup>();
        canvasGroup2.alpha = 0f;

        // フェードイン・待機・フェードアウトのシーケンス開始
        StartCoroutine(FadeInOutSequence());
    }

    private IEnumerator FadeInOutSequence()
    {
        //効果音
        GetComponent<AudioSource>().Play();
        // フェードイン
        yield return StartCoroutine(FadeIn());

        // 待機
        yield return new WaitForSeconds(waitDuration);

        // フェードアウト
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < duration)
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

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;

        RectTransform rectTransform1 = textMeshPro1.GetComponent<RectTransform>();
        RectTransform rectTransform2 = textMeshPro2.GetComponent<RectTransform>();
        startPosition1.y = endPosition1.y - startPosition1.y;
        startPosition2.y = endPosition2.y - startPosition2.y;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // 左側のテキストの位置と透明度を逆方向に更新
            rectTransform1.anchoredPosition = Vector3.Lerp(endPosition1, startPosition1, progress);
            canvasGroup1.alpha = Mathf.Lerp(1, 0, progress);

            // 右側のテキストの位置と透明度を逆方向に更新
            rectTransform2.anchoredPosition = Vector3.Lerp(endPosition2, startPosition2, progress);
            canvasGroup2.alpha = Mathf.Lerp(1, 0, progress);

            yield return null;
        }
    }
}
