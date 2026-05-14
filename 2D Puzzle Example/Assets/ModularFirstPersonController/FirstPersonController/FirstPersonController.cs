using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Linyifei_Game.Script.Onmi_sys;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Cinematic Transition Settings")]
    public bool startWithTransition = false;
    public Camera camB;
    public float transitionDuration = 2.5f;
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Camera Settings")]
    public Camera playerCamera;
    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    [Header("Crosshair")]
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Camera Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    [Header("Camera Zoom Settings")]
    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;

    // Internal Zoom Variables
    private bool isZoomed = false;

    [Header("Movement Settings")]
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    // Internal Movement Variables
    private bool isWalking = false;
    private bool isGrounded = false;

    [Header("Sprint Settings")]
    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;

    // Internal Sprint Variables
    private bool isSprinting = false;
    private float sprintRemaining;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;

    [Header("Head Bob Settings")]
    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Head Bob Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    [Header("Interaction Setup")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    private Object3DOmniBehaviour currentTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        crosshairObject = GetComponentInChildren<Image>();

        playerCamera.fieldOfView = fov;
        jointOriginalPos = joint.localPosition;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (crosshair && crosshairObject != null)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else if (crosshairObject != null)
        {
            ToggleCrosshair(false);
        }

        // --- Cinematic Camera Transition Setup ---
        if (startWithTransition && camB != null)
        {
            StartCoroutine(StartCameraTransition());
        }
        else if (camB != null)
        {
            camB.gameObject.SetActive(false);
        }
    }

    public void ToggleCrosshair(bool b)
    {
        crosshairObject.gameObject.SetActive(b);
    }

    private void HandleInteraction()
    {
        if (currentTarget != null && currentTarget.isInteracting)
        {
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            Object3DOmniBehaviour obj = hit.collider.GetComponent<Object3DOmniBehaviour>();

            if (obj != null)
            {
                if (currentTarget == null)
                {
                    obj.Highlight();
                }
                else if (currentTarget != obj)
                {
                    currentTarget.RemoveHighlight();
                    obj.Highlight();
                }
                currentTarget = obj;
            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.RemoveHighlight();
                    currentTarget = null;
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.RemoveHighlight();
                currentTarget = null;
            }
        }

        // 只有当按下左键时才执行
        if (Input.GetKeyDown(KeyCode.Mouse0) && !GlobalInputState.isMouse0ConsumedThisFrame)
        {
            if (currentTarget != null)
            {
                currentTarget.OnInteract();
                Debug.Log("OnInteract");
                GlobalInputState.isMouse0ConsumedThisFrame = true;
            }
        }
    }

    IEnumerator StartCameraTransition()
    {
        bool originalCameraState = cameraCanMove;
        bool originalPlayerState = playerCanMove;

        cameraCanMove = false;
        playerCanMove = false;

        camB.gameObject.SetActive(true);

        Vector3 startPos = camB.transform.position;
        Quaternion startRot = camB.transform.rotation;

        float elapsed = 0;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float linearT = elapsed / transitionDuration;
            float curveT = transitionCurve.Evaluate(linearT);

            camB.transform.position = Vector3.Lerp(startPos, playerCamera.transform.position, curveT);
            camB.transform.rotation = Quaternion.Slerp(startRot, playerCamera.transform.rotation, curveT);

            yield return null;
        }

        camB.transform.position = playerCamera.transform.position;
        camB.transform.rotation = playerCamera.transform.rotation;

        camB.gameObject.SetActive(false);

        cameraCanMove = originalCameraState;
        playerCanMove = originalPlayerState;

        yaw = transform.localEulerAngles.y;
        pitch = playerCamera.transform.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;
    }

    private void Update()
    {
        #region Camera
        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            else
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");

            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        #region Camera Zoom
        if (enableZoom)
        {
            if (Input.GetKeyDown(zoomKey) && !holdToZoom && !isSprinting)
            {
                isZoomed = !isZoomed;
            }

            if (holdToZoom && !isSprinting)
            {
                if (Input.GetKeyDown(zoomKey)) isZoomed = true;
                else if (Input.GetKeyUp(zoomKey)) isZoomed = false;
            }

            if (isZoomed)
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
            else if (!isZoomed && !isSprinting)
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, zoomStepTime * Time.deltaTime);
        }
        #endregion
        #endregion

        #region Sprint
        if (enableSprint)
        {
            if (isSprinting)
            {
                isZoomed = false;
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

                if (!unlimitedSprint)
                {
                    sprintRemaining -= 1 * Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
            }

            if (isSprintCooldown)
            {
                sprintCooldown -= 1 * Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }
        }
        #endregion

        CheckGround();

        if (enableHeadBob)
        {
            HeadBob();
        }

        HandleInteraction();
    }

    void FixedUpdate()
    {
        #region Movement
        if (playerCanMove)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if ((targetVelocity.x != 0 || targetVelocity.z != 0) && isGrounded)
                isWalking = true;
            else
                isWalking = false;

            if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
            {
                targetVelocity = transform.TransformDirection(targetVelocity) * sprintSpeed;

                Vector3 velocity = rb.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                if (velocityChange.x != 0 || velocityChange.z != 0)
                {
                    isSprinting = true;
                }

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            else
            {
                isSprinting = false;
                targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

                Vector3 velocity = rb.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }
        #endregion
    }

    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void HeadBob()
    {
        if (isWalking)
        {
            if (cameraCanMove)
            {
                if (isSprinting)
                    timer += Time.deltaTime * (bobSpeed + sprintSpeed);
                else
                    timer += Time.deltaTime * bobSpeed;

                joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
            }
        }
        else
        {
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }

    // 被外部脚本劫持摄像机后，调用此方法重新同步内部角度，防止镜头弹回
    public void SyncCameraAngles()
    {
        yaw = transform.localEulerAngles.y;
        pitch = playerCamera.transform.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;
    }
}