using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionboat : MonoBehaviour
{
    public GameObject decals;
    public GameObject decals2;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        if(decals2!=null)decals2.SetActive(false);
        if(decals!=null)decals.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Plank"||other.tag=="Shark")
        {
            Bars.ChangeHealth(-5);
        rand = Random.Range(0, 2);
        if (rand == 1)
        {
            if(decals!=null)decals.SetActive(true);
        }
        else
        {
            if(decals2!=null)decals2.SetActive(true);
        }
        if(other.tag=="Plank")
            {
            other.gameObject.SetActive(false);
            } else if(other.tag == "Shark")
            {
                other.gameObject.tag = "Untagged";
            }
        }
    }
}
