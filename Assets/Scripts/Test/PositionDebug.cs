using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDebug : MonoBehaviour
{
    [SerializeField] private Transform debugPrefab;
    void Start()
    {
        Instantiate(debugPrefab, transform.position, Quaternion.identity);

        Debug.Log(transform.position);
    }

    private void Update()
    {
        Debug.Log(transform.forward);
    }
}
