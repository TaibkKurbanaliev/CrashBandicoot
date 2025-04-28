using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _runAcceleration = 5f;
    [SerializeField] private float _runSpeed = 5f;

    [Header("Camera Settings")]
    [SerializeField] private Camera _camera;

    private CharacterController _characterController;
    private InputHandler _inputHandler;

    [Inject]
    private void Construct(InputHandler inputHandler)
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = inputHandler;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 cameraFwd = _camera.transform.forward;
        cameraFwd.y = 0f;
        cameraFwd.Normalize();

        Vector3 cameraRight = _camera.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 moveDirection = _inputHandler.MoveInput.x * cameraRight + cameraFwd * _inputHandler.MoveInput.y;
        Vector3 newVelocity = moveDirection * _runAcceleration * Time.deltaTime;

        _characterController.Move(newVelocity * Time.deltaTime);
    }
}
