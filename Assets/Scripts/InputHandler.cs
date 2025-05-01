using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : ITickable, IInitializable
{
    public event Action Jumped;
    private InputSystem_Actions _inputActions;
    public Vector2 MoveInput {  get; private set; }

    public InputHandler(InputSystem_Actions inputActions)
    {
        _inputActions = inputActions;
        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Enable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Jumped?.Invoke();
    }

    public void Initialize()
    {
    }

    public void Tick()
    {
        Debug.Log("Kek");
        MoveInput = _inputActions.Player.Move.ReadValue<Vector2>();
    }
}
