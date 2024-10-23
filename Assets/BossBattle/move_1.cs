using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] Transform moveObj;
    [SerializeField] float speed;
    List<Transform> points;
    int pointIdx;
    Vector3 nextPos;

    hitpoint move1;
    GameObject Boss;

    private int speeds;                //オブジェクトのスピード
    private int radius;               //円を描く半径
    private Vector3 defPosition;
    private Vector3 _Position;      //defPositionをVector3で定義する。
    float x;                         
    float y;
    bool flag;
    [SerializeField] GameObject target;
    [SerializeField] private ParticleSystem particle;

    void Start()
    {
        points = new List<Transform>();
        points = GetComponentsInChildren<Transform>().Where(t => t != transform).ToList();
        nextPos = points[pointIdx].position;

        //Bossオブジェクトを取得
        Boss = GameObject.Find("Boss");
        
        

        move1 = Boss.GetComponent<hitpoint>();
        speeds = 2;
        radius = 2;   
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (move1.hp > 30)
        {
            if ((nextPos - moveObj.position).sqrMagnitude > Mathf.Epsilon)
            {
                moveObj.position = Vector3.MoveTowards(moveObj.position, nextPos, speed * Time.deltaTime);
            }
            else
            {
                pointIdx++;

                if(pointIdx < points.Count)
                {
                    nextPos = points[pointIdx].position;
                }

                if(pointIdx == 4)
                {
                    nextPos = points[0].position;
                    pointIdx = 0;
                }
            }
            
        }
        else if (move1.hp > 15)
        {
            if ((nextPos - moveObj.position).sqrMagnitude > Mathf.Epsilon)
            {
                moveObj.position = Vector3.MoveTowards(moveObj.position, nextPos, speed * Time.deltaTime);
            }
            else
            {
                pointIdx++;

                if(pointIdx < points.Count)
                {
                    
                    
                    nextPos = points[pointIdx].position;
                }

                if(pointIdx == 8)
                {
                    nextPos = points[4].position;
                    pointIdx = 4;
                }
            }
            if (move1.hp == 30)
            {
                GameObject Boss3 = GameObject.Find("Boss3");
                GameObject Boss4 = GameObject.Find("Boss4");
                Destroy(Boss3);
                Destroy(Boss4);
            }
            
        }
        
        else if(move1.hp <= 15&&move1.hp > 0)
        {
            GameObject Boss1 = GameObject.Find("Boss1");
            GameObject Boss2 = GameObject.Find("Boss2");
            Destroy(Boss1);
            Destroy(Boss2);
            float rand = Random.Range(1.0f, 100.0f);
            if (!flag)
            {
                _Position.x = 10.00f;
                _Position.y = -5.00f;
                transform.position = _Position;

                flag = true;
            }
            
            x = radius * Mathf.Sin(Time.time * speeds);      //X軸の設定
            y = radius * Mathf.Cos(Time.time * speeds);      //Z軸の設定

            if (rand >= 99) 
            {
                moveObj.position = Vector3.MoveTowards(moveObj.position, -moveObj.position, speed * Time.deltaTime);
                Invoke("DelayMethod", 1.0f);
            }//moveObj.transform.RotateAround (target.transform.position, Vector3.forward, angle * Time.deltaTime);
            else {
                moveObj.position = new Vector3((x+transform.position.x),(y+transform.position.y), transform.position.z);  //自分のいる位置から座標を動かす。
            }
            particle.transform.position = moveObj.position;
            
            if (move1.hp <= 15)
            {
                
            }
            
        } 

        else if (move1.hp <= 0)
        {
            move1.hp = 0;
            particle.Play();
            
            
        }
        
    }
}