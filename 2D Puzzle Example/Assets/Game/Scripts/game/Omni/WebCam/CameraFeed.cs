using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class CameraFeed : MonoBehaviour
{
    WebCamTexture webcam;

    IEnumerator Start()
    {
        // 1. Android / iOS permission request
#if UNITY_ANDROID || UNITY_IOS  || UNITY_EDITOR
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogError("Camera access denied");
            yield break;
        }
#endif
        // 2. Fire up the camera
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0) { Debug.LogError("No camera found"); yield break; }

        webcam = new WebCamTexture(devices[0].name, 1280, 720, 30); // width, height, fps
        GetComponent<Renderer>().material.mainTexture = webcam;      // show on a quad
        Debug.Log("webcam");
        Debug.Log(webcam);
        webcam.Play();

        // 3. Optional: grab the pixels
        yield return new WaitUntil(() => webcam.didUpdateThisFrame);
        Color32[] pixels = webcam.GetPixels32();  // BGRA32 data
        // …do something with pixels…
    }

    void OnDestroy()
    {
        if (webcam != null) webcam.Stop();
    }
}