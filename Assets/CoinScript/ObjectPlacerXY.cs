using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class ObjectPlacerXY : MonoBehaviour
{
    public GameObject objectToPlace;  // 配置するオブジェクトのプレハブ
    public int gridSizeX = 4;         // X軸方向のグリッド数（4に設定）
    public int gridSizeY = 10;        // Z軸方向のグリッド数（10に設定）
    public float spacing = 1.5f;        // オブジェクト間のスペース
    public float map_start_time = 0f; //マップが更新された直後の経過時間を格納するための変数
    private bool continueFlag = true; //今後ゲームを続けるかのフラグ
    private int counting; //カウントダウンタイマー用の関数
    public TextMeshProUGUI countText; //カウントダウンタイマー用の表示テキスト

    //tmpの基準点
    private float tmpX;
    private float tmpY;
    
    public AudioSource timeRimitAudio;

    void Start()
    {
        timeRimitAudio = GetComponent<AudioSource>();

        //テキストの座標の基準


        System.Random rnd = new System.Random();    // Randomオブジェクトの作成
        int NextMap = rnd.Next(1, 5);  //1以上5未満の値がランダムに出力 マップA~Dがランダムで出現
        PlaceObjectsInGrid(NextMap); //引数によってマップが変わる 1→マップA、2→マップB、3→マップC、4→マップD、5→マップE）

        //20秒後にマップEに移行
        StartCoroutine(DelayMethod(20f, () =>
        {
            Debug.Log("最後の10秒はマップE");

            //今あるコイン（"coins"タグがついているもの）を消去
            AllDestroy();

            //マップEを生成
            PlaceObjectsInGrid(5);
            continueFlag = false;
        }));

        if (countText != null)
        {
            RectTransform RectCountText = countText.GetComponent<RectTransform>();
            RectCountText.anchoredPosition = new Vector3(tmpX + 8f, tmpY - 8f, 0f);
            RectCountText.sizeDelta = new Vector2(2f,2f);
            countText.fontSize = 1.5f;
            RectCountText.transform.Rotate(0,0,-180);
            countText.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            Debug.LogError("countTextオブジェクトが指定されていません");
        }

        StartCoroutine(CountDown());
        StartCoroutine(WaitAndCallResult(25f));  // 30秒後にresult関数を呼び出すコルーチンを開始
    }
    
    void Update()
    {
        //コインを配列に格納
        GameObject[] CoinsArray = GameObject.FindGameObjectsWithTag("coins");
        //現在の経過時間
        float now_time = Time.time;

        //コインが無い　or 前回のマップ更新から10秒経過 -> マップ更新 かつcontinueFlagがtrueであるとき
        if((CoinsArray.Length == 0　|| now_time - map_start_time >= 10) && (continueFlag == true))
        {
            Debug.Log("マップ更新");

            //今あるコイン（"coins"タグがついているもの）があれば消去
            AllDestroy();

            //新しいマップを生成
            System.Random rnd = new System.Random();
            int NextMap = rnd.Next(1, 5);
            PlaceObjectsInGrid(NextMap);
        }

    }
    
    void PlaceObjectsInGrid(int map) 
    {
        if (objectToPlace == null)
        {
            Debug.LogError("objectToPlace is not assigned.");
            return;
        }
   
        if(map == 1)
        {//マップA
            Debug.Log("マップA");
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Vector3 position = new Vector3(7f + x * 2f, -7.5f + (y * 1.5f), 0);

                    // オブジェクトを配置し、生成されたインスタンスの参照を取得
                    GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);

                    //タグつけ
                    newObject.tag = "coins";

                    // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                    newObject.name = $"Square_X{x}_Y{y}";
                }            
            }
        }

        if (map == 2) 
        {//マップB
            Debug.Log("マップB");
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if(y == x || y == 4 - x)
                    {
                        Vector3 position = new Vector3(4f + x * spacing, -7.5f + y * 1.5f, 0);

                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                        GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);

                        //タグつけ
                        newObject.tag = "coins";

                        // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                        newObject.name = $"Square_X{x}_Y{y}";
                    }              
                }
            }
        }

        if (map == 3)
        {//マップC
            Debug.Log("マップC");
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if ((y == 0 && x == 1)||
                        (y == 1 && x > 2 && x < 5) ||
                        (y == 2 && (x == 0 || x == 5))|| 
                        (y == 3 && x > 0 && x < 3) || 
                        (y == 4 && x == 4))
                    {
                        Vector3 position = new Vector3(4.25f + x * 1.5f, -7.5f + y * 1.5f, 0);

                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                        GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);

                        //タグつけ
                        newObject.tag = "coins";

                        // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                        newObject.name = $"Square_X{x}_Y{y}";
                    }
                }
            }
        }

        if (map == 4)
        {//マップD
            Debug.Log("マップD");
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (y == 1 && (x == 0 || x == 1) ||
                        y == 3 && (x == 3 || x == 4) ||
                        y == 4 - x)
                    {
                        Vector3 position = new Vector3(4f + x * spacing, -7.5f + y * 1.5f, 0);

                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                        GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);

                        //タグつけ
                        newObject.tag = "coins";

                        // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                        newObject.name = $"Square_X{x}_Y{y}";
                    }
                }
            }
        }

        if (map == 5)
        {//マップE
            Debug.Log("マップE");
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (y == x || y == 4 - x ||
                        y == 2 && (x == 0 || x == 4) )
                    {
                        Vector3 position = new Vector3(4f + x * spacing, -7.5f + y * 1.5f, 0);

                        // オブジェクトを配置し、生成されたインスタンスの参照を取得
                        GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);

                        //タグつけ
                        newObject.tag = "coins";

                        // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                        newObject.name = $"Square_X{x}_Y{y}";
                    }
                    
                }
            }
            //for文のみで配置できない部分

            Vector3 position1 = new Vector3(4f + 2f * spacing, -7.5f + 3.5f * 1.5f, 0);
            Vector3 position2 = new Vector3(4f + 2f * spacing, -7.5f + 0.5f * 1.5f, 0);

            // オブジェクトを配置し、生成されたインスタンスの参照を取得
            GameObject newObject1 = Instantiate(objectToPlace, position1, Quaternion.identity);
            GameObject newObject2 = Instantiate(objectToPlace, position2, Quaternion.identity);

            //タグつけ
            newObject1.tag = "coins";
            newObject2.tag = "coins";

            // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
            newObject1.name = $"Square_X{2}_Y{3.5}";
            newObject2.name = $"Square_X{2}_Y{0.5}";
        }

        // 配置位置を計算


        // 必要に応じて、個別に操作を加える (例: 色を変えるなど)
        // newObject.GetComponent<Renderer>().material.color = Color.red;

        //マップの開始時間を記録(10秒後に消す動作用)
        map_start_time = Time.time;
    }

    //(waitTime)秒後に(action)を行う関数
    IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    //今あるコインを消す
    public void AllDestroy()
    {
        GameObject[] CoinsArray2 = GameObject.FindGameObjectsWithTag("coins");

        foreach (GameObject coin_Soccer in CoinsArray2)
        {
            Destroy(coin_Soccer);
        }
    }

    IEnumerator CountDown()
    {
        countText.enabled = true;
        for(int i=0; i<25; i++){
            counting = 25 - i;
            countText.text = counting.ToString();
            if(i > 19){
                for(int j=0; j<2; j++){
                    yield return new WaitForSeconds(0.25f);
                    countText.enabled = false;
                    yield return new WaitForSeconds(0.25f);
                    countText.enabled = true;
                }
                timeRimitAudio.Play();
            }else{
                yield return new WaitForSeconds(1f);
            }
        }
        counting = 0;
        countText.text = counting.ToString();
    }

    // 30秒後にすべてを壊す
    IEnumerator WaitAndCallResult(float waitTime)
    {
        Debug.Log("result");
        yield return new WaitForSeconds(waitTime);
        AllDestroy();
    }

    
}