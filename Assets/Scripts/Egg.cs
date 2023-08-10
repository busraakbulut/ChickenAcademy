using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public event Action OnCrackedAnimComplated;

    public void OnCrackAnimComplatedAnimEvent()
    {
        OnCrackedAnimComplated?.Invoke();
    }
}
