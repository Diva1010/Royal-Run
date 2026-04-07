using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float minFov = 20f;
    [SerializeField] ParticleSystem speedUpParticleSystem;
    [SerializeField] float maxFov = 120f;
    [SerializeField] float zoomDuration = 1f;
    CinemachineCamera cinemachineCamera;
    [SerializeField] float zoomSpeedModifier = 5f; 

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));
        
        if(speedAmount > 0)
        {
            speedUpParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFov = cinemachineCamera.Lens.FieldOfView;
        float targetFov = Math.Clamp(startFov + speedAmount * zoomSpeedModifier, minFov, maxFov);

        float elapsedTime = 0f;
        while(elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;

            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFov, targetFov, t);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = targetFov; 

    }
}
