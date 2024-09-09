using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpriteColorChange : MonoBehaviour
{
    // mallet（対象オブジェクト）
    public Transform mallet;
    // 色を変更する範囲の半径
    public float rangeRadius = 5f;
    // 変更する色
    public Color targetColor = Color.red;
    // 元の色に戻すかどうか
    public bool revertColor = true;
    // 元の色に戻すまでの遅延時間
    public float revertDelay = 2f;
    // 対象とするレイヤー
    public LayerMask targetLayer;

    // Update is called once per frame
    void Update()
    {
        if (mallet == null)
        {
            Debug.LogWarning("malletが指定されていません！");
            return;
        }

        // malletの現在の位置を取得
        Vector2 malletPosition = mallet.position;

        // malletの範囲内にあるスプライトを取得
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(malletPosition, rangeRadius, targetLayer);
        

        foreach (var hitCollider in hitColliders)
        {
            SpriteRenderer spriteRenderer = hitCollider.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // スプライトの色を赤に変更
                spriteRenderer.color = targetColor;

                // 元の色に戻す処理
                if (revertColor)
                {
                    StartCoroutine(RevertColor(spriteRenderer, revertDelay));
                }
            }
        }
    }

    // 元の色に戻すコルーチン
    IEnumerator RevertColor(SpriteRenderer spriteRenderer, float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.color = Color.white;  // 元の色を白に戻す
    }

    // 範囲をギズモで表示するための処理
    private void OnDrawGizmosSelected()
    {
        if (mallet != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(mallet.position, rangeRadius);
        }
    }
}