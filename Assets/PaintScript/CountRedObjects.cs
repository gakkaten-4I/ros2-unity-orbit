using UnityEngine;

public class CountRedObjects : MonoBehaviour
{
    public LayerMask targetLayer; // 調べたいレイヤーをInspectorから設定できる

        private float timer = 0.0f; // タイマー用の変数
    private float interval = 0.1f; 
    
    public GameObject Canvas;// 0.1秒の間隔

    public int redObjectCount;
    private int layerObjectCount;
    private float ratio;

    void Start(){
        Invoke("result", 5f);
        Canvas.SetActive(true);
    }


    void Update()
    {
          timer += Time.deltaTime;

        // 0.1秒が経過したかチェック
        if (timer >= interval)
        {
            // 0.1秒ごとに実行したい処理
            Count();

            // タイマーをリセット
            timer = 0.0f;
        }
        

        // 指定したレイヤーのすべてのオブジェクトを取得
        
    }

    void Count(){

        redObjectCount = 0;
        layerObjectCount = 0;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // オブジェクトのレイヤーがターゲットレイヤーかチェック
            if (((1 << obj.layer) & targetLayer) != 0)
            {
                layerObjectCount++;
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    // マテリアルの色が赤かどうかを判定
                    if (renderer.material.color == Color.red)
                    {
                        redObjectCount++;
                    }
                }
            }
        }

        // 割り算結果を計算
        float ratio = layerObjectCount > 0 ? (float) redObjectCount / layerObjectCount  *100: 0;

        // 小数点1位まで丸める
        float roundedRatio = Mathf.Round(ratio * 10) / 10;

        // 結果をデバッグログに出力
        Debug.Log("割合（小数点1位まで）: " + roundedRatio);


        Debug.Log("赤色のオブジェクトの個数: " + redObjectCount);
     
    }



    // result関数
    private void result()
    {
        Debug.Log("やあ");
        Canvas.SetActive(false);
    }
}
