using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public GameObject targetObject;   // アニメーションが再生されるオブジェクト
    public float DestroyTime; // アニメーションを持つオブジェクトの Animator

    void Start()
    {
        StartCoroutine(DestroyAfterSeconds(DestroyTime));
    }

    void Update()
    {

    }




    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds); // 指定された秒数待機
        if (targetObject != null)
        {
            Destroy(this.gameObject); // ターゲットオブジェクトを破壊
        }
        else
        {
            Debug.LogWarning("TargetObjectが設定されていません。");
        }
    }
}
