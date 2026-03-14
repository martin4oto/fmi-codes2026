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

    private Vector2 oldMouseDir = Vector2.zero;


    private void Awake()
    {
        instance = this;

        hpBar.maxValue = hp;
        hpBar.value = hp;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        LookDirection();
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

    private void LookDirection()
    {
        Vector2 direction = Vector2.zero;
        Vector2 mousePos = InputManager.instance.MouseRelativeToBrainPosition;

        direction.x = Mathf.Sign(mousePos.x);
        direction.y = Mathf.Sign(mousePos.y);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var cell = collision.gameObject.GetComponent<Cell>();

        if (cell.isEnemy)
        {
            TakeDamage(cell.DMG);

            CellManager.instance.RemoveCell(cell);
        }
    }
}
