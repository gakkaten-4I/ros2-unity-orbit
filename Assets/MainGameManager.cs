//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public static int PointOfA = 0; // Blue?
    public static int PointOfB = 0; // Red?
    public static int state = 0; //ミニゲームで誰が勝ったか(0:MainScene, 1: Blue, 2:Red)

    [SerializeField]
    private TextMeshProUGUI ScoreTextOfA, ScoreTextOfB;

    public static int SceneMoveCount = 0; // 本番はこっち
    //private int SceneMoveCount = 0;
    public bool IsMain;

    // ゴール管理
    public bool IsRedBombed = false;
    public bool IsBlueBombed = false;
    public bool IsCharged = false; // チャージャーが有効かどうか
    public bool IsFever = true;
    public bool IsRedShielded = false;
    public bool IsBlueShielded = false;
    public bool IsRedGoalable = true;
    public bool IsBlueGoalable = true;

    public bool IsRedDetected = false;
    public bool IsBlueDetected = false;

    public static short EnergyCount = 0;

    [SerializeField] BallManager ballManager;
    [SerializeField] ItemManager itemManager;

    GameObject RedSheild;
    GameObject BlueSheild;
    GameObject BackGround;
    Material material;

    private DisplayScoreManager DisplayScoreManager;
    private DisplayEnergyCountManager DisplayEnergyCountManager;

    private ItemAreaScript biaScript;
    private ItemAreaScript riaScript;

    public TransToMinigame transToMinigame;//ミニゲーム遷移時のアニメーションのため、TranToMinigame.csを参照する
    public StartCount startCount;//ゲーム開始のカウントダウンのため、StartCount.csを参照する

    public GameObject ball;
    public GameObject ScoreOfA;
    public GameObject ScoreOfB;

    public GameObject BluePost;
    public GameObject RedPost;


    public GoalEffect goaleffect;//ゴール演出を参照する

    // Start is called before the first frame update
    void Start()
    {
        //ScoreTextOfA.text = "00";
        //ScoreTextOfB.text = "00";//スコア表示の初期化
        Application.targetFrameRate = 60;//フレームレートを60に固定
        //UnityEngin.Random.InitState(DateTime.Now.Millisecond);
        GameObject dsmObj = GameObject.Find("DisplayScoreManager");
        DisplayScoreManager = dsmObj.GetComponent<DisplayScoreManager>();
        GameObject decmObj = GameObject.Find("DisplayEnergyCountManager");
        DisplayEnergyCountManager = decmObj.GetComponent<DisplayEnergyCountManager>();

        GameObject biaObject = GameObject.Find("BlueItemArea");
        GameObject riaObject = GameObject.Find("RedItemArea");
        biaScript = biaObject.GetComponent<ItemAreaScript>();
        riaScript = riaObject.GetComponent<ItemAreaScript>();

        goaleffect = GetComponent<GoalEffect>();

        AddMiniGameBonus(state);

        // コルーチンの起動
        StartCoroutine(DelayCoroutine());

        // シーン読み込み時にエナジー缶の数を更新
        DisplayEnergyCountManager.ReflectCount(EnergyCount);
    }

    private void AddMiniGameBonus(int State)
    {
        Debug.Log("Test is called");
        switch (State)
        {
            case 0:
                break;
            case 1:
                DisplayScoreManager.AddDisplayScore(5, 0);
                //PointOfA += 5;
                break;
            case 2:
                DisplayScoreManager.AddDisplayScore(0,5);
                //PointOfB += 5;
                break;
            default:
                break;
        }
        state = 0;
        //DisplayScoreManager.ReflectScore();
    }


    // コルーチン本体
    private IEnumerator DelayCoroutine()
    {
        ball.SetActive(false);
        ScoreOfA.SetActive(false);
        ScoreOfB.SetActive(false);
        startCount.GameStartCount(5);
        yield return new WaitForSeconds(6f);
        ball.SetActive(true);
        ScoreOfA.SetActive(true);
        ScoreOfB.SetActive(true);

        itemManager.StartSpawn();
        // 60秒間待つ
        // Time.timeScale の影響を受けずに実時間で60秒待つ
        yield return new WaitForSecondsRealtime(60);
        DelayMethod();
    }

    void DelayMethod()
    {
        StartCoroutine("delayMethod");
    }

    private IEnumerator delayMethod()
    {
        ++SceneMoveCount;
        

        transToMinigame.StartCountdownOfMinigame(5);
        yield return new WaitForSeconds(5f);
        ball.SetActive(false);
        if (SceneMoveCount >= 3)
        {
            SceneManager.LoadScene("ResultScene");
        }


        int GameSceneNumber = UnityEngine.Random.Range(0, 3);
        IsMain = false;
        switch (GameSceneNumber)
        {
            
            case 0:
                transToMinigame.StartAnimeOfTransMinigame("CoinGame");//ミニゲーム遷移アニメーションの開始
                yield return new WaitForSeconds(6f);//ミニゲーム遷移アニメーションを行っている間待つ必要がある
                SceneManager.LoadScene("CoinGame");
                break;
            case 1:
                transToMinigame.StartAnimeOfTransMinigame("ColoringGame");
                yield return new WaitForSeconds(6f);
                SceneManager.LoadScene("ColoringGame");
                break;
            case 2:
                transToMinigame.StartAnimeOfTransMinigame("BossBattle");
                yield return new WaitForSeconds(6f);
                SceneManager.LoadScene("BossBattle");
                break;
        }


    }
    // Update is called once per frame
    void Update()
    {
        //今のシーンがメインがかどうか
        IsMain = (SceneManager.GetActiveScene().name == "MainScene");

        //最初に4点以上の差がついたら
        /*
        if (Mathf.Abs(PointOfA - PointOfB) >= 4 && (SceneMoveCount == 0))
        {
            DelayMethod();
        }
        */

        // Escキーが押されたらメニューに戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
        OnBlueGoalEnter();
        OnRedGoalEnter();
    }

    public async ValueTask BombBlue(CancellationToken token)
    {
        IsBlueBombed = true;
        itemManager.Emergence(BallManager.turn);
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        itemManager.DestroyEmergency();
        IsBlueBombed = false;
    }

    public async ValueTask BombRed(CancellationToken token)
    {
        IsRedBombed = true;
        itemManager.Emergence(BallManager.turn);
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        itemManager.DestroyEmergency();
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
        BlueSheild = Instantiate(itemManager.BarrierPrefab, itemManager.BarrierSpawnPoints[1].position, Quaternion.identity);
        IsBlueShielded = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        //TODO: シールドを非表示にする処理
        Destroy(BlueSheild);
        IsBlueShielded = false;
    }

    public async ValueTask EnableRedShield(CancellationToken token)
    {
        //TODO: シールドを表示する処理
        //回転を設定
        Quaternion rot = Quaternion.Euler(0, 0, 180);
        RedSheild = Instantiate(itemManager.BarrierPrefab, itemManager.BarrierSpawnPoints[0].position, rot);
        IsRedShielded = true;
        await Task.Delay(TimeSpan.FromSeconds(10), token);
        //TODO: シールドを非表示にする処理
        Destroy(RedSheild);
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
        DisplayEnergyCountManager.ReflectCount(EnergyCount);
    }

    // Blueチーム側のゴールセンサーが反応したときの処理
    public void OnBlueGoalEnter()
    {
        if (!IsBlueDetected)
        {
            return;
        }
        if (!IsBlueShielded&&IsBlueGoalable) // PointOfB (RedTeam)の得点を増やす
        {
            if (IsBlueBombed&&IsCharged)
            {
                PointOfB += 4;
                IsBlueBombed = false;
                itemManager.DestroyEmergency();
                biaScript.RemoveBomb();
            }
            else if (IsBlueBombed)
            {
                PointOfB += 2;
                IsBlueBombed = false;
                itemManager.DestroyEmergency();
                biaScript.RemoveBomb();
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
            goaleffect.MakeGoalEffect(1);
        }
        if (IsBlueShielded)
        {
            // アイテム欄からシールドを削除
            biaScript.RemoveShield();
            Destroy(BlueSheild);
            IsBlueShielded = false;
            StartCoroutine(SetBlueInvincible());
            //TODO: シールドを非表示にする処理
        }
    }

    // Redチーム側のゴールセンサーが反応したときの処理
    public void OnRedGoalEnter()
    {
        if (!IsRedDetected)
        {
            return;
        }
        if (!IsRedShielded && IsRedGoalable)// PointOfA (BlueTeam)の得点を増やす
        {
            if (IsRedBombed && IsCharged)
            {
                PointOfA += 4;
                IsRedBombed = false;
                itemManager.DestroyEmergency();
                riaScript.RemoveBomb();
            }
            else if (IsRedBombed)
            {
                PointOfA += 2;
                IsRedBombed = false;
                itemManager.DestroyEmergency();
                riaScript.RemoveBomb();
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
            goaleffect.MakeGoalEffect(2);
        }
        if (IsRedShielded)
        {
            riaScript.RemoveShield();
            Destroy(RedSheild);
            IsRedShielded = false;
            StartCoroutine(SetRedInvincible());
            //TODO: シールドを非表示にする処理
        }
    }

    public IEnumerator SetBlueInvincible()
    {
        IsBlueGoalable = false;
        //TODO: 無敵状態な事がわかるビジュアルエフェクト
        BluePost.SetActive(false);
        yield return new WaitForSeconds(5); // 5秒間ゴールは無視される
        IsBlueGoalable = true;
        BluePost.SetActive(true);
    }

    public IEnumerator SetRedInvincible()
    {
        IsRedGoalable = false;
        //TODO: 無敵状態な事がわかるビジュアルエフェクト
        RedPost.SetActive(false);
        yield return new WaitForSeconds(5); // 5秒間ゴールは無視される
        IsRedGoalable = true;
        RedPost.SetActive(true);
    }

}

