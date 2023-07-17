using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WormState 
{ 
    None,
    ComingOut,
    Idle, 
    Stretching, 
    Attacking, 
    Taken, 
}

public class Worm : MonoBehaviour
{
    [SerializeField] private Transform worm;

    [SerializeField] private Animator animator;

    [SerializeField] private bool ready;

    [SerializeField] private float collectingTime;

    private int level = 1;

    private float scorePoint = 5;

    private WormState state;

    private float destroyTime = 2f;


    private void Awake()
    {
        state = WormState.None;
        SetState(WormState.ComingOut);
    }

    private void SetState( WormState newState)
    {
        if (state == newState)
        {
            return;
        }

        switch (newState)
        {
            case WormState.ComingOut:
                StartCoroutine(GetReady("Idle"));
                break;

            case WormState.Idle:
                animator.SetBool("IsWormTaken", false);
                animator.SetBool("HasWormAttack", false);

                StartCoroutine(GetReady("Idle"));
                break;

            case WormState.Stretching:
                animator.SetBool("IsWormTaken", true);
                break;

            case WormState.Attacking:
                animator.SetBool("HasWormAttack", true);
                break;

            case WormState.Taken:
                worm.gameObject.SetActive(false);

                StartCoroutine(Destroy());
                break;

        }

        state = newState;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTime);

        gameObject.SetActive(false);

        worm.gameObject.SetActive(true);
    }

    private IEnumerator GetReady(string animationName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        ready = true;
    }

}
