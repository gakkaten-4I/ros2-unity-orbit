using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//文字列を揺らすプログラム
/*[ExecuteInEditMode, RequireComponent(typeof(TMP_Text))]
public class TMP_wave : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float speed;
    [SerializeField] private int length;

    private TMP_Text tmpText;
    private TMP_TextInfo tmpInfo;

    private void Start()
    {
        tmpText = this.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        tmpText.ForceMeshUpdate(true);
        tmpInfo = tmpText.textInfo;

        var count = Mathf.Min(tmpInfo.characterCount, tmpInfo.characterInfo.Length);
        for (int i = 0; i < count; i++)
        {
            var charInfo = tmpInfo.characterInfo[i];
            if (!charInfo.isVisible)
                continue;

            int matIndex = charInfo.materialReferenceIndex;
            int vertIndex = charInfo.vertexIndex;

            Vector3[] verts = tmpInfo.meshInfo[matIndex].vertices;

            float ofs = 0.5f * i;
            float sinWave = Mathf.Sin((ofs + Time.realtimeSinceStartup * Mathf.PI * speed) / length) * amp;
            verts[vertIndex + 0].y += sinWave;
            verts[vertIndex + 1].y += sinWave;
            verts[vertIndex + 2].y += sinWave;
            verts[vertIndex + 3].y += sinWave;
        }

        for (int i = 0; i < tmpInfo.materialCount; i++)
        {
            if (tmpInfo.meshInfo[i].mesh == null) { continue; }

            tmpInfo.meshInfo[i].mesh.vertices = tmpInfo.meshInfo[i].vertices;
            tmpText.UpdateGeometry(tmpInfo.meshInfo[i].mesh, i);
        }
    }
}
*/

public class coinManager : MonoBehaviour
{
    //textオブジェクト
    public TMPro.TextMeshProUGUI scoreLabel;
    private int scoreNum;
    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 0;
        scoreLabel.text = "";
    }
    //コインのポイントを加算させる
    void UpPoint()
    {
        scoreNum += 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = ""+scoreNum;
    }
}