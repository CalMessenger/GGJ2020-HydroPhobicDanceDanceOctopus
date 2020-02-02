using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager getRef;
    
    public struct Inputs{
        public bool t1, t2, t3, t4, t5, t6;
    }
    public Inputs inputs;

    public Inputs GetInputs(){
        return inputs;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(getRef == null)
            getRef = this;
        inputs = new Inputs();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        inputs.t1 = Input.GetButton("T1");
        inputs.t2 = Input.GetButton("T2");
        inputs.t3 = Input.GetButton("T3");
        inputs.t4 = Input.GetButton("T4");
        inputs.t5 = Input.GetButton("T5");
        inputs.t6 = Input.GetButton("T6");

        //Debug.Log($"T1: {inputs.t1} T2: {inputs.t2} T3: {inputs.t3} T4: {inputs.t4} T5: {inputs.t5} T6: {inputs.t6}");
    }

}
