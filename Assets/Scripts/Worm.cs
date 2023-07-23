using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum WormState 
{ 
    None,
    ComingOut,
    Idle, 
    Stretching, 
    Attacking, 
    Taken,
    Disappear,
}

public class Worm : MonoBehaviour
{
    [SerializeField] private Transform worm;

    [SerializeField] private Animator animator;

    [SerializeField] private bool ready;

    [SerializeField] private float disapperTime = 5f;

    private int level = 1;

    private float scorePoint = 5;

    [SerializeField] private WormState state;

    private float destroyTime = 0.25f;

    [SerializeField] private float disapperTimer;


    private void OnEnable()
    {
        disapperTimer = 0f;

        state = WormState.None;

        SetState(WormState.ComingOut);
    }

    private void Update()
    {
        disapperTimer += Time.deltaTime;

        if (disapperTimer > disapperTime)
        {
            SetState(WormState.Disappear);
        }
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
                animator.SetTrigger("OnStart");

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

            case WormState.Disappear:
                animator.SetBool("HasDisappear", true);
                break;

        }

        state = newState;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTime);

        gameObject.SetActive(false);

        worm.gameObject.SetActive(true);

        animator.SetBool("IsWormTaken", false);

        animator.SetBool("HasWormAttack", false);

        animator.SetBool("HasDisappear", false);
    }

    private IEnumerator GetReady(string animationName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        ready = true;
    }

    //Animation Event: Triggered when worm dissepear animation is ended
    public void OnDisappeared()
    {
        StartCoroutine(Destroy());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Chickens"))
        {
            Vector3 direction = (other.transform.position - worm.position).normalized;

            worm.forward = direction;

            worm.Rotate(new Vector3(180, 0, 0));

            disapperTimer = 0;

            //Get Chicken Level
            SetState(WormState.Attacking);

        }
    }


}
