using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    Text scoreText;
    InputManager input;
    public static bool isScoreIncreasing = true;
    static int score;
    void Start() {
        scoreText = GetComponent<Text>();
        input = InputManager.getRef;
    }


    InputManager.Inputs inputs;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(isScoreIncreasing){
            inputs = input.inputs;
            score += (int)(Time.deltaTime * 100);
            scoreText.text = $"Score: {score}";
        }
    }

    public static void ChangeScore(int amount){
        score += amount;
    }


}
