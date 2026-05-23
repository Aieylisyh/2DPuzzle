using Assets.Game.Scripts.game.Omni.WebCam;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class WebcamPreview : MonoBehaviour
{
    WebCamTexture tex;
    RawImage img;

    void Awake()
    {
        img = GetComponent<RawImage>();
    }

    public void StartCapture()
    {
        try
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            var webCamIndex = WebCamDebugger.defaultCamIndex;

            Debug.Log("StartCapture with cam index: " + webCamIndex);
            if (devices.Length == 0)
                throw new Exception("No webcam found");
            if (webCamIndex >= devices.Length)
                throw new Exception("Invalid webcam index");

            ReleaseTexture();

            tex = new WebCamTexture(devices[webCamIndex].name);
            tex.Play();
            if (!tex.isPlaying)
                throw new Exception("Failed to start webcam capture");

            img.texture = tex;
            img.uvRect = tex.videoVerticallyMirrored
                ? new Rect(0, 1, 1, -1)
                : new Rect(0, 0, 1, 1);
            img.rectTransform.localEulerAngles =
                new Vector3(0, 0, -tex.videoRotationAngle);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            ReleaseTexture();
        }
    }

    public void StopCapture()
    {
        ReleaseTexture();
    }

    void ReleaseTexture()
    {
        if (tex == null)
            return;

        if (tex.isPlaying)
            tex.Stop();

        if (img != null && img.texture == tex)
            img.texture = null;

        Destroy(tex);
        tex = null;
    }

    void OnDestroy()
    {
        ReleaseTexture();
    }

    void OnDisable()
    {
        StopCapture();
    }
}
