using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class OvelhaMovement : MonoBehaviour, ISlowable
{
    [SerializeField] private float initialSpeed;
    private float speed;

    private Transform nextTarget;

    [SerializeField] private float xMin, zMin, xMax, zMax;
    private float yAxis= 1f;

    private NavMeshAgent navMesh;

    // [SerializeField] private 

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        yAxis = gameObject.transform.position.y;
    }

    private void Start()
    {
        speed = navMesh.speed;
    }

    private void ChooseTarget()
    {
        float xAxis = Random.Range(xMin, xMax);
        float zAxiz = Random.Range(zMin, zMax);

        Vector3 target = new Vector3(xAxis, yAxis, zAxiz);

        navMesh.SetDestination(target);
    }

    private void FixedUpdate()
    {
        ChooseTarget();
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
