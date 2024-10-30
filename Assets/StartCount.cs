using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCount : MonoBehaviour
{
    public GameObject StartCountOfA,StartCountOfB;//スタート前のカウント

    [SerializeField]
    private TextMeshProUGUI startCountOfA, startCountOfB;//スタート前のカウントのテキスト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        StartCountOfA.SetActive(false);
        StartCountOfB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStartCount(int i){
        StartCoroutine(gameStartCount(i));
    }

    public IEnumerator gameStartCount(int startnum){

        Debug.Log("countdown---------------------------------------");

        StartCountOfA.SetActive(true);
        StartCountOfB.SetActive(true);

        for(int i=startnum;i>=0;i--){
            startCountOfA.text=""+i+"";
            startCountOfB.text=""+i+"";
            yield return new WaitForSeconds(1f);
        }

        startCountOfA.text="GO";
        startCountOfB.text="GO";
        yield return new WaitForSeconds(1f);

        StartCountOfA.SetActive(false);
        StartCountOfB.SetActive(false);

    }
}
