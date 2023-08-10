using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WormSpawner : MonoBehaviour
{
    public event Action<ObjectPool> OnWormsSpawnerReady;

    [SerializeField] private Transform spawnArea;

    [SerializeField] private float spawnOffsetToEdges;

    [SerializeField] private Transform wormPrefab;

    [SerializeField] private float spawnRate = 10f;

    [SerializeField] private int maxSpawnCount = 10;

    private float spawnAreaWidth;

    private float spawnAreaHeight;

    Vector2Int gridPosition;

    [SerializeField] private float gridWidth = 1f;

    [SerializeField] private float gridHeight = 1f;

    private int coloumnCount;

    private int rowCount;

    private Transform [,] gridObjects;

    private bool isSpawningRunning;

    private ObjectPool objectPool;

    private void Start()
    {
        spawnAreaWidth = spawnArea.localScale.x - spawnOffsetToEdges;

        spawnAreaHeight = spawnArea.localScale.z - spawnOffsetToEdges;

        coloumnCount = Convert.ToInt32(spawnAreaWidth / gridWidth);
        
        rowCount = Convert.ToInt32(spawnAreaHeight / gridHeight);

        gridObjects = new Transform[coloumnCount, rowCount];

        objectPool = new(maxSpawnCount, wormPrefab.gameObject, transform);

        OnWormsSpawnerReady?.Invoke(objectPool);
    }


    private void Update()
    {
        if (!isSpawningRunning && 
            objectPool.ActiveObjectCount() < maxSpawnCount)
        {
            StartCoroutine(Spawn());
        }
    }


    private IEnumerator Spawn()
    {
        isSpawningRunning = true;

        yield return new WaitForSeconds(spawnRate);

        if (objectPool.ActiveObjectCount() >= maxSpawnCount)
        {
            yield return null;
        }

        int findAttempt = 0;
        Vector2Int gridPosition = new Vector2Int(0, 0);

        while (!TryFindRandomEmptySlot(coloumnCount, rowCount, ref gridPosition, ref findAttempt))
        {
            if(findAttempt > rowCount * coloumnCount)
            {
                yield return null;
            }
        }

        //Transform gridObjectTransform = Instantiate(wormPrefab);

        Transform gridObjectTransform = objectPool.TakeObject().transform;

        gridObjectTransform.localPosition = SetPostionWithGridPosition(gridPosition);

        gridObjects[gridPosition.x, gridPosition.y] = gridObjectTransform;

        Worm worm = gridObjectTransform.GetComponent<Worm>();

        worm.OnDestroy += Worm_OnDestroy;

        isSpawningRunning = false;
    }

    private void Worm_OnDestroy(object sender, EventArgs e)
    {
        Worm worm = sender as Worm;

        if (worm != null)
        {
            Vector2Int gridPosition = GetGridPositionWithWorldPosition(worm.transform.position);

            gridObjects[gridPosition.x, gridPosition.y] = null;

            worm.OnDestroy -= Worm_OnDestroy;
        }
    }

    private bool TryFindRandomEmptySlot(int xMax, int yMax, ref Vector2Int gridPosition, ref int attempt)
    {
        int x = Random.Range(0, xMax);
        
        int y = Random.Range(0, yMax);

        attempt++;

        if (gridObjects[x, y] == null)
        {
            gridPosition = new Vector2Int(x, y);
            return true;
        }

        return false;
    }

    private Vector3 SetPostionWithGridPosition(Vector2Int gridPosition)
    {
        return new Vector3(spawnArea.transform.position.x + gridPosition.x * gridWidth - spawnAreaWidth / 2,
                           spawnArea.transform.position.y + spawnArea.transform.localScale.y / 2,
                           spawnArea.transform.position.z + gridPosition.y * gridHeight - spawnAreaHeight / 2);
    }
   
    private Vector2Int GetGridPositionWithWorldPosition(Vector3 position)
    {
        int x = Convert.ToInt32((position.x - spawnArea.transform.position.x + spawnAreaWidth / 2) / gridWidth);
        int y = Convert.ToInt32((position.z - spawnArea.transform.position.z + spawnAreaHeight / 2) / gridHeight);
        return new Vector2Int(x,y);
    }

    private void CreateTestDebugBojects()
    {
        for (int x = 0; x < coloumnCount; x++)
        {
            for (int z = 0; z < rowCount; z++)
            {
                GameObject gridObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                gridObject.transform.localScale = new Vector3(gridWidth, 1, gridHeight);

                gridObject.transform.localPosition = SetPostionWithGridPosition(new Vector2Int(x, z));
            }
        }
    }
}