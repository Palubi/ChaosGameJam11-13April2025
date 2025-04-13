using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour, ISlowable
{
    [SerializeField] private float initialSpeed;
    private float speed;

    [SerializeField] private GameObject player;

    private Vector3  dogToPlayer;
    private Rigidbody myrigidbody;

    private NavMeshAgent navMesh;


    private void Awake()
    {
        myrigidbody = GetComponent<Rigidbody>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {

        StartCoroutine(StarMovement());
    }
    private IEnumerator StarMovement()
    {
        while (true)
        {
            Vector3 playerLocation = player.transform.position;
            
            dogToPlayer = playerLocation - transform.position;
            dogToPlayer.Normalize();
            print(dogToPlayer);


          //  myrigidbody.linearVelocity = (dogToPlayer* speed);

            navMesh.SetDestination(playerLocation);


            yield return new WaitForSeconds(0.1f);

        }
    }

    public void NotSlow()
    {
        speed = initialSpeed;
    }

    public void Slow(float amount)
    {
        speed = initialSpeed * amount;
    }
}
