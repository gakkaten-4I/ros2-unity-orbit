using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddMinigamePoint : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI addPointOfA,addPointOfB;//何ポイント加算されるかを表示するテキスト

    public GameObject AddMessageOfA,AddMessageOfB;//テキストオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        AddMessageOfA.SetActive(false);
        AddMessageOfB.SetActive(false);//全て非表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //下記コルーチンを再生する関数
    public void PlusMinigamePoint(char winteam,int addpoint)//winteam:勝ったチーム(A or B),addpoint:何点加算するか
    {
        StartCoroutine(ReflectMinigamePoint(winteam,addpoint));
    }

    IEnumerator ReflectMinigamePoint(char winteam,int addpoint)
    {
        SceneManager.LoadScene(QuietScene, LoadSceneMode.Single);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(MainScene, LoadSceneMode.Single);
    }
}
