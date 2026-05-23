using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class ClearRenderTexture : MonoBehaviour
{
    [Header("关联 RenderTexture，留空则使用 VideoPlayer.targetTexture")]
    public RenderTexture targetRT;

    [Header("视频未播放 / 已停止时显示的颜色")]
    public Color idleColor = new Color(0.12f, 0.12f, 0.14f, 1f);

    VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        ResolveTargetRT();

        if (videoPlayer != null)
            videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void Start()
    {
        ApplyIdleColor();
    }

    void OnEnable()
    {
        ApplyIdleColor();
    }

    void OnDisable()
    {
        ApplyIdleColor();
    }

    void OnVideoFinished(VideoPlayer source)
    {
        ApplyIdleColor();
    }

    public void OnVideoStopped()
    {
        ApplyIdleColor();
    }

    /// <summary>
    /// 将 RenderTexture 填充为 idleColor（视频未开始时的占位色）。
    /// </summary>
    public void ApplyIdleColor()
    {
        ResolveTargetRT();
        FillRenderTexture(targetRT, idleColor);
    }

    public void ClearRenderTextureToBlack()
    {
        ApplyIdleColor();
    }

    void ResolveTargetRT()
    {
        if (targetRT == null && videoPlayer != null)
            targetRT = videoPlayer.targetTexture;
    }

    public static void FillRenderTexture(RenderTexture rt, Color color)
    {
        if (rt == null)
            return;

        var previous = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(true, true, color);
        RenderTexture.active = previous;
    }
}
