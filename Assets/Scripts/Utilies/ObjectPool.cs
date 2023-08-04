using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    private int objectCount;

    private GameObject pooledObject;

    private GameObject parent;

    private List<GameObject> pool;

    public ObjectPool(int objectCount, GameObject pooledObject, Transform parentTransform = null)
    {
        this.objectCount = objectCount;
        this.pooledObject = pooledObject;

        pool = new List<GameObject>();

        parent = new GameObject(pooledObject.name + "Pool");

        parent.transform.parent = parentTransform;

        for (int i = 0; i < objectCount; i++)
        {
            GameObject go = GameObject.Instantiate(pooledObject, parent.transform);

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
