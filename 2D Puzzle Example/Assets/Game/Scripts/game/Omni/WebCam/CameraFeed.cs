using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CameraFeed : MonoBehaviour
{
    WebCamTexture webcam;
    Renderer targetRenderer;
    Material runtimeMaterial;

    void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
    }

    IEnumerator Start()
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogError("Camera access denied");
            yield break;
        }
#endif

        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.LogError("No camera found");
            yield break;
        }

        webcam = new WebCamTexture(devices[0].name, 1280, 720, 30);
        runtimeMaterial = targetRenderer.material;
        runtimeMaterial.mainTexture = webcam;
        webcam.Play();
    }

    void OnDestroy()
    {
        if (webcam != null)
        {
            if (webcam.isPlaying)
                webcam.Stop();
            Destroy(webcam);
            webcam = null;
        }

        if (runtimeMaterial != null)
        {
            Destroy(runtimeMaterial);
            runtimeMaterial = null;
        }
    }
}
