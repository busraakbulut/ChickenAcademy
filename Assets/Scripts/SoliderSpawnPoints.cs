using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderSpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject spawnAreaPrefab;

    [SerializeField] private float gapBetween2SpwanArea = 5f;

    [SerializeField] private float zOffset = 5f;

    private int maxUnit;

    private float totalLenght;

    private Vector3[] positionsToGo;

    public void Init(int maxUnit)
    {
        this.maxUnit = maxUnit;

        if (maxUnit <= 0)
        {
            return;
        }

        totalLenght = (maxUnit - 1) * gapBetween2SpwanArea;

        positionsToGo = new Vector3[maxUnit];

        for (int i = 0; i < maxUnit; i++)
        {
            positionsToGo[i] = new Vector3(-totalLenght / 2 + i * gapBetween2SpwanArea, 0.1f, zOffset) + transform.position;

            Instantiate(spawnAreaPrefab, positionsToGo[i], Quaternion.identity, transform);
        }    
    }


    public Vector3 GetSpawnPosition(int index)
    {
        if (index < 0 || index > maxUnit - 1)
        {
            return new Vector3(0, -1f, 0);
        }

        return positionsToGo[index]; 
    }
}
