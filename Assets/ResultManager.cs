using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI YouWin, YouLose, DrawRight, DrawLeft, ThanksRight, ThanksLeft;
    RectTransform YouWinForm;
    RectTransform YouLoseForm;
    [SerializeField]
    private TextMeshProUGUI RightRedPoint, RightBluePoint, LeftRedPoint, LeftBluePoint;

    // Start is called before the first frame update
    void Start()
    {
        YouWin.enabled = false;
        YouLose.enabled = false;
        DrawRight.enabled = false;
        DrawLeft.enabled = false;
        ThanksLeft.enabled = false;
        ThanksRight.enabled = false;
        RightRedPoint.enabled = false;
        LeftRedPoint.enabled = false;  
        LeftBluePoint.enabled = false;
        RightBluePoint.enabled = false;
        StartCoroutine(ResultAnimation());
    }
    private IEnumerator ResultAnimation()
    {
        // 待機
        yield return new WaitForSeconds(3f);

        // 得点表示
        yield return StartCoroutine(ShowWinner());

        //終わり
        yield return StartCoroutine(Thanks());
    }
    private IEnumerator ShowWinner()
    {
        RectTransform YouWinForm = YouWin.GetComponent<RectTransform>();
        RectTransform YouLoseForm = YouLose.GetComponent<RectTransform>();

        LeftRedPoint.text = "" + MainGameManager.PointOfB + "";
        LeftRedPoint.color = new Color32(191, 7, 5, 255);
        LeftBluePoint.text = "" + MainGameManager.PointOfA + ":";
        LeftBluePoint.color = new Color32(0, 15, 191, 255);
        RightRedPoint.text = "" + MainGameManager.PointOfB + ":";
        RightRedPoint.color = new Color32(191, 7, 5, 255);
        RightBluePoint.text = "" + MainGameManager.PointOfA + "";
        RightBluePoint.color = new Color32(0, 15, 191, 255);

        if (MainGameManager.PointOfA != MainGameManager.PointOfB)
        {
            //LEFT(青)が勝ったら
            if (MainGameManager.PointOfA > MainGameManager.PointOfB)
            {
                YouWinForm.anchoredPosition = new Vector2(-200, -50);
                YouWinForm.rotation = Quaternion.Euler(0, 0, 270);
                YouWin.color = new Color32(0,15,191,255);

                YouLoseForm.anchoredPosition = new Vector2(200, 20);
                YouLoseForm.rotation = Quaternion.Euler(0, 0, 90);
                YouLose.color = new Color32(191, 7, 5, 255);

                
            }
            //Right(赤)が勝ったら
            else if (MainGameManager.PointOfA < MainGameManager.PointOfB)
            {
                YouWinForm.anchoredPosition = new Vector2(200, 50);
                YouWinForm.rotation = Quaternion.Euler(0, 0, 90);
                YouWin.color = new Color32(191, 7, 5, 255);

                YouLoseForm.anchoredPosition = new Vector2(-200, -20);
                YouLoseForm.rotation = Quaternion.Euler(0, 0, 270);
                YouLose.color = new Color32(0, 15, 191, 255);

            }
            GetComponent<AudioSource>().Play();
            for (int i = 0; i < 8; i++)
            {
                YouWin.enabled = !YouWin.enabled;
                YouLose.enabled = !YouLose.enabled;
                RightRedPoint.enabled = !YouWin.enabled; 
                RightBluePoint.enabled = !YouWin.enabled;
                LeftBluePoint.enabled = !YouWin.enabled;
                LeftRedPoint.enabled = !YouWin.enabled;
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            DrawLeft.color = new Color32(0, 15, 191, 255);
            DrawRight.color = new Color32(191, 7, 5, 255);

            //���_��������
            GetComponent<AudioSource>().Play();
            for (int i = 0; i < 8; i++)
            {
                DrawRight.enabled = !DrawRight.enabled;
                DrawLeft.enabled = !DrawLeft.enabled;
                RightRedPoint.enabled = !DrawLeft.enabled;
                RightBluePoint.enabled = !DrawLeft.enabled;
                LeftBluePoint.enabled = !DrawLeft.enabled;
                LeftRedPoint.enabled = !DrawLeft.enabled;
                yield return new WaitForSeconds(0.5f);
            }
        }
        RightRedPoint.enabled = false;
        LeftRedPoint.enabled = false;
        LeftBluePoint.enabled = false;
        RightBluePoint.enabled = false;
        yield break;    
    }

    private IEnumerator Thanks()
    {
        ThanksLeft.enabled = true;
        ThanksRight.enabled = true;
        yield break;
    }

    // Update is called once per frame

    void Update()
    {
        EndGame();
    }

    //ゲーム終了
    private void EndGame()
    {
        //Escが押された時
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
        }

    }
}
