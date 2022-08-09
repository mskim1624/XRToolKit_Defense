using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20;
    public float chargingTime = 2f;

    private int currentBullets;
    private int CurrentBullets
    {
        get => currentBullets;
        set
        {
            if (value < 0)
                currentBullets = 0;
            else if (value > maxBullets)
                currentBullets = maxBullets;
            else
                currentBullets = value;

            OnBulletsChanged?.Invoke(currentBullets);
            OnChargeChanged?.Invoke((float)currentBullets / maxBullets);
        }
    }

    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadEnd;

    public UnityEvent<int> OnBulletsChanged;
    public UnityEvent<float> OnChargeChanged;

    private void Start()
    {
        CurrentBullets = maxBullets;
    }

    public bool Use(int amount = 1)
    {
        if (CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartReload()
    {
        if (currentBullets == maxBullets)
            return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke(); // 장전 시작할때

        var beginTime = Time.time;
        var beginBullets = currentBullets;
        var enoughPercent = 1f - ((float)currentBullets / maxBullets);
        var enoughChargingTime = chargingTime * enoughPercent;

        while (true) 
        {
            var t = (Time.time - beginTime) / enoughChargingTime;
            if (t >= 1f)
                break;

            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t); //begin 과 max 사이의 t 퍼센테이지 만큼 찾아서
            yield return null;
        }

        CurrentBullets = maxBullets; //충전 시간 동안 while 돌면서 충전 다 끝나면 t = 1이 되면서 끝나고 maxbullet 이 되어 잇어야한다.

        OnReloadEnd?.Invoke(); // 장전 끝나면
    }
}
