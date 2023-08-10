using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkerStates
{
    Passive = 0,
    Ready,
    MovingToWorm,
    WormTaken,
    MovingToMother,
}

public class WorkerChickenAI : BaseChickenAI
{
    public event EventHandler OnStateChangedInternally;

    [SerializeField] private GameObject worm;

    public WorkerStates State { get; set; }

    private void OnEnable()
    {
        type = UnitType.Worker;

        ChangeState(WorkerStates.Ready);
    }

    private void OnDisable()
    {
        ChangeState(WorkerStates.Passive);
    }

    public override void Eat()
    {
        base.Eat();

        worm.SetActive(true);

        ChangeState(WorkerStates.WormTaken);
    }

    private void ChangeState(WorkerStates newState)
    {
        State = newState;

        OnStateChangedInternally?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Worms"))
        {
            Vector3 startPosition = new Vector3(transform.position.x, 0, transform.position.z);

            Vector3 endPosition = new Vector3(collision.gameObject.transform.position.x, 0, collision.gameObject.transform.position.z);

            transform.forward = (endPosition - startPosition).normalized;

            Debug.DrawRay(transform.position + new Vector3(0, 5, 0), transform.forward * 10, Color.red, 5);
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("DropArea") && isReadyToDrop)
        {
            Debug.Log("Drop Area");
            if (other.transform.parent.parent.TryGetComponent(out Incubation incubation))
            {
                incubation.TakeEgg();
            }

        }
    }

}
