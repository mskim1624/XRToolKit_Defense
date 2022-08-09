using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    public UnityEvent OnHit;

    public void Hit()
    {
        OnHit?.Invoke();
    }
}