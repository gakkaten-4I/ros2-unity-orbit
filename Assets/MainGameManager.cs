//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public static int PointOfA = 0;
    public static int PointOfB = 0;

    //public static int SceneMoveCount = 0; 本番はこっち
    private int SceneMoveCount = 0;
    public bool IsMain;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;//フレームレートを60に固定
        //UnityEngin.Random.InitState(DateTime.Now.Millisecond);
    }

    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        // 90秒間待つ
        // Time.timeScale の影響を受けずに実時間で90秒待つ
        yield return new WaitForSecondsRealtime(5);
        DelayMethod();
    }

    void DelayMethod()
    {
        ++SceneMoveCount;
        int GameSceneNumber = Random.Range(0, 3);
        IsMain = false;
        switch (GameSceneNumber)
        {
            case 0:
                SceneManager.LoadScene("CoinGame");
                break;
            case 1:
                SceneManager.LoadScene("ColoringGame");
                break;
            case 2:
                SceneManager.LoadScene("BossBattle");
                break;
        }


    }
    // Update is called once per frame
    void Update()
    {
        //今のシーンがメインがかどうか
        IsMain = (SceneManager.GetActiveScene().name == "MainScene");
        Debug.Log(IsMain);
        Debug.Log(SceneMoveCount);
        // コルーチンの起動
        StartCoroutine(DelayCoroutine());

        //最初に4点以上の差がついたら
        if (Mathf.Abs(PointOfA - PointOfB) >= 4 && (SceneMoveCount == 0))
        {
            DelayMethod();
        }
    }
}

