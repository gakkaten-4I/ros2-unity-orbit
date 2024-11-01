using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamingWall : MonoBehaviour
{
    public float duration = 3.0f; // ���F�Ɍ��点�鎞��
    private Material material, Tmaterial;
    [SerializeField] GameObject MainGameManagerobj;
    MainGameManager MainGameManager;
    bool c;

    public float Chnge_Color_Time = 2f;

    public float Smooth = 0.01f;

    public float HSV_Hue = 1.0f;// 0 ~ 1

    public float HSV_Hue_max = 1.0f;// 0 ~ 1

    public float HSV_Hue_min = 0.0f;// 0 ~ 1

    void Start()
    {
        Chnge_Color_Time = 2f;

        MainGameManager = MainGameManagerobj.GetComponent<MainGameManager>();
        // �I�u�W�F�N�g��Renderer���擾
        material = GetComponent<Renderer>().material;

        c = MainGameManager.IsFever;
        //c = true;
        if (c)
        {
            StartCoroutine(Gaming());
            Debug.Log("success");
        }
    }
    private IEnumerator Gaming()
    {
        StartCoroutine(RainbowCoroutine());

        yield return new WaitForSeconds(15);

        // �I�����Ɍ��̐F�ɖ߂�
        UndoGaming();
    }

    private IEnumerator RainbowCoroutine()
    {
        HSV_Hue += Smooth;

        if (HSV_Hue >= HSV_Hue_max)
        {
            HSV_Hue = HSV_Hue_min;
        }
        Color rainbowColor = Color.HSVToRGB(HSV_Hue, 1f, 1f); // S��V��1�ŌŒ�

        // �擾�����I�u�W�F�N�g�̐F��ύX
        material.SetColor("_EmissionColor",rainbowColor);
        Debug.Log(Tmaterial == material);

        yield return new WaitForSeconds(Chnge_Color_Time);
    }
    private void UndoGaming()
    {
        Debug.Log("UndoGaming");
        material=Tmaterial;
        return;
    }
    void Update()
    {
        c = MainGameManager.IsFever;
        //Debug.Log(MainGameManager.IsFever);
        //c = true;
        if (c)
        {
            StartCoroutine(Gaming());
            Debug.Log("success");
        }
    }
}

