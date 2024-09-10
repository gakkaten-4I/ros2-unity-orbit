using UnityEngine;

public class SpriteCounter : MonoBehaviour
{
    private int redSprites = 0; 
    private int blueSprites = 0;
    public float redRatio = 0f;
    public float blueRatio = 0f;
    private int SpriteNum = 0;

    // publicで指定するレイヤーマスク
    public LayerMask targetLayer;

    void Update()
    {
        // シーン内の指定レイヤーのSpriteRendererをカウント
        int spriteCount = CountSpritesInScene();
        Debug.Log($"Total number of sprites in the scene: {SpriteNum}");

        // 赤いスプライトの割合を計算し、小数点1桁に丸める
        if (SpriteNum > 0)
        {
            redRatio = (float)redSprites / (float)SpriteNum * 100f;
            redRatio = Mathf.Round(redRatio * 10f) / 10f;  // 小数点1桁に丸める
            Debug.Log($"Red Sprite Ratio: {redRatio}%");

            blueRatio = (float)blueSprites / (float)SpriteNum * 100f;
            blueRatio = Mathf.Round(blueRatio * 10f) / 10f;  // 小数点1桁に丸める
            Debug.Log($"Red Sprite Ratio: {blueRatio}%");
        }
        else
        {
            Debug.Log("No sprites found in the target layer.");
        }

    }

    int CountSpritesInScene()
    {
        redSprites = 0; 
        blueSprites = 0; // 赤いスプライトの数をリセット

        // SpriteRendererコンポーネントを持つすべてのゲームオブジェクトを検索
        SpriteRenderer[] spriteRenderers = GameObject.FindObjectsOfType<SpriteRenderer>();

        SpriteNum = 0;  // 該当レイヤーのスプライト数をリセット

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            // レイヤーマスクに一致するかどうかを確認
            if ((targetLayer.value & (1 << sr.gameObject.layer)) != 0)
            {
                SpriteNum++;  // 該当レイヤーのスプライト数をカウント

                if (IsRed(sr.color))
                {
                    redSprites++;  // 赤いスプライトをカウント
                }
                else if (IsBlue(sr.color))
                {
                    blueSprites++;  // 青いスプライトをカウント
                }

            }
        }

        return redSprites;
    }

    bool IsRed(Color color)
    {
        // 色が完全な赤 (255, 0, 0) かをチェック
        return Mathf.Approximately(color.r, 1f) && Mathf.Approximately(color.g, 0f) && Mathf.Approximately(color.b, 0f);
    }

    bool IsBlue(Color color)
    {
        // 色が完全な赤 (255, 0, 0) かをチェック
        return Mathf.Approximately(color.r, 0f) && Mathf.Approximately(color.g, 0f) && Mathf.Approximately(color.b, 1f);
    }
}
