using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // list of objects
    List<GameObject> listOfObj = new List<GameObject>();

    // Instance of pools
    PoolManager poolManager;

    // Will set each generated object to the initial starting postion of the Spawner object
    Vector3 currentPostion;



    // The wood to be spawned
    [SerializeField]
    private GameObject currentObj;

 

    private void Awake()
    {
        currentPostion = transform.position;

        // Make a pool of how many objects you want
        PoolManager.CreatePool(currentObj, 30, ref listOfObj);
    }

    public void Spawn()
    {
        GameObject setWoodPostion = PoolManager.GetObjectFromPool(listOfObj);
        setWoodPostion.transform.position = currentPostion;
        setWoodPostion.SetActive(true);
    }
}
