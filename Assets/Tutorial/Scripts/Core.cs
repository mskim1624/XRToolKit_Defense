using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public int maxHP = 10;
    private int hp;

    public UnityEvent<string> OnHpChanged;
    public UnityEvent OnHit;
    public UnityEvent OnDestroy;

    private static Core instance;
    public static Core Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Core>();
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        hp = maxHP;
        UpdateUI();
    }

    public void OnTriggerEnter(Collider other)
    {
        var mob = other.GetComponent<Mob>();
        if (mob != null)
        {
            OnHit?.Invoke();
            DecreaseHP(1);
            mob.Destroy();
        }
    }

    private void DecreaseHP(int amount)
    {
        if (hp <= 0)
            return;

        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            OnDestroy?.Invoke();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        OnHpChanged?.Invoke($"HP : {hp}");
    }
}