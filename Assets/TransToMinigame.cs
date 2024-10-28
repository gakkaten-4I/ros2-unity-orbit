using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransToMinigame : MonoBehaviour
{
    //[SerializeField]
    //private TextMeshProUGUI nextTextOfA,nextTextOfB;
    
    [SerializeField]
    private TextMeshProUGUI toMinigameCountOfA,toMinigameCountOfB;
    [SerializeField]
    private TextMeshProUGUI minigameNameTextOfA,minigameNameTextOfB;

    public GameObject NextText;
    public GameObject MinigameNameText;
    public GameObject ToMinigameCount;
    public GameObject PointBlue,PointRed;

    public AudioClip CountdownSound;
    public AudioClip AlarmSound;
    public AudioClip BossBattleAlarmSound;

    string StringOfBOSSBATTLE="BOSSBATTLE";
    /*
    @" ____   ___  ____ ____    ____    _  _____ _____ _     _____\n"+ 
    @"| __ ) / _ \/ ___/ ___|  | __ )  / \|_   _|_   _| |   | ____|\n"+
    @"|  _ \| | | \___ \___ \  |  _ \ / _ \ | |   | | | |   |  _|  \n"+
    @"| |_) | |_| |___) |__) | | |_) / ___ \| |   | | | |___| |___ \n"+
    @"|____/ \___/|____/____/  |____/_/   \_\_|   |_| |_____|_____|\n";*/

    string StringOfCOINGAME="COINGAME";
    /*
    @"  ____ ___ ___ _   _    ____    _    __  __ _____\n"+ 
    @" / ___/ _ \_ _| \ | |  / ___|  / \  |  \/  | ____|\n"+
    @"| |  | | | | ||  \| | | |  _  / _ \ | |\/| |  _|  \n"+
    @"| |__| |_| | || |\  | | |_| |/ ___ \| |  | | |___ \n"+
    @" \____\___/___|_| \_|  \____/_/   \_\_|  |_|_____|\n";*/

    string StringOfCOLORINGGAME="COLORINGGAME";
    /*
    @"  ____ ___  _     ___  ____  ___ _   _  ____    ____    _    __  __ _____\n"+ 
    @" / ___/ _ \| |   / _ \|  _ \|_ _| \ | |/ ___|  / ___|  / \  |  \/  | ____|\n"+
    @"| |  | | | | |  | | | | |_) || ||  \| | |  _  | |  _  / _ \ | |\/| |  _| \n"+ 
    @"| |__| |_| | |__| |_| |  _ < | || |\  | |_| | | |_| |/ ___ \| |  | | |___\n"+ 
    @" \____\___/|_____\___/|_| \_\___|_| \_|\____|  \____/_/   \_\_|  |_|_____|\n";*/

    // Start is called before the first frame update
    void Start()
    {
        NextText.SetActive(false);//「Next」テキストを非表示
        MinigameNameText.SetActive(false);//ミニゲーム名テキストを非表示
        ToMinigameCount.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdownOfMinigame(int i){
        StartCoroutine(GoMinigameCountdown(i));
    }

    public void StartAnimeOfTransMinigame(string str){
        StartCoroutine(GoMinigame(str));
    }

    //ミニゲーム遷移前にカウントダウンを行う(time 秒)
    IEnumerator GoMinigameCountdown(int time){

        Debug.Log("count-----------------------------------------------------");

        ToMinigameCount.SetActive(true);//カウントダウンを表示
        for(int i=5;i>=1;i--){
            toMinigameCountOfA.text=""+i+"";
            toMinigameCountOfB.text=""+i+"";
            GetComponent<AudioSource>().PlayOneShot(CountdownSound);
            yield return new WaitForSeconds(1f);
        }
        ToMinigameCount.SetActive(false);//カウントダウンを非表示

    }

    //ミニゲームへ遷移する関数
    IEnumerator GoMinigame(string NextMinigame)//引数は次遷移するミニゲームのシーン名
    {
        Debug.Log("-----------------------------------------------------");
        //Step1.「Next」テキストを2度点滅(2s)
        PointBlue.SetActive(false);
        PointRed.SetActive(false);
        for(int i=0;i<2;i++){
            NextText.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(AlarmSound);//警告音を鳴らす
            yield return new WaitForSeconds(0.5f);
            NextText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        NextText.SetActive(true);

        //Step2.「ミニゲームタイトル」テキストを変更
        MinigameNameText.SetActive(true);
        if(NextMinigame=="BossBattle"){
            minigameNameTextOfA.text=StringOfBOSSBATTLE;
            minigameNameTextOfB.text=StringOfBOSSBATTLE;
        }
        else if(NextMinigame=="CoinGame"){
            minigameNameTextOfA.text=StringOfCOINGAME;
            minigameNameTextOfB.text=StringOfCOINGAME;
        }
        else if(NextMinigame=="ColoringGame"){
            minigameNameTextOfA.text=StringOfCOLORINGGAME;
            minigameNameTextOfB.text=StringOfCOLORINGGAME;
        }
        else{
            minigameNameTextOfA.text="ERROR \n<NAME OF MINIGAME IS INCORRECT!!>";
            minigameNameTextOfB.text="ERROR \n<NAME OF MINIGAME IS INCORRECT!!>";
        }
        //MinigameNameText.SetActive(false);

        //Step3.「Next」&「ミニゲームタイトル」テキストを表示＆4度点滅(4s)
        yield return new WaitForSeconds(2f);
        for(int i=0;i<2;i++){
            NextText.SetActive(true);
            MinigameNameText.SetActive(true);

            if(NextMinigame=="BossBattle") GetComponent<AudioSource>().PlayOneShot(BossBattleAlarmSound);
            else GetComponent<AudioSource>().PlayOneShot(AlarmSound);

            yield return new WaitForSeconds(0.5f);
            NextText.SetActive(false);
            MinigameNameText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        //Step4.point表示を元に戻しておく
        PointBlue.SetActive(true);
        PointRed.SetActive(true);

        /*Step3.ミニゲームへ推移
        SceneManager.LoadScene(NextMinigame, LoadSceneMode.Single);
        */
    }

    void DebugTransition(){
        if((Input.GetKeyDown(KeyCode.D))&&(Input.GetKeyDown(KeyCode.B))) StartAnimeOfTransMinigame("BossBattle");
    }
}
