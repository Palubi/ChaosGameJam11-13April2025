using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour, ISlowable
{
    //Components
    private Rigidbody playerRigidbody = null;
    private Animator playerAnimator = null;

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
    private float initialSpeed = 0.00f;

    //Rotation
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 mousePosition = Vector3.zero;
    [SerializeField] private Vector2 playerToMousePosition = Vector2.zero;
    [SerializeField] private Vector3 playerToMouseVector = Vector3.zero;
    private float inputRotation = 0.00f;
    private float rotationInDegrees = 0.00f;
    private Camera mainCamera = null;

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
        mainCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody>();
        groundLayer = LayerMask.NameToLayer("Ground");
        playerAnimator = GetComponent<Animator>();
        initialSpeed = speed;
    }

    private void Start()
    {
        controllsMapping = InputManager.Instance.GetControllsMapping();
        controllsMapping.Player2.Movement.performed += MovementPerformed;
        controllsMapping.Player2.Movement.canceled += MovementCanceled;
        controllsMapping.Player2.Jump.performed += JumpPerformed;
        controllsMapping.Player2.Shoot.performed += ShootPerformed;
        controllsMapping.Player2.Shoot.canceled += ShootCanceled;
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);//Por causa de n�o haver "gravity scale" em 3D.
    }



    private void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float hitDistance))
        {
            Vector3 hitPoint = ray.GetPoint(hitDistance);

            mousePosition = hitPoint - transform.position;
            mousePosition.y = 0f;

            if (mousePosition.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(mousePosition);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void MovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputMovementVector = context.ReadValue<Vector2>();
        movementVector.x = inputMovementVector.x;
        movementVector.y = inputMovementVector.y * (-1);
        movementVector.Normalize();
        if (movementCoroutine == null)
        {
            playerAnimator.SetTrigger("Run");
            movementCoroutine = StartCoroutine(MovementCoroutine());
        }
    }

    private void MovementCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (movementCoroutine != null)
        {
            playerAnimator.SetTrigger("Idle");
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
                playerRigidbody.linearVelocity = new Vector3(movementVector.y, playerRigidbody.linearVelocity.y / speed, movementVector.x) * speed;
                yield return null;
            }
        }
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (GetIsGrounded() == true)
        {
            Jump();
        }
    }

    private bool GetIsGrounded()
    {
        if (transform.position.y <= .15)
        {
            return true;
        }
        else
        {
            return false;
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
            if (ballsDetected.Length != 0)
            {
                foreach (var ballHit in ballsDetected)
                {
                    if (ballHit.GetComponent<Ball>() == true)
                    {
                        print("PlayerToMouseVector: " + mousePosition);
                        playerToMousePosition.x = mousePosition.z;
                        playerToMousePosition.y = mousePosition.x;
                        ballHit.GetComponent<Ball>().BallHit(powerPercentage, playerToMousePosition, playerNumber);
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
            if (powerPercentage >= 1)
            {
                powerPercentage = 1;
            }
            powerBar.GetComponent<PowerBar>().UpdatePower(powerPercentage);
            yield return null;
        }

    }

    /*    private void OnCollisionEnter(Collision collision) // tirei porque n�o estava a funcionar consistentemente
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
        }*/
    public void NotSlow()
    {
        speed = initialSpeed;
    }

    public void Slow(float amount)
    {
        speed = initialSpeed * amount;
    }
}