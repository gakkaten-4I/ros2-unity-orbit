using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inCoinSceneTrail : MonoBehaviour
{
    public GameObject Prefab_Trajectory;//生成する予測や軌跡のプレハブ(設計図)
    public GameObject[] trajectory;//Ballの軌跡を入れる

    private int number_trajectory;//表示する軌跡の数
    private SpriteRenderer[] sprite_trajectory;//軌跡の透明度
    private int count_trajectory;//総軌跡数
    private Vector3[] BallPosition;//Ballの位置
    private int nowindex;//取得したデータを配列のどのindexに入れるか
    private int MarkFrate;

    int number_getposition;//記憶しておくパケットのデータ数
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        // 力を加える
        Vector3 forceDirection = new Vector3(1, 0.8f, 0); // xy方向に力を加える
        rb.AddForce(forceDirection * 300f);

        //---------------------------------------------------------------------ここから初期値の代入、値の定義
        
        number_getposition=10;//記憶しておくパケットのデータ数
        nowindex=0;//取得したデータを配列のどのnowindexに入れるか

        MarkFrate = 3;//何フレームごとに速度計算、軌跡と予測マーク挿入をするか

        number_trajectory = 10;
        trajectory = new GameObject[number_trajectory];//Ballの軌跡を入れる
        sprite_trajectory = new SpriteRenderer[number_trajectory];//軌跡の透明度
        count_trajectory = 0;//総軌跡数
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTrajectory()
    {//軌跡を生成する関数

        if (count_trajectory >= number_trajectory) Destroy(trajectory[count_trajectory % number_trajectory]);//21個目の軌跡を消去

        if (count_trajectory < number_trajectory)
        {//軌跡が20より少ない
            for (int i = 0; i < count_trajectory; i++)
            {
                sprite_trajectory[i] = trajectory[i].GetComponent<SpriteRenderer>();
                sprite_trajectory[i].material.color = sprite_trajectory[i].material.color - new Color32(0, 0, 0, 20);
            }//透明度を下げる
        }
        else
        {
            for (int i = 0; i < number_trajectory; i++)
            {
                sprite_trajectory[i] = trajectory[i].GetComponent<SpriteRenderer>();
                sprite_trajectory[i].material.color = sprite_trajectory[i].material.color - new Color32(0, 0, 0, 20);
            }//透明度を下げる
        }

        trajectory[count_trajectory % number_trajectory] = Instantiate(Prefab_Trajectory, BallPosition[nowindex], new Quaternion(0, 0, 0, 0));//軌跡を生成

        sprite_trajectory[0] = trajectory[0].GetComponent<SpriteRenderer>();
        sprite_trajectory[0].material.color = new Color32(0, 0, 0, 20);//Color32(233,56,214,255)

        count_trajectory += 1;//カウントを増やす

    }
}
