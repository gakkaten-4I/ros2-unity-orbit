 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //�G�i�W�[�ʂ̃Q�[���I�u�W�F�N�g
    private GameObject EnergyCan;
    private GameObject[]  RedItems, BlueItems;
    
    // Start is called before the first frame update
    void Start()
    {
        RedItems = new GameObject[4];
        Application.targetFrameRate = 60;//�t���[�����[�g��60�ɌŒ�
        EnergyCan = GameObject.Find("EnergyCan");
        EnergyCan.SetActive(false);
        RedItems[0]= GameObject.Find("RedItem1");
        Debug.Log("success");
    }
    private IEnumerator Energy()
    {
        // 210�b�ԑ҂�
        yield return new WaitForSecondsRealtime(2);
        EnergyAppeare();
    }
    void EnergyAppeare()
    {
        EnergyCan.SetActive(true);
    }
    void EnergyAppeareRed()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �R���[�`���̋N��
        StartCoroutine(Energy());
    }
}
