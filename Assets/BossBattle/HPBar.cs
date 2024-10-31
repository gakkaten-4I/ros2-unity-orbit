
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

//HPBarがスクリプト名
public class HPBar : MonoBehaviour
{
    public GameObject black;
    public GameObject hpBarImage;
    public Image greenImage;
    hitpoint _hp;

    //ボスのHP
    //public GameObject score_object = null;
    
    private int initialHP;
    private int currentHP;
    //int player_a_hp;
    //int player_b_hp;  

    //public float timeLimit = 30.0f; // 制限時間（30秒）
    ////privateからpublicにした
    //public float remainingTime;    // 残り時間

    //public GameObject timerText;          // UIのTextコンポーネントに表示されるテキスト

    public TextMeshProUGUI BlueScoreText;
    public TextMeshProUGUI RedScoreText;
    public TextMeshProUGUI BossHPText;
    public TextMeshProUGUI GameTimerText;

    private BossBattleGameManager gameManager;


    //ボスの初期設定・ゲームが開始されたときに一度だけ実行される
    void Start()
    {
        GameObject _gcm = GameObject.Find("GameSceneManager");
        gameManager = _gcm.GetComponent<BossBattleGameManager>();

        GameObject boss = GameObject.Find("Boss");
        _hp = boss.GetComponent<hitpoint>();
        currentHP = _hp.hp;
        // 残り時間を初期化
        //remainingTime = timeLimit;

        // 最初に残り時間を表示
        
        initialHP = currentHP;

        //if (hpBarImage == null)
        //{
        //    hpBarImage = GameObject.Find("hpBarImage");
        //    black = GameObject.Find("black");
        //    //hpBarImage = GetComponent<Image>();

        //    if (hpBarImage == null)
        //    {
        //        Debug.LogError("hpBarImage が設定されていません！");
        //    }
        //}

        UpdateTimerText();

        
    
    Debug.Log("Hello World");

    }



//ボスがダメージを受けたときのメソッド(HPバー)
    void UpdateHPBar()
    {
        
        // hpBarImage が null でないか確認
        if (hpBarImage == null)
        {
            Debug.LogError("hpBarImage が設定されていません！");
            return;  // 以降の処理を行わない
        }

        // hpBarImage に Image コンポーネントがあるか確認
        //Image greenImage = hpBarImage.GetComponent<Image>();
        if (greenImage == null)
        {
            Debug.LogError("hpBarImage に Image コンポーネントがありません！");
            return;
        }
        //hpBarImage.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);

        hpBarImage.transform.localScale = new Vector3((float)currentHP/initialHP, 1.0f, 1.0f);
        Debug.Log("green");

        
        if(currentHP <=30){
            greenImage.color = Color.yellow;
            if(currentHP <=15){
                greenImage.color = Color.red;
            }
        }

        BossHPText.text = "Boss HP: " + currentHP.ToString();
    }

    void Update()
    {
        currentHP = _hp.hp;
        UpdateHPBar();
        UpdateTimerText();
        UpdateScoreText();
    }

    // テキストを更新するメソッド
    void UpdateTimerText()
    {
        GameTimerText.text = "Time: " + gameManager.remainingTime.ToString("F2") + "s";
    }
    
    void UpdateScoreText()
    {
        BlueScoreText.text = "Score: " + gameManager.GetBlueDamageString();
        RedScoreText.text = "Score: " + gameManager.GetRedDamageString();
    }
}

