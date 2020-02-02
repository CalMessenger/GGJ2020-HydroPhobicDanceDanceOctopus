using System.Collections.Generic;
using UnityEngine;

//We're all going to die, what does it matter
public class Ocotpus_Script : MonoBehaviour
{
    [SerializeField]
    GameObject[] limbsGO;
    [SerializeField]
    GameObject[] limbsTip;
    [SerializeField]
    Animator[] limbs = { };
    [SerializeField]
    KeyCode Tentacle_01, Tentacle_02,Tentacle_03,Tentacle_04,Tentacle_05,Tentacle_06;
    [SerializeField]
    bool[] canGrab;
    [SerializeField]
    GameObject[] debris;
    [SerializeField]
    GameObject world;


    void Start()
    {
        //for (int i = 0; i < limbsGO.Length; i++)
        //{
        //    limbs[i] = limbsGO[i].GetComponent<Animator>();
        //}
        limbs[0] = limbsGO[0].GetComponent<Animator>();
        limbs[1] = limbsGO[1].GetComponent<Animator>();
        limbs[2] = limbsGO[2].GetComponent<Animator>();
        limbs[3] = limbsGO[3].GetComponent<Animator>();
        limbs[4] = limbsGO[4].GetComponent<Animator>();
        limbs[5] = limbsGO[5].GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(Tentacle_01))
        {
            if(canGrab[0]==true)
            {
                limbs[0].SetBool("Grab", true);
            }
        }

        if(Input.GetKeyDown(Tentacle_02))
        {
            if(canGrab[1]==true)
            {
                limbs[1].SetBool("Grab", true);
            }
        }

        if(Input.GetKeyDown(Tentacle_03))
        {
            if(canGrab[2]==true)
            {
                limbs[2].SetBool("Grab", true);
            }
        }

        if(Input.GetKeyDown(Tentacle_04))
        {
            if(canGrab[3]==true)
            {
                limbs[3].SetBool("Grab", true);
            }
        }

        if(Input.GetKeyDown(Tentacle_05))
        {
            if(canGrab[4]==true)
            {
                limbs[4].SetBool("Grab", true);
            }
        }

        if(Input.GetKeyDown(Tentacle_06))
        {
            if(canGrab[5]==true)
            {
                limbs[5].SetBool("Grab", true);
            }
        }

    }

    public void DebrisLane(bool hasDebris, GameObject debrisGO, int LaneNo)
    {
        if(canGrab[LaneNo]==false)
        {
        canGrab[LaneNo] = hasDebris;
        debris[LaneNo] = debrisGO;
        //Debug.Log("Can grab");
        }
    }


    public void FixTransformDebris(int debrisNo)
    {
        debris[debrisNo].tag = "Untagged";
        debris[debrisNo].transform.parent = limbsTip[debrisNo].transform;
        debris[debrisNo].transform.position = limbsTip[debrisNo].transform.position;
    }

    public void DisableDebris(int debrisNo)
    {
        limbs[debrisNo].SetBool("Grab", false);
        if(debris[debrisNo]!=null)
        { debris[debrisNo].transform.parent = world.transform;
        if (debris[debrisNo].name == "plank(Clone)")
        { debris[debrisNo].tag = "Plank";
            Bars.GetWoodBoard();
        }
        else if (debris[debrisNo].name == "jerrycan(Clone)")
        {
            debris[debrisNo].tag = "JerryCan";
            Bars.GetFuelCan();
            //Debug.Log("got fuel");
        }


        debris[debrisNo].SetActive(false);
        debris[debrisNo] = null;
        canGrab[debrisNo] = false;
        }
        //Bars.ChangeHealth(10);
        //Scoring.ChangeScore(100);
    }
}
