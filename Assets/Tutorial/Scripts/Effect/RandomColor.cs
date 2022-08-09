using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomColor : MonoBehaviour
{
    public float hueMin = 0f;
    public float hueMax = 1f;
    public float saturationMin = 0.7f;
    public float saturationMax = 1f;
    public float valueMin = 0.7f;
    public float valueMax = 1f;

    public UnityEvent<Color> OnCreated;

    public void Call()
    {
        var color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        OnCreated.Invoke(color);
    }
}
