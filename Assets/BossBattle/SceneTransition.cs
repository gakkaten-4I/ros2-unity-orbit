using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public MiniGameManager minigamemanager;
    private  hitpoint HitPoint;
    public bool IsBossBattle;

    

    void Start()
    {
        //minigamemanager = GetComponent<MiniGamemanager>();
        HitPoint = GetComponent<hitpoint>();
        // // 取得したコンポーネントが存在するか確認
        //if (miniGameManager == null)
        // {
        //     Debug.LogError("MiniGameManager コンポーネントが見つかりません");
        // }
        if (HitPoint != null)
         {
             Debug.LogError("HitPoint コンポーネントが見つかりません");
         }
    }

    void Update()
    {
        if(HitPoint != null && minigamemanager != null && (HitPoint.hp < 0) )
        {
            Debug.Log("mainに移動");
            SceneManager.LoadScene("MainScene");
        
        }
    }
}


