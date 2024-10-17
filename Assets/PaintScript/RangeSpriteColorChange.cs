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
    private Color targetColor = Color.red;
    // 元の色に戻すかどうか
    public bool revertColor = true;
    // 元の色に戻すまでの遅延時間
    public float revertDelay = 2f;
    // 対象とするレイヤー
    public LayerMask targetLayer;

    private int PlayerNum =0;

    private float timer = 0f;

    public GameObject ball;  // ボールの参照
    private Vector3 previousVelocity;  // 前フレームの速度
    private Vector3 currentVelocity;   // 現在の速度
    private float wallRight = 20.0f;  // 右の壁
    private float wallLeft = 0.0f;    // 左の壁
    private bool isPlayerA = true;   
    public bool flag = true;  // true = プレイヤーAのターン, false = プレイヤーBのターン

    void Start()
    {
        // ボールの初期速度を取得
        if (ball.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            previousVelocity = rb.velocity;
        }
    }



    bool whichturn(Vector3 position, Vector3 velocity, Vector3 prevVelocity, bool beforeTurn) 
{
    // フィールドの中央をX軸の基準とする
    float fieldCenterX = (wallRight + wallLeft) / 2;

    // 速度の変化量を算出
    float speedChange = (velocity - prevVelocity).magnitude;

    // 壁の反射を検出するためのしきい値（この値は必要に応じて調整）
    float wallHitThreshold = 0.1f; // 反射があったとみなすためのしきい値

    // 壁に当たった場合はターンをそのままにする
    if (position.x <= wallLeft + wallHitThreshold || position.x >= wallRight - wallHitThreshold)
    {
        Debug.Log("Ball hit the wall, maintaining previous turn.");
        return beforeTurn; // 前のターンを維持
    }

    // 速度の急激な変化があるか確認（一定以上の速度変化を検出）
    if (speedChange > 2.0f) // 2.0fは調整可能な閾値
    {
        // パックが中央より左側にあり、速度が右向きならプレイヤーBが打ったと判断
        if (position.x < fieldCenterX && velocity.x > 0) 
        {
            Debug.Log("Player B hit the ball!");
            return true;  // プレイヤーBのターン
        }
        // パックが中央より右側にあり、速度が左向きならプレイヤーAが打ったと判断
        else if (position.x > fieldCenterX && velocity.x < 0) 
        {
            Debug.Log("Player A hit the ball!");
            return false; // プレイヤーAのターン
        }
    }

    // 特定の基準を満たさない場合、前のターンを維持
    return beforeTurn;
}


    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerNum);

        /*if (ball.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            // 現在の速度を取得
            currentVelocity = rb.velocity;

            // 打ち返しを判定
            isPlayerA = whichturn(ball.transform.position, currentVelocity, previousVelocity, isPlayerA);

            if(isPlayerA){
                PlayerNum = 0;
            }else{
                PlayerNum =1;
            }

            // 現在の速度を前の速度として保存
            previousVelocity = currentVelocity;
        }*/

        if (mallet == null)
        {
            Debug.LogWarning("malletが指定されていません！");
            return;
        }

        timer += Time.deltaTime;

        if (timer >= 2)
        {
            // 値を切り替える
            PlayerNum = (PlayerNum == 0) ? 1 : 0;

            // タイマーをリセット
            timer = 0f;

            // 値を確認するためにログを出
        }

        //0or1
        /*if(PlayerNum == 0){
            targetColor = Color.blue;
        }else {
            targetColor = Color.red;
        }*/

        if (PlayerNum == 0)
        {
            // 青 (70, 70, 255)
            targetColor = new Color(50 / 255f, 50 / 255f, 255 / 255f);
        }
        else
        {
            // 赤 (255, 70, 70)
            targetColor = new Color(255 / 255f, 50 / 255f, 50 / 255f);
        }



        timer += Time.deltaTime;

        // 2秒以上経過した場合
        

        // malletの現在の位置を取得
        Vector2 malletPosition = mallet.position;

        // malletの範囲内にあるスプライトを取得
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(malletPosition, rangeRadius, targetLayer);
        
        
            foreach (var hitCollider in hitColliders)
            {
                SpriteRenderer spriteRenderer = hitCollider.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    
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
