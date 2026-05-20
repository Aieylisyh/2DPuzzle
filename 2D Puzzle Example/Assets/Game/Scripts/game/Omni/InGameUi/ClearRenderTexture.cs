using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class ClearRenderTexture : MonoBehaviour
{
    [Header("关联你的 RenderTexture")]
    public RenderTexture targetRT;

    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        targetRT = videoPlayer.targetTexture;
    }

    /// <summary>
    /// 清空 RenderTexture 为纯黑色
    /// </summary>
    public void ClearRenderTextureToBlack()
    {
        if (targetRT == null)
        {
            Debug.LogWarning("未指定 RenderTexture");
            return;
        }

        // 保存当前渲染目标
        RenderTexture currentRT = RenderTexture.active;

        // 设置当前渲染目标为你的 RT
        RenderTexture.active = targetRT;

        // 清空为黑色
        GL.Clear(true, true, Color.black);

        // 恢复之前的渲染目标
        RenderTexture.active = currentRT;
    }

    // 👇 你可以在这些时机调用清空（根据需求选）
    void Start()
    {
        // 游戏启动时清空
        ClearRenderTextureToBlack();
    }

    void OnDisable()
    {
        // 物体禁用时清空
        ClearRenderTextureToBlack();
    }

    /// <summary>
    /// 视频停止时自动清空（推荐）
    /// </summary>
    public void OnVideoStopped()
    {
        ClearRenderTextureToBlack();
    }
}