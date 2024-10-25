using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleCall : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Fight;

    float fontscaleX = 1f;
    float fontscaleY = 0.75f;
    float fontX = 50f;
    float fontY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 WhereTitle = new Vector3(0f, 0f, 0f);
        Vector3 WhereFight = new Vector3(fontX,fontY,0f);
        Vector2 TitleSize = new Vector2(250f, 30f);
        float FontSize = 50f;

        // Titleの設定
        if (Title != null)
        {
            RectTransform rectTransformTitle = Title.GetComponent<RectTransform>();
            rectTransformTitle.anchoredPosition = WhereTitle;
            rectTransformTitle.sizeDelta = TitleSize;
            Title.fontSize = FontSize;
            Title.text = "CoinGame";
            Title.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            Title.enabled = false;
        }
        else
        {
            Debug.LogError("Titleオブジェクトが指定されていません");
        }

        // Fightの設定
        if (Fight != null)
        {
            RectTransform rectTransformFight = Fight.GetComponent<RectTransform>();
            rectTransformFight.anchoredPosition = WhereFight;
            Fight.transform.localScale = new Vector3(fontscaleX, fontscaleY, 0f);
            Fight.fontSize = FontSize;
            Fight.text = "Fight";
            Fight.color = new Color(0.0f, 0.0f, 0.0f, 1f);
            Fight.enabled = false;
        }
        else
        {
            Debug.LogError("Fightオブジェクトが指定されていません");
        }

        //StartCoroutine(StartGame());
    }

    // コルーチンによる順次処理
    private IEnumerator StartGame()
    {
        RectTransform rectTransformFight = Fight.GetComponent<RectTransform>();
        // Titleを表示
        Title.enabled = true;
        var length = Title.text.Length;
        for(int i=0;i<length;i++){
            Title.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.3f);
        }
        Title.maxVisibleCharacters = length;
        yield return new WaitForSeconds(0.8f);

        // Titleを非表示
        Title.enabled = false;

        // Fightを表示
        Fight.enabled = true;
        yield return new WaitForSeconds(0.8f);
        // Fightのアニメーションを開始（拡大しながら色をフェードアウト）
        for (int i = 0; i < 25; i++)
        {
            fontscaleX += 0.08f;  // 拡大速度を調整
            fontscaleY += 0.04f;
            fontY += 2.5f;
            fontX -= 3f;
            
            // Fightの色を徐々に透明に
            Fight.color = new Color(Fight.color.r, Fight.color.g, Fight.color.b, Fight.color.a - 1f / 50f);
            
            // Fightの拡大
            Fight.transform.localScale = new Vector3(fontscaleX, fontscaleY, 1f);
            Fight.fontSize += 1.5f;
            rectTransformFight.anchoredPosition = new Vector3(fontX,fontY,0f);

            // 0.1秒待機
            yield return new WaitForSeconds(0.01f);
        }

        Fight.enabled = false;
    }

    // Updateは特に不要なので削除するか、使用していない場合はそのままでOK
    void Update()
    {
        
    }
}
