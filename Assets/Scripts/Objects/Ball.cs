using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private float ballHeight = 0.00f;
    private Vector3 ballVector = Vector3.zero;
    private Rigidbody ballRigidbody = null;

    /// <summary>       VER AQUI
    /// /public int Player { set; get actualPlayer }; //isso nao deve tar bem  escrito xd - mas é pro colider pegar este valor
    /// </summary>

    private int actualPlayer = 0;

    //quando o player lança a bola envia aqui o seu numero

    // public lançar bola (int playernumber)
    // { actual player = player numebr }
    // 

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)     //para mandar o tipo de collider
    {   
        if (other.gameObject.TryGetComponent<ICollider>(out ICollider collider))
        {
            collider.Colliding(actualPlayer);
        }
    }
    private void FieldCollision()
    {
        //if()
    }

    public void BallHit(float power, Vector2 rotation)
    {
        ballVector.x = rotation.x;
        ballVector.y = ballHeight;
        ballVector.z = rotation.y;

        ballRigidbody.AddForce(rotation * power);
    }
}