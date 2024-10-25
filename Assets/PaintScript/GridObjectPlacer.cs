using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridObjectPlacerXZ : MonoBehaviour
{
    public GameObject objectToPlace;  // 配置するオブジェクトのプレハブ
    public int gridSizeX = 4;         // X軸方向のグリッド数（4に設定）
    public int gridSizeY = 10;        // Z軸方向のグリッド数（10に設定）
    public float spacing = 1f;
    public LayerMask targetLayers;   
    public TextMeshProUGUI Player1text;
    public TextMeshProUGUI Player2text;
    public GameObject P1;
    public GameObject P2;
    public GameObject WIN1;
    public GameObject LOSE1;
    public GameObject WIN2;
    public GameObject LOSE2;
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource; 

    private int countMin = 0; 
    private int Count1 = 0;
    private int Count2 = 0;   // オブジェクト間のスペース

    void Start()
    {
        PlaceObjectsInGrid();
        StartCoroutine(WaitAndCallPlus(3f));
        StartCoroutine(WaitAndCallResult(12f));
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);  // 50秒後にresult関数を呼び出すコルーチンを開始
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
                    Vector3 position = new Vector3(6.875f + x * spacing, -9.375f + y * spacing, 0);

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



    void plus()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        int i = 0;
        int a = 0;
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (x == 1){
                    a = gridSizeX * gridSizeY;
                }
                
                Vector3 position = new Vector3(5.625f + x * 8.75f, -9.375f + y * spacing, 0);
                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);
                            // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                newObject.name = $"Square_{i + a}";
                i++;
                

            }
        }
        
    }


    //コルーチンで呼び出される勝利判定
    void result(){
        SpriteCounter SpriteCounter = GetComponent<SpriteCounter>();
        RangeSpriteColorChange RangeSpriteColorChange = GetComponent<RangeSpriteColorChange>();
        SpriteCounter.CountFlag = 1;
        //変更
        Count1 = SpriteCounter.blueSprites;
        Count2 = SpriteCounter.redSprites;
        RangeSpriteColorChange.enabled = false;
        
        AllWhite();

        P1.SetActive(true);
        P2.SetActive(true);

        if(Count1 > Count2){
            countMin = Count2;
        }else{
            countMin = Count1;
        }

        //
            Player1text.color = Color.blue;
            //
            Player2text.color = Color.red;
        StartCoroutine(ResultShow());
        StartCoroutine(ResultShow2());

        
    }

    //Player1のポイント数える
    IEnumerator ResultShow()
    {

       for (int i = 0; i < countMin; i++)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(sound1);
            string objectName;
            // オブジェクトの名前を生成
            //グリッド数変えるときここも変える
            if(i != 8){
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
                // スプライトレンダラーを取得
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // スプライトの色を赤に変更
                    spriteRenderer.color = Color.blue;
                }

            }
            int ii = i+1;
            Player1text.text = ii.ToString();
            

            Vector2 currentScale = Player1text.transform.localScale;
            // スケールを少しずつ大きくする
            Player1text.transform.localScale = currentScale * 1.03f;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        Finish();
    }

    //Player2のポイント数える
    IEnumerator ResultShow2()
    {
       for (int i = 0; i < countMin; i++)
        {
            // オブジェクトの名前を生成

            //グリッド数変えるときここも変える

            string objectName = $"Square_{63-i}";

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
                    spriteRenderer.color = Color.red;
                }
            }

            Vector2 currentScale = Player2text.transform.localScale;

            // スケールを少しずつ大きくする
            Player2text.transform.localScale = currentScale * 1.03f;

            int ii = i+1;
            Player2text.text = ii.ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }

    //同じ数カウントして一気に買った方塗る
    void Finish(){
        if(Count1 > Count2){
            ikkini(1,Color.blue);
            
        }else{
            ikkini(2,Color.red);
        }
    }


    void ikkini(int num,Color targetColor){

        int GridNum;

            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(sound2);
        if(num == 1){
            GridNum = Count1-Count2;
            Player1text.text = Count1.ToString();
            Vector2 currentScale = Player1text.transform.localScale;
            // スケールを少しずつ大きくする
            Player1text.transform.localScale = currentScale * 1.2f;
        }else{
            GridNum = Count2-Count1;
            Player2text.text = Count2.ToString();
            Vector2 currentScale = Player2text.transform.localScale;
            // スケールを少しずつ大きくする
            Player2text.transform.localScale = currentScale * 1.2f;
        }

        for (int i = 0; i < GridNum; i++)
        {
            string objectName;
            // オブジェクトの名前を生成

            //グリッド数変えるときここも変える
            if(num ==1){
                objectName = $"Square_{Count2+i}";
            }else{            
                objectName = $"Square_{63-Count1-i}";
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

            if(num ==1){
                WIN1.SetActive(true);
                LOSE2.SetActive(true);
            }else{            
                WIN2.SetActive(true);
                LOSE1.SetActive(true);
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
                            spriteRenderer.color = new Color(0.75f, 0.75f, 0.75f);
                        }
                    }
                }
        }

    
}
