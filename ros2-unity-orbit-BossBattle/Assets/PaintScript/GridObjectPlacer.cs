using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectPlacerXZ : MonoBehaviour
{
    public GameObject objectToPlace;  // 配置するオブジェクトのプレハブ
    public int gridSizeX = 4;         // X軸方向のグリッド数（4に設定）
    public int gridSizeY = 10;        // Z軸方向のグリッド数（10に設定）
    public float spacing = 1f;
    public LayerMask targetLayers;   
    
    private int countMin = 0; 
    private int Count1 = 0;
    private int Count2 = 0;   // オブジェクト間のスペース

    void Start()
    {
        PlaceObjectsInGrid();
        StartCoroutine(WaitAndCallPlus(3f));
        StartCoroutine(WaitAndCallResult(12f));  // 50秒後にresult関数を呼び出すコルーチンを開始
    }

    void PlaceObjectsInGrid()
    {
        int i = gridSizeY;

        if (objectToPlace == null)
        {
            Debug.LogError("objectToPlace is not assigned.");
            return;
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3 position = new Vector3(8.5f + x * spacing, -9.5f + y * spacing, 0);

                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                        
                        GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);
                        
                        i++;
                            // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                        
                        newObject.name = $"Square_{i}";
                }
                // 配置位置を計算
                
                
                // 必要に応じて、個別に操作を加える (例: 色を変えるなど)
                // newObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    // 50秒後にresult関数を呼び出すコルーチン
    IEnumerator WaitAndCallPlus(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        plus();  // 50秒後にresult関数を実行
    }

    IEnumerator WaitAndCallResult(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        result();  // 50秒後にresult関数を実行
    }


    // 結果を表示する関数（必要に応じて定義）
    void plus()
    {
        int i = 0;
        int a = 0;
        for (int x = 0; x < gridSizeX/2; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (x == 1){
                    a = gridSizeX * gridSizeY;
                }
                
                Vector3 position = new Vector3(7.5f + x * 5, -9.5f + y * spacing, 0);
                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);
                            // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                newObject.name = $"Square_{i + a}";
                i++;
                

            }
        }
    }

    void result(){
        SpriteCounter SpriteCounter = GetComponent<SpriteCounter>();
        RangeSpriteColorChange RangeSpriteColorChange = GetComponent<RangeSpriteColorChange>();
        SpriteCounter.CountFlag = 1;
        Count1 = SpriteCounter.redSprites;
        Count2 = SpriteCounter.blueSprites;
        RangeSpriteColorChange.enabled = false;
        
        AllWhite();

        if(Count1 > Count2){
            countMin = Count2;
        }else{
            countMin = Count1;
        }

        StartCoroutine(ResultShow());
        StartCoroutine(ResultShow2());

        
    }


    IEnumerator ResultShow()
    {
       for (int i = 0; i < countMin; i++)
        {
            string objectName;
            // オブジェクトの名前を生成
            //グリッド数変えるときここも変える
            if(i != 10){
                objectName = $"Square_{i}";
            }
            else{
                objectName = "Square";
            }

            // 名前でオブジェクトを検索
            GameObject obj = GameObject.Find(objectName);

            if (obj != null)
            {
                // アニメーションコンポーネントを取得
                Animator animator = obj.GetComponent<Animator>();
                if (animator != null)
                {
                    // アニメーションを再生
                    animator.Play("square");
                }
                else
                {
                    Debug.Log($"Animator not found on {objectName}");
                }

                // スプライトレンダラーを取得
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // スプライトの色を赤に変更
                    spriteRenderer.color = Color.red;
                }
                else
                {
                    Debug.LogWarning($"SpriteRenderer not found on {objectName}");
                }
            }
            else
            {
                Debug.LogWarning($"{objectName} not found.");
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        Finish();
    }

    IEnumerator ResultShow2()
    {
       for (int i = 0; i < countMin; i++)
        {
            // オブジェクトの名前を生成

            //グリッド数変えるときここも変える

            string objectName = $"Square_{59-i}";

            // 名前でオブジェクトを検索
            GameObject obj = GameObject.Find(objectName);

            if (obj != null)
            {
                // アニメーションコンポーネントを取得
                Animator animator = obj.GetComponent<Animator>();
                if (animator != null)
                {
                    // アニメーションを再生
                    animator.Play("square");
                }
                else
                {
                    Debug.Log($"Animator not found on {objectName}");
                }

                // スプライトレンダラーを取得
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // スプライトの色を赤に変更
                    spriteRenderer.color = Color.blue;
                }
                else
                {
                    Debug.LogWarning($"SpriteRenderer not found on {objectName}");
                }
            }
            else
            {
                Debug.LogWarning($"{objectName} not found.");
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Finish(){
        if(Count1 > Count2){
            ikkini(1,Color.red);
        }else{
            ikkini(2,Color.blue);
        }
    }


    void ikkini(int num,Color targetColor){

        int GridNum;

        if(num == 1){
            GridNum = Count1-Count2;
        }else{
            GridNum = Count2-Count1;
        }

        for (int i = 0; i < GridNum; i++)
        {
            string objectName;
            // オブジェクトの名前を生成

            //グリッド数変えるときここも変える
            if(num ==1){
                objectName = $"Square_{Count2+i}";
            }else{            
                objectName = $"Square_{59-Count1-i}";
            }

            // 名前でオブジェクトを検索
            GameObject obj = GameObject.Find(objectName);

            if (obj != null)
            {
                // アニメーションコンポーネントを取得
                Animator animator = obj.GetComponent<Animator>();
                if (animator != null)
                {
                    // アニメーションを再生
                    animator.Play("square");
                }

                // スプライトレンダラーを取得
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // スプライトの色を赤に変更
                    spriteRenderer.color = targetColor;
                }

            }

        }
    }


     void AllWhite(){

            GameObject[] gameObjects = FindObjectsOfType<GameObject>();

            foreach (GameObject obj in gameObjects)
                {                
                    // LayerMaskで指定されたレイヤーにオブジェクトが属しているか確認
                    if ((targetLayers.value & (1 << obj.layer)) != 0){
                    // SpriteRendererが付いているかを確認
                        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            // スプライトの色を白に変更
                            spriteRenderer.color = Color.white;
                        }
                    }
                }
        }

    
}
