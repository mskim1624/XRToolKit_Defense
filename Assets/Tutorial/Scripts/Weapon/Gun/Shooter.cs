using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask;
    public GameObject hitEffectPrefab;
    public Transform shootPoint;

    public float shootDelay = 0.1f;
    public float maxDistance = 100f;

    public UnityEvent<Vector3> OnShootSuccess;
    public UnityEvent OnShootFail;

    private Magazine magazine;

    private void Awake()
    {
        magazine = GetComponent<Magazine>();
    }

    private void Start()
    {
        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        var wfs = new WaitForSeconds(shootDelay);

        while (true)
        {
            if (magazine.Use())
            {
                Shoot();
            }
            else
            {
                OnShootFail?.Invoke();
            }

            yield return wfs;
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);

            var hitObject = hitInfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();

            OnShootSuccess?.Invoke(hitInfo.point);
        }
        else
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance; //최대 사정거리 지점(허공)에 부딪힌걸로 하고 실패를 처리
            OnShootSuccess?.Invoke(hitPoint);
        }
    }
}
