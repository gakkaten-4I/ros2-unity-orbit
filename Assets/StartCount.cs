using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCount : MonoBehaviour
{
    public GameObject StartCountOfA,StartCountOfB,GotextOfA,GotextOfB;//スタート前のカウント

    [SerializeField]
    private TextMeshProUGUI startCountOfA, startCountOfB,gotextofA,gotextofB;//スタート前のカウントのテキスト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        StartCountOfA.SetActive(false);
        StartCountOfB.SetActive(false);
        GotextOfA.SetActive(false);
        GotextOfB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStartCount(int i){
        StartCoroutine(gameStartCount(i));
    }

    public IEnumerator gameStartCount(int startnum){

        int tmpsize=250;

        Debug.Log("countdown---------------------------------------");

        StartCountOfA.SetActive(true);
        StartCountOfB.SetActive(true);

        for(int i=startnum;i>=1;i--){
            startCountOfA.text=""+i+"";//テキストの変更
            startCountOfB.text=""+i+"";
            for(int j=0;j<60;j++){
                
                if(j<5){
                    startCountOfA.fontSize=100f+150f*j/5f;
                    startCountOfB.fontSize=100f+150f*j/5f;
                }
                else if(j<7){
                    startCountOfA.fontSize=250f+150f*(j-5)/5f;
                    startCountOfB.fontSize=250f+150f*(j-5)/5f;
                }
                else if(j<9){
                    startCountOfA.fontSize=250f-150f*(j-9)/5f;
                    startCountOfB.fontSize=250f-150f*(j-9)/5f;
                }
                /*
                startCountOfA.color+=new Color(0,0,0,150f/9f);
                    startCountOfB.color+=new Color(0,0,0,150f/9f);
                */
                
                yield return new WaitForSecondsRealtime(1f/90f);
            }
            startCountOfA.fontSize=250f;
            startCountOfB.fontSize=250f;
        }
        

        StartCountOfA.SetActive(false);
        StartCountOfB.SetActive(false);
        GotextOfA.SetActive(true);
        GotextOfB.SetActive(true);
        //初期化処理
        gotextofA.fontSize=150f;
        gotextofB.fontSize=150f;
        Color tmpColorOfA = gotextofA.color;
        gotextofA.color = new Color(tmpColorOfA.r, tmpColorOfA.g, tmpColorOfA.b, 1);
        Color tmpColorOfB = gotextofB.color;
        gotextofB.color = new Color(tmpColorOfB.r, tmpColorOfB.g, tmpColorOfB.b, 1);
        
        for(int j=0;j<30;j++){
            
            if(j<5){
                gotextofA.fontSize+=150f/5f;
                gotextofB.fontSize+=150f/5f;

            }
            else if(j<7){
                gotextofA.fontSize+=50f/5f;
                gotextofB.fontSize+=50f/5f;
            }
            else if(j<9){
                gotextofA.fontSize-=50f/5f;
                gotextofB.fontSize-=50f/5f;
            }
            /*
            startCountOfA.color+=new Color(0,0,0,150f/9f);
                startCountOfB.color+=new Color(0,0,0,150f/9f);
            */
            
            yield return new WaitForSecondsRealtime(1f/60f);
        }
        /*
        gotextofA.fontSize=300f;
        gotextofB.fontSize=300f;
        */

        Vector3 rotationSpeed = new Vector3(0, 0, 45);

        for(int i=0;i<20;i++){
            gotextofA.fontSize+=20f*i/25f;
            gotextofB.fontSize+=20f*i/25f;
            GotextOfA.transform.Rotate(rotationSpeed);
            GotextOfB.transform.Rotate(rotationSpeed);

            
            Color currentColorOfA = gotextofA.color;
            float newAlphaOfA = currentColorOfA.a - 1f/30f;
            gotextofA.color = new Color(currentColorOfA.r, currentColorOfA.g, currentColorOfA.b, newAlphaOfA);

            Color currentColorOfB = gotextofB.color;
            float newAlphaOfB = currentColorOfB.a - 1f/30f;
            gotextofB.color = new Color(currentColorOfB.r, currentColorOfB.g, currentColorOfB.b, newAlphaOfB);
            
            

            yield return new WaitForSecondsRealtime(1f/30f);
        }
        
        GotextOfA.SetActive(false);
        GotextOfB.SetActive(false);
    }
}
