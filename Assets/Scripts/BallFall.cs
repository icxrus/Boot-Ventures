using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallMethod : MonoBehaviour
{

    [SerializeField] private UnityEvent ballFallIntoDitch;

    private void OnTriggerEnter(Collider other)
    {
        ballFallIntoDitch.Invoke();
    }


}
