using UnityEngine;

public class DynamicMaskExample : MonoBehaviour
{
    public Texture2D maskTexture; // 遮罩纹理
    private RenderTexture renderTexture; // 用于存储绘制结果的RenderTexture
    public string shaderFullName = "Your/Custom/Shader";
    void Start()
    {
        // 初始化RenderTexture，尺寸与遮罩纹理相同
        renderTexture = new RenderTexture(maskTexture.width, maskTexture.height, 24);
        renderTexture.Create();

        // 将RenderTexture应用到一个材质上，这个材质可以应用到任何需要遮罩的物体上
        Material maskMaterial = new Material(Shader.Find(shaderFullName));
        maskMaterial.SetTexture("_MaskTex", renderTexture);

        // 将材质应用到需要遮罩的物体上
        // 例如，假设有一个名为maskedObject的GameObject
        // maskedObject.GetComponent<Renderer>().material = maskMaterial;
    }

    void Update()
    {
        // 每次更新时重新绘制遮罩纹理到RenderTexture
        // 这里假设maskTexture是动态变化的遮罩纹理
        DrawMaskTextureToRenderTexture(maskTexture, renderTexture);
    }

    void DrawMaskTextureToRenderTexture(Texture sourceTexture, RenderTexture destination)
    {
        // 设置当前渲染目标为RenderTexture
        RenderTexture.active = destination;

        // 清空RenderTexture，设置为透明背景
        GL.Clear(true, true, Color.clear);

        // 绘制遮罩纹理到RenderTexture
        Graphics.Blit(sourceTexture, destination);

        // 重置渲染目标
        RenderTexture.active = null;
    }
}