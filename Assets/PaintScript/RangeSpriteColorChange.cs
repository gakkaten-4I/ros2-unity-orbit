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
    private Vector3 previousVelocity;
    private Vector3 PrepreviousVelocity;  // 前フレームの速度
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



bool whichturn(Vector3 position, Vector3 velocity, Vector3 prevVelocity, Vector3 prevPrevVelocity,bool turn, bool beforeTurn) 
{
    // フィールドの中央をX軸の基準とする

    

    float fieldCenterX = (wallRight + wallLeft) / 2;

    // 3フレームの平均速度を計算
    Vector3 avgVelocity = (velocity + prevVelocity + prevPrevVelocity) / 3.0f;

    // 現在の速度と前フレームの速度の大きさの変化量を計算
    float speedChange = Mathf.Abs(avgVelocity.magnitude - prevVelocity.magnitude);

    // 壁の反射を検出するためのしきい値（この値は必要に応じて調整）
    float wallHitThreshold = 1.5f;

    // 壁に当たった場合はターンをそのままにする

        if (position.x <= wallLeft + wallHitThreshold || position.x >= wallRight - wallHitThreshold || position.y >= -0.5 - wallHitThreshold || position.y <= -10.5 + wallHitThreshold)
        {
            Debug.Log("Ball hit the wall, maintaining previous turn.");
            return beforeTurn;

        }else if(position.x < fieldCenterX && turn || position.x > fieldCenterX && !(turn)){
            return turn;
        }




    // 特定の基準を満たさない場合、前のターンを維持
    return beforeTurn;
}


    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerNum);
        GameObject otherObject = GameObject.Find("ball");
         BallManager ballManager = otherObject.GetComponent<BallManager>();



        if (ball.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            // 現在の速度を取得
            currentVelocity = rb.velocity;

            // 打ち返しを判定
            //isPlayerA = whichturn(ball.transform.position,ballManager.turn, isPlayerA);

            isPlayerA = whichturn(ball.transform.position, currentVelocity,previousVelocity,PrepreviousVelocity,BallManager.turn, isPlayerA); 
            


            
            

            if(isPlayerA){
                PlayerNum = 0;
            }else{
                PlayerNum =1;
            }

            // 現在の速度を前の速度として保存
            PrepreviousVelocity = previousVelocity;
            previousVelocity = currentVelocity;

        }

        if (mallet == null)
        {
            Debug.LogWarning("malletが指定されていません！");
            return;
        }

        timer += Time.deltaTime;

        /*if (timer >= 2)
        {
            // 値を切り替える
            PlayerNum = (PlayerNum == 0) ? 1 : 0;

            // タイマーをリセット
            timer = 0f;

            // 値を確認するためにログを出
        }*/

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
