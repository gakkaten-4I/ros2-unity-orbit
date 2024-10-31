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

    // Start is called before the first frame update
    void Start()
    {
        RectTransform YouWinForm = YouWin.GetComponent<RectTransform>();
        RectTransform YouLoseForm = YouLose.GetComponent<RectTransform>();
        RectTransform DrawRightForm = DrawRight.GetComponent<RectTransform>();
        RectTransform DrawLeftForm = DrawLeft.GetComponent<RectTransform>();

        YouWin.enabled = false;
        YouLose.enabled = false;
        DrawRight.enabled = false;
        DrawLeft.enabled = false;
        ThanksLeft.enabled = false;
        ThanksRight.enabled = false;
        StartCoroutine(ResultAnimation());
    }
    private IEnumerator ResultAnimation()
    {
        // �ҋ@
        yield return new WaitForSeconds(3f);

        // ���_�\��
        yield return StartCoroutine(ShowWinner());

        //�I���
        yield return StartCoroutine(Thanks());
    }
    private IEnumerator ShowWinner()
    {
        if(MainGameManager.PointOfA != MainGameManager.PointOfB)
        {
            //LEFT(��)����������
            if (MainGameManager.PointOfA > MainGameManager.PointOfB)
            {
                YouWinForm.anchoredPosition = new Vector2(-350, -200);
                YouWinForm.rotation = Quaternion.Euler(0, 0, 270);
                YouWin.color = new Color32(0,15,191,255);

                YouLoseForm.anchoredPosition = new Vector2(350, 200);
                YouLoseForm.rotation = Quaternion.Euler(0, 0, 90);
                YouLose.color = new Color32(191, 7, 5, 255);
            }
            //Right(��)����������
            else if (MainGameManager.PointOfA < MainGameManager.PointOfB)
            {
                YouWinForm.anchoredPosition = new Vector2(350, 200);
                YouWinForm.rotation = Quaternion.Euler(0, 0, 90);
                YouWin.color = new Color32(191, 7, 5, 255);

                YouLoseForm.anchoredPosition = new Vector2(-350, -200);
                YouLoseForm.rotation = Quaternion.Euler(0, 0, 270);
                YouLose.color = new Color32(0, 15, 191, 255);
            }
            for (int i = 0; i < 8; i++)
            {
                YouWin.enabled = !YouWin.enabled;
                YouLose.enabled = !YouLose.enabled;
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            DrawLeft.color = new Color32(0, 15, 191, 255);
            DrawRight.color = new Color32(191, 7, 5, 255);
            //���_��������
            for (int i = 0; i < 8; i++)
            {
                DrawRight.enabled = !DrawRight.enabled;
                DrawLeft.enabled = !DrawLeft.enabled;
                yield return new WaitForSeconds(0.5f);
            }
        }
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

    //�Q�[���I��
    private void EndGame()
    {
        //Esc�������ꂽ��
        if (Input.GetKey(KeyCode.Escape))
        {
        //if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        //else
            //Application.Quit();//�Q�[���v���C�I��
        }

    }
}
