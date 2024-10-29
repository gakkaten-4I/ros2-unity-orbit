
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//HPBarがスクリプト名
public class HPBar : MonoBehaviour
{
    public GameObject black;
    public GameObject hpBarImage;
    hitpoint _hp;
    GameObject boss;

    //ボスのHP
    public int bossHP;
    public GameObject score_object = null;
    
    public int initialHP;
    private int currentHP;
    int player_a_hp;
    int player_b_hp;  

    public float timeLimit = 30.0f; // 制限時間（30秒）
    //privateからpublicにした
    public float remainingTime;    // 残り時間

    public GameObject timerText;          // UIのTextコンポーネントに表示されるテキスト


//ボスの初期設定・ゲームが開始されたときに一度だけ実行される
    void Start()
    {
        boss = GameObject.Find("Boss");
        _hp = boss.GetComponent<hitpoint>();
        bossHP = _hp.hp;
        // 残り時間を初期化
        remainingTime = timeLimit;

        // 最初に残り時間を表示
        
        initialHP = bossHP;
        
    if (score_object == null)
    {
        score_object = GameObject.Find("BossHP"); // ここで "ScoreText" は Text コンポーネントを持つゲームオブジェクトの名前
    }

    if (score_object != null)
    {
        Text score_text = score_object.GetComponent<Text>();
        score_text.text = "Boss HP: " + currentHP;
        
    }
    else
    {
        Debug.LogError("score_object が設定されていません！");
    }
        //Text score_text = score_object.GetComponent<Text>();
        //score_text.text="Boss HP" + bossHP;
    if (hpBarImage == null)
    {
        hpBarImage = GameObject.Find("hpBarImage");
        black = GameObject.Find("black");
        //hpBarImage = GetComponent<Image>();

        if (hpBarImage == null)
        {
            Debug.LogError("hpBarImage が設定されていません！");
        }
    }
    
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
        Image greenImage = hpBarImage.GetComponent<Image>();
        if (greenImage == null)
        {
            Debug.LogError("hpBarImage に Image コンポーネントがありません！");
            return;
        }
        hpBarImage.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);

        hpBarImage.transform.localScale = new Vector3((float)currentHP/initialHP, 1.0f, 1.0f);
        Debug.Log("green");

        
        if(currentHP <=30){
            greenImage.color = Color.yellow;
            if(currentHP <=15){
                greenImage.color = Color.red;
            }
        }
        // ボスHPのテキストを更新
        Text score_text = score_object.GetComponent<Text>();
        if (score_text != null)
        {
            score_text.text = "Boss HP: " + currentHP;
        }
        else
        {
            Debug.LogError("score_object に Text コンポーネントがありません！");
        }
    }

    // ボスにダメージを与えるメソッド
    /*public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;
        Debug.Log("damege");
        UpdateHPBar();

    }*/


    
    void Update()
    {
        //TakeDamage(1);    // ボスの動きや攻撃パターンをここで管理します
        currentHP = _hp.hp;
        UpdateHPBar();
        if (remainingTime > 0)
        {
            // 経過時間を減らす
            remainingTime -= Time.deltaTime;

            // 残り時間が0以下にならないようにする
            if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            // 残り時間を更新
            UpdateTimerText();
        }
    }
        // テキストを更新するメソッド
    void UpdateTimerText()
    {
        timerText = GameObject.Find("gametimer");
        Text timerText1 = timerText.GetComponent<Text>();
    if (timerText1 != null)
    {
         // 残り時間を秒数に変換して小数点以下を2桁にする
        timerText1.text = "残り時間: " + remainingTime.ToString("F2") + "s";
    }
    else
    {
        Debug.LogError("timer Text コンポーネントがありません！");
    }
    }
    
}

