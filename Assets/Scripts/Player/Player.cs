using System.Collections;
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
    private Coroutine moveCoroutine = null;
    private Vector2 inputVector = Vector2.zero;
    private Vector2 movementVector = Vector2.zero;

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
        movementVector.x = inputVector.x;
        movementVector.y = inputVector.y * (-1);
        movementVector.Normalize();
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        movementVector = Vector2.zero;
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            playerRigidbody.linearVelocity = new Vector3(movementVector.y, playerRigidbody.linearVelocity.y, movementVector.x) * speed;
            yield return null;
        }
    }

    private void ShootPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    private void ShootCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }
}