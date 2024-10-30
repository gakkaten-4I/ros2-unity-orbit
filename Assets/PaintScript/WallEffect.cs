using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : MonoBehaviour
{
    public GameObject objectToPlace;

    private float timer; // タイマー変数
    public float interval = 0.1f; 
    private int i = 0; // 生成したオブジェクトのカウント

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter2D は他のコライダーと接触したときに呼ばれる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトのタグが "wall" の場合
        if (collision.CompareTag("Wall"))
        {
            // ここに衝突時の処理を追加
            Debug.Log("Wall hit detected!");

            // 例えば、ここでオブジェクトを生成することもできます
            Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
            GameObject newObject = Instantiate(objectToPlace, position, Quaternion.identity);
            newObject.name = $"effect_{i}";
            i++;
        }
    }
}
