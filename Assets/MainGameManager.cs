//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public static int PointOfA = 0; // Blue?
    public static int PointOfB = 0; // Red?

    //public static int SceneMoveCount = 0; 本番はこっち
    private int SceneMoveCount = 0;
    public bool IsMain;

    // ゴール管理
    public bool IsRedBombed = false;
    public bool IsBlueBombed = false;
    public bool IsCharged = false; // チャージャーが有効かどうか
    public bool IsFever = false;
    public bool IsRedShielded = false;
    public bool IsBlueShielded = false;

    [SerializeField] BallManager ballManager;


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
        yield return new WaitForSecondsRealtime(90);
        DelayMethod();
    }

    void DelayMethod()
    {
        ++SceneMoveCount;
        int GameSceneNumber = UnityEngine.Random.Range(0, 3);
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
        //Debug.Log(IsMain);
        //Debug.Log(SceneMoveCount);
        // コルーチンの起動
        StartCoroutine(DelayCoroutine());

        //最初に4点以上の差がついたら
        if (Mathf.Abs(PointOfA - PointOfB) >= 4 && (SceneMoveCount == 0))
        {
            DelayMethod();
        }
    }

    public async ValueTask BombBlue(CancellationToken token)
    {
        IsBlueBombed = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        IsBlueBombed = false;
    }

    public async ValueTask BombRed(CancellationToken token)
    {
        IsRedBombed = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        IsRedBombed = false;
    }

    public async ValueTask Charge(CancellationToken token)
    {
        IsCharged = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        IsCharged = false;
    }

    public async ValueTask EnableBlueShield(CancellationToken token)
    {
        //TODO: シールドを表示する処理
        IsBlueShielded = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        //TODO: シールドを非表示にする処理
        IsBlueShielded = false;
    }

    public async ValueTask EnableRedShield(CancellationToken token)
    {
        //TODO: シールドを表示する処理
        IsRedShielded = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        //TODO: シールドを非表示にする処理
        IsRedShielded = false;
    }

    public void Goal(bool isBlueGoal)
    { 
        if(isBlueGoal&&!IsBlueShielded) // PointOfB (RedTeam)の得点を増やす
        {
            if (IsBlueBombed&&IsCharged)
            {
                PointOfB += 4;
            }
            else if (IsBlueBombed)
            {
                PointOfB += 2;
            }
            else if (IsFever)
            {
                PointOfB += 3;
            }
            else
            {
                PointOfB++;
            }
        }
        else if(!IsRedShielded)// PointOfA (BlueTeam)の得点を増やす
        {
            if (IsRedBombed&&IsCharged)
            {
                PointOfA += 4;
            }
            else if (IsRedBombed)
            {
                PointOfA += 2;
            }
            else if (IsFever)
            {
                PointOfA += 3;
            }
            else
            {
                PointOfA++;
            }
            
        }
        //TODO: ここでゲームを一旦止めて、パックを戻すように促す
    }
}

