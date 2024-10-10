using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    private float FontSizeReview = 20f;
    private float FontSizeScore = 15f;

    private int RandNum;

    private float boundTime = 0.2f;
    private float buindHeight = 20f;

    // Start is called before the first frame update
    public void ShowCoinResult()
    {
        // Review1の設定
        if (Review1 != null)
        {
            RectTransform RectReview1 = Review1.GetComponent<RectTransform>();
            RectReview1.anchoredPosition = new Vector3(0f,0f,0f);
            //RectReview1.sizeDelta = new Vector2(0f,0f);
            Review1.fontSize = FontSizeReview;
            Review1.transform.Rotate(0,0,-90);
            Review1.text = "結果発表";
            Review1.color = new Color(0.0f, 0.0f, 0.0f, 1f);
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
            RectReview2.anchoredPosition = new Vector3(0f,0f,0f);
            //RectReview2.sizeDelta = new Vector2(0f,0f);
            Review2.fontSize = FontSizeReview;
            Review2.transform.Rotate(0,0,90);
            Review2.text = "結果発表";
            Review2.color = new Color(0.0f, 0.0f, 0.0f, 1f);
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
            RectRed1.anchoredPosition = new Vector3(0f,0f,0f);
            //RectRed1.sizeDelta = new Vector2(0f,0f);
            ShowRed1.fontSize = FontSizeScore;
            ShowRed1.transform.Rotate(0,0,-90);
            ShowRed1.text = "RedTeam:";
            ShowRed1.color = new Color(0.0f, 0.0f, 0.0f, 1f);
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
            RectRed2.anchoredPosition = new Vector3(0f,0f,0f);
            //RectRed2.sizeDelta = TitleSize;
            ShowRed2.fontSize = FontSizeScore;
            ShowRed2.transform.Rotate(0,0,90);
            ShowRed2.text = "RedTeam:";
            ShowRed2.color = new Color(0.0f, 0.0f, 0.0f, 1f);
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
            RectBlue1.anchoredPosition = new Vector3(0f,0f);
            //RectBlue1.sizeDelta = new Vector2(0f,0f);
            ShowBlue1.fontSize = FontSizeScore;
            ShowBlue1.transform.Rotate(0,0,-90);
            ShowBlue1.text = "BlueTeam";
            ShowBlue1.color = new Color(0.0f, 0.0f, 0.0f, 1f);
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
            RectBlue2.anchoredPosition = new Vector3(0f,0f,0f);
            //RectBlue2.sizeDelta = new Vector2(0f,0f);
            ShowBlue2.fontSize = FontSizeScore;
            ShowBlue2.transform.Rotate(0,0,90);
            ShowBlue2.text = "BlueTeam";
            ShowBlue2.color = new Color(0.0f, 0.0f, 0.0f, 1f);  // 0～1の範囲で設定
            ShowBlue2.enabled = false;
        }
        else
        {
            Debug.LogError("ShowBlue1オブジェクトが指定されていません");
        }


        StartCoroutine(ShowCoin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowCoin()
    {
        Review1.enabled = true;
        Review2.enabled = true;
        var length1 = Review1.text.Length;
        var length2 = Review2.text.Length;
        for(int i=0;i<length1;i++){
            Review1.maxVisibleCharacters = i;
            Review2.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.3f);
        }
        Review1.maxVisibleCharacters = length1;
        Review2.maxVisibleCharacters = length2;
        yield return new WaitForSeconds(0.8f);

        ShowRed1.enabled = true;
        ShowRed2.enabled = true;
        ShowBlue1.enabled = true;
        ShowBlue2.enabled = true;

        for(int j=0; j<150; j++){
            RandNum = Random.Range(10,99);
            ShowRed1.text = "Red:" + RandNum.ToString();
            ShowRed2.text = "Red:" + RandNum.ToString();
            ShowBlue1.text = "Blue:" + RandNum.ToString();
            ShowBlue2.text = "Blue:" + RandNum.ToString();
            yield return new WaitForSeconds(0.01f);
        }

        ShowRed1.text = "Red:" + CoinCountRed.ToString();
        ShowRed2.text = "Red:" + CoinCountRed.ToString();
        ShowBlue1.text = "Blue:" + CoinCountBlue.ToString();
        ShowBlue2.text = "Blue:" + CoinCountBlue.ToString();

        if(CoinCountBlue < CoinCountRed){
            //赤チームが勝った時
            Review1.text = "赤チームの勝利";
            Review2.text = "赤チームの勝利";
        }
        else if(CoinCountBlue > CoinCountRed){
            //青チームが勝った時
            Review1.text = "青チームの勝利";
            Review2.text = "青チームの勝利";
        }
        else{
            //引き分けの時
            Review1.text = "引き分け";
            Review2.text = "引き分け";
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");

    }
}
