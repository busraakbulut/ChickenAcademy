using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    private int objectCount;

    private GameObject pooledObject;

    private List<GameObject> pool;

    public ObjectPool(int objectCount, GameObject pooledObject)
    {
        this.objectCount = objectCount;
        this.pooledObject = pooledObject;

        pool = new List<GameObject>();

        for (int i = 0; i < objectCount; i++)
        {
            GameObject go = GameObject.Instantiate(pooledObject);

            go.SetActive(false);

            pool.Add(go);
        }
    }

    public GameObject TakeObject()
    {
        GameObject go = pool[0];
      
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                go = obj;
                break;
            }
        }

        go.SetActive(true);

        return go;
    }

    public void DropObject(GameObject go)
    {
        foreach (GameObject obj in pool) 
        {
            if (obj == go)
            {
                go.SetActive(false);
            }
        }
    }

    public int ActiveObjectCount()
    {
        int count = 0;
        foreach (GameObject obj in pool)
        {
            if (obj.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }
}
