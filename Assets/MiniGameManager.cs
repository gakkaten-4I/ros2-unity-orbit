//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
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

        // 30秒間待つ
        // Time.timeScale の影響を受けずに実時間で30秒待つ
        yield return new WaitForSecondsRealtime(30);
        DelayMethod();
    }

    void DelayMethod()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        //今のシーンがメインがかどうか
        IsMain = (SceneManager.GetActiveScene().name == "MainScene");
        Debug.Log(IsMain);
        //Debug.Log(++MainGameManager.PointOfA);
        // コルーチンの起動
        StartCoroutine(DelayCoroutine());

    }
}

