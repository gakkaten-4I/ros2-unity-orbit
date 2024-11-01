using System;
using UnityEngine;

public class BossDefeatCounter : MonoBehaviour
{
    // プレイヤーごとのボス討伐数を保持する
    public int player1BossCount = 0;
    public int player2BossCount = 0;

    // 現在のターンのプレイヤーを取得 (BallManagerのturnを参照)
    private bool IsPlayer1Turn => BallManager.turn;

    // ボスを討伐したときに呼び出される関数
    public void BossDefeated()
    {
        if (IsPlayer1Turn)
        {
            player1BossCount++;
            Debug.Log($"プレイヤー1のボス討伐数: {player1BossCount}");
        }
        else
        {
            player2BossCount++;
            Debug.Log($"プレイヤー2のボス討伐数: {player2BossCount}");
        }
    }

    // プレイヤーのボス討伐数を取得する関数
    public int GetPlayer1BossCount() => player1BossCount;
    public int GetPlayer2BossCount() => player2BossCount;

    // デバッグ用にボス討伐数を表示する
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 仮にスペースキーで討伐シミュレーション
        {
            BossDefeated();
        }
    }
}
