using System.Collections;
using UnityEngine;
using System;

public class ObjectPlacerXY : MonoBehaviour
{
    public GameObject objectToPlace;  // 配置するオブジェクトのプレハブ
    public int gridSizeX = 4;         // X軸方向のグリッド数（4に設定）
    public int gridSizeY = 10;        // Z軸方向のグリッド数（10に設定）
    public float spacing = 1f;        // オブジェクト間のスペース
    public float map_start_time = 0f; //マップが更新された直後の経過時間を格納するための変数

    void Start()
    {
        System.Random rnd = new System.Random();    // Randomオブジェクトの作成
        int NextMap = rnd.Next(1, 5);  //1以上5未満の値がランダムに出力 マップA~Dがランダムで出現
        PlaceObjectsInGrid(NextMap); //引数によってマップが変わる 1→マップA、2→マップB、3→マップC、4→マップD、5→マップE）

        //20秒後にマップEに移行
        StartCoroutine(DelayMethod(20f, () =>
        {
            Debug.Log("最後の10秒はマップE");

            //今あるコイン（"coins"タグがついているもの）を消去
            AllDEstroy();

            //マップEを生成
            PlaceObjectsInGrid(5);
        }));


        StartCoroutine(WaitAndCallResult(30f));  // 30秒後にresult関数を呼び出すコルーチンを開始
    }
    
    void Update()
    {
        //コインを配列に格納
        GameObject[] CoinsArray = GameObject.FindGameObjectsWithTag("coins");
        //現在の経過時間
        float now_time = Time.time;

        //コインが無い　or 前回のマップ更新から10秒経過 -> マップ更新
        if(CoinsArray.Length == 0　|| now_time - map_start_time >= 10)
        {
            Debug.Log("マップ更新");

            //今あるコイン（"coins"タグがついているもの）があれば消去
            AllDEstroy();

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
   
        if(map == 1) //マップA
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Vector3 position = new Vector3(9f + x * spacing, -9f + y * spacing, 0);

                    // オブジェクトを配置し、生成されたインスタンスの参照を取得
                    GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);

                    //タグつけ
                    newObject.tag = "coins";

                    // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                    newObject.name = $"Square_X{x}_Y{y}";
                }            
            }
        }

        if (map == 2) //マップB
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if(y == x || y == 4 - x)
                    {
                        Vector3 position = new Vector3(6f + x * spacing, -9f + y * spacing, 0);

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

        if (map == 3) //マップC
        {
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
                        Vector3 position = new Vector3(5f + x * spacing, -9f + y * spacing, 0);

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

        if (map == 4) //マップD
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (y == 1 && (x == 0 || x == 1) ||
                        y == 3 && (x == 3 || x == 4) ||
                        y == 4 - x)
                    {
                        Vector3 position = new Vector3(6f + x * spacing, -9f + y * spacing, 0);

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

        if (map == 5) //マップE
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (y == x || y == 4 - x ||
                        y == 2 && (x == 0 || x == 4) )
                    {
                        Vector3 position = new Vector3(6f + x * spacing, -9f + y * spacing, 0);

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

            Vector3 position1 = new Vector3(6f + 2f * spacing, -9f + 3.5f * spacing, 0);
            Vector3 position2 = new Vector3(6f + 2f * spacing, -9f + 0.5f * spacing, 0);

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
    void AllDEstroy()
    {
        GameObject[] CoinsArray2 = GameObject.FindGameObjectsWithTag("coins");

        foreach (GameObject coin_Soccer in CoinsArray2)
        {
            Destroy(coin_Soccer);
        }
    }


    // 50秒後にresult関数を呼び出すコルーチン
    IEnumerator WaitAndCallResult(float waitTime)
    {
        Debug.Log("result");
        yield return new WaitForSeconds(waitTime);
        //result();  // 50秒後にresult関数を実行
    }

    // 結果を表示する関数（必要に応じて定義）
    /*
    void result()
    {
        for (int x = 0; x < gridSizeX / 2; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {

                Vector3 position = new Vector3(7.5f + x * 5, -9.5f + y * spacing, 0);
                // オブジェクトを配置し、生成されたインスタンスの参照を取得
                GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);
                // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                newObject.name = $"Square_2X{x}_2Y{y}";


            }
        }
    }
    */
}