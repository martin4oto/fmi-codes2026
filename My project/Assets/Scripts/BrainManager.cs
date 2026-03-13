using System;
using UnityEngine;
using UnityEngine.UI;

public class BrainManager : MonoBehaviour
{
    public static BrainManager instance;

    public Event gameOver;

    [SerializeField]
    private float hp;

    [SerializeField]
    private Slider hpBar;

    private void Awake()
    {
        instance = this;

        hpBar.maxValue = hp;
        hpBar.value = hp;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBar.value = hp;

        if (hp <= 0) Die();
    }

    private void Die()
    {
        //die
    }
}
