using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index;

    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        if (target.useWorldSpace)
        {
            target.SetPosition(index, worldPosition);
        }
        else
        {
            var localPosition = transform.InverseTransformPoint(worldPosition); //transform 은 target의 포지션이지만 서로 같아서 그냥 쓴다.
            target.SetPosition(index, localPosition);
        }
    }
}
