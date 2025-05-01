using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _runAcceleration = 5f;
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _jumpForce = 1f;

    [Header("Camera Settings")]
    [SerializeField] private Camera _camera;


    private CharacterController _characterController;
    private InputHandler _inputHandler;
    private Vector3 _targetDirection;
    private bool _isGrounded = true;

    [Inject]
    private void Construct(InputHandler inputHandler)
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = inputHandler;
        _inputHandler.Jumped += OnJump;
    }

    private void Update()
    {
        Gravity();
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
        _targetDirection = new Vector3(newVelocity.x, _targetDirection.y, newVelocity.z);

        _characterController.Move(_targetDirection * Time.deltaTime);
    }

    private void OnJump()
    {
        if (_isGrounded)
        {
            _targetDirection.y = _jumpForce;
            _isGrounded = false;
            Debug.Log("Jump");
        }
    }

    private void Gravity()
    {
        if (!_isGrounded)
        {
            _targetDirection.y -= _gravity * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _gravity = -9.8f;
    }
}
