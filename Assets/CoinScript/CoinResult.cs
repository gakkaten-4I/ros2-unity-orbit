using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinResult : MonoBehaviour
{
    public int CoinCountRed;
    public int CoinCountBlue;
    public TextMeshProUGUI Review1;
    public TextMeshProUGUI Review2;
    public TextMeshProUGUI ShowRed1;
    public TextMeshProUGUI ShowRed2;
    public TextMeshProUGUI ShowBlue1;
    public TextMeshProUGUI ShowBlue2;

    // Start is called before the first frame update
    void Start()
    {
        // Review1の設定
        if (Review1 != null)
        {
            RectTransform RectReview1 = Review1.GetComponent<RectTransform>();
            RectReview1.anchoredPosition = WhereTitle;
            RectReview1.sizeDelta = TitleSize;
            Review1.fontSize = FontSize;
            Review1.text = "CoinGame";
            Review1.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            Review1.enabled = false;
        }
        else
        {
            Debug.LogError("Titleオブジェクトが指定されていません");
        }

        // Review2の設定
        if (Review2 != null)
        {
            RectTransform RectReview2 = Review2.GetComponent<RectTransform>();
            RectReview2.anchoredPosition = WhereTitle;
            RectReview2.sizeDelta = TitleSize;
            Review2.fontSize = FontSize;
            Review2.text = "CoinGame";
            Review2.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            Review2.enabled = false;
        }
        else
        {
            Debug.LogError("Titleオブジェクトが指定されていません");
        }

        // ShowRed1の設定
        if (ShowRed1 != null)
        {
            RectTransform RectRed1 = ShowRed1.GetComponent<RectTransform>();
            RectRed1.anchoredPosition = WhereTitle;
            RectRed1.sizeDelta = TitleSize;
            ShowRed1.fontSize = FontSize;
            ShowRed1.text = "CoinGame";
            ShowRed1.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            ShowRed1.enabled = false;
        }
        else
        {
            Debug.LogError("ShowRed1オブジェクトが指定されていません");
        }

        //ShowRed2の設定
        if (ShowRed2 != null)
        {
            RectTransform RectRed2 = ShowRed2.GetComponent<RectTransform>();
            RectRed2.anchoredPosition = WhereTitle;
            RectRed2.sizeDelta = TitleSize;
            ShowRed2.fontSize = FontSize;
            ShowRed2.text = "CoinGame";
            ShowRed2.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            ShowRed2.enabled = false;
        }
        else
        {
            Debug.LogError("ShowRed2オブジェクトが指定されていません");
        }

        //ShowBlue1の設定
        if (ShowBlue1 != null)
        {
            RectTransform RectBlue1 = ShowBlue1.GetComponent<RectTransform>();
            RectBlue1.anchoredPosition = WhereTitle;
            RectBlue1.sizeDelta = TitleSize;
            ShowBlue1.fontSize = FontSize;
            ShowBlue1.text = "CoinGame";
            ShowBlue1.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            ShowBlue1.enabled = false;
        }
        else
        {
            Debug.LogError("ShowBlue1オブジェクトが指定されていません");
        }

        //ShowBlue2の設定
        if (ShowBlue1 != null)
        {
            RectTransform RectBlue2 = ShowBlue2.GetComponent<RectTransform>();
            RectBlue2.anchoredPosition = WhereTitle;
            RectBlue2.sizeDelta = TitleSize;
            ShowBlue2.fontSize = FontSize;
            ShowBlue2.text = "CoinGame";
            ShowBlue2.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            ShowBlue2.enabled = false;
        }
        else
        {
            Debug.LogError("ShowBlue1オブジェクトが指定されていません");
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCoinResult()
    {
        
    }
}
