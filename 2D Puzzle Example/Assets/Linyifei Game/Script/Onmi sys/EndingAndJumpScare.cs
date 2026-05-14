using System.Collections;
using UnityEngine;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class EndingAndJumpScare : MonoBehaviour
    {
        [Header("🎥 摄像机联动 (劫持控制)")]
        public FirstPersonController fpc;
        [Tooltip("预设的看向目标。如果开启了下方墙面对齐，这个点也会自动平移以对准鬼")]
        public Transform targetLookPoint;
        public AnimationCurve lookCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("📐 动态墙面对齐 (正对玩家)")]
        [Tooltip("勾选后，鬼的整个出场轨迹会自动平移，确保正对玩家")]
        public bool alignToPlayerOnWall = true;
        [Tooltip("请在墙上放置一个空物体拖入这里。它的蓝色箭头(Z轴)必须垂直指向房间内部！")]
        public Transform wallReference;

        [Header("🎯 动态扑脸设置 (自动计算落点)")]
        [Tooltip("勾选后，鬼会精准扑向摄像机并停在安全距离，绝不会飞离地面")]
        public bool useDynamicApproach = true;
        [Tooltip("扑脸结束时，距离摄像机多近？(推荐0.5到0.8，防止穿模)")]
        public float stopDistanceFromCamera = 0.6f;

        [Header("👻 图像引用 (拖入带有SpriteRenderer的物体)")]
        public SpriteRenderer ghost1;
        public SpriteRenderer ghost2;

        [Header("🩸 血液飞溅效果")]
        public CameraFilterPack_AAA_BloodOnScreen bloodFilter;
        public float bloodPeakValue = 1.2f;
        public float bloodInDuration = 0.15f;
        public float bloodOutDuration = 2f;

        [Header("📍 位置节点 (Transforms)")]
        public Transform ghost1StartPos;
        public Transform ghost1EndPos;
        public Transform ghost2StartPos;
        public Transform ghost2MidPos;
        public Transform ghost2ApproachPos; // 如果开启了动态扑脸，此节点将作为备用

        [Header("⏱️ 时间参数 - 阶段1 (鬼1)")]
        public float transitionToLookTime = 1f;
        public float delayBeforeGhost1 = 1f;
        public float ghost1FadeInDuration = 1f;
        public float ghost1MoveDuration = 2f;

        [Header("⏱️ 时间参数 - 阶段2 (交替与鬼2)")]
        public float crossfadeDuration = 1.5f;
        public float delayBeforeGhost2Move = 1f;
        public float ghost2FirstMoveDuration = 1f;
        public float ghost2ApproachDuration = 0.2f;
        public float ghost2FadeOutDuration = 2f;

        void Start()
        {
            if (ghost1 != null)
            {
                ghost1.gameObject.SetActive(false);
                SetAlpha(ghost1, 0f);
            }
            if (ghost2 != null)
            {
                ghost2.gameObject.SetActive(false);
                SetAlpha(ghost2, 0f);
            }

            if (bloodFilter != null)
            {
                bloodFilter.Blood_On_Screen = 0.02f;
                bloodFilter.enabled = false;
            }
        }

        public void StartJumpScare()
        {
            StartCoroutine(JumpScareCoroutine());
        }

        public void EndJumpScare()
        {
            if (fpc != null)
            {
                fpc.cameraCanMove = true;
                fpc.playerCanMove = true;
            }
        }

        IEnumerator JumpScareCoroutine()
        {
            // ==========================================
            // 核心计算：提取并计算所有的动态位置
            // ==========================================
            Vector3 lookTarget = targetLookPoint != null ? targetLookPoint.position : fpc.playerCamera.transform.position + fpc.playerCamera.transform.forward;
            Vector3 g1Start = ghost1StartPos.position;
            Vector3 g1End = ghost1EndPos.position;
            Vector3 g2Start = ghost2StartPos.position;
            Vector3 g2Mid = ghost2MidPos.position;

            if (alignToPlayerOnWall && wallReference != null && fpc != null)
            {
                // 1. 构建墙面几何平面 (以 wallReference 的位置为基准，以它的前向为法线)
                Plane wallPlane = new Plane(wallReference.forward, wallReference.position);

                // 2. 将玩家摄像机的位置，垂直投影到这面墙上
                Vector3 projectedCamPos = wallPlane.ClosestPointOnPlane(fpc.playerCamera.transform.position);

                // 3. 计算原本预设的起点，到完美投影点之间的空间偏移量
                Vector3 offset = projectedCamPos - ghost2StartPos.position;
                offset.y = 0; // 强制Y轴偏移为0，保证鬼贴地的绝对高度不发生任何改变！

                // 4. 把整套动画轨迹全部平移过去
                lookTarget += offset;
                g1Start += offset;
                g1End += offset;
                g2Start += offset;
                g2Mid += offset;
            }

            // ==========================================
            // 流程开始
            // ==========================================

            // 1. 劫持控制并看向计算好的完美目标点
            if (fpc != null)
            {
                fpc.cameraCanMove = false;
                fpc.playerCanMove = false;
                yield return StartCoroutine(LookAtTargetRoutine(fpc, lookTarget, transitionToLookTime));
            }
            else
            {
                yield return new WaitForSeconds(transitionToLookTime);
            }

            // 2. 准备让鬼1出场
            yield return new WaitForSeconds(delayBeforeGhost1);
            ghost1.transform.position = g1Start;
            ghost1.gameObject.SetActive(true);

            // 3. 鬼1：逐渐从透明浮现
            yield return StartCoroutine(FadeRoutine(ghost1, 0f, 1f, ghost1FadeInDuration));

            // 4. 鬼1：移动
            yield return StartCoroutine(MoveRoutine(ghost1.transform, g1Start, g1End, ghost1MoveDuration));

            // 5. 交叉渐变
            ghost2.transform.position = g2Start;
            ghost2.gameObject.SetActive(true);
            yield return StartCoroutine(CrossfadeRoutine(ghost1, ghost2, crossfadeDuration));
            ghost1.gameObject.SetActive(false);

            // 6. 等待鬼2准备行动
            yield return new WaitForSeconds(delayBeforeGhost2Move);

            // 7. 鬼2：第一段移动
            yield return StartCoroutine(MoveRoutine(ghost2.transform, g2Start, g2Mid, ghost2FirstMoveDuration));

            // 8. 鬼2：扑脸计算！
            Vector3 finalApproachPos = ghost2ApproachPos != null ? ghost2ApproachPos.position : fpc.playerCamera.transform.position;
            
            if (useDynamicApproach && fpc != null)
            {
                // 获取从鬼指向玩家摄像机的向量，并在 Y 轴拍扁，保持水平滑行
                Vector3 dirToCamera = fpc.playerCamera.transform.position - g2Mid;
                dirToCamera.y = 0;
                dirToCamera.Normalize();

                // 最终位置 = 摄像机位置，向鬼反方向推开一段防穿模安全距离
                finalApproachPos = fpc.playerCamera.transform.position - dirToCamera * stopDistanceFromCamera;
                finalApproachPos.y = g2Mid.y; // 再次强制继承贴地高度
            }

            // 触发血液效果 (并行执行不打断移动)
            if (bloodFilter != null)
            {
                StartCoroutine(BloodSplashRoutine());
            }

            // 鬼2瞬间冲锋
            yield return StartCoroutine(MoveRoutine(ghost2.transform, g2Mid, finalApproachPos, ghost2ApproachDuration));

            // 9. 鬼2：最后逐渐消失
            yield return StartCoroutine(FadeRoutine(ghost2, 1f, 0f, ghost2FadeOutDuration));

            ghost2.gameObject.SetActive(false);
            EndJumpScare();
        }

        // ==========================================
        // 辅助协程 (血液、摄像机、渐变、移动)
        // ==========================================
        private IEnumerator BloodSplashRoutine()
        {
            bloodFilter.enabled = true;
            float time = 0;
            while (time < bloodInDuration)
            {
                time += Time.deltaTime;
                bloodFilter.Blood_On_Screen = Mathf.Lerp(0.02f, bloodPeakValue, time / bloodInDuration);
                yield return null;
            }
            bloodFilter.Blood_On_Screen = bloodPeakValue;

            time = 0;
            while (time < bloodOutDuration)
            {
                time += Time.deltaTime;
                bloodFilter.Blood_On_Screen = Mathf.Lerp(bloodPeakValue, 0.02f, time / bloodOutDuration);
                yield return null;
            }
            bloodFilter.Blood_On_Screen = 0.02f;
            bloodFilter.enabled = false;
        }

        // 注意：这里改为了接受 Vector3 targetPos，以支持动态平移的目标点
        private IEnumerator LookAtTargetRoutine(FirstPersonController fpc, Vector3 targetPos, float duration)
        {
            float time = 0;
            Quaternion startBodyRot = fpc.transform.rotation;
            Quaternion startCamRot = fpc.playerCamera.transform.localRotation;

            Vector3 lookDir = targetPos - fpc.playerCamera.transform.position;
            Quaternion targetWorldRot = Quaternion.LookRotation(lookDir);

            Vector3 targetEuler = targetWorldRot.eulerAngles;
            Quaternion targetBodyRot = Quaternion.Euler(0, targetEuler.y, 0);
            
            float targetPitch = targetEuler.x;
            if (targetPitch > 180f) targetPitch -= 360f;
            targetPitch = Mathf.Clamp(targetPitch, -fpc.maxLookAngle, fpc.maxLookAngle);
            Quaternion targetCamRot = Quaternion.Euler(targetPitch, 0, 0);

            if (duration <= 0) duration = 0.01f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = lookCurve.Evaluate(time / duration);

                fpc.transform.rotation = Quaternion.Slerp(startBodyRot, targetBodyRot, t);
                fpc.playerCamera.transform.localRotation = Quaternion.Slerp(startCamRot, targetCamRot, t);

                yield return null;
            }

            fpc.transform.rotation = targetBodyRot;
            fpc.playerCamera.transform.localRotation = targetCamRot;
            fpc.SyncCameraAngles();
        }

        private void SetAlpha(SpriteRenderer sr, float alpha)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }

        private IEnumerator FadeRoutine(SpriteRenderer target, float startAlpha, float endAlpha, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                SetAlpha(target, Mathf.Lerp(startAlpha, endAlpha, time / duration));
                yield return null;
            }
            SetAlpha(target, endAlpha); 
        }

        private IEnumerator MoveRoutine(Transform target, Vector3 startPos, Vector3 endPos, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                target.position = Vector3.Lerp(startPos, endPos, time / duration);
                yield return null;
            }
            target.position = endPos; 
        }

        private IEnumerator CrossfadeRoutine(SpriteRenderer fadeOutTarget, SpriteRenderer fadeInTarget, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                SetAlpha(fadeOutTarget, Mathf.Lerp(1f, 0f, t));
                SetAlpha(fadeInTarget, Mathf.Lerp(0f, 1f, t));
                yield return null;
            }
            SetAlpha(fadeOutTarget, 0f);
            SetAlpha(fadeInTarget, 1f);
        }
    }
}