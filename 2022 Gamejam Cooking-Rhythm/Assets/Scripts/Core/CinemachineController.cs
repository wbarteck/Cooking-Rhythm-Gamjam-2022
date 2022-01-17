using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] cameras;
    [Button] void GetCameras() => cameras = GetComponentsInChildren<CinemachineVirtualCamera>(true);

    private void Awake()
    {
        foreach (var c in cameras) c.gameObject.SetActive(false);
    }

    [Button] public void ActivateCamera(int cameraIndex)
    {
        foreach (var c in cameras) c.gameObject.SetActive(false);
        cameras[cameraIndex].gameObject.SetActive(true);
    }

}
