using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    [Header("Ray")]
    public LineRenderer ray;
    public LayerMask hitRayMask;
    public float distance = 100f;

    [Header("Reticle Point")]
    public GameObject reticlePoint;
    public bool showReticle = true;

    private void Awake()
    {
        Off();
    }

    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Off()
    {
        StopAllCoroutines();

        ray.enabled = false;
        reticlePoint.SetActive(false);
    }

    private IEnumerator Process()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, distance, hitRayMask))
            {
                ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
                ray.enabled = true;

                reticlePoint.transform.position = hitInfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                ray.enabled = false;

                reticlePoint.SetActive(false);
            }

            yield return null;
        }
    }
}
