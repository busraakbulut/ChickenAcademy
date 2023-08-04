using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseChickenAI : MonoBehaviour
{
    private GameObject chicken;

    private void Awake()
    {
        chicken = GetComponent<GameObject>();
    }
    public abstract void TakeAction();
}
