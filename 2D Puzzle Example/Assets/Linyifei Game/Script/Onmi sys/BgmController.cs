using System.Collections;
using UnityEngine;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class BgmController : MonoBehaviour
    {
        [Header("音频源组件 (Audio Sources)")]
        public AudioSource asPartStart; // 播放一次的开头音乐
        public AudioSource asPartLoop;  // 循环播放的音乐

        void Start()
        {
            // 游戏开始时自动调用播放
            PlayBGM();
        }

        /// <summary>
        /// 执行前奏与循环的无缝播放
        /// </summary>
        public void PlayBGM()
        {
            // 安全检查，防止空指针报错
            if (asPartStart == null || asPartLoop == null || asPartStart.clip == null)
            {
                Debug.LogWarning("BgmController: AudioSource 或 Clip 未正确绑定！");
                return;
            }

            // 获取当前音频系统的绝对精确时间
            double startTime = AudioSettings.dspTime;

            // 计算开头音乐的精确时长（秒）
            // 注意：使用 samples / frequency 比直接用 clip.length 更精准！
            double introDuration = (double)asPartStart.clip.samples / asPartStart.clip.frequency;

            // 安排在当前时间立即播放开头部分
            asPartStart.PlayScheduled(startTime);

            // 安排在 (当前时间 + 前奏时长) 的瞬间，精准启动循环部分
            asPartLoop.PlayScheduled(startTime + introDuration);
        }

        // ==========================================
        // 扩展功能：如果你需要中途停止 BGM，可以调用这个方法
        // ==========================================
        public void StopBGM()
        {
            if (asPartStart.isPlaying) asPartStart.Stop();
            if (asPartLoop.isPlaying) asPartLoop.Stop();
        }

        private void OnDisable()
        {
            StopBGM();
        }
    }
}