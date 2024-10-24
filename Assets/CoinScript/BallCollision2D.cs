using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallDestroyOnCollision2D : MonoBehaviour
{
    // 破壊する対象のレイヤー（Inspectorから設定できるようにする）
    public LayerMask destroyableLayer;

    //点数変数の初期化
    public int CoinCountRed = 0;
    public int CoinCountBlue = 0;
    
    //スコアを表示するTMP
    public TextMeshProUGUI CoinTextBlue;
    public TextMeshProUGUI CoinTextRed;

    private int frameCount = 0;
    private int contableFlag = 1;
    public GameObject CollectEffectRed;　//赤エフェクト
    public GameObject CollectEffectBlue; //青エフェクト

    //赤色コインに変身
    public GameObject CoinRed;
    //青色コインに変身
    public GameObject CoinBlue;
    private Rigidbody2D rb;
    private float jumpForce = 500f;
    //tmpの基準点
    private float tmpX = -553.5f;
    private float tmpY = -279f;

    //どっちサイドかをinCoinSceneTrailから取得
    inCoinSceneTrail cointrail;

    //コイン取得時の効果音
    public AudioSource getCoin;

    /*
        // 物理的な衝突時に呼ばれるメソッド（2D）
        void OnCollisionEnter2D(Collision2D collision)
        {
            // 衝突したオブジェクトのレイヤーが指定のLayerMaskに含まれているか確認
            if (((1 << collision.gameObject.layer) & destroyableLayer) != 0)
            {
                // 指定のレイヤーであれば破壊する
                Destroy(collision.gameObject);
            }
        }
    */

    void Start(){

        //スコアの設定位置
        Vector3 WhereBlueScore = new Vector3(tmpX + 4f, tmpY - 1f, 0f);
        Vector3 WhereRedScore = new Vector3(tmpX + 12f, tmpY - 1f, 0f);
        //スコアの大きさ設定
        Vector2 ScoreSize = new Vector2(8f,2f);
        //スコアのフォントサイズの設定
        float FontSize = 1.5f;

        cointrail = GetComponent<inCoinSceneTrail>();

        getCoin = GetComponent<AudioSource>();

        //CoinTextBlueを指定の位置に移動させ,フォントサイズを変える
        if(CoinTextBlue != null)
        {
            RectTransform rectTransformBlue = CoinTextBlue.GetComponent<RectTransform>();//場所を特定
            rectTransformBlue.anchoredPosition = WhereBlueScore;//青色のスコアの位置に移動
            rectTransformBlue.sizeDelta = ScoreSize;//スコアのサイズの設定
            CoinTextBlue.fontSize = FontSize;//スコアのフォントサイズの設定
            CoinTextBlue.text = "BlueTeam";//最初に表示する文字
        }
        else
        {
            Debug.LogError("BlueScoreオブジェクトが指定されていません");
        }
        //CoinTextRedを指定の位置に移動させ,フォントサイズを変える
        if(CoinTextRed != null)
        {
            RectTransform rectTransformRed = CoinTextRed.GetComponent<RectTransform>();//場所を特定
            rectTransformRed.anchoredPosition = WhereRedScore;//赤色スコアの位置に移動
            rectTransformRed.sizeDelta = ScoreSize;//スコアのサイズの設定
            CoinTextRed.fontSize = FontSize;//スコアのフォントサイズの設定
            CoinTextRed.text = " RedTeam";//最初に表示する文字
        }
        else
        {
            Debug.LogError("RedScoreオブジェクトが指定されていません");
        }
    }

    void Update(){

    }

    // トリガーとしての衝突時に呼ばれるメソッド（2D）
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したオブジェクトのレイヤーが指定のLayerMaskに含まれているか確認
        if (((1 << other.gameObject.layer) & destroyableLayer) != 0 && contableFlag == 1)
        {
            // 指定のレイヤーであれば破壊する
            if(cointrail.turn == true){//エフェクト発生(赤)
                GameObject newEffect = Instantiate(CoinRed, transform.position, transform.rotation);
                getCoin.Play();
                StartCoroutine(Jump(newEffect));
                Instantiate(CollectEffectRed, transform.position, transform.rotation);

                Destroy(other.gameObject);
                UpdateCoinCountTextRed();
                contableFlag = 0;
                StartCoroutine(WaitAndExecuteFunction(0.1f));
            }else{//エフェクト発生(青)
                GameObject newEffect = Instantiate(CoinBlue, transform.position, transform.rotation);
                getCoin.Play();
                StartCoroutine(Jump(newEffect));
                Instantiate(CollectEffectBlue, transform.position, transform.rotation);

                Destroy(other.gameObject);
                UpdateCoinCountTextBlue();
                contableFlag = 0;
                StartCoroutine(WaitAndExecuteFunction(0.1f));
            }


        }
    }

    IEnumerator WaitAndExecuteFunction(float waitTime)
    {
        // 指定した時間だけ待機する

        yield return new WaitForSeconds(waitTime);
        contableFlag = 1;
        // 待機後に実行する関数
        
    }

    //ブルーサイドとレッドサイドの点数の表示    
    void UpdateCoinCountTextBlue()
    {
        CoinCountBlue ++;
        // テキストメッシュプロに破壊数を表示
        CoinTextBlue.text = "Blue:" + CoinCountBlue.ToString();
    }

    void UpdateCoinCountTextRed()
    {
        CoinCountRed ++;
        //テキストメッシュプロに破壊数を表示
        CoinTextRed.text = "Red:" + CoinCountRed.ToString();
    }

    private IEnumerator Jump(GameObject newobject)
    {
        rb = newobject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0,5f), ForceMode2D.Impulse);

        for(int i=0; i<360; i++){
            rb.transform.Rotate(0f,10f,0f);
            yield return new WaitForSeconds(0.01f);
        }
        
        yield return new WaitForSeconds(0.5f);
        Destroy(newobject);
    }
}
