using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclick()

    {

        SceneManager.LoadScene("MainScene_Jacob 2");
    }

    public void exit()

    {

        Application.Quit();
    }
}
