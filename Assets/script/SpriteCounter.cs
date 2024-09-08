using UnityEngine;

public class SpriteCounter : MonoBehaviour
{
    private int redSprites = 0; 
    public float redRatio=0f;

    void Update()
    {
        // シーン内のすべてのSpriteRendererをカウント
        int spriteCount = CountSpritesInScene();
        Debug.Log($"Total number of sprites in the scene: {spriteCount}");
    }

    int CountSpritesInScene()
    {

        int redSprites = 0;
        // SpriteRendererコンポーネントを持つすべてのゲームオブジェクトを検索
        SpriteRenderer[] spriteRenderers = GameObject.FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (IsRed(sr.color))
            {
                redSprites++;
            }
        }
        
        float redSpriteRatio = Mathf.Round(((float)redSprites / spriteRenderers.Length * 100f) * 10f) / 10f;

        // 小数点一桁に丸める
        return redSprites;


    }

    bool IsRed(Color color)
    {
        // 色が完全な赤 (255, 0, 0) かをチェック
        return Mathf.Approximately(color.r, 1f) && Mathf.Approximately(color.g, 0f) && Mathf.Approximately(color.b, 0f);
    }
}
