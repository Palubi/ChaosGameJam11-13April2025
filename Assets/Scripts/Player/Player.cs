using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ISlowable
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
    private Vector2 inputMovementVector = Vector2.zero;
    private Vector2 movementVector = Vector2.zero;
    [SerializeField] private float speed = 0.00f;

    //Rotation
    private bool canRotate = true;
    private Coroutine rotationCoroutine = null;
    private Vector2 inputRotationVector = Vector2.zero;
    private Vector2 rotationVector = Vector2.zero;
    private float inputRotation = 0.00f;
    private float rotationInDegrees = 0.00f;

    //Jump
    [SerializeField] private float gravityScale = 0.00f;//Custom gravity scale
    [SerializeField] private int jumpForce = 0;
    private bool isGrounded = false;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private Vector3 groundCheckSize;
    [SerializeField] private LayerMask groundLayerMask;

    //Ground layer mask
    int groundLayer = 0;

    //Power bar
    [SerializeField] private GameObject powerBarCanvas = null;
    [SerializeField] private GameObject powerBar = null;
    private Coroutine powerBarCoroutine = null;
    [SerializeField] private float maxChargeTime = 0.00f;
    private float powerPercentage = 0.00f;

    //Player shooting
    [SerializeField] private Vector3 maxHalfExtents = Vector3.zero;
    [SerializeField] private LayerMask ballLayerMask;
    private Collider[] ballsDetected;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void Start()
    {
        controllsMapping = InputManager.Instance.GetControllsMapping();
        controllsMapping.Player1.Movement.performed += MovementPerformed;
        controllsMapping.Player1.Movement.canceled += MovementCanceled;
        controllsMapping.Player1.Rotation.performed += RotationPerformed;
        controllsMapping.Player1.Rotation.canceled += RotationCanceled;
        controllsMapping.Player1.Jump.performed += JumpPerformed;
        controllsMapping.Player1.Shoot.performed += ShootPerformed;
        controllsMapping.Player1.Shoot.canceled += ShootCanceled;
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);//Por causa de n�o haver "gravity scale" em 3D.
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputMovementVector = context.ReadValue<Vector2>();
        movementVector.x = inputMovementVector.x;
        movementVector.y = inputMovementVector.y * (-1);
        movementVector.Normalize();
        if (movementCoroutine == null)
        {
            movementCoroutine = StartCoroutine(MovementCoroutine());
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

    private void RotationPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        inputRotationVector = context.ReadValue<Vector2>();
        rotationVector.x = inputRotationVector.x;
        rotationVector.y = inputRotationVector.y * (-1);
        rotationVector.Normalize();

        if (rotationCoroutine == null)
        {
            rotationCoroutine = StartCoroutine(RotationCoroutine());
        }
    }

    private void RotationCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
            gameObject.transform.rotation = Quaternion.Euler(0.00f, rotationInDegrees, 0.00f);
            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private IEnumerator RotationCoroutine()
    {
        while(true)
        {
            if (canRotate)
            {
                inputRotation = Mathf.Atan2(rotationVector.y, rotationVector.x);
                rotationInDegrees = inputRotation * Mathf.Rad2Deg;
                gameObject.transform.rotation = Quaternion.Euler(0.00f, rotationInDegrees, 0.00f);
                yield return null;
            }
        }
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
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
        if (powerBarCoroutine == null)
        {
            powerBarCanvas.SetActive(true);
            powerBarCoroutine = StartCoroutine(PowerBarCoroutine());
        }
    }

    private void ShootCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (powerBarCoroutine != null)
        {
            StopCoroutine(powerBarCoroutine);
            powerBarCoroutine = null;
            ballsDetected = Physics.OverlapBox(transform.position, maxHalfExtents, Quaternion.identity, ballLayerMask);
            if(ballsDetected.Length != 0)
            {
                foreach(var ballHit in ballsDetected)
                {
                    if(ballHit.GetComponent<Ball>() == true)
                    {
                        print("Power percentage: " + powerPercentage + "| Player rotation: " + rotationVector);
                        ballHit.GetComponent<Ball>().BallHit(powerPercentage, rotationVector);
                    }
                }
            }
            powerBarCanvas.SetActive(false);
        }
    }

    private IEnumerator PowerBarCoroutine()
    {
        powerPercentage = 0.00f;
        float elapsedTime = 0.00f;

        while (true)
        {
            elapsedTime += Time.fixedDeltaTime;
            powerPercentage = (elapsedTime / maxChargeTime);
            if(powerPercentage >= 1)
            {
                powerPercentage = 1;
            }
            powerBar.GetComponent<PowerBar>().UpdatePower(powerPercentage);
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
    }
}