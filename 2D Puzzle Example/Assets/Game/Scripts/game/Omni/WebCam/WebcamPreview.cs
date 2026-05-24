using Assets.Game.Scripts.game.Omni.WebCam;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class WebcamPreview : MonoBehaviour
{
    WebCamTexture tex;
    RawImage img;

    public bool IsCapturing => tex != null && tex.isPlaying;

    public bool IsCaptureReady => tex != null && tex.isPlaying && tex.width > 16;

    void Awake()
    {
        img = GetComponent<RawImage>();
    }

    public void StartCapture()
    {
        TryStartCapture();
    }

    public bool TryStartCapture()
    {
        try
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            var webCamIndex = WebCamDebugger.defaultCamIndex;

            Debug.Log("StartCapture with cam index: " + webCamIndex);
            if (devices.Length == 0)
                throw new Exception("No webcam found");
            if (webCamIndex < 0 || webCamIndex >= devices.Length)
                webCamIndex = 0;

            ReleaseTexture();

            tex = new WebCamTexture(devices[webCamIndex].name);
            tex.Play();

            if (img != null)
                img.texture = tex;

            ApplyWebCamTransform();
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            ReleaseTexture();
            return false;
        }
    }

    public IEnumerator WaitUntilCaptureReady(float timeoutSeconds = 3f)
    {
        float elapsed = 0f;
        while (elapsed < timeoutSeconds)
        {
            if (IsCaptureReady)
            {
                ApplyWebCamTransform();
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        ReleaseTexture();
    }

    void ApplyWebCamTransform()
    {
        if (tex == null || img == null)
            return;

        img.uvRect = tex.videoVerticallyMirrored
            ? new Rect(0, 1, 1, -1)
            : new Rect(0, 0, 1, 1);
        img.rectTransform.localEulerAngles =
            new Vector3(0, 0, -tex.videoRotationAngle);
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
