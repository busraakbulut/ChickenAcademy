using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ChickenAllyUnitsManager : MonoBehaviour
{
    public static ChickenAllyUnitsManager Instance;

    [SerializeField] private Incubation workerIncubation;

    [SerializeField] private Incubation soliderInbation;

    [SerializeField] private int maxWorkers = 3;

    [SerializeField] private int maxSoliders = 5;

    private AllyUnit workerUnit;

    private AllyUnit soliderUnit;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }


    private void Start()
    {
        workerUnit = new AllyUnit(maxWorkers);

        workerIncubation.SetUnit(workerUnit);

        soliderUnit = new AllyUnit(maxSoliders);

        soliderInbation.SetUnit(soliderUnit);

        //Test
        workerIncubation.TakeEgg();

        soliderInbation.TakeEgg();
    }


}
