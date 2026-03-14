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

        if (direction.x < 0)
        {
            if (direction.y < 0)
            {
                TurnOffAnimationBools();
                animator.SetBool("down", true);
            }
            else
            {
                TurnOffAnimationBools();
                animator.SetBool("left", true);
            }
        }
        else
        {
            if (direction.y < 0) animator.SetBool("right", true);
            else animator.SetBool("up", true);
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
