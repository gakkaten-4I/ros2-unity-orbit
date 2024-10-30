using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Winner
{
    Blue,
    Red,
    Draw
}

public class BossBattleGameManager : MonoBehaviour
{
    // プレイヤーごとのボス討伐数を保持する
    public int RedDamageCount = 0;
    public int BlueDamageCount = 0;

    public float timeLimit = 30.0f; // 制限時間（30秒）
    public float remainingTime;    // 残り時間

    public BossBattleResult bossBattleResult;

    private bool isInGame = true;


    // Start is called before the first frame update
    void Start()
    {
        remainingTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0 && isInGame)
        {
            // 経過時間を減らす
            remainingTime -= Time.deltaTime;

            // 残り時間が0以下にならないようにする
            if (remainingTime < 0)
            {
                remainingTime = 0;
                OnTimeUp();
            }
        }
    }

    public void OnTimeUp()
    {
        isInGame = false;
        StartCoroutine(Result());
    }

    public void OnBossDestroyed()
    {
        isInGame = false;
        StartCoroutine(Result());
    }

    IEnumerator Result()
    {
        if (RedDamageCount > BlueDamageCount)
        {
            bossBattleResult.DisplayResultMessage(Winner.Red);
        }
        else if (RedDamageCount < BlueDamageCount)
        {
            bossBattleResult.DisplayResultMessage(Winner.Blue);
        }
        else
        {
            bossBattleResult.DisplayResultMessage(Winner.Draw);
        }

        // 3秒後にシーン遷移
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");
    }

    public void OnBlueDamage()
    {
        BlueDamageCount++;
    }

    public void OnRedDamage()
    {
        RedDamageCount++;
    }

    public string GetBlueDamageString()
    {
        return BlueDamageCount.ToString();
    }

    public string GetRedDamageString()
    {
        return RedDamageCount.ToString();
    }
}
