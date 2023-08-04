using System;
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

    [SerializeField] private GameObject wormModel;

    [SerializeField] private WormAnimations wormAnimation;

    [SerializeField] private bool ready;

    [SerializeField] private float disapperTime = 5f;

    [SerializeField] private float disapperTimer;
    
    [SerializeField] private WormState state;
    
    private float destroyTime = 2f;
    
    private int level = 1;

    private float scorePoint = 5;

    public event EventHandler OnDestroy;

    private GameObject chicken;

    private void OnEnable()
    {
        ready = false;

        disapperTimer = 0f;

        state = WormState.None;

        SetState(WormState.ComingOut);

        wormAnimation.OnGettingReady += Animation_OnGettingReady;

        wormAnimation.OnHit += Animation_OnHit;

        wormAnimation.OnDisapear += Animation_OnDisapear;

        wormAnimation.OnWormTaken += WormAnimation_OnWormTaken;
    }

    private void Update()
    {
        disapperTimer += Time.deltaTime;

        if (disapperTimer > disapperTime)
        {
            SetState(WormState.Disappear);
        }
    }

    private void WormAnimation_OnWormTaken(object sender, EventArgs e)
    {
        StartCoroutine(Destroy());
    }

    private void Animation_OnDisapear(object sender, EventArgs e)
    {
        StartCoroutine(Destroy());
    }

    private void Animation_OnHit(object sender, EventArgs e)
    {
        if (chicken != null && ready)
        {
          Debug.Log("Attack");
        }
    }

    private void Animation_OnGettingReady(object sender, EventArgs e)
    {
        ready = true;
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
                wormAnimation.PlayComingOut();
                break;

            case WormState.Idle:
                wormAnimation.PlayIdle();
                break;

            case WormState.Stretching:
                wormAnimation.PlayStretch();
                break;

            case WormState.Attacking:
                wormAnimation.PlayAttack();
                break;

            case WormState.Taken:
                wormAnimation.PlayStretch();
                break;

            case WormState.Disappear:
                wormAnimation.PlayDiseppear();
                break;

        }

        state = newState;
    }

    private IEnumerator Destroy()
    {
        wormModel.SetActive(false);

        yield return new WaitForSeconds(destroyTime);

        worm.gameObject.SetActive(true);

        gameObject.SetActive(false);

        wormAnimation.ResetAnimation();

        OnDestroy?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Chickens"))
        {
            Vector3 startPosition = new Vector3(worm.transform.position.x, 0, worm.transform.position.z);

            Vector3 endPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);

            Vector3 direction = (endPosition - startPosition).normalized;

            //Vector3 direction = (other.transform.position - worm.position).normalized;

            worm.forward = direction;

            worm.Rotate(new Vector3(180, 0, 0));

            disapperTimer = 0;

            chicken = other.gameObject;

            chicken.GetComponent<Chicken>().Eat();
            //Get Chicken Level
            //SetState(WormState.Attacking);
            SetState(WormState.Taken);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Chickens"))
        {
            chicken = null;
        }
    }


}