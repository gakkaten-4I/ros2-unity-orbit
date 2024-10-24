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
        if((winteam=='A')||(winteam=='a')){//操作対象を設定
            AddPoint=addPointOfA;
            AddPointObj=AddPointOfA;
            PointTextObj=AddMessageOfA;
            Hanabi1=HanabiA1;
            Hanabi2=HanabiA2;
        }else{
            AddPoint=addPointOfB;
            AddPointObj=AddPointOfB;
            PointTextObj=AddMessageOfB;
            Hanabi1=HanabiB1;
            Hanabi2=HanabiB2;
        }
        PointTextObj.SetActive(true);

        AddPoint.text=""+0+"";

        yield return new WaitForSeconds(0.5f);

        //Step2.アニメーション1(カウントアップ) 2s
        int DisplayPoint=0;
        for(float j=0;j<=1.0f;j+=0.1f){
            DisplayPoint=(int)(Mathf.Floor(addpoint*Mathf.Pow(j,0.3f)));
            AddPoint.text=""+DisplayPoint+"";
            yield return new WaitForSeconds(0.2f);
        }

        AddPoint.text=""+addpoint+"";

        //Step3.アニメーション2(花火＋加算ポイントテキストオブジェクトの回転)　2s

        for(int i=0;i<60;i++){
            if(i==0) {
                Hanabi1.SetActive(true);
                Hanabi1.GetComponent<Image>().sprite=SmlHanabi;
            }
            else if(i==15){
                Hanabi1.GetComponent<Image>().sprite=MidHanabi;
                Hanabi2.SetActive(true);
                Hanabi2.GetComponent<Image>().sprite=SmlHanabi;
            }
            else if(i==30){
                Hanabi1.GetComponent<Image>().sprite=BigHanabi;
                Hanabi2.GetComponent<Image>().sprite=MidHanabi;
            }
            else if(i==45){
                Hanabi1.GetComponent<Image>().sprite=SmlHanabi;
                Hanabi1.SetActive(false);
                Hanabi2.GetComponent<Image>().sprite=BigHanabi;
            }
            else if(i==59){
                Hanabi2.GetComponent<Image>().sprite=SmlHanabi;
                Hanabi2.SetActive(false);
            }

            if(i>30){
                AddPointObj.transform.Rotate(new Vector3(720/30, 0, 0));
            }
            yield return new WaitForSeconds(2f/60);
        }

        //Step3.アニメーション2(加算ポイントテキストオブジェクトの移動)

        //Step4.シーン移動

        PointTextObj.SetActive(false);

        //SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    void DebugAddText(){
        if((Input.GetKeyDown(KeyCode.D))&&(Input.GetKeyDown(KeyCode.B))){
            PlusMinigamePoint('A',5);
        }
    }
}
