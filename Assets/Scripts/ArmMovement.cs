using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{ 
    public GameObject arm;
    public Vector3 startpos;

    void Start()
    {
       startpos = (arm.transform.position);
       // float roateposx = transform.rotation.x;
       // float roateposy = transform.rotation.y;
       // float roateposz = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExampleCoroutine());
            
        }
    }
    IEnumerator ExampleCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.001f);
            arm.transform.RotateAround(this.transform.position, new Vector3(0, 100, 0), -1000 * Time.deltaTime);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.001f);
            arm.transform.RotateAround(this.transform.position, new Vector3(0, 100, 0), 1000 * Time.deltaTime);
        }

        arm.transform.position = startpos;
   //  transform.rotation.eulerAngles.x   = roateposx;
     //   transform.rotation.eulerAngles.y = roateposy;
      //  transform.rotation.eulerAngles.z = roateposz;
    }
}
