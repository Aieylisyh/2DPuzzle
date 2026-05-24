using System.Collections;
using UnityEngine;


[CreateAssetMenu]
public class OmniGameGlobalConfig : ScriptableObject
{
    public int ClickCallPoliceTime;

    /// <summary>
    /// 好结局回到第一个场景后，跳过 WebcamDebugger，直接显示电脑。
    /// </summary>
    public bool skipWebcamDebugger;
}