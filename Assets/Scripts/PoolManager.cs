using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static void CreatePool(GameObject objToClone, int amount, ref List<GameObject> pool){
        if(pool == null){
            pool = new List<GameObject>();
        }

        for(int i = 0; i < amount; i++){
            GameObject newObj = Instantiate(objToClone);
            newObj.SetActive(false);
            pool.Add(newObj);
        }
    }

    public static GameObject GetObjectFromPool(List<GameObject> pool){
        for(int i = 0; i < pool.Count; i++){
            GameObject obj = pool[i];
            if(!obj.activeInHierarchy){
                return obj;
            }
        }
        Debug.LogWarning("Could not find an inactive object");
        return null;
    }



}
