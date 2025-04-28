using UnityEngine;
using Zenject;

public class InputHandler : ITickable, IInitializable
{
    public Vector2 MoveInput {  get; private set; }

    private InputSystem_Actions _inputActions;

    public InputHandler(InputSystem_Actions inputActions)
    {
        _inputActions = inputActions;
        _inputActions.Enable();
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
