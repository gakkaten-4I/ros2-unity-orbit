using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class AddMinigamePoint : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI addPointOfA,addPointOfB;//何ポイント加算されるかを表示するテキスト

    public GameObject AddMessageOfA,AddMessageOfB;//テキストオブジェクト
    public GameObject AddPointOfA,AddPointOfB;

    public GameObject HanabiA1,HanabiA2,HanabiB1,HanabiB2;//花火演出
    public Sprite BigHanabi,MidHanabi,SmlHanabi;//大きい花火、中ぐらいの花火、小さい花火の絵

    public GameObject BigHanabiObj;
    public GameObject SmallHanabiObj;

    public DisplayScoreManager displayScoreManager;//関数の呼び出しをため、DisplayScoreManager.csを取得

    // Start is called before the first frame update
    void Start()
    {
        AddMessageOfA.SetActive(false);
        AddMessageOfB.SetActive(false);//全て非表示
    }

    // Update is called once per frame
    void Update()
    {
        DebugAddText();//後で消すように
    }

    //下記コルーチンを再生する関数
    public void PlusMinigamePoint(char winteam,int addpoint)//winteam:勝ったチーム(A or B),addpoint:何点加算するか
    {
        StartCoroutine(ReflectMinigamePoint(winteam,addpoint));
    }

    IEnumerator ReflectMinigamePoint(char winteam,int addpoint)
    {
        TextMeshProUGUI AddPoint;
        GameObject AddPointObj;
        GameObject PointTextObj;
        GameObject Hanabi1,Hanabi2;//変数定義(加算ポイントとそれと[pt]表示を含めた全体)

        //Step0.シーン移動
        //SceneManager.LoadScene("QuietScene", LoadSceneMode.Single);//シーン転換
        displayScoreManager.ReflectScore();//現在の得点を反映(加算前)

        yield return new WaitForSeconds(0.5f);

        //Step1.[加算ポイント]pt を表示
        if(winteam=='A'){//操作対象を設定
            AddPoint=addPointOfA;
            AddPointObj=AddPointOfA;
            PointTextObj=AddMessageOfA;
            Hanabi1=HanabiA1;
            Hanabi2=HanabiA2;

            RectTransform rectTransform = AddPoint.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-8f,-2.5f);
            AddPoint.fontSize=150f;

        }else{
            AddPoint=addPointOfB;
            AddPointObj=AddPointOfB;
            PointTextObj=AddMessageOfB;
            Hanabi1=HanabiB1;
            Hanabi2=HanabiB2;

            RectTransform rectTransform = AddPoint.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(2f,-2.5f);
            AddPoint.fontSize=150f;
        }
        PointTextObj.SetActive(true);

        AddPoint.text=""+0+"";

        yield return new WaitForSeconds(0.5f);

        //Step2.アニメーション1(カウントアップ) 1s
        int DisplayPoint=0;
        for(float j=0;j<=1.0f;j+=0.1f){
            DisplayPoint=(int)(Mathf.Floor(addpoint*Mathf.Pow(j,0.7f)));
            AddPoint.text=""+DisplayPoint+"";
            yield return new WaitForSeconds(1f/10f);
        }

        AddPoint.text=""+addpoint+"";

        //Step3.アニメーション2(花火＋加算ポイントテキストオブジェクトの回転)　1.5s

        /*for(int i=0;i<60;i++){
            if(i==0) {
                Hanabi1.SetActive(true);
                Hanabi1.GetComponent<SpriteRenderer>().sprite=SmlHanabi;
            }
            else if(i==5){
                Hanabi1.GetComponent<SpriteRenderer>().sprite=MidHanabi;
                Hanabi2.SetActive(true);
                Hanabi2.GetComponent<SpriteRenderer>().sprite=SmlHanabi;
            }
            else if(i==10){
                Hanabi1.GetComponent<SpriteRenderer>().sprite=BigHanabi;
                Hanabi2.GetComponent<SpriteRenderer>().sprite=MidHanabi;
            }
            else if(i==15){
                Hanabi1.GetComponent<SpriteRenderer>().sprite=SmlHanabi;
                Hanabi1.SetActive(false);
                Hanabi2.GetComponent<SpriteRenderer>().sprite=BigHanabi;
            }
            else if(i==20){
                Hanabi2.GetComponent<SpriteRenderer>().sprite=SmlHanabi;
                Hanabi2.SetActive(false);
            }

            if(i>30){
                AddPointObj.transform.Rotate(new Vector3(720/30, 0, 0));
            }
            yield return new WaitForSeconds(1.5f/60f);
        }*/


        for (int i = 0; i < 60; i++)
        {
            // 最初に花火オブジェクトを生成
            if (i == 0)
            {
                // 勝ったチームに応じた花火位置でオブジェクト生成
                if (winteam == 'A')
                {
                    Instantiate(SmallHanabiObj, new Vector3(7.5f, -2f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(SmallHanabiObj, new Vector3(14.5f, -2f, 0), Quaternion.identity);
                }
            }
            else if (i == 10)
            {
                if (winteam == 'A')
                {
                    Instantiate(BigHanabiObj, new Vector3(3f, -8f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(BigHanabiObj, new Vector3(10.5f, -8f, 0), Quaternion.identity);
                }
            }

            if (i == 20)
            {
                // 勝ったチームに応じた花火位置でオブジェクト生成
                if (winteam == 'A')
                {
                    Instantiate(SmallHanabiObj, new Vector3(7.5f, -8f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(SmallHanabiObj, new Vector3(13f, -8f, 0), Quaternion.identity);
                }
            }
            else if (i == 29)
            {
                if (winteam == 'A')
                {
                    Instantiate(BigHanabiObj, new Vector3(3f, -2f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(BigHanabiObj, new Vector3(10.5f, -2f, 0), Quaternion.identity);
                }
            }


            if (i > 30)
            {
                AddPointObj.transform.Rotate(new Vector3(720 / 30, 0, 0));
            }
            yield return new WaitForSeconds(1.5f / 60f);
        }

        //Step3.アニメーション2(加算ポイントテキストオブジェクトの移動) 0.5s
        if(winteam=='A'){//現在のポジション(-6,2.5)からスコアのポジション(-3.3,1.8)まで移動させる
            for(int i=0;i<60;i++){
                RectTransform rectTransform = AddPoint.GetComponent<RectTransform>();
                // 毎フレーム少しずつ移動させる
                rectTransform.anchoredPosition += new Vector2((1.5f) / 60f, (1.5f) / 60f);
                AddPoint.fontSize*=0.97f;
                yield return new WaitForSeconds(0.5f/60);
            }
            //textMeshPro.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 200);
        }
        else{//現在のポジション(4,-2.5)からスコアのポジション(1.3,1.8)まで移動させる
            for(int i=0;i<60;i++){
                RectTransform rectTransform = AddPoint.GetComponent<RectTransform>();
                // 毎フレーム少しずつ移動させる
                rectTransform.anchoredPosition += new Vector2((-2.4f) / 60f, (1.5f) / 60f);

                AddPoint.fontSize*=0.97f;
                yield return new WaitForSeconds(0.5f/60f);
            }
        }

        //Step4.シーン移動

        PointTextObj.SetActive(false);

        //SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    void DebugAddText(){
        if(Input.GetKeyDown(KeyCode.Return)){
            PlusMinigamePoint('A',5);
            PlusMinigamePoint('B',5);
        }
    }
}
