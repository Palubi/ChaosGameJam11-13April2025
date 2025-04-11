using UnityEngine;

public class Player : MonoBehaviour
{
    //Components
    private Rigidbody playerRigidbody = null;

    //ControllsMapping
    private ControllsMapping controllsMapping = null;

    //Player order
    [SerializeField] private int playerOrder;

    //Movement
    private Vector3 inputVector = Vector3.zero;
    private Vector3 movementVector = Vector3.zero;

    [SerializeField] private float speed = 0.00f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        controllsMapping = InputManager.Instance.GetControllsMapping();
        controllsMapping.Player1.Movement.performed += MovementPerformed;
        controllsMapping.Player1.Movement.canceled += MovementCanceled;
        controllsMapping.Player1.Shoot.performed += ShootPerformed;
        controllsMapping.Player1.Shoot.canceled += ShootCanceled;
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        movementVector.x = (inputVector.y * -1);
        movementVector.y = playerRigidbody.linearVelocity.y;
        movementVector.z = inputVector.x;
        Move(movementVector);
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Move(new Vector3(0.00f, playerRigidbody.linearVelocity.y, 0.00f));
    }

    private void Move(Vector3 input)
    {
        playerRigidbody.linearVelocity = input * speed;
    }

    private void ShootPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    private void ShootCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }
}