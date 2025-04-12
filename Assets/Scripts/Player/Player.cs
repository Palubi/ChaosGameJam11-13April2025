using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Components
    private Rigidbody playerRigidbody = null;

    //ControllsMapping
    private ControllsMapping controllsMapping = null;

    //Player numbers
    [SerializeField] private int playerNumber;

    //Movement
    private bool canMove = true;
    private Coroutine movementCoroutine = null;
    private Vector2 inputVector = Vector2.zero;
    private Vector2 movementVector = Vector2.zero;
    [SerializeField] private float speed = 0.00f;

    //Rotation
    private bool canRotate = true;
    private Coroutine rotationCoroutine = null;
    private float inputRotation = 0.00f;
    private float rotationInDegrees = 0.00f;

    //Jump
    [SerializeField] private float gravityScale = 0.00f;//Custom gravity scale
    [SerializeField] private int jumpForce = 0;
    private bool isGrounded = false;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private Vector3 groundCheckSize;
    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        controllsMapping = InputManager.Instance.GetControllsMapping();
        controllsMapping.Player1.Movement.performed += MovementPerformed;
        controllsMapping.Player1.Movement.canceled += MovementCanceled;
        controllsMapping.Player1.Jump.performed += JumpPerformed;
        controllsMapping.Player1.Shoot.performed += ShootPerformed;
        controllsMapping.Player1.Shoot.canceled += ShootCanceled;
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);//Por causa de não haver "gravity scale" em 3D.
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        movementVector.x = inputVector.x;
        movementVector.y = inputVector.y * (-1);
        movementVector.Normalize();
        if (movementCoroutine == null)
        {
            movementCoroutine = StartCoroutine(MovementCoroutine());
        }
        if (rotationCoroutine == null)
        {
            rotationCoroutine = StartCoroutine(RotationCoroutine());
        }
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
            playerRigidbody.linearVelocity = new Vector3(0.00f, playerRigidbody.linearVelocity.y, 0.00f);
        }
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
            gameObject.transform.rotation = Quaternion.Euler(0.00f, rotationInDegrees, 0.00f);
        }
    }

    private IEnumerator MovementCoroutine()
    {
        while (true)
        {
            if (canMove)
            {
                playerRigidbody.linearVelocity = new Vector3(movementVector.y, playerRigidbody.linearVelocity.y/speed, movementVector.x) * speed;
                yield return null;
            }
        }
    }

    private IEnumerator RotationCoroutine()
    {
        while(true)
        {
            if (canRotate)
            {
                inputRotation = Mathf.Atan2(movementVector.y, movementVector.x);
                rotationInDegrees = inputRotation * Mathf.Rad2Deg;
                gameObject.transform.rotation = Quaternion.Euler(0.00f, rotationInDegrees, 0.00f);
                yield return null;
            }
        }
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //Este groundCheck não está a funcionar
        //isGrounded = Physics.OverlapBox(groundCheck.transform.position, groundCheckSize, Quaternion.identity, groundLayerMask) != null && playerRigidbody.linearVelocity.y <= 0.01f;
        if (isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0.00f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(Vector3.up * jumpForce);
    }

    private void ShootPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }

    private void ShootCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    }
}