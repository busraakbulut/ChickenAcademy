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
        animator.SetTrigger("OnDeath");
    }

    public void PlayAttack()
    {
        animator.SetTrigger("OnAttack");
    }

    public void PlayEat()
    {
        animator.SetTrigger("OnEat");
    }
}
