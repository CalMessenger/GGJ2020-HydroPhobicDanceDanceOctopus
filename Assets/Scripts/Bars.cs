using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bars : MonoBehaviour
{

    float fuelTimer = 3.0f;
    public Slider healthSlider, fuelSlider;

    static float health = 50, fuel = 50;

    public static void ChangeFuel(float amount){
        fuel += amount;
    }

    public static void ChangeHealth(int amount){
        health += amount;
    }
    public static void GetFuelCan(){
        ChangeFuel(10);
        Scoring.ChangeScore(1000);
    }

    public static void GetWoodBoard(){
        ChangeHealth(10);
        Scoring.ChangeScore(250);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        ChangeFuel(-(Time.deltaTime * 0.75f));
        healthSlider.value = health / 100;
        fuelSlider.value = fuel / 100;
        if(health <= 0 || fuel <= 0){
            if(health <= 0)
                GameState.gameState = GameState.State.OutOfFuel;
            if(fuel <= 0)
                GameState.gameState = GameState.State.Dead;
            Scoring.isScoreIncreasing = false;
        }

        if(health>=100)
        {
            health = 100;
        }

        if(fuel>=100)
        {
            fuel = 100;
        }
    }


}
