using UnityEngine;

public class SecurityCameraTracker : MonoBehaviour
{
    [Header("Tracking Settings")]
    [Tooltip("要追踪的目标（通常是玩家）")]
    public Transform target;
    
    [Tooltip("摄像头的最大旋转速度（度/秒）")]
    public float rotationSpeed = 60f;

    void Update()
    {
        // 如果没有指定目标，就不执行任何操作
        if (target == null) return;

        // 1. 计算从摄像头指向目标的向量
        Vector3 directionToTarget = target.position - transform.position;

        // 2. 忽略Y轴高度差，确保摄像头只在水平方向（Y轴）旋转，不会上下抬头/低头
        directionToTarget.y = 0;

        // 确保向量有长度，避免目标和摄像头在X/Z轴完全重合时产生报错
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            // 3. 计算出目标朝向的旋转值
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // 4. 使用 RotateTowards 平滑旋转，限制最大旋转速度
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}