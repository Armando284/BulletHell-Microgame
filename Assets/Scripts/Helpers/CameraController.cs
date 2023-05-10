using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // This pattern has to be at the end
        if (Instance != null)
        {
            Debug.LogError("CameraController Instance duplicated!");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        vCam.Follow = PlayerController.Instance.transform;
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
