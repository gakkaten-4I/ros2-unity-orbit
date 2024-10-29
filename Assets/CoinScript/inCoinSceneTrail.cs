using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inCoinSceneTrail : MonoBehaviour
{

    public GameObject Prefab_Trajectory_Red;//生成する予測や軌跡のプレハブ(設計図)
    public GameObject Prefab_Trajectory_Blue;

    int number_getposition;//記憶しておくパケットのデータ数
    Vector3[] BallPosition;//Ballの位置
    Vector3[] BallVelocity;//Ballの速度
    Vector3 Outlier_velocity;//外れ値が疑われる値を保存
    int count_getposition;//パケットのデータを取得した総回数
    int nowindex;//取得したデータを配列のどのindexに入れるか
    bool Outliers;//外れ値防止(外れ値が疑われる値を検出したらtrueになる)

    int MarkFrate;//何フレームごとに速度計算、軌跡と予測マーク挿入をするか

    int number_trajectory;//表示する軌跡の数
    GameObject[] trajectory;//Ballの軌跡を入れる
    SpriteRenderer[] sprite_trajectory;//軌跡の透明度
    int count_trajectory;//総軌跡数

    public bool turn;//どちらのターンか
    

    //フィールドの大きさ
    float wall_right = 20.5f;
    float wall_left = 0.5f;
    float wall_up = 0f;
    float wall_down = -10f;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        //---------------------------------------------------------------------ここから初期値の代入、値の定義
        
        number_getposition=10;//記憶しておくパケットのデータ数
        BallPosition=new Vector3[number_getposition];//Ballの位置
        BallVelocity=new Vector3[number_getposition];//Ballの速度
        count_getposition=0;//パケットのデータを取得した総回数
        nowindex=0;//取得したデータを配列のどのnowindexに入れるか
        Outliers=false;//外れ値防止

        MarkFrate = 6;//何フレームごとに速度計算、軌跡と予測マーク挿入をするか

        number_trajectory = 5;
        trajectory = new GameObject[number_trajectory];//Ballの軌跡を入れる
        sprite_trajectory = new SpriteRenderer[number_trajectory];//軌跡の透明度
        count_trajectory = 0;//総軌跡数

        turn=true;//どちらがパケットを打っているか

    }

    // Update is called once per frame
    void Update()
    {

        GetBallData();//ボールの情報を更新

        if (Time.frameCount % MarkFrate == 0)
        {

            CreateTrajectory();//軌跡を生成

        }

    }

    void GetBallData()
    {//ボールの速度を計算

        nowindex=count_getposition%number_getposition;//データを記憶するindex
        int beforeindex,before2index;//ひとつ前のindex,そのまたひとつ前のindex
        if(nowindex==0) beforeindex=number_getposition-1; else beforeindex=nowindex-1;
        if(beforeindex==0) before2index=number_getposition-1; else before2index=beforeindex-1;

        Vector3 a=new Vector3(15,15,15);
        if(Input.GetKeyDown(KeyCode.A)) BallPosition[nowindex]=a; else BallPosition[nowindex]=this.transform.position;//現在のボールの位置を記憶

        Vector3 tmp_velocity;
        if(count_getposition==0) BallVelocity[0]=Vector3.zero;//現在のボールの速度を記録
        else {

            tmp_velocity=BallPosition[nowindex]-BallPosition[beforeindex];
            if(count_getposition==1) BallVelocity[nowindex]=tmp_velocity;
            else if(Outliers){//ひとつ前が外れ値の速度の可能性がある場合
                if((5<(Vector3.Angle(tmp_velocity,Outlier_velocity)))|(5<Nanbai(Magnitude_vec3(tmp_velocity),Magnitude_vec3(Outlier_velocity)))){//外れ値だったら
                    BallVelocity[nowindex]=tmp_velocity;
                }
                else{//外れ値じゃなかったら
                    BallVelocity[nowindex]=tmp_velocity;
                    BallVelocity[beforeindex]=Outlier_velocity;
                }
                Outliers=false;
            }
            else {
                if((5<(Vector3.Angle(tmp_velocity,BallVelocity[beforeindex])))|(5<Nanbai(Magnitude_vec3(tmp_velocity),Magnitude_vec3(BallVelocity[beforeindex])))){//ひとつ前の速度と大幅に違ったら
                    Outliers=true;
                    Outlier_velocity=tmp_velocity;
                    BallVelocity[nowindex]=BallVelocity[beforeindex];
                }
                else BallVelocity[nowindex]=tmp_velocity;
            }
        }

        turn=whichturn(BallVelocity[nowindex],turn);//どちらが撃ったパケットか判定

        count_getposition+=1;

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

        if(turn == false){
            trajectory[count_trajectory % number_trajectory] = Instantiate(Prefab_Trajectory_Red, BallPosition[nowindex], new Quaternion(0, 0, 0, 0));//軌跡を生成
        }
        else{
            trajectory[count_trajectory % number_trajectory] = Instantiate(Prefab_Trajectory_Blue, BallPosition[nowindex], new Quaternion(0, 0, 0, 0));//軌跡を生成
        }
        sprite_trajectory[0] = trajectory[0].GetComponent<SpriteRenderer>();
        sprite_trajectory[0].material.color = new Color32(0, 0, 0, 20);//Color32(233,56,214,255)

        count_trajectory += 1;//カウントを増やす

    }

    bool whichturn(Vector3 velocity, bool beforeturn){
        if((velocity.x>0) || (velocity.x==0 && beforeturn==true)) return true; else return false;  
    }

    int Nanbai(int a,int b){
        int tmp_a,tmp_b;
        if(a>b) {tmp_a=a;tmp_b=b;} else{tmp_a=b; tmp_b=a;}
        if(tmp_b==0) return 1; else return (tmp_a/tmp_b);
    }

    int Magnitude_vec3(Vector3 a){
        return Mathf.CeilToInt(Convert.ToSingle(Math.Pow(a.x*a.x+a.y*a.y+a.z*a.z,0.5)));
    }
}