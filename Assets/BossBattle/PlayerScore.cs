using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public GameObject _ScoreTextPlayer1;
    public GameObject _ScoreTextPlayer2;
    GameObject boss;
    BossDefeatCounter Bossdef;

    public int previousPlayer1Score = -1;
    public int previousPlayer2Score = -1;

    // Start is called before the first frame update
    void Start()
    {
        // 各オブジェクトを取得
        boss = GameObject.Find("Boss");
        _ScoreTextPlayer1 = GameObject.Find("PlayerScore1");
        _ScoreTextPlayer2 = GameObject.Find("PlayerScore2");

        Bossdef = boss.GetComponent<BossDefeatCounter>();

        // 初期のスコアを表示
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // スコアが変わった場合のみ更新
        if (Bossdef.player1BossCount != previousPlayer1Score || 
            Bossdef.player2BossCount != previousPlayer2Score)
        {
            UpdateScoreText();
        }
    }    

    // スコアテキストを更新する関数
    void UpdateScoreText()
    {
        Text ScoreText1 = _ScoreTextPlayer1.GetComponent<Text>();
        Text ScoreText2 = _ScoreTextPlayer2.GetComponent<Text>();

        Debug.Log(Bossdef.player1BossCount);

        ScoreText1.text = "Score: " + Bossdef.player1BossCount;
        ScoreText2.text = "Score: " + Bossdef.player2BossCount;

        // 現在のスコアを保存
        previousPlayer1Score = Bossdef.player1BossCount;
        previousPlayer2Score = Bossdef.player2BossCount;
    }
}
