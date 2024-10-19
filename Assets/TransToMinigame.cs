using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransToMinigame : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI minigameNameTextOfA;//ミニゲーム名を表示させるテキスト(左と右の順)
    [SerializeField]private TextMeshProUGUI minigameNameTextOfB;

    public GameObject NextText;//「NEXT...」のテキスト(左右2つ)
    public GameObject MinigameNameText;//ミニゲーム名を表示させるテキスト(こちらはGameObject型)(左右2つ)

    string StringOfBOSSBATTLE="BOSSBATTLE";
    /*
    @" ____   ___  ____ ____    ____    _  _____ _____ _     _____\n"+ 
    @"| __ ) / _ \/ ___/ ___|  | __ )  / \|_   _|_   _| |   | ____|\n"+
    @"|  _ \| | | \___ \___ \  |  _ \ / _ \ | |   | | | |   |  _|  \n"+
    @"| |_) | |_| |___) |__) | | |_) / ___ \| |   | | | |___| |___ \n"+
    @"|____/ \___/|____/____/  |____/_/   \_\_|   |_| |_____|_____|\n";
    */

    string StringOfCOINGAME="COINGAME";
    /*
    @"  ____ ___ ___ _   _    ____    _    __  __ _____\n"+ 
    @" / ___/ _ \_ _| \ | |  / ___|  / \  |  \/  | ____|\n"+
    @"| |  | | | | ||  \| | | |  _  / _ \ | |\/| |  _|  \n"+
    @"| |__| |_| | || |\  | | |_| |/ ___ \| |  | | |___ \n"+
    @" \____\___/___|_| \_|  \____/_/   \_\_|  |_|_____|\n";
    */

    string StringOfCOLORINGGAME="COLORINGGAME";
    /*
    @"  ____ ___  _     ___  ____  ___ _   _  ____    ____    _    __  __ _____\n"+ 
    @" / ___/ _ \| |   / _ \|  _ \|_ _| \ | |/ ___|  / ___|  / \  |  \/  | ____|\n"+
    @"| |  | | | | |  | | | | |_) || ||  \| | |  _  | |  _  / _ \ | |\/| |  _| \n"+ 
    @"| |__| |_| | |__| |_| |  _ < | || |\  | |_| | | |_| |/ ___ \| |  | | |___\n"+ 
    @" \____\___/|_____\___/|_| \_\___|_| \_|\____|  \____/_/   \_\_|  |_|_____|\n";
    */

    // Start is called before the first frame update
    void Start()
    {
        NextText.SetActive(false);//「Next」テキストを非表示
        MinigameNameText.SetActive(false);//「Next」テキストを非表示
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.D))&&(Input.GetKeyDown(KeyCode.B))) DebugTransition();
    }

    //ミニゲームへ遷移する関数
    IEnumerator GoMinigame(string NextMinigame)//引数は次遷移するミニゲームのシーン名
    {
        NextText.SetActive(true);

        //Step1.「Next」テキストを2度点滅(2s)
        for(int i=0;i<2;i++){
            NextText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            NextText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

        //Step2.「Next」&「ミニゲームタイトル」テキストを表示＆4度点滅(4s)
        NextText.SetActive(true);
        MinigameNameText.SetActive(true);

        //Step3.「ミニゲームタイトル」テキストを変更
        if(NextMinigame=="BossBattle") {
            minigameNameTextOfA.text=StringOfBOSSBATTLE;
            minigameNameTextOfB.text=StringOfBOSSBATTLE;
        }
        else if(NextMinigame=="CoinGame") {
            minigameNameTextOfA.text=StringOfCOINGAME;
            minigameNameTextOfB.text=StringOfCOINGAME;
        }
        else if(NextMinigame=="ColoringGame") {
            minigameNameTextOfA.text=StringOfCOLORINGGAME;
            minigameNameTextOfB.text=StringOfCOLORINGGAME;
        }
        else {
            minigameNameTextOfA.text="ERROR!!";
            minigameNameTextOfB.text="ERROR!!";
        }

        //Step4. 4度点滅(4s)
        yield return new WaitForSeconds(2f);
        for(int i=0;i<4;i++){
            NextText.SetActive(true);
            MinigameNameText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            NextText.SetActive(false);
            MinigameNameText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        NextText.SetActive(true);
        MinigameNameText.SetActive(true);

        /*Step5.ミニゲームへ推移
        SceneManager.LoadScene(NextMinigame, LoadSceneMode.Single);
        */
    }

    void DebugTransition(){
        Debug.Log("DEBUG:GOMINIGAME!!");
        StartCoroutine(GoMinigame("BossBattle"));
    }
}
