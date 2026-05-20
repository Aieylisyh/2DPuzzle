using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class WebcamPreview : MonoBehaviour
{
    WebCamTexture tex;
    RawImage img;
    public TMP_InputField inputField;
    public int webCamIndex;

    void Start()
    {
        img = GetComponent<RawImage>();
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (WebCamDevice device in devices)
        {
            Debug.Log(device.name);
            Debug.Log(device.kind);
        }

        gameObject.SetActive(false);
    }

    public void OnClickStart()
    {
        webCamIndex = int.Parse(inputField.text);
        StartCapture();
    }

    public void StartCapture()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0) { Debug.LogError("No webcam found"); return; }


        tex = new WebCamTexture(devices[webCamIndex].name);   // default cam
        tex.Play();                                 // start capturing
        img.texture = tex;                          // show on UI

        // auto-rotate if the driver reports a rotation
        img.uvRect = tex.videoVerticallyMirrored
                   ? new Rect(0, 1, 1, -1)         // flip vertically
                   : new Rect(0, 0, 1, 1);
        img.rectTransform.localEulerAngles =
            new Vector3(0, 0, -tex.videoRotationAngle);
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