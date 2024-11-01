using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OutlineEffect : MonoBehaviour
{
    public Color outlineColor = Color.black;
    public float outlineWidth = 0.03f;

    private Material outlineMaterial;

    void Start()
    {
        // シェーダーを適用したマテリアルを生成
        outlineMaterial = new Material(Shader.Find("Custom/OutlineShader"));
        outlineMaterial.SetColor("_OutlineColor", outlineColor);
        outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);

        // オブジェクトのマテリアルを変更
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = outlineMaterial;
    }

    // プロパティを更新する場合のメソッド
    public void UpdateOutlineProperties(Color color, float width)
    {
        outlineMaterial.SetColor("_OutlineColor", color);
        outlineMaterial.SetFloat("_OutlineWidth", width);
    }
}
