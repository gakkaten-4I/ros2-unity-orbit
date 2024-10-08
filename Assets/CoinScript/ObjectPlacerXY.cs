using System.Collections;
using UnityEngine;

public class ObjectPlacerXY : MonoBehaviour
{
    public GameObject objectToPlace;  // 配置するオブジェクトのプレハブ
    public int gridSizeX = 4;         // X軸方向のグリッド数（4に設定）
    public int gridSizeY = 10;        // Z軸方向のグリッド数（10に設定）
    public float spacing = 1f;        // オブジェクト間のスペース

    void Start()
    {
        PlaceObjectsInGrid();
        StartCoroutine(WaitAndCallResult(3f));  // 50秒後にresult関数を呼び出すコルーチンを開始
    }

    void PlaceObjectsInGrid()
    {
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

                            // オブジェクトに固有の名前を設定 (例: "Square_X2_Y3" など)
                        newObject.name = $"Square_X{x}_Y{y}";
                }
                // 配置位置を計算
                
                
                // 必要に応じて、個別に操作を加える (例: 色を変えるなど)
                // newObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    // 50秒後にresult関数を呼び出すコルーチン
    IEnumerator WaitAndCallResult(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        result();  // 50秒後にresult関数を実行
    }

    // 結果を表示する関数（必要に応じて定義）
    void result()
    {
        for (int x = 0; x < gridSizeX/2; x++)
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
}
