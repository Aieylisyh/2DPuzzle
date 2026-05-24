namespace Assets.Game.Scripts.game.Omni.WebCam
{
    /// <summary>
    /// 仅在一次 LoadScene(0) 时生效，不写入 ScriptableObject，避免直接运行 2D 场景时误跳过 WebcamDebugger。
    /// </summary>
    public static class WebcamDebuggerSession
    {
        public static bool skipOnNextSceneLoad;

        public static void MarkSkipOnNextSceneLoad()
        {
            skipOnNextSceneLoad = true;
        }

        public static bool ConsumeSkipOnSceneLoad()
        {
            if (!skipOnNextSceneLoad)
                return false;

            skipOnNextSceneLoad = false;
            return true;
        }
    }
}
