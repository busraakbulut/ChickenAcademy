using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTypeSelector : MonoBehaviour
{
    [SerializeField] private BaseChickenAI workerAI;

    [SerializeField] private BaseChickenAI soliderAI;

    public void PrepareUnit(UnitType type)
    {
        switch (type) 
        {
            case UnitType.Worker:
                workerAI.enabled = true; 
                soliderAI.enabled = false;
                break;

            case UnitType.Solider:
                workerAI.enabled = false;
                soliderAI.enabled = true;
                break;
        }

    }

}
