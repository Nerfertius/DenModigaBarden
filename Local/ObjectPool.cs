using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class ObjectPool {
    private static List<GameObject> pools;

    public static void CreateNewPool(int size, GameObject defaultObject){
        if(pools == null) {
            pools = new List<GameObject>();
        }
        if (!defaultObject.GetType().IsGenericType) {
            Debug.LogError("Default object isn't of the generic type.");
        }

        Debug.Log("NOTE: objectPool is untested");
        foreach (GameObject obj in pools) {
            if (obj.GetType().IsGenericType)
                Debug.LogError("already exists a pool containing that type");
            return;
        }


        pools.Add(new ObjectPoolInner(size, defaultObject));
    }

    public static GameObject Get(){
        Debug.Log("NOTE: objectPool is untested");
        foreach (GameObject obj in pools) {
            if (obj.GetType().IsGenericType) {
                ObjectPoolInner pool = obj;
                return pool.Get();
            }
        }
        Debug.LogError("There isn't any pool of rquested type");
        return null;
    }

    public static void Free<T>(GameObject freeObj){
        Debug.Log("NOTE: objectPool is untested");
        foreach (GameObject obj in pools) {
            if (obj.GetType().IsGenericType) {
                ObjectPoolInner<T> pool = obj;
                pool.Free(freeObj);
            }
        }
        Debug.LogError("There isn't any pool of rquested type");
    }


// actual pool
    private class ObjectPoolInner{

        List<GameObject> objects;
        LinkedList<int> freeIndexes;
        GameObject defaultObject;

        public ObjectPoolInner(int size, GameObject defaultObject) {
            this.defaultObject = defaultObject;

            Debug.Log("NOTE: objectPool is untested");
            objects = new List<GameObject>(size);
            freeIndexes = new LinkedList<int>();
            for (int i = 0; i < size; i++) {
                objects.Add(create());
                freeIndexes.AddFirst(i);
            }
        }

        public GameObject Get() {
            Debug.Log("NOTE: objectPool is untested");
            if (freeIndexes.Count > 0) {
                int index = freeIndexes.First.Value;
                if(objects[index] == null) {
                    objects[index] = create();
                }
                objects[index].SetActive(false);

                freeIndexes.RemoveFirst();

                return objects[index];
            }
            else {
                objects.Add(create());
                return objects[objects.Count - 1];
            }
        }

        public void Free(int index) {
            Debug.Log("NOTE: objectPool is untested");
            freeIndexes.AddLast(index);
        }
        public void Free(GameObject obj) {
            Debug.Log("NOTE: objectPool is untested");
            int index = objects.IndexOf(obj);
            Free(index);
        }

        private GameObject create() {
            GameObject o = GameObject.Instantiate(defaultObject);
            o.SetActive(false);
            return o;
        }
    }
}
*/