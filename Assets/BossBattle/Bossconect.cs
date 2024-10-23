using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossconect : MonoBehaviour
{
    private List<Vector2> points;	//頭オブジェクトの位置を記録します。
    float appendDistance = 0.5f;	//各身体間、頭の固定した距離です。
    private float appendSqrDistance;

    int maxPointCount = 5;			//奇跡の最大記録数です。

    public GameObject _body;		//身体オブジェクトのプレハブです。
    public GameObject Head;			//頭オブジェクトを参照します。

    List<GameObject> _bodyList = new List<GameObject>();	//身体オブジェクトのListです。

    private int _bodyNum = 5;		//多関節の数です。身体オブジェクトの個数です。

    void Start()
    {
        Head = GameObject.Find("Boss");

        //身体オブジェクトを生成しています。
	    //Listで管理しています。
        for (int i = 0; i < _bodyNum; i++)
        {

    	    GameObject bodypref = Instantiate(_body) as GameObject;
		    _bodyList.Add(bodypref);
        }
    }

    //頭オブジェクトのtransform.positionを記録します
    void setPoints()
    {
        

        Vector2 curPoint = new Vector2(Head.transform.position.x, Head.transform.position.y);

        if (points == null)
        {
            points = new List<Vector2>();
            points.Add(curPoint);
        }

        // ポイントの追加.
        //addPoint(curPoint);

        // 最大数を超えた場合、削除.
        while (points.Count > maxPointCount)
        {
            points.RemoveAt(0);
        
        }

    }

    void Update()
    {

    }

    

}
