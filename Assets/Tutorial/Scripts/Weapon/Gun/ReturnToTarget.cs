using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target;

    public float duration = 1f;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public UnityEvent OnCompleted;

    public void Call()
    {
        if (!gameObject.activeInHierarchy)
            return;

        StopAllCoroutines();
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        if (target == null)
            yield break;

        var beginTime = Time.time;

        while (true)
        {
            var t = (Time.time - beginTime) / duration;
            if (t >= 1f)
                break;

            t = curve.Evaluate(t);

            transform.position = Vector3.Lerp(transform.position, target.position, t);
            
            yield return null;
        }

        transform.position = target.position;

        OnCompleted?.Invoke();
    }
}