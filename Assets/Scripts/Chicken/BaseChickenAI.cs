using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum UnitType
{
    Worker,
    Solider,
}

public abstract class BaseChickenAI : MonoBehaviour
{
    [SerializeField] private ChickenAnimations chickenAnimations;

    [SerializeField] protected float moveSpeed = 5f;

    protected Vector3 moveDirection;

    protected UnitType type;

    protected bool isReadyToDrop = false;

    public bool IsReady { get; protected set; }

    public virtual void Eat()
    {
        chickenAnimations.PlayEat();
    }

    public virtual void Walk()
    {
        chickenAnimations.PlayWalk();
    }

    public virtual void GoIdle()
    {
        chickenAnimations.PlayIdle();
    }

    public virtual void Die()
    {
        chickenAnimations.PlayDeath();
    }

    public virtual void Attack()
    {
        chickenAnimations.PlayAttack();
    }


    public virtual void Move(Vector3 targetPosition) 
    {
        StartCoroutine(MoveToTarget(targetPosition));
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        float distance;

        moveDirection = targetPosition - transform.position;

        transform.forward = moveDirection;

        Walk();

        do
        {
            yield return new WaitForEndOfFrame();
            
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            distance = Vector3.Distance(transform.position, targetPosition);
        } while (distance > 0.1f);

        transform.position = targetPosition;

        transform.forward = Vector3.forward;

        GoIdle();
    }
}
