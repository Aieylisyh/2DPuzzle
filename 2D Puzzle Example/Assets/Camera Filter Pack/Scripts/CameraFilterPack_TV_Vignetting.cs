///////////////////////////////////////////
//  CameraFilterPack - by VETASOFT 2020 ///
///////////////////////////////////////////

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Vignetting")]
public class CameraFilterPack_TV_Vignetting : MonoBehaviour
{
    #region Variables
    public Shader SCShader;
    private Material SCMaterial;
    private Texture2D Vignette;

    [Range(0, 1)] public float Vignetting = 1f;
    [Range(0, 1)] public float VignettingFull = 0f;
    [Range(0, 1)] public float VignettingDirt = 0f;
    public Color VignettingColor = new Color(0, 0, 0, 1);

    // ========== 新增：渐变时间参数 ==========
    [Header("渐变设置")]
    public float duration = 1f; 
    
    // 内部变量，用于追踪当前正在运行的渐变协程
    private Coroutine currentTween;
    // =====================================

    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (SCMaterial == null)
            {
                SCMaterial = new Material(SCShader);
                SCMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return SCMaterial;
        }
    }
    #endregion

    void Start()
    {
        SCShader = Shader.Find("CameraFilterPack/TV_Vignetting");
        Vignette = Resources.Load("CameraFilterPack_TV_Vignetting1") as Texture2D;

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (SCShader != null)
        {
            material.SetTexture("Vignette", Vignette);
            material.SetFloat("_Vignetting", Vignetting);
            material.SetFloat("_Vignetting2", VignettingFull);
            material.SetColor("_VignettingColor", VignettingColor);
            material.SetFloat("_VignettingDirt", VignettingDirt);

            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            SCShader = Shader.Find("CameraFilterPack/TV_Vignetting");
            Vignette = Resources.Load("CameraFilterPack_TV_Vignetting1") as Texture2D;
        }
#endif
    }

    void OnDisable()
    {
        if (SCMaterial)
        {
            DestroyImmediate(SCMaterial);
        }
    }

    // ========== 新增的 Public 方法与协程 ==========

    /// <summary>
    /// 逐渐增加/减少 Vignetting 的值
    /// </summary>
    /// <param name="v">要增加的值 (可以为负数来减少)</param>
    public void AddVignetting(float v)
    {
        // 确保必须在运行模式下才能调用协程
        if (!Application.isPlaying) return;

        // 计算目标值，并限制在 0 到 1 之间（对应原来 Range 的限制）
        float targetValue = Mathf.Clamp01(Vignetting + v);

        // 如果当前有正在进行的渐变协程，先停止它，防止动画冲突
        if (currentTween != null)
        {
            StopCoroutine(currentTween);
        }

        // 启动新的渐变协程
        currentTween = StartCoroutine(VignettingTween(targetValue, duration));
    }

    private IEnumerator VignettingTween(float targetValue, float tweenDuration)
    {
        float startValue = Vignetting;
        float elapsed = 0f;

        // 防止 duration 设置为 0 导致除以 0 的错误
        if (tweenDuration <= 0f)
        {
            Vignetting = targetValue;
            yield break;
        }

        while (elapsed < tweenDuration)
        {
            elapsed += Time.deltaTime;
            // 使用 Mathf.SmoothStep 让过渡的首尾更平滑自然，而不是生硬的匀速线性渐变
            float t = elapsed / tweenDuration;
            Vignetting = Mathf.SmoothStep(startValue, targetValue, t);
            
            yield return null; // 等待下一帧
        }

        // 确保最终值准确对齐
        Vignetting = targetValue;
        currentTween = null;
    }
    // ==============================================
}