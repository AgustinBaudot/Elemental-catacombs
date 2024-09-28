using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager _instance;

    [SerializeField] private float _shakeForce = 1f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(_shakeForce);
    }
}
