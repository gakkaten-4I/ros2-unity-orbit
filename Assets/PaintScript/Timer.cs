using UnityEngine;
using TMPro;  // TextMeshPro用の名前空間
using UnityEngine.UI;

public class TimerAndImageScript : MonoBehaviour
{
    public float timeLimit = 25.0f; // 制限時間（秒数）
    public float remainingTime;     // 残り時間

    public GameObject timerText;    // 残り時間を表示する1つ目のテキスト
    public GameObject timerText2;   // 残り時間を表示する2つ目のテキスト
    public Image circleImage; 
    public Image circleImage2;
    
    public Image timeImage1;
    
    public Image timeImage2;       // 円形オブジェクト (UI Image)
    public Color normalColor = Color.white;  // 通常時の色
    public Color warningColor = Color.red;   // 残り時間が少なくなった時の色

    private SpriteRenderer spriteRenderer;  
    private SpriteRenderer spriteRenderer2;  // スプライトレンダラー

    void Start()
    {
        // タイマーを初期化
        remainingTime = timeLimit;

        // SpriteRendererの取得（円形オブジェクトがSpriteRendererを持つ場合に使用）
        spriteRenderer = circleImage.GetComponent<SpriteRenderer>();
        spriteRenderer2 = circleImage2.GetComponent<SpriteRenderer>();

        // 初期色設定
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor;
            spriteRenderer2.color = normalColor;
        }
        
        // テキストと円形の初期状態に更新
        UpdateTimerText();
        UpdateCircleFill();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            // 経過時間を減らす
            remainingTime -= Time.deltaTime;

            // 残り時間が0以下にならないようにする
            if (remainingTime < 0)
            {
                remainingTime = 0;
                timerText.SetActive(false);
                timerText2.SetActive(false);
                circleImage.enabled = false;
                circleImage2.enabled = false;
                timeImage1.enabled = false;
                timeImage2.enabled = false;
            }

            // テキストと円形の状態を更新
            UpdateTimerText();
            UpdateCircleFill();

            // 残り時間に応じて色を徐々に赤に変化させる
            float t = Mathf.Clamp01(1 - (remainingTime / timeLimit));  // 0.0 から 1.0 までの補間値
            Color newColor = Color.Lerp(normalColor, warningColor, t); // 白から赤への補間

            if (spriteRenderer != null)
            {
                spriteRenderer.color = newColor;
                spriteRenderer2.color = newColor; // スプライトの色を変化
            }
            else
            {
                circleImage.color = newColor;
                circleImage2.color = newColor; // イメージの色を変化
            }
        }
    }

    // テキストを更新するメソッド
    void UpdateTimerText()
    {
        // テキスト1の更新 (TextMeshPro)
        if (timerText != null)
        {
            TMP_Text timerText1 = timerText.GetComponent<TMP_Text>();
            if (timerText1 != null)
            {
                timerText1.text = remainingTime.ToString("F1") + " s";
            }
            else
            {
                Debug.LogError("timerText に TMP_Text コンポーネントがありません！");
            }
        }

        // テキスト2の更新 (TextMeshPro)
        if (timerText2 != null)
        {
            TMP_Text timerText2Component = timerText2.GetComponent<TMP_Text>();
            if (timerText2Component != null)
            {
                timerText2Component.text = remainingTime.ToString("F1") + " s";
            }
            else
            {
                Debug.LogError("timerText2 に TMP_Text コンポーネントがありません！");
            }
        }
    }

    // 円形イメージを更新するメソッド
    void UpdateCircleFill()
    {
        if (circleImage != null)
        {
            circleImage.fillAmount = remainingTime / timeLimit;
            circleImage2.fillAmount = remainingTime / timeLimit;
        }
        else
        {
            Debug.LogError("circleImage が設定されていません！");
        }
    }
}
