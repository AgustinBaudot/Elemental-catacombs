using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    public static HitStopManager _instance;
    //[SerializeField] private float _stopTime;
    private bool _isStopped = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void StartHitStop(float hitStopTime = 0.1f)
    {
        StartCoroutine(HitStop(hitStopTime));
    }

    private IEnumerator HitStop(float hitStopTime)
    {
        _isStopped = true;
        yield return new WaitForSeconds(hitStopTime);
        _isStopped = false;
    }

    public bool IsStopped() { return _isStopped; }
}
