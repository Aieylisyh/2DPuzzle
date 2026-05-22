using Assets.Game.Scripts.game.Omni.WebCam;
using System;
using TMPro;
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
            {
                throw new Exception("No webcam found"); // failed to start
            }
            if (webCamIndex >= devices.Length)
            {
                throw new Exception("Invalid webcam index");
            }
            StopCapture();

            tex = new WebCamTexture(devices[webCamIndex].name);   // default cam

            tex.Play();                                 // start capturing
            if (!tex.isPlaying)
            {
                throw new Exception("Failed to start webcam capture"); // failed to start
            }
            img.texture = tex;                          // show on UI

            // auto-rotate if the driver reports a rotation
            img.uvRect = tex.videoVerticallyMirrored
                       ? new Rect(0, 1, 1, -1)         // flip vertically
                       : new Rect(0, 0, 1, 1);
            img.rectTransform.localEulerAngles =
                new Vector3(0, 0, -tex.videoRotationAngle);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            StopCapture();
        }
    }

    public void StopCapture()
    {
        if (tex != null && tex.isPlaying) tex.Stop();
    }

    void OnDestroy()
    {
        StopCapture();
    }
    void OnDisable()
    {
        StopCapture();
    }
}