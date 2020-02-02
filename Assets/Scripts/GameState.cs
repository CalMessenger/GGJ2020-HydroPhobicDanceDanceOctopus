using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum State{
        Playing,
        Menu,
        OutOfFuel,
        Dead
    }

    public static State gameState = State.Menu;




}
