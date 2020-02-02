using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStuff : MonoBehaviour
{
    public GameObject btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.gameState == GameState.State.Dead || GameState.gameState == GameState.State.OutOfFuel)
        {
            btn.SetActive(true);
        }
        else
        {
            if (btn.activeInHierarchy)
            {
                btn.SetActive(false);
            }
        }
    }
}
