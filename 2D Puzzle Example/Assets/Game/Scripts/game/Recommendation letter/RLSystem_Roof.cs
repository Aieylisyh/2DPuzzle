using System.Collections;
using UnityEngine;


public partial class RLSystem : MonoBehaviour
{

    [SerializeField] EyeBehaviour eb;

    void OnAdmissionLetterSceneEnd()
    {
        StartCoroutine(DelayAction(1.5f, () => ToggleContinueButton(true)));
    }
}