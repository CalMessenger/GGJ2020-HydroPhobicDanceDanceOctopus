using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnshark : MonoBehaviour
{
   public GameObject shark;
    public GameObject boat;
    public GameObject[] spawnpointsleft;
    public GameObject[] spawnpointsright;
    public int rand;
    public bool check = true;
    public float timeLeft;
    public float timeLeft2;

    public 
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 2;
        timeLeft2 = 5;


    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                check = false;
                timeLeft = 3f;
                rand = (Random.Range(0, 5));
                int rand2 = (Random.Range(0, 2));
                if (rand2 == 1)
                {
                    Instantiate(shark, (spawnpointsleft[rand].transform.position), Quaternion.RotateTowards(transform.rotation, boat.transform.rotation, -90));
                }
                else
                {
                    Instantiate(shark, (spawnpointsright[rand].transform.position), Quaternion.RotateTowards(transform.rotation, boat.transform.rotation, 90));
                }
            }
            
        }
        else
        {
            timeLeft2 -= Time.deltaTime;
            if (timeLeft2 < 0)
            {
                check = true;
            }
        }
        
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);

    }
}
