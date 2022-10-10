using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachineVC;
    private CinemachineBasicMultiChannelPerlin cinemachineBMCP;

    private float shakeTime;

    private void Awake()
    {
        cinemachineBMCP = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTime > 0f)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime < 0f)
            {
                cinemachineBMCP.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBMCP.m_AmplitudeGain = intensity;
        shakeTime = time;
    }
}
