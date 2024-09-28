using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    public static HitStopManager _instance;
    [SerializeField] private float _stopTime;
    private bool _isStopped = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public IEnumerator HitStop()
    {
        _isStopped = true;
        yield return new WaitForSeconds(_stopTime);
        _isStopped = false;
    }

    public bool IsStopped() { return _isStopped; }
}
