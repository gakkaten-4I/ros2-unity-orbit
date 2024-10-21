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
        ScoreTextOfA.text = "00";
        ScoreTextOfA.text = "00";//スコア表示の初期化
    }

    // Update is called once per frame
    void Update()
    {
        DebugText();//後で消すように！！
    }

    public void ReflectScore()//テキストオブジェクトにスコアの値を反映させる(A,Bどちらも)
    {
        tmpScoreOfA=""+MainGameManager.PointOfA+"";
        tmpScoreOfB=""+MainGameManager.PointOfB+"";//スコアの値をとってくる

        if(MainGameManager.PointOfA <=9) tmpScoreOfA="0"+tmpScoreOfA;
        if(MainGameManager.PointOfB <=9) tmpScoreOfB="0"+tmpScoreOfB;//数字が一桁なら先頭に0を追加

        ScoreTextOfA.text = ""+tmpScoreOfA+"";
        ScoreTextOfB.text = ""+tmpScoreOfB+"";//スコアを反映させる
    }

    public void DisplayNumber(int scoreA,int scoreB)//テキストオブジェクトに任意の値を表示する
    {
        if(scoreA>=0) tmpScoreOfA=""+scoreA+""; else tmpScoreOfA="";
        if(scoreB>=0) tmpScoreOfB=""+scoreB+""; else tmpScoreOfB="";//スコアの値をとってくる

        if(scoreA<=9) tmpScoreOfA="0"+tmpScoreOfA;
        if(scoreB<=9) tmpScoreOfB="0"+tmpScoreOfB;

        if(tmpScoreOfA != "")ScoreTextOfA.text = ""+tmpScoreOfA+"";
        if(tmpScoreOfB != "")ScoreTextOfB.text = ""+tmpScoreOfB+"";//スコアを反映させる
    }

    void DebugText()//テキストの表示が変わるか確認
    {
        if((Input.GetKeyDown(KeyCode.D))&&(Input.GetKeyDown(KeyCode.B))){
            ScoreTextOfA.text = "0"+5;
            ScoreTextOfB.text = "0"+4;
        }
    }
}
