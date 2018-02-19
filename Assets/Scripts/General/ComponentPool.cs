using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : Behaviour {
    private GameObject pooledObject;
    private int numberOfObjects;

    private Transform parent;

    private List<T> objects;
    private LinkedList<int> available;

    public ComponentPool(GameObject pooledObject, int numberOfObjects, Transform parent)  {
        if (numberOfObjects < 0)
            Debug.LogError(this + " pool cant have negative size");
        if(pooledObject == null)
            Debug.LogError(this + " pooledObject cant be null");
        /*else if (pooledObject.GetComponent<T>() != null)
            Debug.LogError(this + " pooledObject must have a component of the generic type " + typeof(T).FullName);
*/
        this.pooledObject = pooledObject;

        this.parent = parent;
        available = new LinkedList<int>();
        objects = new List<T>(numberOfObjects);
        for(int i = 0; i < numberOfObjects; i++) {
            objects.Add(GameObject.Instantiate(pooledObject).GetComponent<T>());
            if(parent != null) {
                objects[i].transform.parent = parent;
            }

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
            if (parent != null) {
                ret.transform.parent = parent;
            }
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
