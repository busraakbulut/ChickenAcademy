using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderAllyUnitsManager : BaseAllyUnitsManager
{
    public static SoliderAllyUnitsManager Instance;

    [SerializeField] private SoliderSpawnPoints spawnPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        Init();



    }

    private void Update()
    {
        //Test
        if (!incubation.HasEgg)
        {
            incubation.TakeEgg();
        }
    }

    protected override void Incubation_OnHatched()
    {
        base.Incubation_OnHatched();

        Vector3 targetPosition = spawnPoints.GetSpawnPosition(units.ActualMember - 1);

        unit.GetComponent<SoliderChickenAI>().Move(targetPosition);
    }


    

}
