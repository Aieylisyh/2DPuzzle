using UnityEngine;
using UnityEngine.Video;

public class WebGLVideoFix : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    
    // Type JUST the file name here (e.g., work2.mp4 or work4.mp4)
    public string videoFileName = "work2.mp4"; 

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        
        // Force the source type to URL
        videoPlayer.source = VideoSource.Url;

        // This line automatically picks the correct path for Editor OR WebGL Build!
        string totalPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        
        videoPlayer.url = totalPath;
        
        // Optional: Pre-load the video so it's ready to play smoothly
        videoPlayer.Prepare();
    }
}