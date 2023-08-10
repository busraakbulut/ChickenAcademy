using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WorkerAllyUnitsManager : BaseAllyUnitsManager
{
    [SerializeField] private Transform motherTransfrom;

    [SerializeField] private WormSpawner wormSpawner;

    public static WorkerAllyUnitsManager Instance;

    private ObjectPool wormsPool;

    private Vector3[] wormsPositions;

  //  private WorkerStates[] chickenStates;

    private WorkerChickenAI[] chickenAIs;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        Init();

        chickenAIs = new WorkerChickenAI[units.MaxMember];

        for (int i = 0; i < chickenAIs.Length; i++)
        {
            chickenAIs[i] = unitPool.GetAll()[i].GetComponent<WorkerChickenAI>();

            chickenAIs[i].OnStateChangedInternally += WorkerChickenAI_OnStateChangedInternally;
        }

        wormSpawner.OnWormsSpawnerReady += WormSpawner_OnWormsSpawnerReady;
    }

    private void WorkerChickenAI_OnStateChangedInternally(object sender, System.EventArgs e)
    {
        WorkerChickenAI chickenAI = sender as WorkerChickenAI;

        if (chickenAI != null)
        {
            if (chickenAI.State == WorkerStates.WormTaken)
            {
                chickenAI.Move(motherTransfrom.position);

                chickenAI.State = WorkerStates.MovingToMother;
            }
        }
    }

    private void Start()
    {
        //Test
        if (!incubation.HasEgg)
        {
            incubation.TakeEgg();
        }
    }

    private void WormSpawner_OnWormsSpawnerReady(ObjectPool objectPool)
    {
        wormsPool = objectPool;

        wormsPositions = new Vector3[wormsPool.GetAll().Length];

        wormsPool.OnObjectInPoolChanged += WormsPool_OnObjectInPoolChanged;
    }

    private void WormsPool_OnObjectInPoolChanged()
    {
        FindWorms();
    }
    protected override void Incubation_OnHatched()
    {
        base.Incubation_OnHatched();

        FindChikens();
    }

    private void FindWorms()
    {
        for(int i = 0; i < wormsPositions.Length; i++)
        {
            wormsPositions[i] = wormsPool.GetAll()[i].transform.position;
        }
    }

    private void FindChikens()
    {
        for (int i = 0; i < chickenAIs.Length; i++)
        {
            if (chickenAIs[i].State != WorkerStates.Ready)
            {
                continue;
            }

            if (TryFindWormToTake(out Vector3 targetPosition))
            {
                chickenAIs[i].State = WorkerStates.MovingToWorm;

                chickenAIs[i].Move(targetPosition);
            }

        }
    }

    private bool TryFindWormToTake(out Vector3 targetPosition)
    {
        targetPosition = Vector3.zero;

        for(int i = 0; i < wormsPool.GetAll().Length; i++)
        {
            if (wormsPool.GetAll()[i].activeSelf)
            {
                targetPosition = wormsPositions[i];

                return true;
            }
        }

        return false;
    }


}
