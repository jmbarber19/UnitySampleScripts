using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState state = GameState.IntroCutscene;
    [SerializeField] private KeyCode restartKey = KeyCode.R;
    
    private static GameManager instance;

    /// <summary>
    /// Check if the current state is in a list of supplied states
    /// </summary>
    /// <param name="states">States to check if are current</param>
    /// <returns></returns>
    public static bool IsState(params GameState[] states) {
        for (int i = 0; i < states.Length; i++) {
            if (states[i] == instance.state) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Fetch the current state
    /// </summary>
    /// <returns>Current state of the GameManager</returns>
    public static GameState GetState() {
        return instance.state;
    }

    /// <summary>
    /// Set the new state
    /// </summary>
    /// <param name="newState">New state to change to</param>
    public static void SetState(GameState newState) {
        Debug.Log($"GameState has been change from {instance.state} to {newState}");
        instance.state = newState;
    }


    private void Start() {
        instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(restartKey)) {
            instance.state = 0;
        }
    }
}
