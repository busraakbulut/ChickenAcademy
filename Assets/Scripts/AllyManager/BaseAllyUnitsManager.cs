using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseAllyUnitsManager : MonoBehaviour
{
    [SerializeField] protected Incubation incubation;

    [SerializeField] protected int maxUnits = 3;

    [SerializeField] protected ObjectPool unitPool;

    [SerializeField] protected GameObject unitPrefab;

    [SerializeField] protected UnitType type;

    protected AllyUnit units;

    protected GameObject unit;

    protected virtual void Init()
    {
        units = new AllyUnit(maxUnits);

        if (unitPrefab.TryGetComponent(out UnitTypeSelector unitTypeSelector))
        {
            unitTypeSelector.PrepareUnit(type);
        }

        unitPool = new ObjectPool(units.MaxMember, unitPrefab, transform);

        incubation.SetUnit(units);

        incubation.OnHatched += Incubation_OnHatched;
    }

    protected virtual void Incubation_OnHatched()
    {
        units.ActualMember++;

        unit = unitPool.TakeObject();

        unit.transform.position = incubation.transform.position;
    }

}
