using UnityEngine;

public class Ranhoca : MonoBehaviour, ISlowable
{
    [SerializeField] private float slowValue; //percentage
    private Collider myCollider;

    private void Awake()
    {
        myCollider = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ISlowable>(out ISlowable collision))
        {
            collision.Slow(slowValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ISlowable>(out ISlowable collision))
        {
            collision.NotSlow();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
