using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 cameraPos;

    [SerializeField]
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField]
    [Range(0.1f, 1f)] float duration = 0.5f;

    public void Start()
    {
        mainCamera = Camera.main;
    }

    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }
    public void ReSize()
    {
        InvokeRepeating("StartResize", 0f, 0.005f);
        Invoke("StopResize", duration);
    }
    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        cameraPos = new Vector3(0, 0, -10);
        mainCamera.transform.position = cameraPos;
    }
    void StartResize()
    {
        mainCamera.fieldOfView-= 0.3f;
    }
    void StopResize()
    {
        CancelInvoke("StartResize");
        mainCamera.fieldOfView = 45;
    }
}
