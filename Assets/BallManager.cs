using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    public GameObject Prefab_Trajectory, Prefab_Prediction;//生成する予測や軌跡のプレハブ(設計図)

    int number_getposition;//記憶しておくパケットのデータ数
    Vector3[] BallPosition;//Ballの位置
    Vector3[] BallVelocity;//Ballの速度
    int count_getposition;//パケットのデータを取得した総回数
    int index;//取得したデータを配列のどのindexに入れるか
    bool Outliers;//外れ値防止(外れ値が疑われる値を検出したらtrueになる)

    int MarkFrate;//何フレームごとに速度計算、軌跡と予測マーク挿入をするか

    int number_trajectory;//表示する軌跡の数
    GameObject[] trajectory;//Ballの軌跡を入れる
    SpriteRenderer[] sprite_trajectory;//軌跡の透明度
    int count_trajectory;//総軌跡数

    int number_prediction;//表示する予測マークの数
    GameObject[] prediction;//Ballの予測マークを入れる
    SpriteRenderer[] sprite_prediction;//予測マークの透明度
    int count_prediction;//総予測マーク数

    //フィールドの大きさ
    int wall_right = 10;
    int wall_left = -10;
    float wall_up = 4.75f;
    float wall_down = -4.75f;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        // 力を加える
        Vector3 forceDirection = new Vector3(1, 0.8f, 0); // xy方向に力を加える
        rb.AddForce(forceDirection * 300f);

        //---------------------------------------------------------------------ここから初期値の代入、値の定義
        
        number_getposition=20;//記憶しておくパケットのデータ数
        BallPosition=new Vector3[number_getposition];//Ballの位置
        BallVelocity=new Vector3[number_getposition];//Ballの速度
        count_getposition=0;//パケットのデータを取得した総回数
        index=0;//取得したデータを配列のどのindexに入れるか
        Outliers=false;//外れ値防止

        MarkFrate = 3;//何フレームごとに速度計算、軌跡と予測マーク挿入をするか

        number_trajectory = 10;
        trajectory = new GameObject[number_trajectory];//Ballの軌跡を入れる
        sprite_trajectory = new SpriteRenderer[number_trajectory];//軌跡の透明度
        count_trajectory = 0;//総軌跡数

        number_prediction = 10;
        prediction = new GameObject[number_prediction];//Ballの予測マークを入れる
        sprite_prediction = new SpriteRenderer[number_prediction];//予測マークの透明度
        count_prediction = 0;//総予測マーク数

    }

    // Update is called once per frame
    void Update()
    {

        GetBallData();//ボールの情報を更新

        if (Time.frameCount % MarkFrate == 0)
        {

            CreateTrajectory();//軌跡を生成

            CreatePrediction();//予測マークを生成   

        }

    }

    void GetBallData()
    {//ボールの速度を計算

        index=count_getposition%number_getposition;//データを記憶するindex
        int beforeindex,before2index;//ひとつ前のindex,そのまたひとつ前のindex
        if(index==0) beforeindex=number_getposition-1; else beforeindex=index-1;
        if(beforeindex==0) before2index=number_getposition-1; else before2index=beforeindex-1;
        BallPosition[index]=this.transform.position;//現在のボールの位置を記憶

        Vector3 tmp_velocity;
        if(count_getposition==0) BallVelocity[0]=Vector3.zero;
        else {
            tmp_velocity=BallPosition[index]-BallPosition[beforeindex];
            if(Outliers){//ひとつ前が外れ値の速度の可能性がある場合
                if(((Vector3.Angle(tmp_velocity,BallVelocity[beforeindex]))>20)|((Vector3.Angle(tmp_velocity,BallVelocity[before2index]))>20))//外れ値だったら
                    BallVelocity[index]=BallVelocity[beforeindex]=tmp_velocity;
                else//外れ値じゃなかったら
                    BallVelocity[index]=tmp_velocity;
                Outliers=false;
            }
            else {
                if(((Vector3.Angle(tmp_velocity,BallVelocity[beforeindex]))>20)|((Vector3.Angle(tmp_velocity,BallVelocity[before2index]))>20))
                    Outliers=true;
                BallVelocity[index]=tmp_velocity;
            }
        }

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

        trajectory[count_trajectory % number_trajectory] = Instantiate(Prefab_Trajectory, BallPosition[index], new Quaternion(0, 0, 0, 0));//軌跡を生成

        sprite_trajectory[0] = trajectory[0].GetComponent<SpriteRenderer>();
        sprite_trajectory[0].material.color = new Color32(0, 0, 0, 20);//Color32(233,56,214,255)

        count_trajectory += 1;//カウントを増やす

    }

    //軌道を予測
    void CreatePrediction()
    {

        if (count_prediction == 0)
        {
            for (int i = 0; i < number_prediction; i++)
            {
                prediction[i] = Instantiate(Prefab_Prediction, Prediction(BallPosition[index] + BallVelocity[index] * i * MarkFrate), new Quaternion(0, 0, 0, 0));//軌跡を生成
                sprite_prediction[i] = prediction[i].GetComponent<SpriteRenderer>();
                sprite_prediction[i].material.color += new Color32(0, 0, 0, 20);
                count_prediction += 1;
            }
        }
        else
        {

            for (int i = 0; i < number_prediction; i++)
            {
                sprite_prediction[i] = prediction[i].GetComponent<SpriteRenderer>();
                sprite_prediction[i].material.color += new Color32(0, 0, 0, 20);
            }//透明度を上げる

            Destroy(prediction[count_prediction % number_prediction]);//予測マークを消去

            prediction[count_prediction % number_prediction] = Instantiate(Prefab_Prediction, Prediction(BallPosition[index] + BallVelocity[index] * number_prediction * MarkFrate), new Quaternion(0, 0, 0, 0));//軌跡を生成

            //予測軌道の先頭を別の色に
            sprite_prediction[number_prediction-1] = prediction[number_prediction-1].GetComponent<SpriteRenderer>();
            sprite_prediction[number_prediction-1].material.color = new Color32(233, 56, 214, 255);

            count_prediction += 1;
        }

    }

    Vector3 Prediction(Vector3 locate)
    {//跳ね返りを予測する

        Vector3 locate_return = locate;
        Vector3 malletsize = this.GetComponent<Renderer>().bounds.size;//マレットのサイズを取得
        float malletmargin = malletsize.x/2;

        if (locate_return.x <= wall_left+malletmargin)
        {
            locate_return.x -= 2 * (locate_return.x - wall_left-malletmargin);
        }
        else if (wall_right-malletmargin <= locate_return.x)
        {
            locate_return.x -= 2 * (locate_return.x - (wall_right-malletmargin));
        }

        if (locate_return.y <= wall_down+malletmargin)
        {
            locate_return.y -= 2 * (locate_return.y - wall_down-malletmargin);
        }
        else if (wall_up-malletmargin <= locate_return.y)
        {
            locate_return.y -= 2 * (locate_return.y - (wall_up-malletmargin));
        }

        return locate_return;
    }
}
