using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmissionIntensity : MonoBehaviour
{
    public float min = 0f;
    public float max = 3f;

    private Renderer target;

    private void Awake()
    {
        target = GetComponent<Renderer>();
    }

    public void Call(float ratio)
    {
        var intensity = Mathf.Lerp(min, max, ratio);
        target.material.SetColor("_EmissionColor", target.material.color * intensity);
    }
}