using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkdead : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameState.gameState == GameState.State.Dead) || (GameState.gameState == GameState.State.OutOfFuel))
            {
            button.SetActive(true);
        }
    }

       public void gotomainmenu()
        {
        GameState.gameState = GameState.State.Playing;
        SceneManager.LoadScene("Menu");
        
        }

}
