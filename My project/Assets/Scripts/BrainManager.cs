using System;
using UnityEngine;
using UnityEngine.UI;

public class BrainManager : MonoBehaviour
{
    public static BrainManager instance;

    public Event gameOver;

    private Animator animator;

    [SerializeField]
    private float hp;
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private GameObject deathEffect;

    private Vector2 oldMouseDir = Vector2.zero;
    ParticleSystem bloodSplat;


    private void Awake()
    {
        instance = this;

        hpBar.maxValue = hp;
        hpBar.value = hp;

        animator = GetComponent<Animator>();
        bloodSplat = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        LookDirection();

        if (InputManager.instance.TestInput)
        {
            var blink = GetComponent<SpriteBlink>();
            if (blink) blink.Blink();
            InputManager.instance.UseTestInput();
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpBar.value = hp;

        var blink = GetComponent<SpriteBlink>();
        if (blink) blink.Blink();

        if (hp <= 0)
        {
            Death();
            return;
        }

        animator.SetTrigger("hurt");

        AudioManager.PlaySFX("burp");
        bloodSplat.Play();

        if (hp <= 0) Death();
    }

    private void Death()
    {
        deathEffect.SetActive(true);
        deathEffect.transform.parent = null;

        gameObject.SetActive(false);
    }

    private void LookDirection()
    {
        Vector2 direction = GameManager.instance.GetScreenQuadrant();

        if (oldMouseDir == direction) return;
        oldMouseDir = direction;

        if (direction.x < 0)
        {
            if (direction.y < 0)
            {
                var animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                TurnOffAnimationBools();
                animator.SetBool("down", true);
                animator.Play("BrainIdleDown", 0, animTime);
            }
            else
            {
                var animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                TurnOffAnimationBools();
                animator.SetBool("left", true);
                animator.Play("BrainIdleLeft", 0, animTime);
            }
        }
        else
        {
            if (direction.y < 0)
            {
                var animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                TurnOffAnimationBools();
                animator.SetBool("right", true);
                animator.Play("BrainIdleRight", 0, animTime);
            }
            else
            {
                var animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                TurnOffAnimationBools();
                animator.SetBool("up", true);
                animator.Play("BrainIdleUp", 0, animTime);
            }
        }
    }

    private void TurnOffAnimationBools()
    {
        animator.SetBool("down", false);
        animator.SetBool("up", false);
        animator.SetBool("left", false);
        animator.SetBool("right", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cell = collision.GetComponent<Cell>();

        if (cell && cell.isEnemy)
        {
            TakeDamage(cell.DMG);

            cell.Remove();
        }
    }
}
