using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoalEffect : MonoBehaviour
{
    //赤色エフェクト
    public GameObject RedCircle;
    //青色エフェクト
    public GameObject BlueCircle;

    //エフェクトの間の時間
    private float IntervalTime;
    //大きくする速度
    private float ScaleSpeed;

    //最大サイズ
    private Vector3 MaxScale = new Vector3(4f, 4f, 4f);

    //goalテキスト
    public TextMeshProUGUI GoalText;
    private string displayText = "GOAL"; //表示するテキスト
    private float boundHeight = 50f;
    private float boundTime = 0.5f;
    private float bounceDuration = 0.5f;
    private float characterDelay = 0.2f;
    private float elapseTime = 0;

    private Func<float, float> CalcFunc = null;

    //ballの座標を取得する
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        IntervalTime = 0.2f;
        ScaleSpeed = 1.5f;

        CalcFunc = GetBoundFunc(boundHeight, boundTime);
        elapseTime = 0.0f;

        // GoalTextの設定
        if (GoalText != null)
        {
            RectTransform RectGoalText = GoalText.GetComponent<RectTransform>();
            RectGoalText.anchoredPosition = new Vector3(0f, 0f, 0f);
            RectGoalText.sizeDelta = new Vector2(700,300);
            GoalText.fontSize = 200;
            GoalText.text = "GOAL";
            GoalText.color = new Color(0.92f, 0.843f, 0f, 1f);
            GoalText.outlineColor = Color.white;
            GoalText.outlineWidth = 0.2f;
            GoalText.enabled = false;
        }
        else
        {
            Debug.LogError("GoalTextオブジェクトが指定されていません");
        }

    }

    // Update is called once per frame
    public void MakeGoalEffect(int x)
    {
        Debug.Log("GoalEffect");
        Vector3 targetPosition = targetObject.transform.position;
        StartCoroutine(DisplayBouncingText());
        StartCoroutine(MakeCircle(x,targetPosition));
    }

    //波紋の元のなる円を作成するコード
    private IEnumerator MakeCircle(int x,Vector3 GoalPositon)
    {
        for(int j=0; j<3; j++){
            if(x == 1){
                GameObject instance = Instantiate(RedCircle, GoalPositon, Quaternion.identity);
                StartCoroutine(ScaleUP(instance));
            }else{
                GameObject instance = Instantiate(BlueCircle, GoalPositon, Quaternion.identity);
                StartCoroutine(ScaleUP(instance));
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    //波紋を広げるコード
    private IEnumerator ScaleUP(GameObject GoalCircle)
    {
        for(int i= 0; i<300; i++){
            if(GoalCircle.transform.localScale.x < MaxScale.x 
                && GoalCircle.transform.localScale.y < MaxScale.y 
                && GoalCircle.transform.localScale.z < MaxScale.z){//指定の大きさより小さいかを判別
                    GoalCircle.transform.localScale += Vector3.one * ScaleSpeed * Time.deltaTime;//円を大きくする
            }else {
                break;//指定の大きさより大きくなったら消す
            }
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(GoalCircle);
    }


    //GOALテキストを一文字ずつ表示する
    IEnumerator DisplayBouncingText()
    {
        
        GoalText.text = "";  // 初期化
        GoalText.enabled = true;

        for (int i = 0; i < displayText.Length; i++)
        {
            GoalText.text += displayText[i];  // 次の文字を追加
            //StartCoroutine(BounceCharacter(i));  // バウンドアニメーションを開始
            yield return new WaitForSeconds(characterDelay);  // 次の文字の前に遅延
        }
        Animation(elapseTime, boundTime);
        yield return new WaitForSeconds(3f);
        GoalText.enabled = false;
    }

    //テキストを一文字ずつバウンドさせる
    /*
    IEnumerator BounceCharacter(int index)
    {
         Vector3 originalPosition = GoalText.transform.localPosition;
        Vector3 targetPosition = originalPosition + new Vector3(0, bounceHeight, 0);

        // バウンドアニメーション
        float elapsedTime = 0f;
        while (elapsedTime < bounceDuration)
        {
            // 簡単なバウンドエフェクト
            float t = elapsedTime / bounceDuration;
            float height = Mathf.Sin(t * Mathf.PI); // サイン波でバウンドを表現
            GoalText.transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, height);

            elapsedTime += Time.deltaTime;
            yield return null; // フレーム毎に待機
        }

        // 元の位置に戻す
        GoalText.transform.localPosition = originalPosition;
    }
    */

    private void Animation(float time, float maxTime)
    {
        GoalText.ForceMeshUpdate(true);
        TMP_TextInfo textInfo = GoalText.textInfo;

        for (int i = 0; i < textInfo.characterInfo.Length; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int mIndex = charInfo.materialReferenceIndex;
            int vIndex = charInfo.vertexIndex;

            Vector3[] vectors = textInfo.meshInfo[mIndex].vertices;

            float height = CalcFunc(time - i * boundTime * 0.5f);
            vectors[vIndex + 0].y += height;
            vectors[vIndex + 1].y += height;
            vectors[vIndex + 2].y += height;
            vectors[vIndex + 3].y += height;
        }

        for (int i = 0; i < textInfo.materialCount; i++)
        {
            if (textInfo.meshInfo[i].mesh == null) continue;

            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            GoalText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

    private Func<float, float> GetBoundFunc(float maxHeight, float maxTime)
    {
        float v0 = 4 * maxHeight / maxTime;
        float g = 2 * v0 / maxTime;

        return GetBoundFuncFromInitSpeed(g, v0);
    }

    private Func<float, float> GetBoundFuncFromInitSpeed(float g, float v0)
    {
        return (float t) =>
        {
            float h = v0 * t - (g * Mathf.Pow(t, 2.0f) * 0.5f);
            return h < 0 ? 0 : h;
        };
    }


}
