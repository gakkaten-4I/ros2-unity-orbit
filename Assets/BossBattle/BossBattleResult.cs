using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // Textコンポーネントに必要
using UnityEngine.SceneManagement;  // シーン遷移に必要

public class BossBattleResult : MonoBehaviour
{
    public GameObject _ScoreTextPlayer1;
    public GameObject _ScoreTextPlayer2;
    public GameObject boss;
    PlayerScore PlayerS;
    hitpoint hp;

    // プレイヤーのスコアを保持する変数
    public int _previousPlayer1Score;
    public int _previousPlayer2Score;

    // 動的に生成するTextの親オブジェクト (Canvas)
    public Canvas canvas;

    // 勝者メッセージの生成を開始
    private void Start()
    {
        _ScoreTextPlayer1 = GameObject.Find("PlayerScore1");
        _ScoreTextPlayer2 = GameObject.Find("PlayerScore2");
        boss = GameObject.Find("Boss");

        PlayerS = _ScoreTextPlayer1.GetComponent<PlayerScore>();
        hp = boss.GetComponent<hitpoint>();

        _previousPlayer1Score = PlayerS.previousPlayer1Score;
        _previousPlayer2Score = PlayerS.previousPlayer2Score;
        Debug.Log(_previousPlayer1Score);

        // 30秒後にDisplayResultMessageを呼び出す
        StartCoroutine(DisplayResultMessageAfterDelay(5f));

        
        
    }

    void Update()
    {
        _previousPlayer1Score = PlayerS.previousPlayer1Score;
        _previousPlayer2Score = PlayerS.previousPlayer2Score;
        Debug.Log(_previousPlayer1Score);

        if(hp.hp <= 0)
        {
            DisplayResultMessage();
        }
    }

     // 遅延実行とシーン遷移のためのコルーチン
    private IEnumerator DisplayResultMessageAfterDelay(float delay)
    {
        // 指定した秒数待機
        yield return new WaitForSeconds(delay);

        // 勝者メッセージを表示
        DisplayResultMessage();

        // 3秒後にシーン遷移
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");  // 次のシーンに遷移、"NextScene"はシーン名に変更
    }

    // 勝者メッセージを動的に生成する関数
    private void DisplayResultMessage()
    {
        // スコアを比較してメッセージを決定
        string message;
        if (_previousPlayer1Score > _previousPlayer2Score)
        {
            message = "Player1 Win!";
        }
        else if (_previousPlayer1Score < _previousPlayer2Score)
        {
            message = "Player2 Win!";
        }
        else
        {
            message = "It's a Draw!";
        }

        // Textコンポーネントを生成してメッセージを設定
        GameObject messageObject = new GameObject("ResultText");
        Text resultText = messageObject.AddComponent<Text>();

        // Canvasを親に設定
        messageObject.transform.SetParent(canvas.transform);

        // Textのプロパティを設定
        resultText.text = message;
        Font font = 
        resultText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        resultText.fontSize = 50;
        resultText.alignment = TextAnchor.MiddleCenter;
        resultText.color = Color.white;

        // RectTransformの設定で画面中央に配置
        RectTransform rectTransform = resultText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;  // 画面中央に配置
        rectTransform.sizeDelta = new Vector2(1102, 702);  // テキストの表示領域
    }
}
