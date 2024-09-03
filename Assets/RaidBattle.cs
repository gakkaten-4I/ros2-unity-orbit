using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCentipedeBoss : MonoBehaviour
{
    //ボスのHP
    public int boss_HP = 45;
    //ボスを構成するパーツを保持する配列
    public GameObject[] bossParts;
    //爆発エフェクト設定
    public GameObject explosionEffect;

    private int initalHP;
    private int currentHP;
    private int partsCount;
    private int stage = 1;
    private SpriteRenderer hpBar;
    int player_a_hp;
    int player_b_hp;

} 

//ボスの初期設定
void Start()
{
    initalHP = bossHP;
    currentHP = bossHP;
    partsCount = bossParts.Length;
    hpBar = GameObject.Find("HPBar").GetComponent<SpriteRenderer>();
    UpdateHPBar();
}

//ボスがダメージを受けたときのメソッド(HPバー)
void UpdateHPBar()
{
    float hpPercentage = (float)currentHP / initialHP;
    UpdateHPBar.size = new Vector2(hpPercentage, UpdateHPBar.size.y);

    if(hpPercentage > 0.66f)
    {
        hbBar.color = Color.green;
    }
    else if(hpParcentage > 0.33f)
    {
        UpdateHPBar.color = Color.yellow;
    }
    else
    {
        UpdateHPBar.color = Color.red;
    }
}

 // ボスにダメージを与えるメソッド
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UpdateHPBar();

        if (currentHP <= 0)
        {
            // ボスを倒す
            Destroy(gameObject);
        }
        else if (currentHP <= initialHP * 0.33f && stage == 2)
        {
            // 最終形態へ移行
            ChangeForm(2);
        }
        else if (currentHP <= initialHP * 0.66f && stage == 1)
        {
            // 第2形態へ移行
            ChangeForm(1);
        }
    }

    // ボスの形態を変更するメソッド
    void ChangeForm(int newStage)
    {
        stage = newStage;
        partsCount -= 2;

        // ボスのパーツを2つ破壊
        for (int i = 0; i < 2; i++)
        {
            Destroy(bossParts[partsCount + i]);
            Instantiate(explosionEffect, bossParts[partsCount + i].transform.position, Quaternion.identity);
        }

        // ボスの動きや攻撃パターンを変更
        // 移動速度を上げるなど
        GetComponent<Movement>().IncreaseSpeed();
    }

    // ボスの動きを制御するメソッド
    void Update()
    {
        // ボスの動きや攻撃パターンをここで管理します
    }
