using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 通过三阶贝塞尔曲线绘制选择箭头 - 支持外部控制版本
/// </summary>
public class DynamicArrowSystem : MonoBehaviour
{
    [Header("箭头预制体")]
    public RectTransform arrowHeadPrefab;
    public RectTransform arrowNodePrefab;

    [Header("箭头配置")]
    public int arrowNodeDist = 100;
    public float refControlPoint1Height = 1.0f;
    [Header("贝塞尔曲线参数")]
    [Range(0f, 1f)]
    public float controlPoint1LengthRatio = 0.6f; // P1为P0上方的点，P0到P1占P0到P3的y轴高度差的比例
    [Range(0f, 1f)]
    public float controlPoint2LengthRatio = 0.5f; // P2为P3左右侧的点，P2到P3占P0到P3的x轴横向长度差的比例

    [Header("外部控制设置")]
    [SerializeField] private bool useExternalControl = false; // 是否使用外部控制
    [SerializeField] private Vector2 externalStartPosition = Vector2.zero; // 外部设置的起点
    [SerializeField] private Vector2 externalEndPosition = Vector2.zero; // 外部设置的终点

    // 内部变量
    private RectTransform origin;
    private List<RectTransform> arrowNodes = new List<RectTransform>();
    private List<Vector2> controlPoints = new List<Vector2>();
    private Canvas parentCanvas;

    // 外部控制状态
    private bool isExternallyControlled = false;

    private void Awake()
    {
        origin = this.GetComponent<RectTransform>();

        // 获取Canvas
        parentCanvas = GetComponentInParent<Canvas>();

        var arrowNodeNum = 10;
        // 创建节点
        for (int i = 0; i < arrowNodeNum; ++i)
        {
            var node = Instantiate(arrowNodePrefab, arrowNodePrefab.parent);
            arrowNodes.Add(node);
            //node.gameObject.SetActive(true);
        }

        // 创建箭头头部
        var head = Instantiate(arrowHeadPrefab, arrowHeadPrefab.parent);
        arrowNodes.Add(head);
        head.gameObject.SetActive(true);

        // 初始化控制点
        for (int i = 0; i < 4; ++i)
            controlPoints.Add(Vector2.zero);
    }

    private void Update()
    {
        if (parentCanvas == null || arrowNodes.Count == 0) return;

        // 如果外部控制，使用外部设置的位置
        if (isExternallyControlled)
        {
            UpdateWithExternalControl();
        }
        else
        {
            UpdateWithMouseControl();
        }
    }

    /// <summary>
    /// 使用鼠标控制的更新（原有逻辑）
    /// </summary>
    private void UpdateWithMouseControl()
    {
        // 获取鼠标位置
        Vector2 parentLocal;
        bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            origin.parent as RectTransform,
            Input.mousePosition,
            null,
            out parentLocal);

        if (!success) return;

        // 计算控制点
        CalculateControlPoints(Vector2.zero, parentLocal);

        // 更新节点
        UpdateArrowNodes();
    }

    /// <summary>
    /// 使用外部控制的更新
    /// </summary>
    private void UpdateWithExternalControl()
    {
        // 计算控制点
        CalculateControlPoints(externalStartPosition, externalEndPosition);

        // 更新节点
        UpdateArrowNodes();
    }

    /// <summary>
    /// 计算控制点
    /// </summary>
    private void CalculateControlPoints(Vector2 startPosition, Vector2 targetPosition)
    {
        controlPoints[0] = startPosition; // P0: 起点
        controlPoints[3] = targetPosition; // P3: 终点

        Vector2 direction = targetPosition - startPosition;
        float distance = direction.magnitude;
        float noCurveDist = 0.1f;

        if (distance <= noCurveDist)
        {
            // 距离太小时，退化为直线
            controlPoints[1] = Vector2.Lerp(controlPoints[0], controlPoints[3], 0.4f);
            controlPoints[2] = Vector2.Lerp(controlPoints[0], controlPoints[3], 0.6f);
        }
        else
        {
            // 计算垂直向量
            //Vector2 perpendicular = new Vector2(-direction.y, direction.x).normalized;
            var lenY = Mathf.Abs(controlPoints[3].y - controlPoints[0].y);
            var lenXSigned = controlPoints[3].x - controlPoints[0].x;

            // 计算控制点位置
            controlPoints[1] = controlPoints[0] + Vector2.up * lenY * controlPoint1LengthRatio;
            var y1 = (refControlPoint1Height + controlPoints[1].y) * 0.5f;
            controlPoints[1] = new Vector2(controlPoints[1].x, y1);
            controlPoints[2] = controlPoints[3] + Vector2.left * lenXSigned * controlPoint2LengthRatio;
            var y2 = (controlPoints[1].y + controlPoints[2].y) * 0.5f;
            controlPoints[2] = new Vector2(controlPoints[2].x, y2);
        }
    }

    /// <summary>
    /// 更新箭头节点 - 改为公共方法
    /// </summary>
    public void UpdateArrowNodes()
    {
        for (int i = 0; i < arrowNodes.Count; ++i)
        {
            var node = arrowNodes[i];
            node.gameObject.SetActive(true);
            // 计算t值
            float t = CalculateTValue(i);

            // 计算贝塞尔曲线位置
            Vector2 pos = CalculateBezierPoint(t);

            // 验证计算结果
            if (float.IsNaN(pos.x) || float.IsNaN(pos.y))
            {
                pos = Vector2.Lerp(controlPoints[0], controlPoints[3], t);
            }

            // 设置位置
            node.anchoredPosition = pos;

            // 设置旋转
            if (i > 0)
            {
                Vector2 dir = arrowNodes[i].anchoredPosition - arrowNodes[i - 1].anchoredPosition;
                if (dir.magnitude > 0.001f)
                {
                    float angle = Vector2.SignedAngle(Vector2.up, dir);
                    arrowNodes[i].rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }

        // 设置第一个节点旋转
        if (arrowNodes.Count >= 2)
        {
            arrowNodes[0].rotation = arrowNodes[1].rotation;
        }
    }

    private float CalculateTValue(int index)
    {
        if (index == 0)
        {
            // 第一个节点始终在起点
            return 0f;
        }

        return (float)index / (arrowNodes.Count - 1);
    }

    private Vector2 CalculateBezierPoint(float t)
    {
        float oneMinusT = 1f - t;
        return Mathf.Pow(oneMinusT, 3) * controlPoints[0] +
               3 * Mathf.Pow(oneMinusT, 2) * t * controlPoints[1] +
               3 * oneMinusT * Mathf.Pow(t, 2) * controlPoints[2] +
               Mathf.Pow(t, 3) * controlPoints[3];
    }

    private float CalculateScale(int index)
    {
        float progress = (float)index / Mathf.Max(1, arrowNodes.Count - 1);
        //return this.scaleFactor * Mathf.Lerp(minScale, maxScale, progress);
        return 1;
    }

    #region 公共接口 - 供外部控制使用

    /// <summary>
    /// 启用外部控制模式
    /// </summary>
    public void EnableExternalControl(Vector2 startPosition, Vector2 endPosition)
    {
        isExternallyControlled = true;
        externalStartPosition = startPosition;
        externalEndPosition = endPosition;

        // 立即更新一次
        UpdateWithExternalControl();
    }

    /// <summary>
    /// 禁用外部控制，返回鼠标控制模式
    /// </summary>
    public void DisableExternalControl()
    {
        isExternallyControlled = false;
    }

    /// <summary>
    /// 设置起点位置（外部控制模式下）
    /// </summary>
    public void SetStartPosition(Vector2 startPosition)
    {
        externalStartPosition = startPosition;

        // 如果正在外部控制模式，立即更新
        if (isExternallyControlled)
        {
            UpdateWithExternalControl();
        }
    }

    /// <summary>
    /// 设置终点位置（外部控制模式下）
    /// </summary>
    public void SetEndPosition(Vector2 endPosition)
    {
        externalEndPosition = endPosition;

        // 如果正在外部控制模式，立即更新
        if (isExternallyControlled)
        {
            UpdateWithExternalControl();
        }
    }

    #endregion
}