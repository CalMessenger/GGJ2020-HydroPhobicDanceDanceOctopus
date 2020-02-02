using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnwater : MonoBehaviour
{
    public GameObject water;
    public bool check = true;
    
    // Start is called before the first frame update
    void Start()
    {
       // water.transform.position = new Vector3(0,0,0);

        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if (gameObject.activeInHierarchy == true)
            {
                Instantiate(water, this.transform.position + new Vector3(0,-1,0), Quaternion.identity);
                check = false;
            }
        }
    }
}
