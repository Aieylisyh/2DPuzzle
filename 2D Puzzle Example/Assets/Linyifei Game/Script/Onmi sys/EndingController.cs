using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 定义一个结构体，把图片和它的停留时间打包在一起
[System.Serializable]
public struct EndingFrame
{
    [Tooltip("要显示的图片")]
    public Sprite sprite;
    [Tooltip("这张图片在屏幕上停留的总时间（秒）")]
    public float duration;
}

public class EndingController : MonoBehaviour
{
    [Header("UI 组件引用")]
    public CanvasGroup panelCanvasGroup;
    [Tooltip("主要的 Image (默认显示图片的那个)")]
    public Image endingImage;
    // ▼▼▼ 新增：辅助 Image 引用槽位 ▼▼▼
    [Tooltip("辅助处理 crossfade 的 Image")]
    public Image crossfadeAuxImage;

    [Header("动画时间设置")]
    [Tooltip("Panel 整体淡入浮现所需的时间（秒）")]
    public float panelFadeInDuration = 2.0f;

    // ▼▼▼ 新增：交叉淡入淡出的过渡时间 ▼▼▼
    [Tooltip("交叉淡入淡出（Crossfade）的过渡时间（秒）")]
    public float crossfadeDuration = 0.5f;

    [Header("第一张图效果")]
    [Tooltip("第一张图从纯黑变回原色的时间（秒）")]
    public float firstImageTintDuration = 1.5f;

    [Header("结局画面序列")]
    [Tooltip("在这里依次添加结局的图片，并为每张图设置独立的持续时间")]
    public EndingFrame[] endingFrames;

    public RectTransform endingButton;
    public FirstPersonController fpc;

    [Header("多结局的好结局")]
    public bool isBeOrHe;

    //  [Header("玩家控制器引用")]
    //   [Tooltip("拖入玩家的 FirstPersonController")]
    //   public FirstPersonController fpc;

    [Header("🎥 运镜节点 (拖入 Hierarchy 中的 4 个点)")]
    public Transform standPoint;       // 站立准备点
    public Transform seatPoint;        // 最终坐姿点
    public Transform swivelPoint;      // 转椅转完点
    public Transform lookTargetPoint;  // 屏幕注视点

    [Header("⏱️ 运镜时间设置 (秒)")]
    public float moveToStandDuration = 1.0f;  // 走到椅子前的时间
    public float sitDownDuration = 1.2f;      // 坐下的时间
    public float swivelDuration = 1.0f;       // 椅子旋转的时间
    public float focusScreenDuration = 1.5f;  // 凑近聚焦屏幕的时间

    [Header("⏱️ 动作停顿间隙 (秒)")]
    public float delayAfterStand = 0.2f;      // 站定后停顿
    public float delayAfterSit = 0.3f;        // 刚坐下后感受重力的停顿
    public float delayAfterSwivel = 0.5f;     // 转过来看到屏幕后的停顿

    [Header("曲线设置")]
    [Tooltip("建议使用 EaseInOut 曲线，让每次移动都有自然的加速和减速")]
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    // 内部防误触标记
    private bool isCinematicPlaying = false;

    private void Start()
    {
        endingButton.gameObject.SetActive(false);
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 0f;
            panelCanvasGroup.interactable = false;
            panelCanvasGroup.blocksRaycasts = false;
        }

        // ▼▼▼ 初始化：确保辅助 Image 此时是不可见的，以免遮挡 ▼▼▼
        if (crossfadeAuxImage != null)
        {
            // 将辅助 Image 设为完全透明，并且不拦截点击（防止播完动画后点不到按钮）
            SetImageAlpha(crossfadeAuxImage, 0f);
            crossfadeAuxImage.raycastTarget = false;
        }
    }

    public void StartEndingSequence()
    {
        if (panelCanvasGroup == null || endingImage == null || crossfadeAuxImage == null)
        {
            Debug.LogError("EndingController: 缺少 CanvasGroup 或 Image 组件引用！请确保默认 Image 和辅助 Image 都连接了引用。");
            return;
        }

        if (endingFrames == null || endingFrames.Length == 0)
        {
            Debug.LogWarning("EndingController: 你的画面序列是空的！");
            return;
        }

        if (isBeOrHe)
            StartCoroutine(EndingRoutineBe());
        else
            PlayEndingCinematic();
    }

    /// <summary>
    /// 供你的 Trigger 脚本调用的公开方法
    /// </summary>
    public void PlayEndingCinematic()
    {
        if (isCinematicPlaying) return;

        if (fpc == null)
        {
            Debug.LogError("ChairEndingCinematic: 未绑定 FirstPersonController！");
            return;
        }

        StartCoroutine(CinematicSequence());
    }

    private IEnumerator CinematicSequence()
    {
        isCinematicPlaying = true;

        // 1. 劫持玩家控制器
        fpc.playerCanMove = false;
        fpc.cameraCanMove = false;

        // 提取摄像机对象进行世界坐标的绝对移动
        Camera cam = fpc.playerCamera;

        // ==========================================
        // 阶段 1：走到站立准备点 (Stand Point)
        // ==========================================
        yield return StartCoroutine(MoveCameraRoutine(cam.transform, standPoint, moveToStandDuration));
        if (delayAfterStand > 0) yield return new WaitForSeconds(delayAfterStand);

        // ==========================================
        // 阶段 2：坐下，移动到最终坐姿点 (Seat Point)
        // ==========================================
        yield return StartCoroutine(MoveCameraRoutine(cam.transform, seatPoint, sitDownDuration));
        if (delayAfterSit > 0) yield return new WaitForSeconds(delayAfterSit);

        // ==========================================
        // 阶段 3：转动椅子，移动到转椅转完点 (Swivel Point)
        // ==========================================
        yield return StartCoroutine(MoveCameraRoutine(cam.transform, swivelPoint, swivelDuration));
        if (delayAfterSwivel > 0) yield return new WaitForSeconds(delayAfterSwivel);

        // ==========================================
        // 阶段 4：凑近屏幕，移动到屏幕注视点 (Look Target)
        // ==========================================
        yield return StartCoroutine(MoveCameraRoutine(cam.transform, lookTargetPoint, focusScreenDuration));

        // ==========================================
        // 运镜结束
        // ==========================================
        Debug.Log("椅子运镜结束！你可以在这里调用 EndingController 播放 UI 结局动画了。");

        // 示例：如果你在同一个物体上挂了上一个回答里的 EndingController
        // EndingController endingUI = GetComponent<EndingController>();
        // if(endingUI != null) endingUI.StartEndingSequence();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 通用摄像机平滑移动协程
    /// </summary>
    private IEnumerator MoveCameraRoutine(Transform cameraTransform, Transform targetTransform, float duration)
    {
        if (targetTransform == null) yield break;

        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        float elapsed = 0f;

        // 防止 duration 为 0 导致除以 0
        if (duration <= 0)
        {
            cameraTransform.position = targetTransform.position;
            cameraTransform.rotation = targetTransform.rotation;
            yield break;
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // 通过 AnimationCurve 获取平滑后的进度 t
            float t = transitionCurve.Evaluate(elapsed / duration);

            cameraTransform.position = Vector3.Lerp(startPos, targetTransform.position, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, targetTransform.rotation, t);

            yield return null;
        }

        // 确保最终精确对齐目标点
        cameraTransform.position = targetTransform.position;
        cameraTransform.rotation = targetTransform.rotation;
    }
    private IEnumerator EndingRoutineBe()
    {
        // 1. 拦截点击
        panelCanvasGroup.blocksRaycasts = true;
        panelCanvasGroup.interactable = true;
        fpc.ToggleCrosshair(false);
        // ▼▼▼ 逻辑优化 ▼▼▼
        // 声明 FadeOut (旧) Image 和 FadeIn (新) Image 的引用，通过交换引用来实现交叉淡入淡出。
        Image activeImage = endingImage;
        Image inactiveImage = crossfadeAuxImage;

        // 确保 activeImage 颜色为黑色，为第一张图的褪色做准备
        activeImage.color = Color.black;
        // 确保 activeImage 是可见的，而 inactiveImage 是不可见的
        SetImageAlpha(activeImage, 1f);
        SetImageAlpha(inactiveImage, 0f);

        // 2. Panel 整体透明度淡入
        float elapsedTime = 0f;
        while (elapsedTime < panelFadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / panelFadeInDuration);
            yield return null;
        }
        panelCanvasGroup.alpha = 1f;

        // 3. 依次播放每一帧画面
        for (int i = 0; i < endingFrames.Length; i++)
        {
            // 如果不是第一张图，执行 crossfade 过渡
            if (i > 0)
            {
                // 将新图 Sprite 赋值给 inactive Image，准备淡入
                inactiveImage.sprite = endingFrames[i].sprite;
                inactiveImage.color = Color.white;
                activeImage.color = Color.white;

                // 【关键修复 1】：将新图片置于 UI 层级的最前方（覆盖在旧图片之上）
                inactiveImage.transform.SetAsLastSibling();

                // 【关键修复 2】：旧图片（底图）在过渡期间保持 100% 不透明度，防止画面发虚！
                SetImageAlpha(activeImage, 1f);

                float actualCrossfadeDuration = Mathf.Min(crossfadeDuration, endingFrames[i].duration);

                float fadeTime = 0f;
                while (fadeTime < actualCrossfadeDuration)
                {
                    fadeTime += Time.deltaTime;
                    float t = fadeTime / actualCrossfadeDuration;

                    // 只有新图片（顶图）在进行透明度变化，逐渐盖住底图
                    SetImageAlpha(inactiveImage, Mathf.Lerp(0f, 1f, t));

                    yield return null;
                }

                // 过渡完成，此时新图已经 100% 不透明完全盖住了旧图。
                // 这时再把藏在下面的旧图彻底隐藏，节约性能。
                SetImageAlpha(activeImage, 0f);
                SetImageAlpha(inactiveImage, 1f);

                // 交换引用。现在的 inactiveImage 变成了下一次的 FadeOut 目标
                Image temp = activeImage;
                activeImage = inactiveImage;
                inactiveImage = temp;

                // 等待指定的持续时间
                yield return new WaitForSeconds(endingFrames[i].duration);
            }
            // 如果是第一张图，执行你的 Panel 淡入 + Tint 第一张图从黑变白的逻辑
            else
            {
                // 设置图片
                activeImage.sprite = endingFrames[i].sprite;

                float tintTime = 0f;
                // 防止配置的变色时间比图片总停留时间还长
                float actualTintDuration = Mathf.Min(firstImageTintDuration, endingFrames[i].duration);

                while (tintTime < actualTintDuration)
                {
                    tintTime += Time.deltaTime;
                    // Color.Lerp 从黑色平滑过渡到白色（白色即原图颜色）
                    activeImage.color = Color.Lerp(Color.black, Color.white, tintTime / actualTintDuration);
                    yield return null;
                }
                activeImage.color = Color.white;

                // 等待第一张图剩余的时间
                float remainingTime = endingFrames[i].duration - actualTintDuration;
                if (remainingTime > 0)
                {
                    yield return new WaitForSeconds(remainingTime);
                }
            }
        }
        Cursor.lockState = CursorLockMode.None;
        // --- 序列播放完毕 ---
        Debug.Log("结局序列播放完成！");
        yield return new WaitForSeconds(1);
        var ap = endingButton.anchoredPosition;
        endingButton.anchoredPosition += new Vector2(0, 300);
        endingButton.gameObject.SetActive(true);
        endingButton.DOAnchorPos(ap, 2);
    }

    /// <summary>
    /// 设置 Image 的 Alpha 值（不透明度）的快捷方法
    /// </summary>
    /// <param name="img">Image组件</param>
    /// <param name="alpha">Alpha值 (0f - 1f)</param>
    private void SetImageAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}