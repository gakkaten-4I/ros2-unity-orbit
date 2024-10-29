using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreTextOfA,ScoreTextOfB;//テキストオブジェクト(左と右)

    string tmpScoreOfA,tmpScoreOfB;//関数内でスコアが入る

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DebugText();//後で消すように！！
    }

    public void ReflectScore()//テキストオブジェクトにスコアの値を反映させる(A,Bどちらも)
    {
        Debug.Log("ReflectScore is called");
        Debug.Log("AddDisplayScore: " + MainGameManager.PointOfA);
        tmpScoreOfA =""+MainGameManager.PointOfA+"";
        tmpScoreOfB=""+MainGameManager.PointOfB+"";//スコアの値をとってくる

        if(MainGameManager.PointOfA <=9) tmpScoreOfA="0"+tmpScoreOfA;
        if(MainGameManager.PointOfB <=9) tmpScoreOfB="0"+tmpScoreOfB;//数字が一桁なら先頭に0を追加

        ScoreTextOfA.text = ""+tmpScoreOfA+"";
        ScoreTextOfB.text = ""+tmpScoreOfB+"";//スコアを反映させる

        Debug.Log("ReflectScore:");
    }

    public void DisplayScore(int scoreA,int scoreB)//テキストオブジェクトに任意の値を表示する
    {
        if(scoreA>=0) tmpScoreOfA=""+scoreA+""; else tmpScoreOfA="";
        if(scoreB>=0) tmpScoreOfB=""+scoreB+""; else tmpScoreOfB="";//スコアの値をとってくる

        if(scoreA<=9) tmpScoreOfA="0"+tmpScoreOfA;
        if(scoreB<=9) tmpScoreOfB="0"+tmpScoreOfB;

        if(tmpScoreOfA != "")ScoreTextOfA.text = ""+tmpScoreOfA+"";
        if(tmpScoreOfB != "")ScoreTextOfB.text = ""+tmpScoreOfB+"";//スコアを反映させる

        MainGameManager.PointOfA=scoreA;
        MainGameManager.PointOfB=scoreB;//ポイントが入っている変数の中身も変更
    }

    public void AddDisplayScore(int AddscoreA,int AddscoreB)//テキストオブジェクトに任意の値を表示+加算
    {
        MainGameManager.PointOfA+=AddscoreA;
        MainGameManager.PointOfB+=AddscoreB;//ポイントが入っている変数の中身を変更
        Debug.Log("AddDisplayScore: " + MainGameManager.PointOfA);

        /*
        string tmpScoreOfA = MainGameManager.PointOfA.ToString("00");

        //tmpScoreOfA=""+MainGameManager.PointOfA+"";
        tmpScoreOfB =""+MainGameManager.PointOfB+"";//スコアの値をとってくる

        //if(MainGameManager.PointOfA <=9) tmpScoreOfA="0"+tmpScoreOfA;
        if(MainGameManager.PointOfB <=9) tmpScoreOfB="0"+tmpScoreOfB;//数字が一桁なら先頭に0を追加

        //ScoreTextOfA.text = ""+tmpScoreOfA+"";
        ScoreTextOfA.text = "" + tmpScoreOfA + "";
        ScoreTextOfB.text = ""+tmpScoreOfB+"";//スコアを反映させる

        Debug.Log("AddDisplayScore: " + tmpScoreOfA);
        */
        ReflectScore();
    }

    void DebugText()//テキストの表示が変わるか確認
    {
        if((Input.GetKeyDown(KeyCode.D))&&(Input.GetKeyDown(KeyCode.B))){
            ScoreTextOfA.text = "db";
            ScoreTextOfB.text = "db";
        }
    }
}
