using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState.NormalUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.PhysicsUpdate();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
