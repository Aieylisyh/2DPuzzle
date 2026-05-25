namespace Assets.Game.Scripts.game.Omni.Mission
{
    /// <summary>
    /// 动态台词常量。涉及「首次成功/正确」或「首次失败/错误」时，均指首次点击 Finish 提交任务的结果，
    /// 而非单个选项被点选时的对错。
    /// </summary>
    public static class MissionDialogLines
    {
        public const string FirstMissionOpen = "Here we go again.";

        /// <summary>Work1：首次点开任务。</summary>
        public const string Work1Open = "Pushing things they might be interested in, boring.";
        /// <summary>Work1：任意点击一个关键词（进度提示，非提交结果）。</summary>
        public const string Work1FirstKeyword = "It's so easy to choose these 8 key similarities.";
        /// <summary>Work1：八个关键词都点完（进度提示，非提交结果）。</summary>
        public const string Work1AllKeywords = "Choose 3 restaurants that match the criteria for this hungry man.";
        /// <summary>Work1：八个关键词都点完且勾选三个餐厅（提交前提示；点击 Finish 后消失）。</summary>
        public const string Work1ReadySubmit = "Now Submit.";

        /// <summary>Work2：首次点开任务。</summary>
        public const string Work2Open = "Listen to what they are saying.";
        /// <summary>Work2：六个选项都勾选（提交前提示；点击 Finish 后消失，非提交成功台词）。</summary>
        public const string Work2AllAnswered = "Lovely";

        /// <summary>Work3：视频首次播放约 35%（非提交结果）。</summary>
        public const string Work3Video35 = "Wait-no. That's... that's my wife. Driving our car. What the hell's going on?";
        /// <summary>Work3：视频首次播放约 70%（非提交结果）。</summary>
        public const string Work3Video70 = "Jesus Christ... he's getting in... they're kissing. What the bloody hell is this? My head's all over the place.";

        /// <summary>Work3：首次点击播放视频。</summary>
        public const string Work3Open = "Hold on, that's my number plate. David, you having a laugh?";
        /// <summary>Work3：首次点击 Finish 提交且全部选项正确。</summary>
        public const string Work3FirstSubmitSuccess = "Brilliant. Don't know what's real anymore-work, home, none of it makes sense.";
        /// <summary>Work3：首次点击 Finish 提交且存在错误选项。</summary>
        public const string Work3FirstSubmitFail = "It's all blurring together now. The mission, my marriage, me... nothing feels solid.";

        /// <summary>Work4：首次打开任务（首次点击红点后消失）。</summary>
        public const string Work4Open = "Mess in my chest, but I've still got a job to finish. No point stopping now.";
        /// <summary>Work4：首次点击 Finish 提交任务成功。</summary>
        public const string Work4FirstSubmitSuccess = "OK.";

        // 兼容旧命名
        public const string Work3FirstCorrect = Work3FirstSubmitSuccess;
        public const string Work3FirstWrong = Work3FirstSubmitFail;
        public const string Work4FirstCorrect = Work4FirstSubmitSuccess;
    }
}
