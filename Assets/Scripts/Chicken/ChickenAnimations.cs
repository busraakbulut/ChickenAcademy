using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void PlayDeath()
    {
        animator.SetBool("Death", true);
    }

    public void PlayAttack()
    {
        animator.SetBool("Attack", true);

        animator.SetBool("Eat", false);

        animator.SetBool("Walk", false);
    }

    public void PlayEat()
    {
        animator.SetBool("Eat", true);

        animator.SetBool("Attack", false);

        animator.SetBool("Walk", false);
    }

    public void PlayWalk()
    {
        animator.SetBool("Eat", false);

        animator.SetBool("Attack", false);

        animator.SetBool("Walk", true);
    }

    public void PlayIdle()
    {
        animator.SetBool("Eat", false);

        animator.SetBool("Attack", false);

        animator.SetBool("Walk", false);
    }
}
