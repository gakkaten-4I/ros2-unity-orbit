using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//HPBarがスクリプト名
public class HPBar : MonoBehaviour
{
    public Image hpBarImage;
    //ボスのHP
    public int bossHP = 45;
    
    public int initialHP;
    private int currentHP;
    int player_a_hp;
    int player_b_hp;


//ボスの初期設定・ゲームが開始されたときに一度だけ実行される
    void Start()
    {
        initialHP = bossHP;
        currentHP = bossHP;
         if (hpBarImage == null)
        {
            Debug.LogError("hpBarImage が設定されていません！");
        }
        UpdateHPBar();

        

        Debug.Log("Hello World");

    }



//ボスがダメージを受けたときのメソッド(HPバー)
    void UpdateHPBar()
    {
        hpBarImage.fillAmount=currentHP/initialHP;

    }

    // ボスにダメージを与えるメソッド
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;
        UpdateHPBar();

    }

    
    void Update()
    {
            // ボスの動きや攻撃パターンをここで管理します
    }
}
