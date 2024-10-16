using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterLine : MonoBehaviour
{
    Material material = null;

    [Header("�F�ύX�X�p��")]
    public float Chnge_Color_Time = 0.1f;

    [Header("�ύX�̊��炩��")]
    public float Smooth = 0.01f;

    [Header("�F��")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;// 0 ~ 1

    [Header("�ʓx")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;// 0 ~ 1

    [Header("���x")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;// 0 ~ 1

    [Header("�F�� MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;// 0 ~ 1

    [Header("�F�� MIN")]
    [Range(0, 1)] public float HSV_Hue_min = 0.0f;// 0 ~ 1

    public Material emissiveMaterial;
    public float intensity = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        HSV_Hue = HSV_Hue_min;
        StartCoroutine("Change_Color");
    }

    IEnumerator Change_Color()
    {
        HSV_Hue += Smooth;

        if (HSV_Hue >= HSV_Hue_max)
        {
            HSV_Hue = HSV_Hue_min;
        }
        material.EnableKeyword("_EMISSION");

        // HSV����RGB�ւ̕ϊ�
        Color activeColor = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness); // �ʓx�Ɩ��x�͍ő�ɐݒ�

        Color finalColor = activeColor * intensity;
        material.color = activeColor;
        //�����𑝕�
        float factor = Mathf.Pow(1.3f, intensity);

        material.SetColor("_EmissionColor", finalColor *factor );
        //emissiveMaterial.SetColor("_EmissionColor", finalColor);
        //emissiveMaterial.SetColor("_Color", activeColor);
        DynamicGI.SetEmissive(GetComponent<Renderer>(), finalColor);

        yield return new WaitForSeconds(Chnge_Color_Time);

        StartCoroutine("Change_Color");
    }
}
