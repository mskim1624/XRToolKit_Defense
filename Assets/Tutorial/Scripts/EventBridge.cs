using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBridge : MonoBehaviour
{
    public UnityEvent OnCall;

    public void Call()
    {
        OnCall?.Invoke();
    }
}