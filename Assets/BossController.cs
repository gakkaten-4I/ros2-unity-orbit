using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//HPBarがスクリプト名
public class BossController : MonoBehaviour
{
    public HPBar hpBarController;
    //ボスのHP
    public int bossHP = 45;
    
    public int initialHP;
    public int currentHP;
    int player_a_hp;
    int player_b_hp;


//ボスの初期設定・ゲームが開始されたときに一度だけ実行される
    void Start()
    {
         // HPバーに最大HPを設定
        if (hpBarController != null)
        {
            hpBarController.initialHP = bossHP;
        }
        else
        {
            Debug.LogError("HPBarController がアタッチされていません");
        }

    }


    // ボスにダメージを与えるメソッド
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpBarController.TakeDamage(damage);   

    }

    
    void Update()
    {
            // ボスの動きや攻撃パターンをここで管理します
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(3);
        }
    }
}