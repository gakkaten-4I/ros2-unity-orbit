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
    public bool IsRedGoalable = true;
    public bool IsBlueGoalable = true;

    public static short EnergyCount = 0;

    [SerializeField] BallManager ballManager;
    private DisplayScoreManager DisplayScoreManager;

    private ItemAreaScript biaScript;
    private ItemAreaScript riaScript;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;//フレームレートを60に固定
        //UnityEngin.Random.InitState(DateTime.Now.Millisecond);
        GameObject dsmObj = GameObject.Find("DisplayScoreManager");
        DisplayScoreManager = dsmObj.GetComponent<DisplayScoreManager>();

        GameObject biaObject = GameObject.Find("BlueItemArea");
        GameObject riaObject = GameObject.Find("RedItemArea");
        biaScript = biaObject.GetComponent<ItemAreaScript>();
        riaScript = riaObject.GetComponent<ItemAreaScript>();

        // 60秒後にミニゲームに遷移
        StartCoroutine(DelayCoroutine());
    }

    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        // 60秒間待つ
        // Time.timeScale の影響を受けずに実時間で60秒待つ
        yield return new WaitForSecondsRealtime(60);
        NextScene();
    }

    void NextScene()
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
        

        //最初に4点以上の差がついたら
        //if (Mathf.Abs(PointOfA - PointOfB) >= 4 && (SceneMoveCount == 0))
        //{
        //    DelayMethod();
        //}

        //TODO: エナジードリンクの本数をどこかに何らかの形で表示
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

    public IEnumerator EnterFeverMode()
    {
        IsFever = true;
        //TODO: フィーバーモードになったことがわかるビジュアルエフェクト
        //TODO: 場の効果をすべて無効にする処理
        
        biaScript.RemoveAllItems();
        riaScript.RemoveAllItems();

        IsRedBombed = false;
        IsBlueBombed = false;
        IsCharged = false;
        IsRedShielded = false;
        IsBlueShielded = false;

        yield return new WaitForSeconds(15);
        IsFever = false;
    }

    public void OnEnergyTaken()
    {
        EnergyCount++;
        if (EnergyCount >= 3)
        {
            // 15sフィーバー状態になる
            StartCoroutine(EnterFeverMode());
            EnergyCount = 0;
        }
    }

    // Blueチーム側のゴールセンサーが反応したときの処理
    public void OnBlueGoalEnter()
    {
        if (!IsBlueShielded&&IsBlueGoalable) // PointOfB (RedTeam)の得点を増やす
        {
            if (IsBlueBombed&&IsCharged)
            {
                PointOfB += 4;
                IsBlueBombed = false;
            }
            else if (IsBlueBombed)
            {
                PointOfB += 2;
                IsBlueBombed = false;
            }
            else if (IsCharged)
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
            StartCoroutine(SetBlueInvincible());
            DisplayScoreManager.ReflectScore();
        }
        if (IsBlueShielded)
        {
            // アイテム欄からシールドを削除
            biaScript.RemoveShield();
            IsBlueShielded = false;
            StartCoroutine(SetBlueInvincible());
            //TODO: シールドを非表示にする処理
        }
    }

    // Redチーム側のゴールセンサーが反応したときの処理
    public void OnRedGoalEnter()
    {
        if (!IsRedShielded && IsRedGoalable)// PointOfA (BlueTeam)の得点を増やす
        {
            if (IsRedBombed && IsCharged)
            {
                PointOfA += 4;
                IsRedBombed = false;
            }
            else if (IsRedBombed)
            {
                PointOfA += 2;
                IsRedBombed = false;
            }
            else if (IsCharged)
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
            StartCoroutine(SetRedInvincible());
            DisplayScoreManager.ReflectScore();
        }
        if (IsRedShielded)
        {
            riaScript.RemoveShield();
            IsRedShielded = false;
            StartCoroutine(SetRedInvincible());
            //TODO: シールドを非表示にする処理
        }
    }

    public IEnumerator SetBlueInvincible()
    {
        IsBlueGoalable = false;
        //TODO: 無敵状態な事がわかるビジュアルエフェクト
        yield return new WaitForSeconds(5); // 5秒間ゴールは無視される
        IsBlueGoalable = true;
    }

    public IEnumerator SetRedInvincible()
    {
        IsRedGoalable = false;
        //TODO: 無敵状態な事がわかるビジュアルエフェクト
        yield return new WaitForSeconds(5); // 5秒間ゴールは無視される
        IsRedGoalable = true;
    }

}

