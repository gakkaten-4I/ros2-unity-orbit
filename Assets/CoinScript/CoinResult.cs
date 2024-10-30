using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CoinResult : MonoBehaviour
{
    public TextMeshProUGUI Review1;
    public TextMeshProUGUI Review2;
    public TextMeshProUGUI ShowRed1;
    public TextMeshProUGUI ShowRed2;
    public TextMeshProUGUI ShowBlue1;
    public TextMeshProUGUI ShowBlue2;

    private int RandNum;

    private float boundTime = 0.2f;
    private float buindHeight = 20f;

    //得点をBollCollision2Dから取得
    private GameObject score;
    public BallDestroyOnCollision2D bdoc;

    //得点をmainに反映させる
    //private GameObject 
    private DisplayScoreManager displayscoremanager;

    private Vector2 sizeReview = new Vector2(420f,120f);
    private Vector2 sizeScore = new Vector2(210f, 120f);

    private float FontSizeReview = 100f;
    private float FontSizeScore = 60f;

    //結果発表効果音
    public AudioSource resultAudio;

    //結果表示する際の白背景
    public GameObject WhiteCurtain;

    // Start is called before the first frame update
    void Start()
    {

        resultAudio = GetComponent<AudioSource>();

        //テキストの座標の基準
        // CanvasオブジェクトのRectTransformを取得
        
        // Review1の設定
        if (Review1 != null)
        {
            RectTransform RectReview1 = Review1.GetComponent<RectTransform>();
            RectReview1.anchoredPosition = new Vector3(-180f, 0f, 0f);
            RectReview1.sizeDelta = sizeReview;
            Review1.fontSize = FontSizeReview;
            Review1.transform.Rotate(0,0,-90);
            Review1.text = " Result  ";
            Review1.color = new Color(0f, 0f, 0f, 1f);
            Review1.outlineColor = Color.white;
            Review1.outlineWidth = 0.1f;
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
            RectReview2.anchoredPosition = new Vector3(180f, 0f, 0f);
            RectReview2.sizeDelta = sizeReview;
            Review2.fontSize = FontSizeReview;
            Review2.transform.Rotate(0,0,90);
            Review2.text = " Result  ";
            Review2.color = new Color(0f, 0f, 0f, 1f);
            Review2.outlineColor = Color.white;
            Review2.outlineWidth = 0.1f;
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
            RectRed1.anchoredPosition = new Vector3( -300f, 105f, 0f);
            RectRed1.sizeDelta = sizeScore;
            ShowRed1.fontSize = FontSizeScore;
            ShowRed1.transform.Rotate(0,0,-90);
            ShowRed1.text = "RedTeam";
            ShowRed1.color = new Color(1f, 0.1f, 0.1f, 1f);
            ShowRed1.outlineColor = Color.white;
            ShowRed1.outlineWidth = 0.1f;
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
            RectRed2.anchoredPosition = new Vector3( 300f, -105f, 0f);
            RectRed2.sizeDelta = sizeScore;
            ShowRed2.fontSize = FontSizeScore;
            ShowRed2.transform.Rotate(0,0,90);
            ShowRed2.text = "RedTeam";
            ShowRed2.color = new Color(1f, 0.1f, 0.1f, 1f);
            ShowRed2.outlineColor = Color.white;
            ShowRed2.outlineWidth = 0.1f;
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
            RectBlue1.anchoredPosition = new Vector3( -300f, -105f, 0f);
            RectBlue1.sizeDelta = sizeScore;
            ShowBlue1.fontSize = FontSizeScore;
            ShowBlue1.transform.Rotate(0,0,-90);
            ShowBlue1.text = "BlueTeam";
            ShowBlue1.color = new Color(0.1f, 0.1f, 1f, 1f);
            ShowBlue1.outlineColor = Color.white;
            ShowBlue1.outlineWidth = 0.1f;
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
            RectBlue2.anchoredPosition = new Vector3( 300f, 105f, 0f);
            RectBlue2.sizeDelta = sizeScore;
            ShowBlue2.fontSize = FontSizeScore;
            ShowBlue2.transform.Rotate(0,0,90);
            ShowBlue2.text = "BlueTeam";
            ShowBlue2.color = new Color(0.1f, 0.1f, 1f, 1f);  // 0～1の範囲で設定
            ShowBlue2.outlineColor = Color.white;
            ShowBlue2.outlineWidth = 0.1f;
            ShowBlue2.enabled = false;
        }
        else
        {
            Debug.LogError("ShowBlue1オブジェクトが指定されていません");
        }

        score = GameObject.Find("ball");
        bdoc = score.GetComponent<BallDestroyOnCollision2D>();
        if(bdoc == null)
        {
            Debug.LogError("can't read BallDestoryOnCollision2D");
        }

        if(WhiteCurtain != null)
        {
            WhiteCurtain.transform.localScale = new Vector3( 4f, 7f, 1f);
            SpriteRenderer WhiteRenderer = WhiteCurtain.GetComponent<SpriteRenderer>();
            WhiteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            Debug.LogError("WhiteCurtainオブジェクトが指定されていません");
        }

        //30秒後に結果発表
        StartCoroutine(ShowCoin(25f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowCoin(float waiting)
    {
        yield return new WaitForSeconds(waiting);
        GameObject whitecurtain1 = Instantiate(WhiteCurtain, new Vector2(4f, -4.5f), Quaternion.identity);
        GameObject whitecurtain2 = Instantiate(WhiteCurtain, new Vector2(12f, -4.5f), Quaternion.identity);
        Review1.enabled = true;
        Review2.enabled = true;
        var length1 = Review1.text.Length;
        var length2 = Review2.text.Length;
        for(int i=0;i<length1;i++){
            Review1.maxVisibleCharacters = i;
            Review2.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
        }
        Review1.maxVisibleCharacters = length1;
        Review2.maxVisibleCharacters = length2;
        yield return new WaitForSeconds(0.2f);

        ShowRed1.enabled = true;
        ShowRed2.enabled = true;
        ShowBlue1.enabled = true;
        ShowBlue2.enabled = true;

        for(int j=0; j<80; j++){
            RandNum = Random.Range(10,99);
            ShowRed1.text = "Red:" + RandNum.ToString();
            ShowRed2.text = "Red:" + RandNum.ToString();
            ShowBlue1.text = "Blue:" + RandNum.ToString();
            ShowBlue2.text = "Blue:" + RandNum.ToString();
            yield return new WaitForSeconds(0.01f);
        }

        resultAudio.Play();
        ShowRed1.text = "Red:" + bdoc.CoinCountRed.ToString();
        ShowRed2.text = "Red:" + bdoc.CoinCountRed.ToString();
        ShowBlue1.text = "Blue:" + bdoc.CoinCountBlue.ToString();
        ShowBlue2.text = "Blue:" + bdoc.CoinCountBlue.ToString();

        if(bdoc.CoinCountBlue < bdoc.CoinCountRed){
            //赤チームが勝った時
            Review1.text = "You Lose";
            Review2.text = "You Win";
            //displayscoremanager.AddDisplayScore(0,5);
        }
        else if(bdoc.CoinCountBlue > bdoc.CoinCountRed){
            //青チームが勝った時
            Review1.text = "You Win";
            Review2.text = "You Lose";
            //splayscoremanager.AddDisplayScore(5,0);
        }
        else{
            //引き分けの時
            Review1.text = "Draw";
            Review2.text = "Draw";
        }

        yield return new WaitForSeconds(2f);

        StartCoroutine(ChangeScene());

    }

    private IEnumerator ChangeScene()
    {
        Debug.Log("to mainscene");
        //SceneManager.LoadScene("MainScene");
        yield return null;
    }
}
