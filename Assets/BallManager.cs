using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    public GameObject Prefab_Trajectory, Prefab_Prediction;

    Vector3 BallPosition;//Ballの位置
    Vector3 PreBallPosition;//1フレーム前のボールの位置
    Vector3 BallVelocity;//ボールの速度
    Vector3 PreBallVelocity;//1フレーム前のボールの速度
    Vector3 BallAcceleration;//ボールの加速度

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
        BallPosition = Vector3.zero;//Ballの位置
        PreBallPosition = Vector3.zero;//1フレーム前のボールの位置
        BallVelocity = Vector3.zero;//ボールの速度
        PreBallVelocity = Vector3.zero;//1フレーム前のボールの速度
        BallAcceleration = Vector3.zero;//ボールの加速度

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

        PreBallPosition = BallPosition;//ボールの位置の記憶
        PreBallVelocity = BallVelocity;//ボールの速度を記憶
        BallPosition = this.transform.position;//ボールの位置を取得
        BallVelocity = (BallPosition - PreBallPosition);//ボールの速度を計算
        BallAcceleration = (BallVelocity - PreBallVelocity);

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

        trajectory[count_trajectory % number_trajectory] = Instantiate(Prefab_Trajectory, BallPosition, new Quaternion(0, 0, 0, 0));//軌跡を生成

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
                prediction[i] = Instantiate(Prefab_Prediction, Prediction(BallPosition + BallVelocity * i * MarkFrate), new Quaternion(0, 0, 0, 0));//軌跡を生成
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

            prediction[count_prediction % number_prediction] = Instantiate(Prefab_Prediction, Prediction(BallPosition + BallVelocity * number_prediction * MarkFrate), new Quaternion(0, 0, 0, 0));//軌跡を生成

            //予測軌道の先頭を別の色に
            sprite_prediction[number_prediction-1] = prediction[number_prediction-1].GetComponent<SpriteRenderer>();
            sprite_prediction[number_prediction-1].material.color = new Color32(233, 56, 214, 255);

            count_prediction += 1;
        }

    }

    Vector3 Prediction(Vector3 locate)
    {//跳ね返りを予測する

        Vector3 locate_return = locate;

        if (locate_return.x <= wall_left)
        {
            locate_return.x -= 2 * (locate_return.x - wall_left);
        }
        else if (wall_right <= locate_return.x)
        {
            locate_return.x -= 2 * (locate_return.x - wall_right);
        }

        if (locate_return.y <= wall_down)
        {
            locate_return.y -= 2 * (locate_return.y - wall_down);
        }
        else if (wall_up <= locate_return.y)
        {
            locate_return.y -= 2 * (locate_return.y - wall_up);
        }

        return locate_return;
    }
}
