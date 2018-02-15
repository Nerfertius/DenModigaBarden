using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public GameObject pooledObject;
    public int numberOfObjects;
    public bool autoExpand;

    private List<GameObject> objects;
    private LinkedList<int> available;

    void Start() {
        if (numberOfObjects < 0)
            Debug.LogError(this + " pool cant have negative size");

       available = new LinkedList<int>();
        objects = new List<GameObject>(numberOfObjects);
        for(int i = 0; i < numberOfObjects; i++) {
            objects.Add(Instantiate(pooledObject));
            objects[i].SetActive(false);
            available.AddLast(i);
        }
    }

    public GameObject Get() {
        if(available.Count > 0) {
            GameObject ret = objects[available.Last.Value];
            ret.SetActive(true);
            available.RemoveLast();
            return ret;
        }
        else {
            GameObject ret = Instantiate(pooledObject);
            objects.Add(ret);
            return ret;
        }
    }

    public void Free(GameObject obj) {
        int index = objects.IndexOf(obj);
        if(index == -1) {
            Debug.LogError(obj + " object is not part of the pool");
        }
        else {
            UnityEditor.PrefabUtility.ResetToPrefabState(obj);
            obj.SetActive(false);
            available.AddLast(index);
        }
    }
}
