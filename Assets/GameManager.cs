using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;//フレームレートを60に固定
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class BossController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // スプライトレンダラーを参照

    void Start()
    {
        // スプライトレンダラーを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 必要に応じてスプライトを変更
        Sprite newSprite = Resources.Load<Sprite>("NewBossSprite");
        spriteRenderer.sprite = newSprite;
    }

    void Update()
    {
        // ここでボスの移動やアニメーションを制御
    }
}
