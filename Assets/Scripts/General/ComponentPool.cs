using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : Behaviour {
    private GameObject pooledObject;
    private int numberOfObjects;

    private List<T> objects;
    private LinkedList<int> available;

    public ComponentPool(GameObject pooledObject, int numberOfObjects)  {
        if (numberOfObjects < 0)
            Debug.LogError(this + " pool cant have negative size");
        if(pooledObject == null)
            Debug.LogError(this + " pooledObject cant be null");
        else if (pooledObject.GetComponent<T>() != null)
            Debug.LogError(this + " pooledObject must ");

        available = new LinkedList<int>();
        objects = new List<T>(numberOfObjects);
        for(int i = 0; i < numberOfObjects; i++) {
            objects.Add(GameObject.Instantiate(pooledObject).GetComponent<T>());
            objects[i].enabled = false;
            available.AddLast(i);
        }
    }

    public T Get() {
        if(available.Count > 0) {
            T ret = objects[available.Last.Value];
            ret.enabled = true;
            available.RemoveLast();
            return ret;
        }
        else {
            T ret = GameObject.Instantiate(pooledObject.GetComponent<T>());
            objects.Add(ret);
            return ret;
        }
    }

    public void Free(T obj) {
        int index = objects.IndexOf(obj);
        if(index == -1) {
            Debug.LogError(obj + " object is not part of the pool");
        }
        else {
            //UnityEditor.PrefabUtility.ResetToPrefabState(obj);
            obj.enabled = false;
            available.AddLast(index);
        }
    }
}
