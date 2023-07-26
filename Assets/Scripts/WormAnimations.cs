using System;
using UnityEngine;

public class WormAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public event EventHandler OnDisapear;
    public event EventHandler OnHit;
    public event EventHandler OnGettingReady;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    // Animation Event: trigs when disepear animation end
    private void OnDisapearAnim()
    {
        OnDisapear?.Invoke(this, EventArgs.Empty);
    }

    // Animation Event: trigs when attack animation at hit action
    private void OnHitAnim()
    {
        OnHit?.Invoke(this, EventArgs.Empty);
    }

    // Animation Event: trigs when appear animation end
    private void OnGettingReadyAnim()
    {
        OnGettingReady?.Invoke(this, EventArgs.Empty);
    }

    public void PlayComingOut()
    {
        animator.SetTrigger("OnStart");
    }

    public void PlayIdle()
    {
        animator.SetBool("IsWormTaken", false);

        animator.SetBool("HasWormAttack", false);
    }

    public void PlayStretch()
    {
        animator.SetBool("IsWormTaken", true);
    }

    public void PlayAttack()
    {
        animator.SetBool("HasWormAttack", true);
    }

    public void PlayDiseppear()
    {
        animator.SetBool("HasDisappear", true);
    }
    
    public void ResetAnimation()
    {
        animator.SetBool("IsWormTaken", false);

        animator.SetBool("HasWormAttack", false);

        animator.SetBool("HasDisappear", false);
    }
}
